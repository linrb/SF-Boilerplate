﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DapperExtensions;
using SF.Entitys.Abstraction;

namespace SF.Core.Dapper.Expressions
{
    /// <summary>
    ///     This class converts an Expression{Func{TEntity, bool}} into an IPredicate group that can be used with
    ///     DapperExtension's predicate system
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <seealso cref="System.Linq.Expressions.ExpressionVisitor" />
    internal class DapperExpressionVisitor<TEntity, TPrimaryKey> : ExpressionVisitor where TEntity :  BaseEntity<TPrimaryKey>
    {
        private PredicateGroup _pg;
        private Expression _processedProperty;
        private bool _unarySpecified;

        public DapperExpressionVisitor()
        {
            Expressions = new HashSet<Expression>();
        }

        /// <summary>
        ///     Holds BinaryExpressions
        /// </summary>
        public HashSet<Expression> Expressions { get; }

        public IPredicate Process(Expression exp)
        {
            _pg = new PredicateGroup { Predicates = new List<IPredicate>() };
            Visit(Evaluator.PartialEval(exp));

            // the 1st expression determines root group operator
            if (Expressions.Any())
            {
                _pg.Operator = Expressions.First().NodeType == ExpressionType.OrElse ? GroupOperator.Or : GroupOperator.And;
            }

            return _pg.Predicates.Count == 1 ? _pg.Predicates[0] : _pg;
        }

        private static PredicateGroup GetLastPredicateGroup(PredicateGroup grp)
        {
            IList<IPredicate> groups = grp.Predicates;

            if (!groups.Any())
            {
                return grp;
            }

            IPredicate last = groups.Last();

            if (last is PredicateGroup)
            {
                return GetLastPredicateGroup(last as PredicateGroup);
            }

            return grp;
        }

        private IFieldPredicate GetLastField()
        {
            PredicateGroup lastGrp = GetLastPredicateGroup(_pg);

            IPredicate last = lastGrp.Predicates.Last();

            return last as IFieldPredicate;
        }

        private static Operator DetermineOperator(Expression binaryExpression)
        {
            switch (binaryExpression.NodeType)
            {
                case ExpressionType.Equal:
                    return Operator.Eq;
                case ExpressionType.GreaterThan:
                    return Operator.Gt;
                case ExpressionType.GreaterThanOrEqual:
                    return Operator.Ge;
                case ExpressionType.LessThan:
                    return Operator.Lt;
                case ExpressionType.LessThanOrEqual:
                    return Operator.Le;
                default:
                    return Operator.Eq;
            }
        }

        private void AddField(MemberExpression exp, Operator op = Operator.Eq, object value = null, bool not = false)
        {
            PredicateGroup pg = GetLastPredicateGroup(_pg);

            // need convert from Expression<Func<T, bool>> to Expression<Func<T, object>> as this is what Predicates.Field() requires
            Expression<Func<TEntity, object>> fieldExp = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(exp, typeof(object)), exp.Expression as ParameterExpression);

            IFieldPredicate field = Predicates.Field(fieldExp, op, value, not);
            pg.Predicates.Add(field);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expressions.Add(node);

            ExpressionType nt = node.NodeType;

            if (nt == ExpressionType.OrElse || nt == ExpressionType.AndAlso)
            {
                var pg = new PredicateGroup
                {
                    Predicates = new List<IPredicate>(),
                    Operator = nt == ExpressionType.OrElse ? GroupOperator.Or : GroupOperator.And
                };

                _pg.Predicates.Add(pg);
            }

            Visit(node.Left);

            if (node.Left is MemberExpression)
            {
                IFieldPredicate field = GetLastField();
                field.Operator = DetermineOperator(node);

                if (nt == ExpressionType.NotEqual)
                {
                    field.Not = true;
                }
            }

            Visit(node.Right);

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.MemberType != MemberTypes.Property || node.Expression.Type != typeof(TEntity))
            {
                throw new NotSupportedException($"The member '{node}' is not supported");
            }

            // skip if prop is part of a VisitMethodCall
            if (_processedProperty != null && _processedProperty == node)
            {
                _processedProperty = null;
                return node;
            }

            AddField(node);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            IFieldPredicate field = GetLastField();
            field.Value = node.Value;

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Type == typeof(bool) && node.Method.DeclaringType == typeof(string))
            {
                object arg = ((ConstantExpression)node.Arguments[0]).Value;
                var op = Operator.Like;

                switch (node.Method.Name.ToLowerInvariant())
                {
                    case "startswith":
                        arg = arg + "%";
                        break;
                    case "endswith":
                        arg = "%" + arg;
                        break;
                    case "contains":
                        arg = "%" + arg + "%";
                        break;
                    case "equals":
                        op = Operator.Eq;
                        break;
                    default:
                        throw new NotSupportedException($"The method '{node}' is not supported");
                }

                // this is a PropertyExpression but as it's internal, to use, we cast to the base MemberExpression instead (see http://social.msdn.microsoft.com/Forums/en-US/ab528f6a-a60e-4af6-bf31-d58e3f373356/resolving-propertyexpressions-and-fieldexpressions-in-a-custom-linq-provider)
                _processedProperty = node.Object;
                var me = _processedProperty as MemberExpression;

                AddField(me, op, arg, _unarySpecified);

                // reset if applicable
                _unarySpecified = false;

                return node;
            }

            throw new NotSupportedException($"The method '{node}' is not supported");
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType != ExpressionType.Not)
            {
                throw new NotSupportedException($"The unary operator '{node.NodeType}' is not supported");
            }

            _unarySpecified = true;

            return base.VisitUnary(node); // returning base because we want to continue further processing - ie subsequent call to VisitMethodCall
        }
    }
}

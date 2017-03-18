﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SF.Core.QueryExtensions.SearchExtensions
{
    public class EnumerableLevenshteinCompare<T> : EnumerableSearchBase<T, string>
    {
        public EnumerableLevenshteinCompare(IEnumerable<T> source) 
            : base(source, new Expression<Func<T, string>>[0])
        {
        }
    }
}
﻿ 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SF.Core.Abstraction.GenericServices
{

    /// <summary>
    /// This class handles feedback of success or errors from an operation
    /// </summary>
    public class SuccessOrErrors : ISuccessOrErrors
    {
        private readonly List<string> _localWarnings = new List<string>();
        private List<ValidationResult> _localErrors;
        private string _localSuccessMessage;

        public SuccessOrErrors() { }

        protected SuccessOrErrors(ISuccessOrErrors nonResultStatus)
        {
            var status = (SuccessOrErrors)nonResultStatus;
            _localWarnings = status._localWarnings;
            _localErrors = status._localErrors;
        }

        /// <summary>
        /// Holds the list of errors. Empty list means no errors.
        /// </summary>
        public IReadOnlyList<ValidationResult> Errors
        {
            get
            {
                if (_localErrors == null)
                    throw new InvalidOperationException("The status must have an error set or the success message set before you can access errors.");
                return _localErrors;
            }
        }

        /// <summary>
        /// This returns any warning messages
        /// </summary>
        public IReadOnlyList<string> Warnings { get { return _localWarnings; }}

        /// <summary>
        /// Returns true if not errors or not validated yet, else false. 
        /// </summary>
        public bool IsValid { get { return (_localErrors != null && Errors.Count == 0); }}

        /// <summary>
        /// Returns true if any errors. Note: different to IsValid in that it just checks for errors,
        /// i.e. different to IsValid in that no errors but unset Validity will return false.
        /// Useful for checking inside a method where the status is being manipulated.
        /// </summary>
        public bool HasErrors { get { return (_localErrors != null && Errors.Count > 0); } }

        /// <summary>
        /// Returns true if not errors or not validated yet, else false. 
        /// </summary>
        public bool HasWarnings { get { return (_localWarnings.Count > 0); } }

        /// <summary>
        /// This returns the success message with suffix is nay warning messages
        /// </summary>
        public string SuccessMessage
        {
            get { return HasWarnings ? string.Format("{0} (has {1} warnings)",_localSuccessMessage,_localWarnings.Count  ) : _localSuccessMessage; }
        }

        //---------------------------------------------------
        //public methods


        /// <summary>
        /// Adds a warning message. It places the test 'Warning: ' before the message
        /// </summary>
        /// <param name="warningformat"></param>
        /// <param name="args"></param>
        public void AddWarning(string warningformat, params object[] args)
        {
            _localWarnings.Add("Warning: " + string.Format(warningformat, args));
        }

        /// <summary>
        /// Copies in validation errors found outside into the status
        /// </summary>
        public ISuccessOrErrors SetErrors(IEnumerable<ValidationResult> errors)
        {
            _localErrors = errors.ToList();
            _localSuccessMessage = string.Empty;
            return this;
        }

        /// <summary>
        /// This sets the error list to a series of non property specific error messages
        /// </summary>
        /// <param name="errors"></param>
        public ISuccessOrErrors SetErrors(IEnumerable<string> errors)
        {
            _localErrors = errors.Where(x => !string.IsNullOrEmpty(x)).Select(x => new ValidationResult(x)).ToList();
            _localSuccessMessage = string.Empty;
            return this;
        }

        /// <summary>
        /// Allows a single error to be added
        /// </summary>
        /// <param name="errorformat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ISuccessOrErrors AddSingleError(string errorformat, params object[] args)
        {
            if (_localErrors == null)
                _localErrors = new List<ValidationResult>();
            _localErrors.Add(new ValidationResult(string.Format(errorformat, args)));
            _localSuccessMessage = string.Empty;
            return this;
        }

        /// <summary>
        /// This adds an error for a specific, named parameter
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="errorformat"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ISuccessOrErrors AddNamedParameterError(string parameterName, string errorformat, params object[] args)
        {
            if (_localErrors == null)
                _localErrors = new List<ValidationResult>();
            _localErrors.Add(new ValidationResult(string.Format(errorformat, args), new[] { parameterName }));
            _localSuccessMessage = string.Empty;
            return this;
        }

        /// <summary>
        /// This combines any errors or warnings into the current status.
        /// Note: it does NOT copy any success message into the current status
        /// as it is the job of the outer status to set its own success message
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public ISuccessOrErrors Combine(object status)
        {
            var castISuccessOrErrors = status as ISuccessOrErrors;
            if (castISuccessOrErrors == null)
                throw new ArgumentNullException("status", "The status parameter was not derived from a type that supported ISuccessOrErrors.");

            if (castISuccessOrErrors.HasErrors)
            {
                if (castISuccessOrErrors.HasErrors)
                {
                    if (_localErrors == null)
                        _localErrors = castISuccessOrErrors.Errors.ToList();
                    else
                    {
                        _localErrors.AddRange(castISuccessOrErrors.Errors);
                    }
                }
                _localSuccessMessage = string.Empty;
            }

            if (castISuccessOrErrors.HasWarnings)
                _localWarnings.AddRange(castISuccessOrErrors.Warnings);
            //Note: we do NOT copy over any success message from the status being combined in.

            return this;
        }

        /// <summary>
        /// This sets a success message and sets the IsValid flag to true
        /// </summary>
        /// <param name="successformat"></param>
        /// <param name="args"></param>
        public virtual ISuccessOrErrors SetSuccessMessage(string successformat, params object [] args)
        {
            _localErrors = new List<ValidationResult>();         //empty list means its been validated and its Valid
            _localSuccessMessage = string.Format(successformat, args);
            return this;
        }

        /// <summary>
        /// This is a quick way to create an ISuccessOrErrors with a success message
        /// </summary>
        /// <param name="formattedSuccessMessage"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ISuccessOrErrors Success(string formattedSuccessMessage, params object[] args)
        {
            return new SuccessOrErrors().SetSuccessMessage(string.Format(formattedSuccessMessage, args));
        }

        /// <summary>
        /// Useful one line error statement where brevity is needed
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IsValid)
                return _localSuccessMessage ?? "The task completed successfully";

            return _localErrors == null 
                ? "Not currently setup"
                : string.Format("Failed with {0} error{1}", _localErrors.Count, _localErrors.Count > 1 ? "s" : string.Empty);
        }

        /// <summary>
        /// This returns all the error messages, with parameter name prefix if appropriate, joined together into one long string
        /// </summary>
        /// <param name="joinWith">By default joined using \n, i.e. newline. Can provide different join string </param>
        /// <returns></returns>
        public string GetAllErrors(string joinWith = "\n")
        {
            return string.Join(joinWith, Errors.Select(x => FormatParamNames(x) + x.ErrorMessage));
        }

        /// <summary>
        /// This returns the errors as:
        /// If only one error then as a html p 
        /// If multiple errors then as an unordered list
        /// </summary>
        /// <returns>simple html data without any classes</returns>
        public string ErrorsAsHtml()
        {
            if (IsValid)
                throw new InvalidOperationException("You should not call this if there are no errors.");

            if (Errors.Count == 1)
                return string.Format("<p>{0}{1}</p>", FormatParamNames(Errors[0]), Errors[0].ErrorMessage);

            var sb = new StringBuilder("<ul>");
            foreach (var validationResult in Errors)
            {
                sb.AppendFormat("<li>{0}{1}</li>", FormatParamNames(validationResult), validationResult.ErrorMessage);
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        private string FormatParamNames(ValidationResult validationResult)
        {
            if (validationResult.MemberNames.Any())
                return string.Join(",", validationResult.MemberNames) + ": ";

            return string.Empty;
        }

    }
}

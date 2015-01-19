using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace thewall9.bll.Exceptions
{
    public class RuleException : Exception
    {
        public string CodeRuleException { get; set; }
        public List<ValidationResult> ValidationResult { get; set; }
        public object Model { get; set; }
        public string RuleExceptionMessage { get; set; }
        public RuleException(string Message)
        {
            this.RuleExceptionMessage = Message;
        }
        public RuleException(string Message, string CodeRuleException)
        {
            this.RuleExceptionMessage = Message;
            this.CodeRuleException = CodeRuleException;
        }
        public RuleException(string Message, object Model)
        {
            this.Model = Model;
            this.RuleExceptionMessage = Message;
        }
        public RuleException(List<ValidationResult> Errors, object Model)
        {
            this.ValidationResult = Errors;
            this.Model = Model;
        }
        public static bool ValidateObject(object Model)
        {
            ValidationContext context = new ValidationContext(Model, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            if (Validator.TryValidateObject(Model, context, results, true))
                return true;
            else
                throw new RuleException(results, Model);
        }
        public string GetMessage()
        {
            if (String.IsNullOrEmpty(RuleExceptionMessage))
            {
                foreach (var item in ValidationResult)
                {
                    RuleExceptionMessage += item.ErrorMessage;
                }
            }
            return RuleExceptionMessage;
        }
        public Object GetObjectError()
        {
            return new
            {
                Message = GetMessage(),
                CodeRuleException = CodeRuleException
            };
        }
        public void CopyToModelState(ModelStateDictionary modelState)
        {
            modelState.AddModelError("", RuleExceptionMessage);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC
{
    public class MinimumElements : ValidationAttribute
    {
        private readonly int minElements;

        public MinimumElements(int minElements)
        {
            this.minElements = minElements;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList;

            var result = list?.Count >= minElements;

            return result
                ? ValidationResult.Success
                : new ValidationResult($"{validationContext.DisplayName} requires at least {minElements} element" + (minElements > 1 ? "s" : string.Empty));
        }

        // Old code, more generic solution above
        //public override bool IsValid(object value)
        //{
        //    var list = value as IList;
        //    if (list != null)
        //    {
        //        return list.Count > 0;
        //    }
        //    return false;
        //}
    }
}

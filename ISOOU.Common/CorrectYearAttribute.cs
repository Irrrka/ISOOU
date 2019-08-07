namespace ISOOU.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CorrectYearAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var year = (int)value;
           
            var maxYear = DateTime.Now.Year;
            var minYear = DateTime.Now.Year - 18;

            return (year <= maxYear || year >= minYear);
        }
    }
}

namespace ISOOU.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class CorrectUCNAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var regex = new Regex(@"^\d{10}$");

            var ucn = (string)value;

            var result1 = regex.IsMatch(ucn);

            string date = ucn.Substring(0, 6);
            string dateToParse = date.Substring(4, 2) + "/"
                                 + date.Substring(2, 2) + "/"
                                 + date.Substring(0, 2);

            DateTime dateValue;
            var result2 = DateTime.TryParse(dateToParse, out dateValue);

            var result = result1 && result2;

            return result;
        }
    }
}

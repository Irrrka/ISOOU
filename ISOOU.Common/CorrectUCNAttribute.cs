namespace ISOOU.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
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

            DateTime dateOut = new DateTime();
            var result2 = DateTime.TryParseExact(dateToParse, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut);

            var result = result1 && result2;

            return result;
        }
    }
}

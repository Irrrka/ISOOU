namespace ISOOU.Common
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class EqualUCNandYearOfBirthAttribute : ValidationAttribute
    {
        private const string ExceptionMessage = "Годината на раждане и първите две цифри от ЕГН трябва да съвпадат!";

        private readonly string otherPropertyName;

        public EqualUCNandYearOfBirthAttribute(string otherPropertyName)
        {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var year = value.ToString();
           
            var last2digits = year.Substring(2, 2);

            var otherValue = validationContext
                .ObjectType
                .GetProperty(this.otherPropertyName)
                .GetValue(validationContext.ObjectInstance).ToString();

            var yearFromUCN = otherValue.Substring(0, 2);

            if (yearFromUCN != last2digits)
            {
                return new ValidationResult(ExceptionMessage);
            }

            return ValidationResult.Success;
        }
    }
}

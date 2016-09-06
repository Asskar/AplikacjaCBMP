using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;


namespace AppCBMP.Helpers
{
    public class PostCodeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Kod pocztowy nie może być pusty.");
            string postCode = value.ToString();
            return !Regex.IsMatch(postCode, @"[\d]{2}-[\d]{3}|[\d]{5}")
                ? new ValidationResult(false, "Kod pocztowy ma zły format.")
                : new ValidationResult(true, null);
        }
    }
}
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AppCBMP.Helpers
{
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(true, null);
            string phoneNumber = value.ToString();
            return !Regex.IsMatch(phoneNumber, @"[\d]{7}|[\d]{9}|[\d]{10}")
                ? new ValidationResult(false, "Błędny numer telefonu.")
                : new ValidationResult(true, null);
        }
    }
}
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace AppCBMP.Helpers
{
    public class OnlyDigitsRule : ValidationRule
    {
        public string PropertyName { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, PropertyName + " nie może być puste.");
            string valueString = value.ToString();
            return !(valueString.All(char.IsDigit))
                ? new ValidationResult(false, PropertyName + " zawiera niedozwolone znaki.")
                : new ValidationResult(true, null);
        }
    }
}
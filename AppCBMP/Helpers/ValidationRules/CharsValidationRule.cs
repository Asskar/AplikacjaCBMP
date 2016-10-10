using System.Globalization;
using System.Windows.Controls;
using System.Linq;

namespace AppCBMP.Helpers
{
    public class CharsValidationRule: ValidationRule
    {
        public string PropertyName { private get; set; }
        public string CharSet { private get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, PropertyName + " nie może być puste.");
            string valueString = value.ToString();
            return !(valueString.All(c => CharSet.Contains(c) || char.IsLetter(c)))
                ? new ValidationResult(false, PropertyName + " zawiera niedozwolone znaki.")
                : new ValidationResult(true, null);
        }
    }
}
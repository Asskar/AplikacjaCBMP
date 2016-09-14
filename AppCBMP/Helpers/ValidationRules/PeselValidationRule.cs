using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace AppCBMP.Helpers
{
    public class PeselValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Pesel nie może być pusty");
            string pesel = value.ToString();
            if (!pesel.All(char.IsDigit))
                return new ValidationResult(false, "Pesel może zawierać tylko cyfry.");
            if (pesel.
                Length != 11)
                return new ValidationResult(false, "Pesel musi mieć 11 cyfr.");
            byte[] scales = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int peselSum = 0;
            for (int i = 0; i < 10; i++)
            {
                peselSum += (int)char.GetNumericValue(pesel[i]) * scales[i];
            }
            peselSum = peselSum % 10;
            if (peselSum == 0)
                peselSum = 10;
            return 10 - peselSum == (int)char.GetNumericValue(pesel[10])
                ? new ValidationResult(true, "Poprawny.")
                : new ValidationResult(false, "Błedna suma kontrolna");
        }
    }
}
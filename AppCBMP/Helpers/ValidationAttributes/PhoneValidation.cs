using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AppCBMP.Helpers.ValidationAttributes
{
    public class PhoneValidation:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            string phoneNumber = value.ToString();
            return Regex.IsMatch(phoneNumber, @"^[\d]{7}$|^[\d]{9}$|^[\d]{10}$");

        }
    }
}
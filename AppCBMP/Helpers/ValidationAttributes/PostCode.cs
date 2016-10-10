using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows.Navigation;

namespace AppCBMP.Helpers.ValidationAttributes
{
    public class PostCode:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            return value.ToString() == string.Empty || Regex.IsMatch(value.ToString(), @"^[\d]{2}-[\d]{3}$|^[\d]{5}$");
        }
    }
}
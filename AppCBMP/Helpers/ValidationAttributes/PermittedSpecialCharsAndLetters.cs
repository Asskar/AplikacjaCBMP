using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppCBMP.Helpers.ValidationAttributes
{
    public class PermittedSpecialCharsAndLetters:ValidationAttribute
    {
        private readonly string _charset;
    
        public PermittedSpecialCharsAndLetters(string charSet)
        {
            _charset = charSet;
        }
        
        public override bool IsValid(object value)
        {
            string strvalue = value as string;
            return string.IsNullOrEmpty(strvalue) || strvalue.All(c => _charset.Contains(c) || char.IsLetter(c));
        }
    }
}
using System;
using System.Linq;

namespace PSP
{
    public class PasswordChecker
    {
        private readonly int _minLength = 8;

        public bool IsValidPassword(string password)
        {
            return CorrectLength(password) && IncludesUpperCase(password) && IncludesSpecialChar(password);
        }

        private bool IncludesSpecialChar(string password)
        {
            var specialChars = password.ToCharArray()
                .Where(c => !Char.IsLetter(c) && !Char.IsDigit(c))
                .Where(c => !String.IsNullOrWhiteSpace(c.ToString()))
                .ToArray();

            if (specialChars.Count() == 0)
            {
                return false;
            }

            return true;
        }

        private bool CorrectLength(string password)
        {
            return password.Length >= _minLength;
        }

        private bool IncludesUpperCase(string password)
        {
            var upperCaseChars = password.ToCharArray()
                .Where(c => Char.IsLetter(c))
                .Where(c => Char.IsUpper(c))
                .ToArray();

            if (upperCaseChars.Count() == 0)
            {
                return false;
            }

            return true;
        }


    }
}

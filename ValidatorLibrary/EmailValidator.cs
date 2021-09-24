namespace PSP
{
    using System.Linq;
    using System;
    using System.Collections.Generic;

    public class EmailValidator
    {
        private readonly char _eta = '@';
        private readonly List<char> _allowedSpecialChars = new List<char> { '.', '-', '@', '_' };
        private readonly List<string> _allowedDomains = new List<string> { "gmail", "mail", "yahoo" };
        private readonly List<string> _allowedTLD = new List<string> { "com", "ru", "net"};
        public bool IsValidEmail(string email)
        {

            return ContainsEta(email) && NoSpecialSymbols(email) && ValidDomain(email) && ValidTLD(email);
        }

        private bool ValidTLD(string email)
        {
            var TLD = email.Split('.').Last();

            if (_allowedTLD.Contains(TLD))
            {
                return true;
            }

            return false;
        }

        private bool ValidDomain(string email)
        {
            var secondPart = email.Split(_eta).Last();
            var domain = secondPart.Substring(0, secondPart.LastIndexOf('.'));

            if (_allowedDomains.Contains(domain))
            {
                return true;
            }

            return false;
        }

        private bool ContainsEta(string email)
        {
            if (!email.Contains(_eta))
            {
                return false;
            }

            if (email.IndexOf(_eta) != email.LastIndexOf(_eta))
            {
                return false;
            }

            return true;
        }

        private bool NoSpecialSymbols(string email)
        {
           var illegalSpecialChars = email.ToCharArray()
                .Where(c => (!Char.IsLetter(c) && !Char.IsDigit(c)))
                .Where(c => !_allowedSpecialChars.Contains(c))
                .ToArray();

            if (illegalSpecialChars.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}

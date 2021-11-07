using System;

namespace LibraryUsageImplementation.Handlers
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message)
        : base(message)
        {
        }
    }
}

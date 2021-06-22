using System;

namespace Infrastructure.Exceptions
{
    public class EmailBlockedException : CustomException
    {
        public EmailBlockedException(string message) : base(message)
        {
        }

        public EmailBlockedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
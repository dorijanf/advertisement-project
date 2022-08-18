using System;

namespace SharedModels.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("Record not found.") { }

        public BadRequestException(string message) : base(message) { }
    }
}
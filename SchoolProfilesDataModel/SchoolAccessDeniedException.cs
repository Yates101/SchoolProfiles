using System;

namespace SchoolProfilesDataModel
{
    // This exception is thrown when an agent tries to access a resource that
    // they are not allowed to access.
    public class SchoolAccessDeniedException : Exception
    {
        public SchoolAccessDeniedException()
        {
        }

        public SchoolAccessDeniedException(string message)
            : base(message)
        {
        }

        public SchoolAccessDeniedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Task3.Exeptions
{
    public abstract class UserTaskException : Exception
    {
    }

    public class InvalidUserException : UserTaskException
    {
        private readonly string message = "Invalid userId";
        public override string Message => message;
    }

    public class UserNotFoundException : UserTaskException
    {
        private readonly string message = "User not found";
        public override string Message => message;
    }

    public class DuplicateTaskException : UserTaskException { 
        private readonly string message = "The task already exists";

        public override string Message => message;
    }



}

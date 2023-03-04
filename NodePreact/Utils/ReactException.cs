using System;

namespace NodePreact.Utils
{
    public class NodePreactException : Exception
    {
        public NodePreactException(string message) : base(message)
        {

        }

        public NodePreactException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

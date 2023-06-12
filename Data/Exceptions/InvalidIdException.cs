using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException()
        {
        }

        public InvalidIdException(string? message) : base(message)
        {
        }

        public InvalidIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

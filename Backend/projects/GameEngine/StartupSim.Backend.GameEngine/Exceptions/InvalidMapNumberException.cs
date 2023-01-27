using System;
using System.Runtime.Serialization;

namespace StartupSim.Backend.GameEngine.Exceptions
{
    [Serializable]
    public class InvalidMapNumberException : Exception
    {
        public InvalidMapNumberException()
        {
        }

        public InvalidMapNumberException(string message) : base(message)
        {
        }

        public InvalidMapNumberException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidMapNumberException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
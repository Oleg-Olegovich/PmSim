using System;
using System.Runtime.Serialization;

namespace PmSim.Backend.GameEngine.Exceptions
{
    [Serializable]
    public class InvalidActionException : Exception
    {
        public InvalidActionException()
        {
        }

        public InvalidActionException(string message) : base(message)
        {
        }

        public InvalidActionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidActionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace PmSim.Shared.GameEngine.Exceptions
{
    [Serializable]
    internal class InvalidActionException : Exception
    {
        internal InvalidActionException()
        {
        }

        internal InvalidActionException(string message) : base(message)
        {
        }

        internal InvalidActionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidActionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
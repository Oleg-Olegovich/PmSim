using System;
using System.Runtime.Serialization;

namespace PmSim.Shared.GameEngine.Exceptions
{
    [Serializable]
    internal class InvalidMapNumberException : Exception
    {
        internal InvalidMapNumberException()
        {
        }

        internal InvalidMapNumberException(string message) : base(message)
        {
        }

        internal InvalidMapNumberException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidMapNumberException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
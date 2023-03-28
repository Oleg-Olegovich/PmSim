using System;
using System.Runtime.Serialization;

namespace PmSim.Shared.GameEngine.Exceptions
{
    [Serializable]
    internal class GameLogicException : Exception
    {
        internal GameLogicException()
        {
        }

        internal GameLogicException(string message) : base(message)
        {
        }

        internal GameLogicException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GameLogicException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
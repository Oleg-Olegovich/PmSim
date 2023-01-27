using System;
using System.Runtime.Serialization;

namespace PmSim.Backend.GameEngine.Exceptions
{
    [Serializable]
    public class GameLogicException : Exception
    {
        public GameLogicException()
        {
        }

        public GameLogicException(string message) : base(message)
        {
        }

        public GameLogicException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GameLogicException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace PmSim.Backend.GameEngine.Exceptions
{
    [Serializable]
    public class WrongGameStageException : Exception
    {
        public WrongGameStageException()
        {
        }

        public WrongGameStageException(string message) : base(message)
        {
        }

        public WrongGameStageException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WrongGameStageException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace PmSim.Shared.GameEngine.Exceptions
{
    [Serializable]
    internal class WrongGameStageException : Exception
    {
        internal WrongGameStageException()
        {
        }

        internal WrongGameStageException(string message) : base(message)
        {
        }

        internal WrongGameStageException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WrongGameStageException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
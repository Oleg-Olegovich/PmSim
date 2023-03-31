namespace PmSim.Shared.Contracts.Exceptions;

public class PmSimException : Exception
{
    public PmSimException(string message)
        : base(message) { }
}
namespace PmSim.Frontend.Client.Exceptions;

public class PmSimClientException : Exception
{
    public PmSimClientException(string message)
        : base(message) { }
}
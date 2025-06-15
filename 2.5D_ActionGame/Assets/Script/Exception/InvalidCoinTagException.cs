using System;

public class InvalidCoinTagException : Exception
{
    public InvalidCoinTagException() { }

    public InvalidCoinTagException(string message) : base(message) { }

    public InvalidCoinTagException(string message, Exception innerException) : base(message, innerException) { }
}

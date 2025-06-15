using System;


/// <summary>
/// 無効なコインタグが指定された場合にスローされる例外
/// </summary>
public class InvalidCoinTagException : Exception
{
    public InvalidCoinTagException() { }

    public InvalidCoinTagException(string message) : base(message) { }

    public InvalidCoinTagException(string message, Exception innerException) : base(message, innerException) { }
}

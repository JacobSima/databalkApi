namespace Databalk.Core.Exceptions;

public abstract class CustomException : Exception
{
  protected CustomException(string message) : base(message) { }

  protected CustomException() : base() { }

  protected CustomException(string message, Exception innerException) : base(message, innerException) { }
}

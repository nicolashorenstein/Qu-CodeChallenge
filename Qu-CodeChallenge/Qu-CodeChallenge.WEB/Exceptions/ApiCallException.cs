using System.Net;

namespace Qu_CodeChallenge.Exceptions;

public class ApiCallException : Exception
{
    public ApiCallException()
    {
    }

    public ApiCallException(string message)
        : base(message)
    {
    }

    public ApiCallException(string message, HttpStatusCode statusCode)
        : base(statusCode + " - " + message)
    {
        _statusCode = statusCode;
    }

    public ApiCallException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public HttpStatusCode _statusCode { get; }
}
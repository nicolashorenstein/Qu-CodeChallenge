namespace Qu_CodeChallenge.Interfaces;

public interface IApiCallGeneric
{
    Task<object> CallApi(string url, string method, Dictionary<string, string>? headers, object? comando);
}
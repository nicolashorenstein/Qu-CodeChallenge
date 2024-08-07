using System.Net;

namespace Qu_CodeChallenge.DOMAIN.Responses;

public class BaseResult
{
    public string Error { get; set; } = "";
    public bool Ok { get; set; } = true;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    public void SetError()
    {
        Ok = false;
        Error = "Error procesando la solicitud";
        StatusCode = HttpStatusCode.InternalServerError;
    }

    public void SetError(string error, HttpStatusCode statusCode)
    {
        Ok = false;
        Error = error;
        StatusCode = statusCode;
    }
}
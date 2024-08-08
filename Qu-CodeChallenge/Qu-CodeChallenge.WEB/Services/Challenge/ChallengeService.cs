using System.Net;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Qu_CodeChallenge.DOMAIN.DTO.Matrix;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;
using Qu_CodeChallenge.Exceptions;
using Qu_CodeChallenge.Interfaces;
using Qu_CodeChallenge.Interfaces.Challenge;

namespace Coovilros.Medidores.Web.Servicios.Challenge;

public class ChallengeService : IChallengeService
{
    private IConfiguration _config { get; set; }
    private ILocalStorageService _localStorage { get; set; }
    private HttpClient _httpClient { get; set; }
    private NavigationManager _navManager { get; set; }
    private IApiCallGeneric _apiCalls { get; set; }
    
    
    public ChallengeService(IConfiguration config, HttpClient httpClient, ILocalStorageService localStorage,
        NavigationManager navManager, IApiCallGeneric apiCalls)
    {
        _config = config;
        _httpClient = httpClient;
        _localStorage = localStorage;
        _navManager = navManager;
        _apiCalls = apiCalls;
    }
    
    public async Task<MatrixResult> StartChallenge()
    {
        var resultado = new MatrixResult();
        try
        {
            var urlApi = _config["ApiURL"] + "api/qu/loadResources";
            var words = (_config["SampleWords"].Split(",")).ToList();
          
            var query = new LoadResources
            {
                Words = words,
                XSize = int.Parse(_config["XSize"]),
                YSize = int.Parse(_config["YSize"])
            };

            var contenido = await _apiCalls.CallApi(urlApi, "POST",null, query);
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            resultado = JsonSerializer.Deserialize<MatrixResult>(contenido.ToString(), options);
               
            return resultado;
        }
        catch (ApiCallException ex)
        {
            resultado.SetError(ex.Message, ex._statusCode);
            return resultado;
        }
        catch (Exception ex)
        {
            resultado.SetError("Error al obtener consumos mensuales", HttpStatusCode.InternalServerError);
            return resultado;
        }
    }

    public async Task<WordFinderResult> ResolveChallenge(List<string> matrix)
    {
        var resultado = new WordFinderResult();
        try
        {
            var urlApi = _config["ApiURL"] + "api/qu/resolve";

            var words = (_config["SampleWords"].Split(',')).ToList();
           
            var query = new ResolveChallenge()
            {
                Words = words,
                Matrix = matrix 
            };
            var headers = new Dictionary<string, string>();
            var contenido = await _apiCalls.CallApi(urlApi, "POST", headers, query);
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            resultado = JsonSerializer.Deserialize<WordFinderResult>(contenido.ToString(), options);
               
            return resultado;
        }
        catch (ApiCallException ex)
        {
            resultado.SetError(ex.Message, ex._statusCode);
            return resultado;
        }
        catch (Exception ex)
        {
            resultado.SetError("Error when trying to resolve the challenge", HttpStatusCode.InternalServerError);
            return resultado;
        }
    }

    public async Task<MatrixResult> GetConsumosDiario(string token, int idConexion, string? mes, string? anio)
    {
        var resultado = new MatrixResult();
        try
        {
            var urlApi = _config["ApiURL"] + "api/qu/loadResources";

            var words = (_config["SampleWords"].Split(',')).ToList();
           
            var query = new LoadResources
            {
                Words = words,
                XSize = 64,
                YSize = 64
            };
            var headers = new Dictionary<string, string>();
            var contenido = await _apiCalls.CallApi(urlApi, "POST", headers, query);
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            resultado = JsonSerializer.Deserialize<MatrixResult>(contenido.ToString(), options);
               
            return resultado;
        }
        catch (ApiCallException ex)
        {
            resultado.SetError(ex.Message, ex._statusCode);
            return resultado;
        }
        catch (Exception ex)
        {
            resultado.SetError("Error when trying to init the challenge", HttpStatusCode.InternalServerError);
            return resultado;
        }
    }
}
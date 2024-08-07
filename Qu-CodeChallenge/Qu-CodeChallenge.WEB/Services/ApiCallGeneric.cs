using System.Net;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Qu_CodeChallenge.Exceptions;
using Qu_CodeChallenge.Interfaces;

namespace Coovilros.Medidores.Web.Servicios;

public class ApiCallGeneric : IApiCallGeneric
{
    private readonly HttpClient _httpClient;
    private ILocalStorageService _localStorage { get; set; }
    private NavigationManager _navManager { get; set; }

    public ApiCallGeneric(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navManager)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _navManager = navManager;
    }

    public async Task<object> CallApi(string url, string method, Dictionary<string, string>? headers, object? comando)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Clear();
            var urlApi = url;
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }
            }

            var response = new HttpResponseMessage();

            switch (method)
            {
                case "GET":
                    response = await _httpClient.GetAsync(urlApi);
                    break;
                case "POST":
                    var comandoPOSTJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(comando),
                        Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(urlApi, comandoPOSTJson);
                    break;
                case "PUT":
                    var comandoPUTJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(comando),
                        Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync(urlApi, comandoPUTJson);
                    break;
                case "DELETE":
                    response = await _httpClient.DeleteAsync(urlApi);
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                var apiCallException =
                    new ApiCallException(((int)response.StatusCode).ToString(), response.StatusCode);

                if (apiCallException._statusCode == HttpStatusCode.Unauthorized)
                {
                    await _localStorage.ClearAsync();
                    await _localStorage.SetItemAsStringAsync("ErrorPermisos",
                        "Se requiere volver a ingresar al sistema");
                    _navManager.NavigateTo("/");
                }

                throw apiCallException;
            }

            var contenido = await response.Content.ReadAsStringAsync();

            return contenido;
        }
        catch (ApiCallException exApi)
        {
            throw exApi;
        }
        catch (Exception ex)
        {
            throw new ApiCallException(((int)HttpStatusCode.InternalServerError).ToString());
        }
    }
}
using Blazored.LocalStorage;
using Coovilros.Medidores.Web.Servicios;
using Coovilros.Medidores.Web.Servicios.Challenge;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Qu_CodeChallenge.Interfaces;
using Qu_CodeChallenge.Interfaces.Challenge;
using Qu_CodeChallenge.WEB;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => http);

using var response = await http.GetAsync("appsettings.json");
using var stream = await response.Content.ReadAsStreamAsync();

builder.Configuration.AddJsonStream(stream);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IApiCallGeneric, ApiCallGeneric>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
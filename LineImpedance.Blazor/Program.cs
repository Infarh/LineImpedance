global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

global using LineImpedance.Blazor;
using LineImpedance.Blazor.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var root_components = builder.RootComponents;
root_components.Add<App>("#app");
root_components.Add<HeadOutlet>("head::after");

var services = builder.Services;
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
services.AddScoped<MainViewModel>();

await builder.Build().RunAsync();

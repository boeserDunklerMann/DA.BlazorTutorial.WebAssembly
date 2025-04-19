using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieApp.Client;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSyncfusionBlazor();

string graphqlServerPath = builder.HostEnvironment.BaseAddress + "graphql";
builder.Services.AddMovieClient()
	.ConfigureHttpClient(client =>
	{
		client.BaseAddress = new Uri(graphqlServerPath);
	});

await builder.Build().RunAsync();

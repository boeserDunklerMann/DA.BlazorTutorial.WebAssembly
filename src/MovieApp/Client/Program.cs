using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieApp.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("MovieApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MovieApp.ServerAPI"));

string graphQLServerPath = builder.HostEnvironment.BaseAddress + "graphql";
builder.Services.AddMovieClient()
	.ConfigureHttpClient(client =>
	{
		client.BaseAddress = new Uri(graphQLServerPath);
	});

await builder.Build().RunAsync();

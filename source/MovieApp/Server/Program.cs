using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MovieApp.Server.DataAccess;
using MovieApp.Server.GraphQL;
using MovieApp.Server.Interfaces;
using MovieApp.Server.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", false);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddPooledDbContextFactory<MovieDBContext>
    (opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMovie, MovieDataAccessLayer>();
builder.Services.AddGraphQLServer()
    .AddQueryType<MovieQueryResolver>()
    .AddMutationType<MovieMutationResolver>()
	.AddFiltering()
    .AddSorting()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseEndpoints(endpts => endpts.MapGraphQL());

string FileProviderPath = app.Environment.ContentRootPath + "/Poster";

if (!Directory.Exists(FileProviderPath))
{
	Directory.CreateDirectory(FileProviderPath);
}

app.UseFileServer(new FileServerOptions
{
	FileProvider = new PhysicalFileProvider(FileProviderPath),
	RequestPath = "/Poster",
	EnableDirectoryBrowsing = true
});

app.Run();

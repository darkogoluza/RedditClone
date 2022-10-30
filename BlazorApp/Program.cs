using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IPostService, PostHttpClient>();
builder.Services.AddScoped<ISubFormService, SubFormHttpClient>();
builder.Services.AddScoped<ICommentService, CommentHttpClient>();


builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://localhost:7207/")});


await builder.Build().RunAsync();
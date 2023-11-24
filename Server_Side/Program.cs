using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCoreRateLimit;
using Server_Side.Services;

// Done:
// ddos protection

// Now:
// dynamic variables
// set port

// Later:
// Encrypted bytes (server to server)
// certificate
// sql injection
// ssrf protection
// assured authorization


var builder = WebApplication.CreateBuilder(args);

// Define the port here


// This is the part that used to be in ConfigureServices
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


// Add the rate limiting services
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientID";
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*", //"*" is used to target any HTTP verb
            Limit = 25,  // Adjust the limit as needed
            Period = "30s" // Adjust the period as needed (e.g., 20s for 20 seconds)
        }
    };
});

builder.Services.AddSingleton<Analysis_Report_Center>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();


var app = builder.Build();

// This is the part that used to be in the Configure method in Startup.cs
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseIpRateLimiting(); // Add the rate limiting middleware

//app.UseHttpsRedirection();  //more info about pages
app.UseStaticFiles();       //allows to use wwwroot

app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Analytics}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// This is the part that used to be in ConfigureServices
builder.Services.AddControllersWithViews();

var app = builder.Build();

// This is the part that used to be in the Configure method in Startup.cs
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Analytics}/{action=Index}/{id?}");
});

app.Run();
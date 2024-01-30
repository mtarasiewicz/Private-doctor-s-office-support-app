using System.Globalization;
using DocHub.Ui.StartupExtensions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

/*Services*/
builder.Services.ConfigureServices(builder.Configuration); //Use custom extension class 'ConfigureServicesExtensions.cs'
var app = builder.Build();

/*Built-in exception pages*/
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHangfireDashboard("/jobs");
}
else
{
    /*
     In case of an error in the production environment, 
     it redirects to the created user-friendly error view
     */
    app.UseExceptionHandler("/Error");
}
/*Middlewares*/

/*Https*/
app.UseHsts(); // Active Hsts
app.UseHttpsRedirection();

/*App config*/
app.UseStaticFiles(); // Enable static files such as HTML, CSS, JavaScript in folder (wwwroot)

/*Enable routing*/
app.UseRouting();

/*Enable authentication*/
app.UseAuthentication();

/*Enable authorizatiojn*/
app.UseAuthorization();
app.UseSession();
/*Map controllers*/
app.MapControllers();





app.Run(); //Start app


public partial class Program
{
}
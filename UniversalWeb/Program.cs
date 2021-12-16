using UniversalWeb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel;
using System.Security.Cryptography.X509Certificates;
using static UniversalWeb.WebStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDirectoryBrowser();

var env = builder.Environment;
var webHost = builder.WebHost;
var hostOnly = builder.Host;

webHost.UseKestrel().ConfigureKestrel(opt =>
{
    
    opt.ListenAnyIP(443, lisOpt =>
    {   
        lisOpt.UseHttps(connAdaptOpt =>
        {
            connAdaptOpt.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            connAdaptOpt.ServerCertificate =X509Certificate2.CreateFromPemFile(@"Certificate/Origin_Certificate",@"Certificate/Private_key");
        });
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
/*
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}*/

//app.UseHttpsRedirection();


app.UseStaticFiles();

//Enable FTP-like directory.
app.UseStaticFiles(WebStorage.UseOtherStaticFilesOptions(env));
app.UseDirectoryBrowser(WebStorage.UseBrowserDirectoryOptions(env));

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

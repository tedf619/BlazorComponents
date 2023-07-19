using BlazorComponents.Server;

using Microsoft.AspNetCore.ResponseCompression;

namespace BlazorComponents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // SignalR support
            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression
            (
                options => options.MimeTypes = ResponseCompressionDefaults
                    .MimeTypes
                    .Concat(new[] { "application/octet-stream" })
            );


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

            // SignalR support
            app.MapHub<EventHub>("/eventhub");

            app.Run();
        }
    }
}
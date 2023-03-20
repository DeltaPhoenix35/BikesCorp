using BikesTest.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Net.WebSockets;
using System.Threading;
using System.Text;

namespace BikesTest
{
    public class Startup
    {
        private readonly List<WebSocket> _connections = new();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Services.ServicesConfiguration.ConfigureServices(services);

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = "/Home/login";
                    options.AccessDeniedPath = "/Home/Denied";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo; 
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo; 

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSockets(new() { KeepAliveInterval = TimeSpan.FromSeconds(30) });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.Map("/ws/{id}", async ctx =>
                {
                    var buffer = new byte[1024 * 4];
                    var webSocket = await ctx.WebSockets.AcceptWebSocketAsync();
                    _connections.Add(webSocket);

                    var result = await webSocket.ReceiveAsync(new(buffer), CancellationToken.None);
                    while (!result.CloseStatus.HasValue)
                    {
                        result = await webSocket.ReceiveAsync(new(buffer), CancellationToken.None);
                        var id = Encoding.UTF8.GetString(buffer[..result.Count]);
                        Console.WriteLine($"Received: {id}");

                        var message = Encoding.UTF8.GetBytes($"{id}");
                        foreach (var c in _connections)
                        {
                            await c.SendAsync(new(message, 0, message.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                        }
                    }

                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    _connections.Remove(webSocket);
                });
            });

           
        }
    }
}

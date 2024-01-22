using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Json;
using Bugsnag.AspNet.Core;
using Castle.Facilities.Logging;
using Hangfire;
using Kaizen.Authentication.JwtBearer;
using Kaizen.Configuration;
using Kaizen.EntityFrameworkCore;
using Kaizen.Identity;
using Kaizen.Web.Resources;
using Kaizen.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Newtonsoft.Json.Serialization;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Kaizen.Web.Startup
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IWebHostEnvironment env)
        {
            _hostingEnvironment = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddControllersWithViews(
                    options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                        options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                    }
                )
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm";
                    options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            var connectionString = _appConfiguration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<KaizenDbContext>(options => options.UseSqlServer(connectionString));

            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddScoped<IWebResourceManager, WebResourceManager>();
            services.AddScoped<ICloudFileStorage, CloudFileStorageService>();

            services.AddSignalR().AddAzureSignalR();

            services.AddBugsnag(configuration => {
                configuration.ApiKey = "d3a9088b771b7482773874e401ed4865";
            });

            // Configure Abp and Dependency Injection
            services.AddAbpWithoutCreatingServiceProvider<KaizenWebMvcModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig(
                        _hostingEnvironment.IsDevelopment()
                            ? "log4net.config"
                            : "log4net.Production.config"
                        )
                )
            );

            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(_appConfiguration.GetConnectionString("Default"));
            });

            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); // Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseJwtTokenMiddleware();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }
    }
}

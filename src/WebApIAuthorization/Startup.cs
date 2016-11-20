using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AuthorizationLab;
using WebApIAuthorization.requirement;

namespace WebApIAuthorization
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // david 1:

            services.AddAuthorization(options=> {
                //david 9----add policy based on the role
                options.AddPolicy("adminpolicy", policy => policy.RequireRole("Administrator"));

                //david 11 --id id empty check , it works, but useless, so give id
               // options.AddPolicy("EmployeeId", policy => policy.RequireClaim("EmployeeId"));

                //david 14
                options.AddPolicy("EmployeeId", policy => policy.RequireClaim("EmployeeId","12","34","43"));

                //david 18 you create a policy class then you register as a reqirement here
                //then in homecontroller, add this policy
                options.AddPolicy("Over21Only", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));
                //server side data for check, claim homecontroller need to check this comapre to see true or not

                //david 24
                options.AddPolicy("BuildingEntry", policy => policy.Requirements.Add(new Officeentryrequirement()));
                //one requirement policy

                //david 26


            });

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);


            //david 6 ---this enable  all controller [authorize]
            services.AddMvc(config=> {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //david 26 ----one requirement , two handlers
            services.AddSingleton<IAuthorizationHandler, HasBadgeHandler>();
            services.AddSingleton<IAuthorizationHandler, HasTemporaryPassHandler>();
            services.AddSingleton<IAuthorizationHandler, DocumentEditHandler>();

            //david 28---security resource link a visible link b disable etc.
            services.AddSingleton<IDocumentRepository, DocumentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //david 2, set cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                AuthenticationScheme = "Cookie",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login"),
                AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Forbidden"),
                AutomaticAuthenticate=true,
                AutomaticChallenge=true
            });

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QFlow.Data;
using QFlow.Data.Managers;
using QFlow.Helper;
using QFlow.Models.DataModels;
using QFlow.Services;

namespace QFlow
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer( connection ));

            services.AddIdentity<ApplicationUser, IdentityRole>( options =>
                {
                    // Password settings
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes( 5 );
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                } )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie( options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes( 60 );
                options.LoginPath = new PathString("/Account/Login");
                options.LogoutPath = new PathString( "/Account/Logout");
                options.AccessDeniedPath = new PathString("/");
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            } );

            // Application services.
            services.AddTransient<RequestManager>();
            services.AddTransient<UserManager>();
            services.AddTransient<MessageManager>();
            services.AddTransient<AlertNotificationManager>();
            services.AddTransient<NotificationHelper>();
            services.AddTransient<AlertTypeManager>();

            services.AddTransient<SettingsService>();
            services.AddTransient<EmailNotificationManager>();
            services.AddTransient<EmailSenderService>();
            services.AddTransient<FileService>();

            services.AddMvc();

            //background email notification service
            services.AddSingleton<IHostedService, EmailNotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider )
        {
            //init log file
            loggerFactory.AddFile( Configuration.GetSection( "Logging" ) );

            if ( env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Request/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Request}/{action=Index}/{id?}");
            });
        }
    }
}

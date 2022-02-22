using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClientSuite.Data;
using Microsoft.EntityFrameworkCore;
using ClientSuite.Repo; 
using ClientSuite.Service; 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Localization;  
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting; 
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using Lamar.Microsoft.DependencyInjection;

namespace ClientSuite.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {  
            services.AddTransient<SetViewDataFilter>();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
             
            services.AddHttpContextAccessor(); //for access AppContext.Current

             //Localization 
            services.AddMvc(options =>
            {
                options.Filters.AddService<SetViewDataFilter>();
            }).AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddPortableObjectLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"), 
                new CultureInfo("ar-SA"),
            };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            //Localization end

            //this service is used for mysql data base.
             //services.AddDbContext<ApplicationContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),  mysqlOptions => {  mysqlOptions.ServerVersion(new Version(5, 7, 14), ServerType.MySql);    }));

            //if you want to use MS Sql server database than comment above servies and uncomment below.
           services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddLamar(new LamarRegistry()); 

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
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
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseHttpsRedirection(); 

            app.UseRouting();

            app.UseAuthorization();

            //Localization
            app.UseRequestLocalization();
            //Localization end

            AppContext.Configure(app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>()); //for access AppContext.Current

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "AreaDefault",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); 
            });
        }
    }
}


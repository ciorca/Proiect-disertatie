using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WLoveAnimals.Pages.Account;
//using WLoveAnimals.Pages.Authorization;
using WLoveAnimals.Services;

namespace WLoveAnimals
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

            ////we add authentication for cookies
            //services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            //{
            //    options.Cookie.Name = "MyCookieAuth";
            //    options.LoginPath = "/Account/Login"; //we specify the location of the login page with the related path


            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //    //options.ExpireTimeSpan = TimeSpan.FromSeconds(45);  //set to disappear cookies in 45 seconds (disappear if browser closes)
            //});
            // services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminOnly",
            //        policy => policy.RequireClaim("Admin"));
            //                         //.Requirements.Add(new AdminRequirement(3)));
            //     });

            //services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();  // add handler-ul from  AdminRequirement class

            services.AddDbContextPool<AppDbContext>(options =>   //connect with the database
            {
                options.UseSqlServer(Configuration.GetConnectionString("AnimalDBConnection"));
            });

            // for register an user
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddRazorPages();
            services.AddScoped<IAnimalsRepository , SQLAnimalRepository>();  
            services.Configure<RouteOptions>(options =>
                 {
                     options.LowercaseUrls = true;
                     options.LowercaseQueryStrings = true;
                     options.AppendTrailingSlash = true;
                     options.ConstraintMap.Add("even", typeof(EvenConstraint));

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

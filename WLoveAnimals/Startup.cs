using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WLoveAnimals.Pages.Authorization;
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

            //adaugam autentificarea pt cookies
            services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/Account/Login"; //specificam locatia paginii login cu calea aferenta

                options.AccessDeniedPath = "/Account/AccessDenied";
                //options.ExpireTimeSpan = TimeSpan.FromSeconds(45);  //setare ca sa dispara cokkies in 45 secunde(dispar si daca se inchide browserul)
            });
            services.AddAuthorization(options =>
           {
               options.AddPolicy("AdminOnly",
                   policy => policy.RequireClaim("Admin"));
                                    //.Requirements.Add(new AdminRequirement(3)));
                });

            //services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();  // adugam handler-ul din clasa AdminRequirement

            services.AddDbContextPool<AppDbContext>(options =>   //connect with the database
            {
                options.UseSqlServer(Configuration.GetConnectionString("AnimalDBConnection"));
            });


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

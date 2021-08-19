using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using likedGames.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace likedGames
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
            services.AddDbContext<likedGameContext>(dbCtxOptions =>
    {
        dbCtxOptions.UseMySql(Configuration["DBInfo:ConnectionString"], mySqlOptions => mySqlOptions.EnableRetryOnFailure());
    });
    // to access session directly from view, corresponds with:
    // @using Microsoft.AspNetCore.Http in Views/_ViewImports.cshtml
    // Example: <p>@Context.Session.GetString("UserId")</p>
    services.AddHttpContextAccessor();
    services.AddSession();
    services.AddMvc(options => options.EnableEndpointRouting = false);
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
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        });

        }
    }
}

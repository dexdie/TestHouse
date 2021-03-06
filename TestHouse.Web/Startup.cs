﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Services;
using TestHouse.Infrastructure.Identity;
using TestHouse.Infrastructure.Repositories;


namespace TestHouse.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string _myAllowSpecificOrigins = "_localAllowOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_myAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            services.AddMvc().AddNewtonsoftJson();

            services.AddDbContext<ProjectRespository>
                (options => options.UseSqlServer(Configuration.GetConnectionString("TestHouseConnection")));
            services.AddDbContext <AppIdentityDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("TestHouseConnection")));
            services.AddScoped<IProjectRepository,ProjectRespository>();
            services.AddScoped<ProjectService>();
            services.AddScoped<SuitService>();
            services.AddScoped<TestCaseService>();
            services.AddScoped<TestRunService>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(_myAllowSpecificOrigins);
            }
            app.UseClientSideBlazorFiles<TestHouse.Web.Blazor.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<TestHouse.Web.Blazor.Startup>("index.html");
            });             

            _initializeDatabase(app);
        }

        private void _initializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ProjectRespository>();
                context.Database.SetCommandTimeout(300);
                context.Database.Migrate();
            }
        }
    }
}

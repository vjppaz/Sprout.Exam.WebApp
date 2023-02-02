using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sprout.Exam.Business.Command;
using Sprout.Exam.Business.Interfaces.Command;
using Sprout.Exam.Business.Interfaces.Query;
using Sprout.Exam.Business.Query;
using Sprout.Exam.Business.SaralyHandler;
using Sprout.Exam.Business.SaralyHandler.Computations;
using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Middleware;
using System;

namespace Sprout.Exam.WebApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            AddCommands(services);
            AddQueries(services);
            AddFactories(services);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        private void AddFactories(IServiceCollection services)
        {
            services.AddScoped<ISalaryHandlerFactory>(m =>
            {
                var factory = ActivatorUtilities.CreateInstance<SalaryHandlerFactory>(m);

                factory.RegisterCalculator(ActivatorUtilities.CreateInstance<ContractualSalaryCalculator>(m));
                factory.RegisterCalculator(ActivatorUtilities.CreateInstance<RegularSalaryCalculator>(m));
                //TODO: Add new salary calculator here when a new Employee Type is added.

                return factory;
            });
        }

        private static void AddQueries(IServiceCollection services)
        {
            services.AddScoped<IGetEmployeesQuery, GetEmployeesQuery>();
            services.AddScoped<IGetEmployeeByIdQuery, GetEmployeeByIdQuery>();
        }

        private static void AddCommands(IServiceCollection services)
        {
            services.AddScoped<IAddEmployeeCommand, AddEmployeeCommand>();
            services.AddScoped<ICalculateSalaryCommand, CalculateSalaryCommand>();
            services.AddScoped<IDeleteEmployeeCommand, DeleteEmployeeCommand>();
            services.AddScoped<IUpdateEmployeeCommand, UpdateEmployeeCommand>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}

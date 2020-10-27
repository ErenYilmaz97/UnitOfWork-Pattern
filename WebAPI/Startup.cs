using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundJob.Schedules;
using Business;
using Business.ValidationRules.FluentValidation.Validators;
using Core;
using Core.Business;
using Core.DataAccess;
using Core.UnitOfWork;
using DataAccess.Repository;
using DataAccess.UnitOfWork;
using Entities.DbContext;
using Entities.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAPI.ActionAttributes;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()

                .ConfigureApiBehaviorOptions(options =>
                {
                    //AUTO VALIDATION DISABLED
                    options.SuppressModelStateInvalidFilter = true;
                })

                
                .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<ProductValidator>(); })
                .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<CategoryValidator>(); })
                .AddNewtonsoftJson(o =>
                {
                    //INCLUDE
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

           


            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });


            //HANGFIRE CONFIGURATION
            var hangfireConString = Configuration["ConnectionStrings:HangFireConnectionString"];

            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true

                };

                config.UseSqlServerStorage(hangfireConString, option)
                    .WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });
            services.AddHangfireServer();

            //RETRY
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });



            //IOC
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();


            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();


            services.AddScoped<AbstractValidator<Product>, ProductValidator>();
            services.AddScoped<AbstractValidator<Category>, CategoryValidator>();

            //ILOGMANAGER ISTENIRSE DBLOGGER NESNESINI SETLE
            services.AddScoped<ILogManager, DbLogger>();

            services.AddScoped<IDatabaseManager, DatabaseOperations>();


           
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHangfireDashboard("/BackgroundJobs",new DashboardOptions
            {
                DashboardTitle = "Eren Yýlmaz Hangfire Dashboard",  //TITLE
                AppPath = "/weatherforecast"   //BACK TO SITE
            });


            //HANGFIRE SERVER CONFIGURATION
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                //5 SANÝYEDE BÝR KONTROL ET
                SchedulePollingInterval = TimeSpan.FromSeconds(5)
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RecurringJobs.DatabaseBackupOperation();
        }
    }
}

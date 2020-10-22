using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

           


            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });




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

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreLoggerWebAPI.Filters;
using Business.Abstract;
using Business.Concrete;
using Core.Results;
using Core.Serilog;
using Core.Validations;
using Entities;
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
using Repository.Abstract;
using Repository.Concrete;
using Repository.UnýtOfWork.Abstract;
using Repository.UnýtOfWork.Concrete;
using DbLogger = Core.Log.DbLogger;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Newtonsoft;
using Newtonsoft.Json;

namespace AspNetCoreLoggerWebAPI
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

            services.AddControllers(opts =>
                {
               
                        opts.Filters.Add<ValidationFilter>();
                })

                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })

                .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<CategoryValidator>(); })

                
                .AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });




            services.AddDbContext<AppDbContext>(x=>
            {
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });
            



            //IoC
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<Core.Log.ILogger, DbLogger>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<AbstractValidator<Product>, ProductValidator>();
            services.AddScoped<AbstractValidator<Category>, CategoryValidator>();

            services.AddScoped<ILogManager, SerilogDbLogger>();


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

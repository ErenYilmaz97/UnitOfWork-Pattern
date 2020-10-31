using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.ValidationRules.FluentValidation.Validators;
using Core;
using Core.ApiServices;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC.ApiServices;

namespace MVC
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


            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });



            //IOC
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();


            services.AddScoped<AbstractValidator<Product>, ProductValidator>();
            services.AddScoped<AbstractValidator<Category>, CategoryValidator>();

            //ILOGMANAGER ISTENIRSE DBLOGGER NESNESINI SETLE
            services.AddScoped<ILogManager, DbLogger>();

            services.AddScoped<IDatabaseManager, DatabaseOperations>();



            //HTTPCLIENT
            services.AddHttpClient<ProductApiService>(opts =>
            {
                opts.BaseAddress = new Uri(Configuration["baseUrl"]);
            });

            services.AddHttpClient<CategoryApiService>(opts =>
            {
                opts.BaseAddress = new Uri(Configuration["baseUrl"]);
            });


            services.AddMvc(x => x.EnableEndpointRouting = false)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>()); ;
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();  
            app.UseStatusCodePages();         
            app.UseStaticFiles();
            app.UseAuthentication();         
            app.UseMvc(configureRoutes);
        }

        private void configureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("DefaultRoute", "{Controller=Product}/{Action=Index}");
        }
    }

    }


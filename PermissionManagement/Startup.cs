using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PermissionManagement.Model.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.OpenApi.Models;
using PermissionManagement.Service.Mapper;
using FluentValidation;
using PermissionManagement.Service;
using PermissionManagement.Repository;
using PermissionManagement.Model.Entities;
using PermissionManagement.Service.DTOs;
using PermissionManagement.Service.FluentValidation;

namespace PermissionManagement
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
            services.AddControllers();
            services.AddDbContext<PermissionContext>(options => options.
            UseMySql(Configuration.
            GetConnectionString("Connection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PermissionManagementCRUD", Version = "v1" });
            });
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<PermissionProfile>();
                x.AddProfile<PermissionTypeProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IBaseRepository<Permission>, PermissionRepository>();
            services.AddScoped<IBaseService<PermissionDto>, PermissionService>();
            services.AddScoped<IValidator<PermissionDto>, PermissionValidation>();
            services.AddScoped<IBaseRepository<PermissionType>, PermissionTypeRepository>();
            services.AddScoped<IBaseService<PermissionTypeDto>, PermissionTypeService>();
            services.AddScoped<IValidator<PermissionTypeDto>, PermissionTypeValidation>();
            services.AddCors();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PermissionManagementCRUD v1"));
            }
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Student.BLL.CacheHelper;
using Student.BLL.Interfsces;
using Student.BLL.Models;
using Student.BLL.Services;
using Student.DAL.EntityContext;
using Student.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Students.API", Version = "v1" });
            });

            services.AddSingleton<IConfiguration>(Configuration);

            var type = Configuration.GetSection("StrategySave").Value == "json" ? StrategySaveType.json : StrategySaveType.xml;
            var count = Convert.ToInt32(Configuration.GetSection("CacheDeep").Value);
            var cache = new LruCache<StudentModel>(count);
            services.AddTransient<IStudentService, StudentService>(obj => new StudentService(type, cache));
            //var y = services.AddSingleton<ICache<StudentModel>, LruCache<StudentModel>>(obj => new LruCache<StudentModel>(count));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students.API v1"));
            }

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

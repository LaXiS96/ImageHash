using AutoMapper;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.WebApi.Repositories;
using LaXiS.ImageHash.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LaXiS.ImageHash.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.Configure<WebApiSettings>(Configuration.GetSection("WebApi"));
            services.Configure<LiteDbSettings>(Configuration.GetSection("LiteDb"));
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));

            switch (Configuration.GetSection("WebApi").GetValue("BackendDb", BackendDbType.LiteDb))
            {
                case BackendDbType.LiteDb:
                    services.AddSingleton<IImagesRepository, ImagesLiteDbRepository>();
                    break;
                case BackendDbType.MongoDb:
                    services.AddSingleton<IImagesRepository, ImagesMongoDbRepository>();
                    break;
            }

            services.AddSingleton<IImagesService, ImagesService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

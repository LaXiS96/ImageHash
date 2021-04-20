using AutoMapper;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.WebApi.Repositories;
using LaXiS.ImageHash.WebApi.Services;
using Microsoft.AspNet.OData.Extensions;
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

            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));

            services.AddSingleton<IRepository<ImageDomainModel>, ImagesRepository>();
            services.AddSingleton<IRepository<TagDomainModel>, TagsRepository>();
            services.AddSingleton<IRepository<TagCategoryDomainModel>, TagCategoriesRepository>();

            services.AddSingleton<IImagesService, ImagesService>();
            services.AddSingleton<ITagsService, TagsService>();

            services.AddControllers();
            services.AddOData();
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

                endpoints.EnableDependencyInjection();
                endpoints.Filter().OrderBy().MaxTop(50).Count();
            });
        }
    }
}

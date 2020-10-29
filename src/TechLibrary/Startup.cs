using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TechLibrary.Config;
using TechLibrary.Controllers;
using TechLibrary.Data;
using TechLibrary.Extensions;
using TechLibrary.Interfaces;
using TechLibrary.Services;

namespace TechLibrary
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

            services.AddAppConfig(Configuration);

            services.AddLoggerFactory();
            services.AddLogger<BooksContext>();
            services.AddLogger<BooksController>();

            services.AddDbContext<BooksContext>(options => options.UseSqlite("Data Source=techLibrary.db"));

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ISearchService, SearchService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                            .WithOrigins(new string[] { "https://localhost:4000", "http://localhost:4000" })
                            .SetIsOriginAllowed((host) => true);
                    });
            });


            services.AddControllers();

            services.AddAutoMapperConfig();

            //services.AddAutoMapperConfig();
            //services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors("AllowAll"); // second

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


    }
}

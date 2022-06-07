using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace StudentsCourses
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
            services.Configure<SchoolDatabaseSettings>(
                Configuration.GetSection(nameof(SchoolDatabaseSettings)));

            services.AddSingleton<ISchoolDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<SchoolDatabaseSettings>>().Value);

              services.AddSingleton<StudentService>();
              services.AddSingleton<CourseService>();
         //   services.AddScoped<IStudentService, StudentService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

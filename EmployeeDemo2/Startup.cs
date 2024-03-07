using EmployeeDemo.Domain.Model;
using EmployeeDemo.EF;
using EmployeeDemo.EF.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDemo.web
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration _configuration, IWebHostEnvironment webHostEnvironment)
        {
             configuration = _configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConfiguration")));
            services.AddScoped<EmployeesService>();
            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<SkillsService>();
            services.AddTransient<ISkillsService,SkillsService>();
            services.AddTransient<ISkillsRepository, SkillsRepository>();
            services.AddAutoMapper( x=> x.AddProfile(new AutoMapperProfile(_webHostEnvironment)));
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

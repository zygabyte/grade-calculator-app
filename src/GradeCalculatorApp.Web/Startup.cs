using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Repositories.Implementations;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Implementations;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GradeCalculatorApp.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            var connString = Configuration.GetConnectionString("GradeCalculatorContext");

            services.AddDbContext<GradeCalculatorContext>(s => s.UseSqlServer(connString));

            //  configure repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ILecturerCourseRepository, LecturerCourseRepository>();
            services.AddScoped<ILecturerRepository, LecturerRepository>();
            services.AddScoped<IProgrammeCourseRepository, ProgrammeCourseRepository>();
            services.AddScoped<IProgrammeRepository, ProgrammeRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionSemesterCourseRepository, SessionSemesterCourseRepository>();
            services.AddScoped<ISessionSemesterRepository, SessionSemesterRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IRegistrationCourseRepository, RegistrationCourseRepository>();
            services.AddScoped<IRegisteredCourseRepository, RegisteredCourseRepository>();
            
            //  configure services
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ILecturerCourseService, LecturerCourseService>();
            services.AddScoped<ILecturerService, LecturerService>();
            services.AddScoped<IProgrammeCourseService, ProgrammeCourseService>();
            services.AddScoped<IProgrammeService, ProgrammeService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISemesterService, SemesterService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionSemesterCourseService, SessionSemesterCourseService>();
            services.AddScoped<ISessionSemesterService, SessionSemesterService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IRegistrationCourseService, RegistrationCourseService>();
            services.AddScoped<IRegisteredCourseService, RegisteredCourseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
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
using Microsoft.EntityFrameworkCore;
using MONQTest.Infrastructure;


namespace MONQTest
{
    public class Startup
    {
        ///<summary>
        ///��������� ������������ � ������� DI
        ///</summary>>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        ///<summary>
        ///�������� ���������� ������� IConfiguration
        ///</summary>>
        public IConfiguration Configuration { get; }

        ///<summary>
        ///����� ��� ����������� �������� DI
        ///</summary>>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        ///<summary>
        ///Middleware 
        ///</summary>>
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

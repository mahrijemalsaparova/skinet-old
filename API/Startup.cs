using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;

        }

        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();

            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            // Startup'taki kod kalabalığını azaltmak için genişlettiğimiz IServiceCollection'ı çağırma.
            services.AddApplicationServices();

            services.AddSwaggerDocumentation();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // using our own created middleware

           app.UseMiddleware<ExceptionMiddleware>();

            // in the event that request comes into our API server but we don't have an end point that
            // matches that particular request then we're going to hit this bit of middleware

            app.UseStatusCodePagesWithReExecute("/errors/{0}"); // {0} placeholder of the status code.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles(); // for pictures

            app.UseAuthorization();

            app.UseSwaggerDocumentation();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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
           // AutoMapper için gereken işlemleri içeren class MappingProfiles olduğu için typeof() ile belirttik
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();

            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(_config.GetConnectionString("IdentityConnection")));

            // Redis için
            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"),
                true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            // Startup'taki kod kalabalığını azaltmak için genişlettiğimiz IServiceCollection'ı çağırma.
            services.AddApplicationServices();

            //IdentityService için 
            services.AddIdentityServices(_config);

            services.AddSwaggerDocumentation();
            //angular tarafında header görebilmemiz için 
            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // using our own created middleware
            // ExceptionMiddleware sınıfını kullanıyoruz
           app.UseMiddleware<ExceptionMiddleware>();

            // in the event that request comes into our API server but we don't have an end point that
            // matches that particular request then we're going to hit this bit of middleware
            // olmayan endpoint hatası için

            app.UseStatusCodePagesWithReExecute("/errors/{0}"); // {0} placeholder of the status code.

            app.UseHttpsRedirection();

            app.UseRouting();
                // wwwroot'taki fotografların url'si ile postmanda çagırdığımızda gelmesi için önemli metod.
            app.UseStaticFiles();
            //yukarıdaki AddCors için
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerDocumentation();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

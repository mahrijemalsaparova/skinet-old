using System;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {   
            // Migrate database when app start
           var host = CreateHostBuilder(args).Build();
            // DbContext'e ulaşmak için servis sağlayıcıları.
           using(var scope = host.Services.CreateScope())
           {
               var services = scope.ServiceProvider;
               var loggerFactory = services.GetRequiredService<ILoggerFactory>();

               try 
               {
                    // İlgili servisle StoreContex'miz çagırıldı.
                   var context = services.GetRequiredService<StoreContext>();
                   // Bağlam için bekleyen geçişleri zaman uyumsuz olarak veritabanına uygular.
                   //  Zaten mevcut değilse veritabanını oluşturacaktır.
                   await context.Database.MigrateAsync();
                    // seed the data from StoreContextSeed  when app start
                    await StoreContextSeed.SeedAsync(context, loggerFactory);

                    // Identity için
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    // creating database for identity
                    await identityContext.Database.MigrateAsync();
                    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
                }
               catch(Exception ex)
               {
                   var logger = loggerFactory.CreateLogger<Program>();
                   logger.LogError(ex, "An error occured during migration");
               }
           }

           host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

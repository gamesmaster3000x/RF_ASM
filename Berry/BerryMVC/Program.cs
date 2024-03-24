using Microsoft.EntityFrameworkCore;
using BerryMVC.Data;
using BerryMVC.Common;
using NLog;
using LogLevel = NLog.LogLevel;
using Microsoft.Data.Sqlite;

namespace BerryMVC
{
    public class Program
    {
        private static Logger LOGGER;

        public static void Main (string[] args)
        {
            ConfigureNLog();

            var builder = WebApplication.CreateBuilder(args);
            SqliteConnectionStringBuilder sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = "berry.sqlite",
            };
            builder.Services.AddDbContext<BerryMVCContext>(options =>
                options
                .UseSqlite(sqliteConnectionStringBuilder.ToString())
                );

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BerryMVCContext>();
                DbInitialiser.Initialise(context);
            }

            app.MapControllerRoute(
                name: "home",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "browse",
                pattern: "{controller=Browse}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "api",
                pattern: "{controller=Browse}/");

            app.Run();
        }

        private static void ConfigureNLog ()
        {
            Console.WriteLine("Configuring NLog...");
            NLog.Config.LoggingConfiguration config = new NLog.Config.LoggingConfiguration();
            var fileTarget = new NLog.Targets.FileTarget("CrimsonFileLogTarget")
            {
                FileName = "Crimson_${shortdate}.log",
                Layout = "${level} | ${time} | ${logger} > ${message:withexception=true}",
                DeleteOldFileOnStartup = true
            };
            var consoleTarget = new NLog.Targets.ConsoleTarget("CrimsonConsoleLogTarget")
            {
                Layout = "${level:uppercase=true:padding=-5} | ${time} | ${threadname:whenEmpty=${threadid}:padding=-6} | ${logger} > ${message:withexception=true}"
            };
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);

            LogManager.Configuration = config;
            Console.WriteLine("NLog configured!");

            LOGGER = LogManager.GetCurrentClassLogger();
            Console.WriteLine("Testing TRACE and FATAL level logging...");
            LOGGER.Trace("Testing trace level logging...");
            LOGGER.Fatal("Testing fatal level logging...");
            Console.WriteLine("Did you see the TRACE and FATAL test messages?");
        }
    }
}
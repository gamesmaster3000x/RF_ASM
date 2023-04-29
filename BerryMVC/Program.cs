using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BerryMVC.Data;
using BerryMVC.Common;
using NLog;
using LogLevel = NLog.LogLevel;

namespace BerryMVC
{
    public class Program
    {
        private static Logger LOGGER;

        public static void Main (string[] args)
        {
            ConfigureNLog();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BerryMVCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BerryMVCContext") ?? throw new InvalidOperationException("Connection string 'BerryMVCContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            var app = builder.Build();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

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
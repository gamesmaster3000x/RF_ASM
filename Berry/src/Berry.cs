using Berry.DB;
using Berry.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Config;
using NLog;
using System.Reflection;

namespace Berry.src
{
    public class Berry
    {

        public static int Main (string[] args)
        {
            WebApplication app = BuildApp();
            ConfigureApp(app);

            app.Run();

            return 0;
        }

        private static WebApplication BuildApp ()
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            _ = builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ConfigureEndpointDefaults(listenOptions =>
                {
                });
                serverOptions.ConfigureHttpsDefaults(listenOptions => { });
            });


            // Add services to the container.
            _ = builder.Services.AddRazorPages();
            _ = builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = "berry.sqlite",
            };
            string connectionString = connectionStringBuilder.ToString();
            _ = builder.Services.AddDbContext<BerryDbContext>(options =>
            {
                _ = options.UseSqlite(connectionString);
            });

            var optionsBuilder = new DbContextOptionsBuilder<BerryDbContext>();
            optionsBuilder.UseSqlite("DataSource=berry.sqlite");




            var app = builder.Build();
            return app;
        }

        private static void ConfigureApp (WebApplication app)
        {
            MapRoutes(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                _ = app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<BerryDbContext>();
                Initialiser.Initialise(context);
            }
            _ = app.UseHttpsRedirection();
            _ = app.UseStaticFiles();

            _ = app.UseRouting();
            _ = app.UseAuthorization();

            _ = app.MapRazorPages();
            _ = app.UseHsts();
        }

        private static void InitialiseDatabase (BerryDbContext berryDbContext)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Berry.Models.initialise.sql"))
            using (var reader = new StreamReader(stream!))
            {
                string query = reader.ReadToEnd();
                _ = berryDbContext.Database.ExecuteSql($"{query}");
            }
        }

        private static void MapRoutes (WebApplication app)
        {
            string expression = @".+exp.+";
            _ = app.MapGet("/get/{id:required:regex(" + expression + ")}", (string id) => Get(id));
        }

        public static string Get (string id)
        {
            return $"Hello, World! {id}";
        }
    }
}
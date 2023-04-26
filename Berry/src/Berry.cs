using Berry.DB;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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
                Mode = SqliteOpenMode.ReadWriteCreate,
                // Password = "INSECURE_PASSWORD", // Not supported by e_sqlite
            };
            _ = builder.Services.AddDbContext<BerryDBContext>(options => _ = options.UseSqlite(connectionStringBuilder.ToString()));


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
            _ = app.MapRazorPages();
            _ = app.UseRouting();
            _ = app.UseStaticFiles();
            _ = app.UseAuthorization();
            _ = app.UseHsts();
            _ = app.UseHttpsRedirection();
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
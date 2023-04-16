namespace Berry
{
    public class Berry
    {
        public static int Main (string[] args)
        {
            Run();
            return 0;
        }

        public static void Run ()
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

            var app = builder.Build();

            string expression = @".+exp.+";
            _ = app.MapGet("/get/{id:required:regex(" + expression + ")}", (string id) => Get(id));


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                _ = app.UseExceptionHandler("/Error");
            }
            _ = app.UseStaticFiles();

            _ = app.UseRouting();

            _ = app.UseAuthorization();

            _ = app.MapRazorPages();

            app.Run();
        }

        public static string Get (string id)
        {
            return $"Hello, World! {id}";
        }
    }
}

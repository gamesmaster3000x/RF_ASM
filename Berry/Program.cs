using Berry.DB;
using Berry.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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
    DbInitialiser.Initialise(context);
}

_ = app.UseHttpsRedirection();
_ = app.UseStaticFiles();

_ = app.UseRouting();
_ = app.UseAuthorization();

_ = app.MapRazorPages();
_ = app.UseHsts();

app.Run();
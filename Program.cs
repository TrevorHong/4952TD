using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<UserAuthentication>();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(connectionString,serverVersion));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.OpenConnection();
        context.Database.CloseConnection();
        Console.WriteLine("Successfully connected to the database.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Failed to connect to the database.");
        Console.WriteLine(ex.Message);
    }
}

app.Run();

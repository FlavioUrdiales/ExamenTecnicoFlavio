
using ExamenTecnicoFlavio.Data;
using Microsoft.EntityFrameworkCore;
using ExamenTecnicoFlavio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AdventureWorksContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AdventureWorks")
    );
});

builder.Services.AddScoped<ProductoService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

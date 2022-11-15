using Microsoft.EntityFrameworkCore;
using SGPI_Andres.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SGPIDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //Para que por defecto abra la pagina en este apartado
    //Aca cambio el controller por el administrador, y la faction por login
    pattern: "{controller=Administrador}/{action=Login}/{id?}");

app.Run();

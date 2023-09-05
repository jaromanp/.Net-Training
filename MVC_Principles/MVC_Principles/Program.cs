using Microsoft.EntityFrameworkCore;
using MVC_Principles.Factories;
using MVC_Principles.Interfaces;
using MVC_Principles.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with the connection string from the appsettings.json file
builder.Services.AddDbContext<WebApidbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WebApidbConnection")));

// Read the max number of products from the appsettings.json file
int maxNumberOfProducts = builder.Configuration.GetValue<int>("ApplicationSettings:MaxNumberOfProducts");

builder.Services.AddScoped<IProductsControllerFactory>(services => new ProductsControllerFactory(services.GetRequiredService<WebApidbContext>(), maxNumberOfProducts));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

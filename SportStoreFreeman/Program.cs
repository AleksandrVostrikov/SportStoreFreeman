using Microsoft.EntityFrameworkCore;
using SportStoreFreeman.Data;
using SportStoreFreeman.Models;
using SportStoreFreeman.Repositories.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SportStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportStoreConnectionString")));

builder.Services.AddScoped<IStoreRepository, StoreRepository>();

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
    name: "pagination",
    pattern: "{controller=Home}/{action=Index}/Products/Page{productPage}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

SeedData.EnsurePopulated(app);
app.Run();


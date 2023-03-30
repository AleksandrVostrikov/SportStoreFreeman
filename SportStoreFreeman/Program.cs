using Microsoft.EntityFrameworkCore;
using SportStoreFreeman.Data;
using SportStoreFreeman.Models;
using SportStoreFreeman.Repositories.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SportStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportStoreConnectionString")));

builder.Services.AddRazorPages();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
app.UseSession();
app.MapRazorPages();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "catpage",
    pattern: "{controller=Home}/{action=Index}/{category}/Page{productPage:int}");
app.MapControllerRoute(
    name: "page",
    pattern: "{controller=Home}/{action=Index}/Page{productPage:int}");
app.MapControllerRoute(
    name: "category",
    pattern: "{controller=Home}/{action=Index}/{category}");
app.MapControllerRoute(
    name: "pagination",
    pattern: "{controller=Home}/{action=Index}/Products/Page{productPage}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

SeedData.EnsurePopulated(app);
app.Run();


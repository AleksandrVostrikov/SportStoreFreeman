using Microsoft.EntityFrameworkCore;
using SportStoreFreeman.Data;
using SportStoreFreeman.Models;
using SportStoreFreeman.Repositories.Db;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SportStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportStoreConnectionString")));

builder.Services.AddDbContext<ApiIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApiIdentityDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddServerSideBlazor();




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
app.MapBlazorHub();
app.UseAuthorization();
app.UseAuthentication();

app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
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
IdentitySeedData.EnsurePopulated(app);
app.Run();


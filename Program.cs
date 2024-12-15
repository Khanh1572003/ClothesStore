using ClothesShoping.Logic;
using ClothesShoping.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ClothesShopingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ClothesConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(options =>
 {
	 options.Cookie.Name = "ITShop.Cookie";
	 options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
	 options.SlidingExpiration = true;
	 options.LoginPath = "/Home/Login";
	 options.LogoutPath = "/Home/Logout";
	 options.AccessDeniedPath = "/Home/Forbidden";
 });
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.Name = "ITShop.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});

builder.Services.AddTransient<IMailLogic, MailLogic>();

// Lấy thông tin cấu hình trong tập tin appsettings.json và gán vào đối tượng MailSettings 
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

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
app.UseAuthentication();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllerRoute(name: "adminareas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

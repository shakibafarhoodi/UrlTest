using Data.PnsContext;
using IOC;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persent_App.SessionCheckMiddleware;
using WebMarkupMin.AspNetCore6;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContext, UrlContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    Options =>
    {

        Options.SignIn.RequireConfirmedAccount = true;

    })
    .AddEntityFrameworkStores<UrlContext>()
    .AddDefaultTokenProviders();

DependencyContainer.RegisterServices(builder.Services);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // مدت زمان انقضای سشن
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // مدت زمان انقضای سشن
//    options.Cookie.HttpOnly = true; // فقط برای درخواست‌های سمت سرور
//    options.Cookie.IsEssential = true; // الزامی
//});


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/account/Login";
//        options.LogoutPath = "/account/Logout";
//        options.AccessDeniedPath = "/account/AccessDenied";
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//    });
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // مسیر صفحه ورود
    options.AccessDeniedPath = "/Account/AccessDenied"; // مسیر صفحه عدم دسترسی
});


builder.Services.AddAuthorization();
builder.Services.AddWebMarkupMin(options =>
{
    options.AllowCompressionInDevelopmentEnvironment = true;
})
.AddHtmlMinification()
.AddHttpCompression()
.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("X-Frame-Options");
    context.Response.Headers.Add("X-Frame-Options", "Allow-From http://172.20.198.18/");
    context.Response.Headers.Add("Content-Security", "default-src 'self'");
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebMarkupMin();
app.UseRouting();
app.UseSession();
app.UseMiddleware<SessionCheck>();

app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Url}/{action=Index}/{id?}");
});

app.Run();


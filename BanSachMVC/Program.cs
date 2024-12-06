using BanSachMVC.Controllers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cấu hình session
builder.Services.AddDistributedMemoryCache();  // Cấu hình bộ nhớ cho session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Thời gian hết hạn session
    options.Cookie.HttpOnly = true;  // Ngăn truy cập cookie từ JavaScript
    options.Cookie.IsEssential = true;  // Đảm bảo cookie hoạt động trong mọi tình huống
});

// Cấu hình các dịch vụ cần thiết
builder.Services.AddHttpClient();  // Dịch vụ HttpClient chung cho toàn ứng dụng
builder.Services.AddMvc();  // Đảm bảo MVC được thêm vào nếu cần

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();  // Cấu hình session

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Cấu hình định tuyến cho các controller trong Admin
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{controller=QuanLySach}/{action=Index}/{id?}");

// Cấu hình định tuyến mặc định cho các controller khác
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.StartsWith("/QuanLySach"))
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Not Found");
    }
    else
    {
        await next();
    }
});

app.Run();

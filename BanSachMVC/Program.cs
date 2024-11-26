using BanSachMVC.Controllers;

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

builder.Services.AddMvc();
builder.Services.AddHttpClient<HomeController>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

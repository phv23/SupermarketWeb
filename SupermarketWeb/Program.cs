using Data_Access.Context;
using Data_Access.Repositories.Impl;
using Data_Access.Repositories;
using Microsoft.EntityFrameworkCore;
using WebSystem.Injection;
using Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Thêm phần quản lý Container cho ASP.MET
builder.Services.AddDependencyInjection();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


// Kiểm tra: khi bạn sử dụng AddDbContext<TContext>, nó mặc định sẽ đăng ký DbContext với ServiceLifetime.Scoped, có nghĩa là
//DbContext sẽ được khởi tạo một lần cho mỗi request HTTP và sẽ được dùng lại cho toàn bộ các thao tác trong cùng một request đó.
builder.Services.AddDbContext<MyDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Bắt đầu mapping các controller
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

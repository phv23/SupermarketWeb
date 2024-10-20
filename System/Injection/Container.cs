using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Repositories;
using Business_Logic.Services;
using Data_Access.Repositories.Impl;
using Business_Logic.Services.Impl;

namespace WebSystem.Injection
{
    // Lớp này có tác dụng đăng kí các triển khai và class của đối tượng để ASP.NET quản lý.
    //Nhằm việc tránh phải tạo thủ công các đối tượng. VD: IUser A= new User(); thay thì vậy, khi đăng kí trong Container, chỉ cần khai báo IUser A;. Khi
    //chương trình chạy, ASP.NET sẽ dựa vào dữ kiện được chỉ định, nó lấy User được impl từ IUser và tạo 1 đối tượng thuộc kiểu User
    public static class Container
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryManager, RepositoryManager>();
            services.AddScoped<ICategoryService, CategoryService>();
           
        }
    }
}

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Context
{

    // Lớp này có tác dụng điều hướng các command line như "Add-Migration Init", "Update-Database" từ Secrets.json trong Connetected Serivces sang appsetting.json
    //Vì theo mặc định, khi ở chế độ Designed-Time ( Chế độ không chạy cả chương trình - Run-time nhưng chạy console làm chương trình thực hiện lệnh )
    //Các lệnh như Add-Migration Init sẽ ưu tiên lấy thông tin ở file Secrets.json
    public class ConfigurationDbContext : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
            {
            // Lấy đường dẫn đến thư mục gốc của dự án
            //Ví dụ đường dẫn cụ thể sau sẽ được lấy ra: D:\.Virtual Studio\Code Storage\SupermarketWeb\SupermarketWeb
            var basePath = Directory.GetCurrentDirectory();

                // Load cấu hình từ appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json")  // Sử dụng appsettings.json
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

                // Lấy chuỗi kết nối từ appsettings.json
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Cấu hình DbContext để sử dụng SQL Server
                optionsBuilder.UseSqlServer(connectionString);

                return new MyDbContext(optionsBuilder.Options);
        }
    }
}

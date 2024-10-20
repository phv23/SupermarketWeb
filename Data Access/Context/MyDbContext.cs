using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;
using Data_Access.Entities.Ready;

namespace Data_Access.Context
{
    // Lớp này có tác dụng quản lý các kết nối đến CSDL và ánh xạ các Entity vào các bảng trong CSDL
    // IdentityDbContext<UserEntity, IdentityRole, string> là lớp có sẵn trong ASP.NET Core giúp quản lý user, authen, autho,..
    //Kế thừa lớp IdentityDbContext giúp tích hợp sẵn hệ thống Identity của ASP.NET
    public class MyDbContext : IdentityDbContext<UserEntity, IdentityRole, string>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {}

        // Nếu không khai báo DbSet cho các entity, EF sẽ không biết quản lý những entity nào trong CSDL và
        //không thể tự động tạo hoặc ánh xạ bảng tương ứng. Mặc dù tạo nhiều lớp Entity nhưng cũng cần tạo ra 1 tập hợp Entity để EF
        //có thể quản lý
        public DbSet<UserEntity> Users {  get; set; }
        public DbSet<RoleEntity> Roles {  get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set;}

        // Cấu hình cách các Entity sẽ được ánh xạ vào CSDL. VD: Đặt tên bảng, thiết lập phạm vi các trường,
        //thiết lập mối quan hệ 1 - n, n -n
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gọi method của lớp cha (IdentityDbContext) để đảm bảo rằng các cấu hình mặc định của ASP.NET Identity
            //vẫn được áp dụng. Nếu không gọi OnModelCreating, ASP.NET sẽ không tạo các bảng như AspNetRoles, AspNetUsers,...
            base.OnModelCreating(modelBuilder);

            //AspNetUser
            modelBuilder.Entity<UserEntity>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("ASPNEt_Role");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("ASPNEt_User_Role");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("ASPNEt_User_Claim");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("ASPNEt_User_Token");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("ASPNEt_User_Login");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("ASPNEt_Role_Claim");
        }
    }
}

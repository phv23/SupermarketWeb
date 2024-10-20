using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    [Table("user")]
    // IdentityUser là class trong ASP.NET Identity để quản lý User, cung cấp các thuộc tính như PasswordHash,
    //SecurityStamp, Lockout,... 
    public class UserEntity : IdentityUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Status { get; set; }
        public int? Point { get; set; }
        // Cần trường Id để quản lý role. ( Có thể ko cần! )
        public int RoleId   { get; set; }
        // Đại diện cho mối quan hệ n-1 giữa UserEntity và RoleEntity
        public virtual RoleEntity Role { get; set; }
        public int? CartID { get; set; }
        public int? RankID { get; set; }

        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

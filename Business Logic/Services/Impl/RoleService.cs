//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Business_Logic.Services.Impl
//{
//    public class RoleService : IRoleService
//    {
//        private readonly RoleManager<IdentityRole> _roleManager;

//        public RoleService(RoleManager<IdentityRole> roleManager)
//        {
//            _roleManager = roleManager;
//        }

//        //Hàm này để trả về toàn bộ danh sách của bảng role, return để trả về tên của role,
//        ///vì sau này nếu select liên quan tới role, từ name có thể => id của role
//        public async Task<IEnumerable<SelectListItem>> GetRoleForDropdownList()
//        {
//            var roles = await _roleManager.Roles.ToListAsync();

//            return roles.Select(x => new SelectListItem
//            {
//                Value = x.Name,
//                Text = x.Name
//            });
//        }
//    }
//}

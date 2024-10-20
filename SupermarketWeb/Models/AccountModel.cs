using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class AccountModel
    {
        public string Id {  get; set; }
        [Required(ErrorMessage ="Vui lòng chọn quyền sở hữu")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Độ dài từ 2 tới 50 ký tự")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [MinLength(3, ErrorMessage ="Mật khẩu chứa ít nhất 3 ký tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
        [StringLength(maximumLength: 40, MinimumLength = 10, ErrorMessage = "Độ dài từ 10 tới 40 ký tự")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
        public string? Address {  get; set; }
        [Required(ErrorMessage ="Điều khoản chưa được đồng ý")]
        public bool IsActive {  get; set; }
        //[Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        [Phone]
        [RegularExpression(@"^(?:0)(?:3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-9])\d{7}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Upload)]
        //[ImageValidation(new string[] { ".png", ".jpg", ".jpeg"}, ErrorMessage="Ảnh không đúng định dạng")]
        public IFormFile? Avatar { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    
    public class NguoiDung
    {
        [Key]
        [Display(Name = "Mã nhân viên")]
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        public string UserID { get; set; }

        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        public string UserName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Tel { get; set; }

        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email không đúng!")]
        [Display(Name = "Email:")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mật khẩu:")]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        [NotMapped]
        [Display(Name = "Nhập lại mật khẩu:")]
        [DataType(DataType.Password)]
        public string ConfirmPassword
        {
            get;
            set;
        }

        public byte Disable { get; set; }
       

    }
}

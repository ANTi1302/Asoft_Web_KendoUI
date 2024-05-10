using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    
    public class NguoiDung
    {
        [Key]
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        public string UserID { get; set; }


        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        public string UserName { get; set; }
        public string Tel { get; set; }

        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        [Display(Name = "Account Email:")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [DataType(DataType.Password)]
        public string F_Password { get; set; }

        public byte Disable { get; set; }
       

    }
}

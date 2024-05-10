using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [Keyless]
    public class NguoiDungDTO
    {
        
        public string UserID { get; set; }

        public string UserName { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public byte Disable { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsEcomAPI.Data
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int? Id { get; set; }
        public string? UserName { get; set; }

        public string? PassWord { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Roles { get; set; } = "User";

        public int? Status { get; set; } = 1;
    }
}

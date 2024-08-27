using System.ComponentModel.DataAnnotations;

namespace KidsEcomAPI.Models
{
    public class ThongbaoModel
    {

        [Key]
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public int? IdSp { get; set; }
        public string? Size { get; set; }
        public DateTime DateTime { get; set; }
        public string? TenSp { get; set; }
        public int? Status { get; set; } = 1;
    }
}

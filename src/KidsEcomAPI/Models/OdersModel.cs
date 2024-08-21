using System.ComponentModel.DataAnnotations;

namespace KidsEcomAPI.Models
{
    public class OdersModel
    {
        [Key]
        public int? Id { get; set; }
        public int? IdSp { get; set; }
        public string? UserName { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public int? Status { get; set; } = 1;
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public string? Size { get; set; }
        public int? SoLuong { get; set; }
        public string? TenSp { get; set; }
        public decimal? GiaSp { get; set; }
        public decimal? subtotal { get; set; }
        public DateTime DateTime { get; set; }
    }
}

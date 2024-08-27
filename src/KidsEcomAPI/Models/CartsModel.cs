using System.ComponentModel.DataAnnotations;

namespace KidsEcomAPI.Models
{
    public class CartsModel
    {
        [Key]
        public int? Id { get; set; }
        public int? IdSp { get; set; }
        public string? UserName { get; set; }
        public string? TenSp { get; set; }
        public int? Status { get; set; } = -1;
        public string? Img { get; set; }
        public string? DanhMuc { get; set; }
        public decimal GiaSp { get; set; }
        public string? Size { get; set; }
        public int? Soluong { get; set; }
        public decimal? subtotal { get; set; }
        public string? Email { get; set; }
        public DateTime DateTime { get; set; }
    }
        public class CartInformodel
        {
            public int? IdSp { get; set; }
            public string? UserName { get; set; }
            public string? TenSp { get; set; }
            public string Status { get; set; }
            public string? Img { get; set; }
            public string? DanhMuc { get; set; }
            public decimal GiaSp { get; set; }
            public string? Size { get; set; }
            public int? Soluong { get; set; }
            public decimal? subtotal { get; set; }
            public string? Email { get; set; }
            public DateTime DateTime { get; set; }

        }
  
}
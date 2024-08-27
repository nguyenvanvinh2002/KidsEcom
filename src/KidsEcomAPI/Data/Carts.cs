using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KidsEcomAPI.Data
{
    [Table("Carts")]
    public class Carts
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
            public string? Size {  get; set; }
            public int? Soluong { get; set; }

            public decimal? subtotal { get; set; }
            public string? Email { get; set; }
             public DateTime DateTime {  get; set; }

        }
}

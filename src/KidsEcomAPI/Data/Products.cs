using KidsEcomAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsEcomAPI.Data
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int Id {  get; set; }

        public string TenSp {  get; set; }

   

        public decimal GiaSp { get; set; }

        public string? Img {  get; set; }
        public string  DanhMuc { get; set; } 
        
        public string Mota {  get; set; }
        public string GiamGia {  get; set; }
        public string? Imgphu { get; set; }
        public string? Imgphu1 { get; set; }



    }
}

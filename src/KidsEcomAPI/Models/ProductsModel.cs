using KidsEcomAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsEcomAPI.Models
{
    public class ProductsModel
    {
        [Key]
        public int Id { get; set; }
        public string TenSp { get; set; }

       
       
        public string DanhMuc { get; set; }
       
        public decimal GiaSp { get; set; }

        public string? Img { get; set; }
        public string Mota { get; set; }
        public string GiamGia { get; set; }
        public string? Imgphu { get; set; }
        public string? Imgphu1 { get; set; }
    }
    public class ProductsDTO
    {
      
        public string TenSp { get; set;}
        public string DanhMuc { get; set; }
        public decimal GiaSp { get; set; }
        public string? Img { get; set; }
        public string Mota { get; set; }
        public string GiamGia { get; set; }


    }
}

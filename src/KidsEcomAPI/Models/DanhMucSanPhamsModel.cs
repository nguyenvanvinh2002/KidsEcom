using KidsEcomAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsEcomAPI.Models
{
    public class DanhMucSanPhamsModel
    {
        [Key]
        public int? Id { get; set; }
        
        public string? DanhMuc { get; set; }
        public string? Img { get; set; }

    }
    public class DanhMucSanPhamDTO
    {
        public string? DanhMuc { get; set; }
        public string? Img { get; set; }
    }
}

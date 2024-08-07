using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsEcomAPI.Data
{
    [Table("DanhMucSanPhams")]
    public class DanhMucSanPhams
    {
        [Key]
        public int? Id { get; set; }
       
        public string? DanhMuc { get; set; }

        public string? Img {  get; set; }


       
    }
      
}

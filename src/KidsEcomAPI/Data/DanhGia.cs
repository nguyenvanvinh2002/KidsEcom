using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KidsEcomAPI.Data
{
    [Table("DanhGia")]
    public class DanhGia
    {
   
    
        [Key]
        public int? Id { get; set; }
        public int? IdSp { get; set; }
        public string? UserName { get; set; }

        public string? Comments {  get; set; }

        public DateTime? Datetime {  get; set; }
        
    }
}

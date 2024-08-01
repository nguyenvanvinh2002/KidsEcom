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

        public string MaSp {  get; set; }

        public string DanhMuc {  get; set; }

        public int GiaSp { get; set; }

        public string Img {  get; set; }
    }
}

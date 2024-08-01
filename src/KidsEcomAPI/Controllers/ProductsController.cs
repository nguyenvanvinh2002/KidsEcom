using KidsEcomAPI.Apikey;
using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KidsEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ProductsController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Attribuils]
        public IActionResult Get() {

            var lstproducts = _context.Products.ToList();
            return Ok(lstproducts);
        
        }
        [HttpPost]
        public IActionResult CreateProducts(ProductsModel products) {
            var lstSp = new Products
            {
                TenSp = products.TenSp,
                DanhMuc = products.DanhMuc,
                GiaSp = products.GiaSp,
                Img = products.Img,
                MaSp = products.MaSp,

            };
            _context.Add(lstSp);
            _context.SaveChanges();
            return Ok("thêm sản phẩm thành công");
        }
    }
}

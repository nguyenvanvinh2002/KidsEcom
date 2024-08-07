using KidsEcomAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KidsEcomAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;
        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.DanhMucs.ToList();
            return Ok(categories);
           
        }
        [ApiVersion("2.0")]
        [HttpGet("{DanhMuc}")]
        public IActionResult GetCategoriesDanhMuc(string DanhMuc)
        {
            var categories = _context.DanhMucs.SingleOrDefault(x => x.DanhMuc == DanhMuc);
            return Ok(categories);

        }
    }
}

using KidsEcomAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsEcomAPI.Controllers
{
  
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;
        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }
        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.DanhMucs.ToListAsync();
            return Ok(categories);
           
        }
        [ApiVersion("1.0")]
        [HttpGet("{DanhMuc}")]
        public async Task<IActionResult> GetCategoriesDanhMuc(string DanhMuc)
        {
            var categories = await _context.DanhMucs.SingleOrDefaultAsync(x => x.DanhMuc == DanhMuc);
            return Ok(categories);

        }
    }
}

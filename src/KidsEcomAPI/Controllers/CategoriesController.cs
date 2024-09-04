using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
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
        private readonly string _imgFodelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
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
        [ApiVersion("1.0")]
      
        [HttpPost]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> AddCategoriesDanhMuc([FromForm] DanhMucSanPhamDTO sanPhamDTO, IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                var extension = Path.GetExtension(formFile.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                var filePath = Path.Combine(_imgFodelPath, newFileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                sanPhamDTO.Img = newFileName;

            }
            var categories = new DanhMucSanPhams
            {
                DanhMuc = sanPhamDTO.DanhMuc,
                Img = sanPhamDTO.Img,
            };
            await _context.DanhMucs.AddAsync(categories);
            await _context.SaveChangesAsync();
            return Ok(categories);

        }
        [ApiVersion("1.0")]
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delecategories(int Id)
        {
            var lst = await _context.DanhMucs.FirstOrDefaultAsync(x => x.Id == Id);
            if (lst == null)
            {
                return Ok(new
                {
                    messege = "lỗi",
                    code=400
                });
                
            }
            else
            {   
                _context.DanhMucs.Remove(lst);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    messege = "Xóa thành công",
                    code = 200
                });
            }
        }
    }
}

using KidsEcomAPI.Apikey;
using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KidsEcomAPI.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly string _imgFodelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
        private readonly MyDbContext _context;
        public ProductsController(MyDbContext context)
        {
            _context = context;
        }
        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> Get() {

            var lstproducts = await _context.Products.ToListAsync();
            var random = new Random();
            var lstrandom = lstproducts.OrderBy(x =>random.Next()).ToList();
            return Ok(lstrandom);
        
        }
        [ApiVersion("1.0")]
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetProductsbyId(int Id) 
        {
            var lstproducts = await _context.Products.SingleOrDefaultAsync(x=>x.Id==Id);
            return Ok(lstproducts);

        }
        [ApiVersion("1.0")]
        [HttpGet("{DanhMuc}")]

        public async Task<IActionResult> Getcate(string DanhMuc)
        {


            var lstproducts =  _context.Products.Where(x =>x.DanhMuc  == DanhMuc);
            return Ok(lstproducts);

        }
        
        [ApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> AddProducts([FromForm] ProductsDTO productsDTO, IFormFile formFile)
        {
            if(formFile != null && formFile.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                var extension = Path.GetExtension(formFile.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
              
                var filePath = Path.Combine(_imgFodelPath, newFileName);
               

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                
                productsDTO.Img = newFileName;
                
            }
            var product = new Products
            {
                TenSp = productsDTO.TenSp,
                GiaSp = productsDTO.GiaSp,
                GiamGia = productsDTO.GiamGia,
                Mota = productsDTO.Mota,
                Img= productsDTO.Img,
                DanhMuc = productsDTO.DanhMuc,
             
                
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [ApiVersion("1.0")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleProducts(int Id)
        {
            var products = await _context.Products.SingleOrDefaultAsync(x => x.Id == Id);
            if(products == null)
            {
                return Ok("sai");
            }
            else
            {
                _context.Products.Remove(products);
              await  _context.SaveChangesAsync();
                return Ok(new
                {
                    messege="xóa thành công",
                    code=200
                });
            }
        }
        [ApiVersion("1.0")]
        [HttpPut("{Id}")]
        public async Task<IActionResult> updateProducts(int Id,ProductsModel products)
        {
            var lst = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            lst.DanhMuc = products.DanhMuc;
            lst.TenSp = products.TenSp;
            lst.Mota = products.Mota;
            lst.GiamGia = products.GiamGia;
            lst.GiaSp = products.GiaSp;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                messege = "cập nhật thành công",
                code = 200
            });

        }
        [ApiVersion("1.0")]
        [HttpGet("Search")]
        public async Task<IActionResult> search(string name)
        {
            var products = await _context.Products.Where(p => p.TenSp.Contains(name)).ToListAsync();
            return Ok(products);
        }

    }
}

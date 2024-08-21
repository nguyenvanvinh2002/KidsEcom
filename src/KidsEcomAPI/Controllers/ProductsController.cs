using KidsEcomAPI.Apikey;
using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return Ok(lstproducts);
        
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
        //[ApiVersion("1.0")]
        //[HttpPost]
        //public async Task<IActionResult> CreateProducts(ProductsModel products) {
        //    var lstSp = new Products
        //    {
        //        TenSp = products.TenSp,
        //       DanhMuc = products.DanhMuc,
        //        GiaSp = products.GiaSp,
        //        Img = products.Img,
        //        MaSp = products.MaSp,
        //        Mota = products.Mota,
        //        GiamGia = products.GiamGia,
        //        Imgphu = products.Imgphu,
        //        Imgphu1 = products.Imgphu1

        //    };
        //  await  _context.AddAsync(lstSp);
        //   await _context.SaveChangesAsync();
        //    return Ok("thêm sản phẩm thành công");
        //}
        [ApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> AddProducts([FromForm] ProductsDTO productsDTO, IFormFile formFile)
        {
            if(formFile != null && formFile.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                var extension = Path.GetExtension(formFile.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var newFileName1 = $"{fileName}_{Guid.NewGuid()}{extension}";
                var newFileName2 = $"{fileName}_{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_imgFodelPath, newFileName, newFileName1, newFileName2);
               
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                productsDTO.Img = newFileName;
                productsDTO.Imgphu = newFileName1;
                productsDTO.Imgphu1 = newFileName2;
            }
            var product = new Products
            {
                TenSp = productsDTO.TenSp,
                GiaSp = productsDTO.GiaSp,
                GiamGia = productsDTO.GiamGia,
                Mota = productsDTO.Mota,
                Img= productsDTO.Img,
                DanhMuc = productsDTO.DanhMuc,
                Imgphu = productsDTO.Imgphu,
                Imgphu1 = productsDTO.Imgphu1
                
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

    }
}

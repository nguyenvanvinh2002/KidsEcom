using KidsEcomAPI.Data;
using KidsEcomAPI.EnumBase;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using KidsEcomAPI.Common;
namespace KidsEcomAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly MyDbContext _context;
        public CartsController(MyDbContext context)
        {
            _context = context;
        }
        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            var lst = await _context.Carts.ToListAsync();
            return Ok(lst);
        }
        [ApiVersion("1.0")]
        [HttpGet("{UserName}")]
        public async Task<IActionResult> GetCartsbyUserName(string UserName)
        {
            List<CartInformodel> lstResult = new List<CartInformodel>();
            var lst =  _context.Carts.Where(x => x.UserName== UserName);
            foreach(var item in lst)
            {
                CartInformodel data = new CartInformodel();
                data.Status = ((Cart_Status)item.Status).GetDescriptionNew();
                data.Size = item.Size;
                data.UserName = item.UserName;
                data.TenSp = item.TenSp;
                data.DanhMuc = item.DanhMuc;
                data.IdSp = item.IdSp;
                data.Img = item.Img;
                data.Soluong = item.Soluong;
                data.GiaSp = item.GiaSp;
                lstResult.Add(data);

            }
            var soluong = lst.Count();
            HttpContext.Items["soluong"] = soluong;
            return Ok(lstResult);
        }
        [ApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> AddCarts(CartsModel carts)
        {
            var checkcart = await _context.Carts.FirstOrDefaultAsync(c => c.IdSp == carts.IdSp && c.UserName == carts.UserName && c.Size==carts.Size);
            if (checkcart != null)
            {
                checkcart.Soluong += carts.Soluong;
                _context.Carts.Update(checkcart);
                await _context.SaveChangesAsync();
                return Ok(checkcart);
            }
            else
            {
                var lst = new Carts
                {
                    Id = carts.Id,
                    IdSp = carts.IdSp,
                    UserName = carts.UserName,
                    TenSp = carts.TenSp,
                    Img = carts.Img,
                    DanhMuc = carts.DanhMuc,
                    GiaSp = carts.GiaSp,
                    Size = carts.Size,
                    Soluong = carts.Soluong
                    
                };

                await _context.AddAsync(lst);
                await _context.SaveChangesAsync();
                
                return Ok(lst);
            }
        }
        [ApiVersion("1.0")]
        [HttpPost("ByCart")]
        public async Task<IActionResult> ByCart(CartsModel carts)
        {
            var checkcart = await _context.Carts.FirstOrDefaultAsync(c => c.IdSp == carts.IdSp );
           
            
                var lst = new Carts
                {
                    Id = carts.Id,
                    IdSp = carts.IdSp,
                    UserName = carts.UserName,
                    TenSp = carts.TenSp,
                    Img = carts.Img,
                    DanhMuc = carts.DanhMuc,
                    GiaSp = carts.GiaSp,
                    Size = carts.Size,
                    Soluong = carts.Soluong,
                    Email = carts.Email,
                    subtotal = carts.GiaSp * carts.Soluong
                };
                await _context.AddAsync(lst);
             
                return Ok(lst);
            
        }
        [ApiVersion("1.0")]
        [HttpDelete("{IdSp}/{Size}/{UserName}")]
        public async Task<IActionResult> DeleteCartByIdAndSize(int IdSp, string Size,string UserName)
        {
        
            var lst = await _context.Carts.SingleOrDefaultAsync(x => x.IdSp == IdSp && x.Size == Size && x.UserName==UserName);

            if (lst == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Không tìm thấy bản ghi với Id và Size cung cấp."
                });
            }
            else
            {
                _context.Carts.Remove(lst);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    code = 200,
                    message = "Xóa thành công"
                });
            }
        }

    }
}

using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsEcomAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ThongbaoController : ControllerBase
    {
        public readonly MyDbContext _context;
        public ThongbaoController(MyDbContext context)
        {
            _context = context;
        }

        [ApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Addnotify(ThongbaoModel thongbao)
        {
            var lst = new Thongbao
            {
                IdSp = thongbao.IdSp,
                UserName = thongbao.UserName,
                TenSp = thongbao.TenSp,
                Size = thongbao.Size,
                Title = thongbao.Title,
                DateTime = DateTime.Now
            };
            await _context.AddAsync(lst);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                messege = "Thành công",
                code = 200
            });
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetallNotify()
        {
            var lst = await _context.Thongbaos.OrderByDescending(x=>x.DateTime).Take(10).ToListAsync();
            return Ok(lst);
        }
        [ApiVersion("1.0")]
        [HttpDelete("{UserName}")]
        public async Task<IActionResult> RemoveNotify(string UserName)
        {
            var lst = await _context.Thongbaos.Where(x => x.UserName == UserName).ToListAsync();
            _context.Thongbaos.RemoveRange(lst);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                messege = "thành công",
                code = 200
            });

        }
    }
}

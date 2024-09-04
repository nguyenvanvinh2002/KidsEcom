using KidsEcomAPI.Common;
using KidsEcomAPI.Data;
using KidsEcomAPI.EnumBase;
using KidsEcomAPI.Migrations;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsEcomAPI.Controllers
{
    [Route("api/v{vesion:apiVersion}/[controller]")]
    [ApiController]

    public class OdersController : ControllerBase
    {
        private readonly MyDbContext _context;
        public OdersController(MyDbContext context)
        {
            _context = context;
        }
        [ApiVersion("1.0")]
        [HttpGet]
      

        public async Task<IActionResult> getodder()
        {
            var lst = await _context.Oders.OrderByDescending(o => o.DateTime).ToListAsync();
            
            var sum = lst.Sum(lst => lst.GiaSp * lst.SoLuong);
            return Ok(new
            {
                lst,
                sum
            });
        }
        [ApiVersion("1.0")]
        [HttpGet("{UserName}")]
        

        public async Task<IActionResult> GetCartsbyUserName(string UserName)
        {
            List<OdersInfomodel> lstResult = new List<OdersInfomodel>();
            var lst = await _context.Oders.Where(x => x.UserName == UserName)
            .OrderByDescending(x => x.DateTime).Take(10).ToListAsync();
            foreach (var item in lst)
            {
                OdersInfomodel data = new OdersInfomodel();
                data.Status = ((Cart_Status)item.Status).GetDescriptionNew();
                data.Size = item.Size;
                data.Id = item.Id;
                data.UserName = item.UserName;
                data.TenSp = item.TenSp;
                data.IdSp = item.IdSp;
                data.HoVaTen = item.HoVaTen;
                data.subtotal = item.subtotal;
                data.SoLuong = item.SoLuong;
                data.GiaSp = item.GiaSp;
                data.DateTime = item.DateTime;
                data.Email = item.Email;
                data.SoDienThoai = item.SoDienThoai;
                data.DiaChi = item.DiaChi;
                data.DateTime = DateTime.Now;
                lstResult.Add(data);

            }
            return Ok(lstResult);
        }
        [ApiVersion("1.0")]
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> AddOders(OdersModel oders)
        {
            var lst = await _context.Oders.FirstOrDefaultAsync(x => x.IdSp == oders.IdSp);
           
                var newoder = new Oders
                {
                    IdSp = oders.IdSp,
                    UserName = oders.UserName,
                    DiaChi = oders.DiaChi,
                    SoDienThoai = oders.SoDienThoai,
                    HoVaTen = oders.HoVaTen,
                    Email = oders.Email,
                    Size = oders.Size,
                    SoLuong = oders.SoLuong,
                    TenSp = oders.TenSp,
                    GiaSp = oders.GiaSp,
                    subtotal = oders.subtotal,
                    DateTime = DateTime.Now
                };

                await _context.AddAsync(newoder);
                await _context.SaveChangesAsync();
                return Ok(newoder);

        }
        [ApiVersion("1.0")]
        [HttpGet("Oders/{Status}")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> getoder(int Status)
        {
            var lst = await _context.Oders.Where(x => x.Status == Status).OrderByDescending(x => x.DateTime).ToListAsync();
            if (lst == null)
            {
                return Ok("lỗi");
            }
            else
            {
                return Ok(new
                {
                    lst
                });
            }
        }
        [ApiVersion("1.0")]
        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> updatestatus(int? Id, [FromBody] OdersModel oders)
        {
            
            var oder = await _context.Oders.FirstOrDefaultAsync(x => x.Id == Id);
       
            //if (oder == null)
            //{
            //    return NotFound(new
            //    {
            //        message = "Đối tượng không tìm thấy",
            //        code = 404
            //    });
            //}

            if (oder.Status == 0)
            {
                return BadRequest(new
                {
                    message = "Trạng thái đã hoàn thành",
                    code = 400
                });
            }
            else
            {
                oder.Status = oders.Status;
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    messege = "thành công",
                    code = 200
                });
            }
        }
    }
}

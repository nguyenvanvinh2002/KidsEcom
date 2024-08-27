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
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        public UsersController(MyDbContext context)
        {
            _context = context;
        }
        [ApiVersion("1.0")]
        [HttpPut("{UserName}")]
        public async Task<IActionResult> UpdateUser(string UserName, UsersModel acc)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == UserName);

            user.HoVaTen = string.IsNullOrWhiteSpace(acc.HovaTen) ? user.HoVaTen : acc.HovaTen;
            user.GioiTinh = string.IsNullOrWhiteSpace(acc.GioiTinh) ? user.GioiTinh : acc.GioiTinh;
            user.Email = string.IsNullOrWhiteSpace(acc.Email) ? user.Email : acc.Email;
            user.SoDienThoai = string.IsNullOrWhiteSpace(acc.SoDienThoai) ? user.SoDienThoai : acc.SoDienThoai;
            user.DiaChi = string.IsNullOrWhiteSpace(acc.DiaChi) ? user.DiaChi : acc.DiaChi;
            user.Avatar = string.IsNullOrWhiteSpace(acc.Avatar) ? user.Avatar : acc.Avatar;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                messege ="cập nhật thành công",
                code = 200
            });

        }
        [ApiVersion("1.0")]
        [HttpGet("{UserName}")]
        public async Task<IActionResult> ProfileUser(string UserName)
        {
            var user = await _context.Users.Where(x => x.UserName == UserName).FirstOrDefaultAsync();
            return Ok(user);
        }
        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetallUser()
        {
            var user = await _context.Users.ToListAsync();
            var count = user.Count();
            return Ok(new
            {
                user,
               count
            });
          
        }
        [ApiVersion("1.0")]
        [HttpPut("Edit/{UserName}")]
        public async Task<IActionResult> EditUser(string UserName,UsersModel users)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == UserName);
            user.Status = users.Status;
            user.Roles = users.Roles;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                messege="thành công"
            });
        }
        [ApiVersion("1.0")]
        [HttpDelete("{UserName}")]
        public async Task<IActionResult> removeUser(string UserName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == UserName);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                messege = "thành công"
            });
        }

    }
}

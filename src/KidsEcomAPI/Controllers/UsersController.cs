using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [Authorize]
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
                messege = "cập nhật thành công",
                code = 200
            });
        }
        [ApiVersion("1.0")]
        [HttpGet]
        
        public async Task<IActionResult> GetallUser(string? q, int? type = 0)
        {
            try
            {
                var query = from u in _context.Users select new { u };
                if (!string.IsNullOrEmpty(q))
                {
                    query = query.Where(x => x.u.UserName.Contains(q));
                }
                var user = await query.Select(u => new Users()
                {
                    Id = u.u.Id,
                    UserName = u.u.UserName,
                    Avatar = u.u.Avatar,
                    DiaChi = u.u.DiaChi,
                    Email = u.u.Email,
                    GioiTinh = u.u.GioiTinh,
                    HoVaTen = u.u.HoVaTen,
                    SoDienThoai = u.u.SoDienThoai,
                    Status = u.u.Status,
                    Roles = u.u.Roles,
                }).ToListAsync();

                if (type == 1)
                {
                    Users newUser = new Users();
                    newUser = user.FirstOrDefault() ?? new Users();
                    return Ok(new
                    {
                        newUser
                    });

                }
                var count = query.Count();
                return Ok(new
                {
                    user,
                    count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while processing your request.",
                    Details = ex.Message
                });
            }
        }
        [ApiVersion("1.0")]
        [HttpPut("Edit/{UserName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string UserName, UsersModel users)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == UserName);
            user.Status = users.Status;
            user.Roles = users.Roles;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                messege = "thành công"
            });
        }
        [ApiVersion("1.0")]
        [HttpDelete("{UserName}")]
        [Authorize(Roles = "Admin")]
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

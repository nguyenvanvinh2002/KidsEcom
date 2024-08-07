using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MyDbContext _context;
        public RegisterController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register(UsersModel acc)
        {
            
            if (string.IsNullOrWhiteSpace(acc.UserName) || string.IsNullOrWhiteSpace(acc.PassWord)|| string.IsNullOrWhiteSpace(acc.DiaChi)|| string.IsNullOrWhiteSpace(acc.SoDienThoai))
            {
                return Ok(new
                {
                    messege = "Không được để trống tên người dùng hoặc mật khẩu.",
                    code = 400

                });
            }
            var check = _context.Users.Any(x => x.UserName == acc.UserName);
            if (check)
            {
                return Ok(new
                {
                    messege = "Tài khoản đã tồn tại",
                    code= 401
                });
            }
            var newUser = new Users
            {
                
                UserName = acc.UserName,
                PassWord = acc.PassWord,
                SoDienThoai = acc.SoDienThoai,
                DiaChi = acc.DiaChi,



            };

            _context.Add(newUser);
            _context.SaveChanges();
            return Ok(new
            {
                messege = "Đăng ký thành công",
                code = 200
            });

        }
    }
}

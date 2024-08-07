using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KidsEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _context;
        public LoginController(MyDbContext context)
        {
              _context = context;
        }
        [HttpPost]
        public IActionResult Login(UsersModel acc)
        {

            var user = _context.Users.SingleOrDefault(x => x.UserName == acc.UserName && x.PassWord == acc.PassWord);
            if ( user == null)
            {
                return Ok(new
                {
                    messege = "Tài khoản hoặc mật khẩu không chính xác",
                    code =400
                });
            }
            if(user != null && user.Status == 0)
            {
                return Ok(new
                {
                    messege =" Tài khoản đang bị khóa",
                    code = 401
                });
            }
            else
            {
               
                return Ok(new
                {
                    messege = "Đăng nhập thành công",
                    user.UserName,
                    user.DiaChi,
                    user.SoDienThoai,
                    user.Status,
                    user.Roles,
                    user.Id,
                    code = 200


                });
            }

        }
       
    }
}

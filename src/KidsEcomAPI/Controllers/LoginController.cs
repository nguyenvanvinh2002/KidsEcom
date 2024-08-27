using KidsEcomAPI.Apikey;
using KidsEcomAPI.Data;
using KidsEcomAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KidsEcomAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(MyDbContext context,IConfiguration configuration)
        {
              _context = context;
            _configuration = configuration;

        }
        [HttpPost]
        public async Task<IActionResult> Login(UsersModel acc)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == acc.UserName && x.PassWord == acc.PassWord);
            var lst = _context.Carts.Where(x => x.UserName == acc.UserName);
            var soluong = lst.Count();
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
                var claims = new[]
                {
                     new Claim(JwtRegisteredClaimNames.Sub , _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Roles",user.Roles)

                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var Login = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                     _configuration["Jwt:Issuer"],
                     _configuration["Jwt:Audience"],
                     claims,
                     expires: DateTime.UtcNow.AddMinutes(60),
                     signingCredentials: Login
                    );
                string tokenvalue = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    messege = "Đăng nhập thành công",
                    token = tokenvalue,
                    user.UserName,
                    user.DiaChi,
                    user.SoDienThoai,
                    user.Status,
                    user.Roles,
                    user.Id,
                    user.HoVaTen,
                    user.Email,
                    user.GioiTinh,
                    user.Avatar,
                    code = 200,


                });

                //return Ok(new
                //{
                //    messege = "Đăng nhập thành công",
                //    user.UserName,
                //    user.DiaChi,
                //    user.SoDienThoai,
                //    user.Status,
                //    user.Roles,
                //    user.Id,
                //    user.HoVaTen,
                //    user.Email,
                //    user.GioiTinh,
                //    user.Avatar,
                //    soluong,
                //    code = 200,



           
            }

        }
       
    }
}

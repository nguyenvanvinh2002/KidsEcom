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

    public class DanhGiaController : ControllerBase
        {
            private readonly MyDbContext _context;
            public DanhGiaController(MyDbContext context)
            {
                _context = context;
            }
            [ApiVersion("1.0")]
            [HttpGet]
            public async Task<IActionResult> GetAllDanhGia()
            {
                var lst =await _context.DanhGias.ToListAsync();
           
                return Ok(lst);

           
            }
            [ApiVersion("1.0")]
            [HttpPost]
        [Authorize]    

        public async Task<IActionResult> PostDanhGia(DanhGiaModel danhGia)
            {
            
                var lst = new DanhGia
                {
                    IdSp = danhGia.IdSp,
                    UserName = danhGia.UserName,
                    Comments = danhGia.Comments,
                    Datetime = DateTime.Now
                };
            if(lst.Comments == "")
            {
                return Ok(new
                {
                    messege = "Hãy đánh giá thật lòng",
                    code = 400
                });
            }
            else
            {
                await _context.AddAsync(lst);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    messege = "Đánh giá của bạn đã được gửi đi",
                    code = 200
                });
            }
                



            }

        }
    }

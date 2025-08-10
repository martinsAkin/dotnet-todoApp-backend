using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoApp.Data;
using todoApp.models;
using todoApp.models.requests;

namespace todoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            var userExisting = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (userExisting)
            {
                return BadRequest(new { error = "User already exists!" });
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User { Email = request.Email, PasswordHash = hashedPassword };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User Created successfully." });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if(user == null)
            {
                return BadRequest("Invalid Email or Password! Please check credentials and try again");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid) {
                return BadRequest("Invalid Email or Password!");
            }

            return Ok(new { message = "Login Successful!" });
        }
    }

}
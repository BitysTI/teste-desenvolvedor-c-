using BitysTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitysTest.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase {

        private readonly UsersContext _context;

        public UsersController(UsersContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers() {
            var users = await _context.Users.ToListAsync();
            foreach (var user in users) {
                user.Password = "";
            }
            return Ok(users);
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult> GetUser(int id) {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }
            user.Password = "";
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await GetUser(user.Id);
        }

        [HttpPut, Route("{id}")]
        public async Task<ActionResult> PutUser(int id, User user) {
            if (id != user.Id) {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!_context.Users.Any(p => p.Id == id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id) {
            var user = await _context.Users.FindAsync(id);

            if (user == null) { return NotFound(); }

            _context.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}

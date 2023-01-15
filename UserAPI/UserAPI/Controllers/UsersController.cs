using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    public class UsersController : Controller
    {
        private readonly UserDBContext _context;

        public UsersController(UserDBContext context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: Users/Details/5
        [HttpGet("Users/Details/{id}")]
        public async Task<ActionResult<User>> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: Users/Create
        [HttpPost("Users/Create")]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Create), user);
            }
            return user;
        }

        // POST: Users/Edit/5
        [HttpPut("Users/Edit/{id}")]
        public async Task<ActionResult<User>> Edit(string id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return user;
        }

        // POST: Users/Delete/5
        [HttpDelete("Users/Delete/{id}"), ActionName("Delete")]
        public async Task<ActionResult<int>> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return id;
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
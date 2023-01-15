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
    public class WalletsController : Controller
    {
        private readonly UserDBContext _context;

        public WalletsController(UserDBContext context)
        {
            _context = context;
        }

        // GET: Wallets
        [HttpGet("Wallets")]
        public async Task<ActionResult<IEnumerable<Wallet>>> Index()
        {
            return await _context.Wallets.ToListAsync();
        }

        // GET: Wallets for user
        [HttpGet("Wallets/User/{id}")]
        public async Task<ActionResult<IEnumerable<Wallet>>> UsersWallet(string? id)
        {
            if (id != null)
            {
                return await _context.Wallets.Where(s => s.UserId == id).ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Wallets/Details/5
        [HttpGet("Wallets/Details/{id}")]
        public async Task<ActionResult<Wallet>> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return wallet;
        }

        // POST: Wallets/Create
        [HttpPost("Wallets/Create")]
        public async Task<ActionResult<Wallet>> Create([FromBody] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return wallet;
        }

        // POST: Wallets/Edit
        [HttpPut("Wallets/Edit")]
        public async Task<ActionResult<Wallet>> Edit([FromBody] Wallet wallet)
        {
            //string walletID = _context.Wallets.Where(s => s.UserId == wallet.UserId).Where(s => s.CurrencyID == wallet.CurrencyID).FirstOrDefault().Id;
            if (wallet.Id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.Id))
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
            return wallet;
        }

        // POST: Wallets/Delete/5
        [HttpDelete("Wallets/Delete/{id}"), ActionName("Delete")]
        public async Task<ActionResult<string>> DeleteConfirmed(string id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return id;
        }

        private bool WalletExists(string id)
        {
            return _context.Wallets.Any(e => e.Id == id);
        }
    }
}
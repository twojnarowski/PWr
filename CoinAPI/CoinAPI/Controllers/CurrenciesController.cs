using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinAPI.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoinAPI.Controllers
{
    [EnableCors("AllowOrigin")]

    public class CurrenciesController : Controller
    {
        private readonly CoinDBContext _context;

        public CurrenciesController(CoinDBContext context)
        {
            _context = context;
        }

        // GET: Currencies
        [HttpGet("Currencies")]
        public async Task<ActionResult<IEnumerable<Currency>>> Index()
        {
            return await _context.Currencies.ToListAsync();
        }

        // GET: Currencies/Details/5
        [HttpGet("Currencies/Details/{id}")]
        public async Task<ActionResult<Currency>> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.CurrencyID == id);
            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // GET: Currencies/GetID/5
        [HttpGet("Currencies/GetID/{name}")]
        public async Task<ActionResult<string>> GetID(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.Name == name);
            if (currency == null)
            {
                return NotFound();
            }

            return currency.CurrencyID;
        }

        // POST: Currencies/Create
        [HttpPost("Currencies/Create")]
        public async Task<ActionResult<Currency>> Create([FromBody] Currency currency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currency);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Create), currency);
            }
            return currency;
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Currencies/Edit/{id}")]
        public async Task<ActionResult<Currency>> Edit(string id, [FromBody] Currency currency)
        {
            if (id != currency.CurrencyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.CurrencyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction(nameof(Index), currency);
            }
            return currency;
        }

        // POST: Currencies/Delete/5
        [HttpDelete("Currencies/Delete/{id}"), ActionName("Delete")]
        public async Task<ActionResult<string>> DeleteConfirmed(string id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
            return id;
        }

        private bool CurrencyExists(string id)
        {
            return _context.Currencies.Any(e => e.CurrencyID == id);
        }
    }
}
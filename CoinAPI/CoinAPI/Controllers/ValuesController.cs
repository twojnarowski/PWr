using System;
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

    public class ValuesController : Controller
    {
        private readonly CoinDBContext _context;

        public ValuesController(CoinDBContext context)
        {
            _context = context;
        }

        [HttpGet("Values")]
        // GET: Values
        public async Task<ActionResult<IEnumerable<Value>>> Index()
        {
            return await _context.Values.ToListAsync();
        }

        [HttpGet("Values/Details/{id}")]
        // GET: Values/Details/5
        public async Task<ActionResult<Value>> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Values.OrderByDescending(m => m.Timestamp)
                .FirstOrDefaultAsync(m => m.CurrencyID == id);
            if (value == null)
            {
                return NotFound();
            }

            return value;
        }      
        
        [HttpGet("Values/Chart/{id}")]
        // GET: Values/Chart/5
        public async Task<ActionResult<List<Tuple<DateTime?, decimal>>>> Chart(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temp = await _context.Values.OrderByDescending(m => m.Timestamp)
                                        .Where(m => m.CurrencyID == id)
                                        .Take(10)
                                        .ToListAsync();
            return temp.Select(i => new Tuple<DateTime?, decimal>(i.Timestamp, i.Rate)).ToList();
        }

        // POST: Values/Create
        [HttpPost("Values/Create")]
        public async Task<ActionResult<Value>> Create([FromBody] Value value)
        {
            if (ModelState.IsValid)
            {
                _context.Add(value);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return value;
        }

        [HttpPut("Values/Edit/{id}")]
        // POST: Values/Edit/5
        public async Task<ActionResult<Value>> Edit(string id, [FromBody] Value value)
        {
            if (id != value.ValueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(value);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueExists(value.ValueID))
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
            return value;
        }

        // POST: Values/Delete/5
        [HttpDelete("Values/Delete/{id}"), ActionName("Delete")]
        public async Task<ActionResult<string>> DeleteConfirmed(string id)
        {
            var value = await _context.Values.FindAsync(id);
            _context.Values.Remove(value);
            await _context.SaveChangesAsync();
            return id;
        }

        private bool ValueExists(string id)
        {
            return _context.Values.Any(e => e.ValueID == id);
        }
    }
}
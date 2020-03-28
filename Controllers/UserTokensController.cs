using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using usersapi.Models;

namespace usersapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTokensController : ControllerBase
    {
        private readonly DataContext _context;

        public UserTokensController(DataContext context)
        {
            _context = new DataContext();
        }

        // GET: api/UserTokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetUserTokens>>> GetAspNetUserTokens()
        {
            return await _context.AspNetUserTokens.ToListAsync();
        }

        // GET: api/UserTokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AspNetUserTokens>> GetAspNetUserTokens(string id)
        {
            var aspNetUserTokens = await _context.AspNetUserTokens.FindAsync(id);

            if (aspNetUserTokens == null)
            {
                return NotFound();
            }

            return aspNetUserTokens;
        }

        // PUT: api/UserTokens/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserTokens(string id, AspNetUserTokens aspNetUserTokens)
        {
            if (id != aspNetUserTokens.UserId)
            {
                return BadRequest();
            }

            _context.Entry(aspNetUserTokens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserTokensExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserTokens
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AspNetUserTokens>> PostAspNetUserTokens(AspNetUserTokens aspNetUserTokens)
        {
            _context.AspNetUserTokens.Add(aspNetUserTokens);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserTokensExists(aspNetUserTokens.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAspNetUserTokens", new { id = aspNetUserTokens.UserId }, aspNetUserTokens);
        }

        // DELETE: api/UserTokens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AspNetUserTokens>> DeleteAspNetUserTokens(string id)
        {
            var aspNetUserTokens = await _context.AspNetUserTokens.FindAsync(id);
            if (aspNetUserTokens == null)
            {
                return NotFound();
            }

            _context.AspNetUserTokens.Remove(aspNetUserTokens);
            await _context.SaveChangesAsync();

            return aspNetUserTokens;
        }

        private bool AspNetUserTokensExists(string id)
        {
            return _context.AspNetUserTokens.Any(e => e.UserId == id);
        }
    }
}

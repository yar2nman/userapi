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
    public class RoleClaimsController : ControllerBase
    {
        private readonly DataContext _context;

        public RoleClaimsController()
        {
            _context = new DataContext();
        }

        // GET: api/RoleClaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetRoleClaims>>> GetAspNetRoleClaims()
        {
            return await _context.AspNetRoleClaims.ToListAsync();
        }

        // GET: api/RoleClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AspNetRoleClaims>> GetAspNetRoleClaims(int id)
        {
            var aspNetRoleClaims = await _context.AspNetRoleClaims.FindAsync(id);

            if (aspNetRoleClaims == null)
            {
                return NotFound();
            }

            return aspNetRoleClaims;
        }

        // PUT: api/RoleClaims/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetRoleClaims(int id, AspNetRoleClaims aspNetRoleClaims)
        {
            if (id != aspNetRoleClaims.Id)
            {
                return BadRequest();
            }

            _context.Entry(aspNetRoleClaims).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetRoleClaimsExists(id))
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

        // POST: api/RoleClaims
        [HttpPost]
        public async Task<ActionResult<AspNetRoleClaims>> PostAspNetRoleClaims(AspNetRoleClaims aspNetRoleClaims)
        {
            _context.AspNetRoleClaims.Add(aspNetRoleClaims);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAspNetRoleClaims", new { id = aspNetRoleClaims.Id }, aspNetRoleClaims);
        }

        // DELETE: api/RoleClaims/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AspNetRoleClaims>> DeleteAspNetRoleClaims(int id)
        {
            var aspNetRoleClaims = await _context.AspNetRoleClaims.FindAsync(id);
            if (aspNetRoleClaims == null)
            {
                return NotFound();
            }

            _context.AspNetRoleClaims.Remove(aspNetRoleClaims);
            await _context.SaveChangesAsync();

            return aspNetRoleClaims;
        }

        private bool AspNetRoleClaimsExists(int id)
        {
            return _context.AspNetRoleClaims.Any(e => e.Id == id);
        }
    }
}

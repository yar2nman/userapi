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
    public class UserRolesController : ControllerBase
    {
        private readonly DataContext _context;

        public UserRolesController()
        {
            _context = new usersapi.Models.DataContext();
        }

        // GET: api/UserRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetUserRoles>>> GetAspNetUserRoles()
        {
            return await _context.AspNetUserRoles.ToListAsync();
        }

        // GET: api/UserRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AspNetUserRoles>> GetAspNetUserRoles(string id)
        {
            var aspNetUserRoles = await _context.AspNetUserRoles.FindAsync(id);

            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            return aspNetUserRoles;
        }

        // PUT: api/UserRoles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserRoles(string id, AspNetUserRoles aspNetUserRoles)
        {
            if (id != aspNetUserRoles.UserId)
            {
                return BadRequest();
            }

            _context.Entry(aspNetUserRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserRolesExists(id))
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

        // POST: api/UserRoles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AspNetUserRoles>> PostAspNetUserRoles(AspNetUserRoles aspNetUserRoles)
        {
            _context.AspNetUserRoles.Add(aspNetUserRoles);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserRolesExists(aspNetUserRoles.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAspNetUserRoles", new { id = aspNetUserRoles.UserId }, aspNetUserRoles);
        }

        // DELETE: api/UserRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AspNetUserRoles>> DeleteAspNetUserRoles(string id)
        {
            var aspNetUserRoles = await _context.AspNetUserRoles.FindAsync(id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            _context.AspNetUserRoles.Remove(aspNetUserRoles);
            await _context.SaveChangesAsync();

            return aspNetUserRoles;
        }

        private bool AspNetUserRolesExists(string id)
        {
            return _context.AspNetUserRoles.Any(e => e.UserId == id);
        }
    }
}

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
    public class UsersController : ControllerBase
    {
        usersapi.Models.DataContext _context;

        public UsersController()
        {
             _context = new usersapi.Models.DataContext();
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<object>> GetAspNetUsers()
        {
           // return new {data = await _context.AspNetUsers.ToListAsync()};
            return new {
              result = "Ok",  
              data =  await _context.AspNetUsers.ToListAsync(),
              count = await _context.AspNetUsers.CountAsync(),
            }; 
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AspNetUsers>> GetAspNetUsers([FromRoute] string id)
        {
            var aspNetUsers = await _context.AspNetUsers.FindAsync(id);

            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return aspNetUsers;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUsers([FromRoute] string id, [FromBody] AspNetUsers aspNetUsers)
        {
            if (id != aspNetUsers.Id)
            {
                return BadRequest();
            }

            _context.Entry(aspNetUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUsersExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<AspNetUsers>> PostAspNetUsers([FromRoute] AspNetUsers aspNetUsers)
        {
            _context.AspNetUsers.Add(aspNetUsers);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetUsersExists(aspNetUsers.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAspNetUsers", new { id = aspNetUsers.Id }, aspNetUsers);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AspNetUsers>> DeleteAspNetUsers([FromRoute] string id)
        {
            var aspNetUsers = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            _context.AspNetUsers.Remove(aspNetUsers);
            await _context.SaveChangesAsync();

            return aspNetUsers;
        }

        private bool AspNetUsersExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}

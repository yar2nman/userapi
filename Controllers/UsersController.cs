using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using usersapi.Models;

namespace usersapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    [Authorize]
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
        public async Task<ActionResult<AspNetUsers>> PostAspNetUsers([FromBody] AspNetUsers aspNetUsers)
        {
            string oldpass = aspNetUsers.PasswordHash;
            string newpass = IdentityV3PasswordHasher.PasswordHasher.GenerateIdentityV3Hash(oldpass);

            aspNetUsers.PasswordHash = newpass;
            aspNetUsers.Id = Guid.NewGuid().ToString();
            aspNetUsers.SecurityStamp = "d4711bf0-fc08-4ad9-9537-c6979605bb92";

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


        // Patch: api/Users/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser([FromRoute] string id, [FromBody] object value) {
            if (id == "" || id is null)
            {
                return BadRequest();
            }

            try
            {
                AspNetUsers user = await _context.AspNetUsers.FindAsync(id);
                if (user is null)
                {
                    throw new System.Exception();
                }
            }
            catch (System.Exception)
            {
                return NotFound();
            }

            await helper.ApplyPatchAsync(User, value);

            return NoContent();

            
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

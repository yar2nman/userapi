
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using usersapi.Models;

namespace usersapi.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Produces("application/json")]
    [Route("api/Roles")]
    public class RolesController : Controller
    {
        usersapi.Models.DataContext _context;
        public RolesController()
        {
             _context = new usersapi.Models.DataContext();
        }


        // GET: api/Roles
        [HttpGet]
        public async Task<object> GetRoles()
        {

            IQueryable<usersapi.Models.AspNetRoles> result = _context.AspNetRoles;

            result = from r in result
                     orderby r.Id
                     select r;
            
            
            return new {
              result = "Ok",  
              data =  await result.ToListAsync(),
              count = await result.CountAsync(),
            }; 
            
            
                
        }


        [HttpPost("api/[controller]")]
        public async Task<IActionResult> PostRole([FromBody] usersapi.Models.AspNetRoles value) {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AspNetRoles.Add(value);
       
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutRole([FromRoute] int id, [FromBody] AspNetRoles value) {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             _context.Entry(value).State = EntityState.Modified;

             await _context.SaveChangesAsync();

             return AcceptedAtAction("Updated", value);
        }
        
        
    }
}



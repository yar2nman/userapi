
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace usersapi {
static class helper {
    public static string connstr = "";

    public static async Task ApplyPatchAsync(object entity, object patchObject)
{
    DbContext _context = new usersapi.Models.DataContext();

    var dbEntityEntry = _context.Entry(entity);
    dbEntityEntry.CurrentValues.SetValues(patchObject);
    dbEntityEntry.State = EntityState.Modified;
    await _context.SaveChangesAsync();
}



}



}


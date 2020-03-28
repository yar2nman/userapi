
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace usersapi {
static class helper {
    /// <summary>
    /// string holding the conniction string
    /// It is populated using the secret item or the enviroment variable
    /// Use ConnStr to hold the value of the connection string in the secrete or in the enviroment variables
    /// </summary>
    public static string connstr = "";


/// <summary>
/// function to update entity using input object and save the updated to DB
/// </summary>
/// <param name="entity">The entity that we need to Patch</param>
/// <param name="patchObject">Object with keys and values to be updated</param>
/// <returns></returns>
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


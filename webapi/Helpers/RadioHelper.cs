using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Helpers
{
    public class RadioHelper
    {
        private readonly WebApiContext _context;

        public RadioHelper(WebApiContext context)
        {
            _context = context;
        }

        public async Task<Boolean> IsLocationAllowed(string newLocation)
        {
            var list = await GetLocations();
            
            foreach(var location in list )
            {
                if (location.name == newLocation)
                {
                    return true;
                } 
            }
            return false;
            
        }

        public async Task<IEnumerable<AllowedLocation>> GetLocations()
        {
            return await _context.AllowedLocations.ToListAsync();
            
        }
    }
}

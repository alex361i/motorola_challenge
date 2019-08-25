using System;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Radio> Radios { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOTUGraphQL.Data
{
    public class MotuContext : DbContext
    {
        public MotuContext(DbContextOptions<MotuContext> options)
             : base(options)
        {
        }

        public DbSet<Faction> Factions { get; set; }
        public DbSet<MotuCharacter> MotuCharacters { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Models;

namespace PetAdopt.Data
{
    public class PetAdoptContext : DbContext
    {
        public PetAdoptContext (DbContextOptions<PetAdoptContext> options)
            : base(options)
        {
        }

        public DbSet<PetAdopt.Models.Animal> Animal { get; set; } = default!;

        public DbSet<PetAdopt.Models.Post>? Post { get; set; }
    }
}

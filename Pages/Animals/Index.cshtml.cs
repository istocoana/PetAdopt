using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;

namespace PetAdopt.Pages.Animals
{
    public class IndexModel : PageModel
    {
        private readonly PetAdopt.Data.PetAdoptContext _context;

        public IndexModel(PetAdopt.Data.PetAdoptContext context)
        {
            _context = context;
        }

        public IList<Animal> Animal { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Animal != null)
            {
                Animal = await _context.Animal.ToListAsync();
            }
        }
    }
}

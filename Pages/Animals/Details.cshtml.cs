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
    public class DetailsModel : PageModel
    {
        private readonly PetAdopt.Data.PetAdoptContext _context;

        public DetailsModel(PetAdopt.Data.PetAdoptContext context)
        {
            _context = context;
        }

      public Animal Animal { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FirstOrDefaultAsync(m => m.id == id);
            if (animal == null)
            {
                return NotFound();
            }
            else 
            {
                Animal = animal;
            }
            return Page();
        }
    }
}

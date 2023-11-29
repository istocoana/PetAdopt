using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetAdopt.Data;
using PetAdopt.Models;

namespace PetAdopt.Pages.Animals
{
    public class CreateModel : PageModel
    {
        private readonly PetAdopt.Data.PetAdoptContext _context;

        public CreateModel(PetAdopt.Data.PetAdoptContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;
        public AnimalSpecies SelectedSpecies { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Animal == null || Animal == null)
            {
                return Page();
            }

            _context.Animal.Add(Animal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

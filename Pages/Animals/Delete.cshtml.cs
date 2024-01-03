using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;

namespace PetAdopt.Pages.Animals
{
    [Authorize(Policy = "EditOrDeletePolicy")]

    public class DeleteModel : PageModel
    {
        private readonly PetAdopt.Data.PetAdoptContext _context;

        public DeleteModel(PetAdopt.Data.PetAdoptContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null || userId == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FirstOrDefaultAsync(m => m.id == id && m.UserId == userId);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null || userId == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);

            if (animal != null && animal.UserId == userId)
            {
                Animal = animal;
                _context.Animal.Remove(Animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

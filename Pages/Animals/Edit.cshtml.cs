using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Security.Claims;

namespace PetAdopt.Pages.Animals
{
    public class EditModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public EditModel(PetAdoptContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; }

        [BindProperty]
        public AnimalSpecies SelectedSpecies { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obține ID-ul utilizatorului curent
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Animal = await _context.Animal.FirstOrDefaultAsync(m => m.id == id && m.UserId == userId);

            if (Animal == null)
            {
                return NotFound();
            }

            SelectedSpecies = Animal.animal_speacies;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Actualizarea proprietății animal_speacies cu specia selectată
            Animal.animal_speacies = SelectedSpecies;

            _context.Attach(Animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(Animal.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.id == id);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PetAdopt.Pages.Animals
{
    [Authorize] 
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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Animal.UserId = userId; 

            var existingAnimal = await _context.Animal.FindAsync(Animal.id);

            if (existingAnimal == null)
            {
                return NotFound();
            }

            existingAnimal.animal_speacies = SelectedSpecies;
            existingAnimal.animal_breed = Animal.animal_breed;
            existingAnimal.animal_age = Animal.animal_age;
            existingAnimal.animal_name = Animal.animal_name;

            try
            {
                _context.Attach(existingAnimal).State = EntityState.Modified;
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

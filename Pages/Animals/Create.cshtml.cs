using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Animals
{
    public class CreateModel : PageModel
    {
        private readonly PetAdoptContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(PetAdoptContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Animal Animal { get; set; }

        [BindProperty]
        public AnimalSpecies SelectedSpecies { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*
            if (!ModelState.IsValid)
            {
                return Page();
            } */

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                Animal.UserId = currentUser.Id;
                Animal.animal_speacies = SelectedSpecies; 

                _context.Animal.Add(Animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

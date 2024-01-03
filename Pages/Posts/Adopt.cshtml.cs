using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Posts
{
    public class AdoptModel : PageModel
    {
        private readonly PetAdoptContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdoptModel(PetAdoptContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public AdoptionRequest AdoptionRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int id) 
        {
            AdoptionRequest = new AdoptionRequest
            {
                PostId = id 
            };


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }


            var adoptionRequest = new AdoptionRequest
            {
                UserId = _userManager.GetUserId(User),
                PostId = id,
                Message = AdoptionRequest.Message,
                IsAccepted = false
            };


           
            _context.AdoptionRequest.Add(adoptionRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}

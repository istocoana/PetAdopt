using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Posts
{
    public class AdoptModel : PageModel
    {
        private readonly PetAdoptContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public AdoptionRequest AdoptionRequest { get; set; }
        public Post Post { get; set; }

            public Animal Animal { get; set; }

        public AdoptModel(PetAdoptContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await _context.Post.FirstOrDefaultAsync(m => m.id == id);


            if (Post == null)
            {
                return NotFound();
            }

          
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid)
            {
                Post = await _context.Post.FirstOrDefaultAsync(m => m.id == id);
                //return Page();
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
                IsAccepted = false,
                Location = AdoptionRequest.Location,
                PhoneNumber = AdoptionRequest.PhoneNumber,
                Email = AdoptionRequest.Email
            };

            _context.AdoptionRequest.Add(adoptionRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Posts/Index");
        }
    }
}

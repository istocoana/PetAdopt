// Pages/AdoptionRequests/Details.cshtml.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Threading.Tasks;

namespace PetAdopt.Pages.AdoptionRequests
{
    public class DetailsModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public DetailsModel(PetAdoptContext context)
        {
            _context = context;
        }

        public AdoptionRequest AdoptionRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdoptionRequest = await _context.AdoptionRequest
                .Include(ar => ar.Post)
                .Include(ar => ar.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (AdoptionRequest == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

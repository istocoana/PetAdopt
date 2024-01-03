using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Security.Claims;

namespace PetAdopt.Pages.AdoptionRequests
{
    public class MyAdoptionRequestsModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public MyAdoptionRequestsModel(PetAdoptContext context)
        {
            _context = context;
        }

        public List<AdoptionRequest> UserAdoptionRequests { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // ID-ul utilizatorului autentificat

            UserAdoptionRequests = await _context.AdoptionRequest
                .Where(ar => ar.UserId == userId)
                .ToListAsync();
        }
    }

}

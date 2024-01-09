using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdopt.Pages
{
    public class AllAdoptionRequestsModel : PageModel
    {
        private readonly PetAdoptContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AllAdoptionRequestsModel(PetAdoptContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<AdoptionRequest> AllAdoptionRequests { get; set; }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return;
            }

            AllAdoptionRequests = await _context.AdoptionRequest
                .Where(ar => ar.UserId == currentUser.Id)
                .ToListAsync();
        }
    }
}

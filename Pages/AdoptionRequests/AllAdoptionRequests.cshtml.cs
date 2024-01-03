using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdopt.Pages
{
    public class AllAdoptionRequestsModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public AllAdoptionRequestsModel(PetAdoptContext context)
        {
            _context = context;
        }

        public IList<AdoptionRequest> AdoptionRequests { get; set; }

        public async Task OnGetAsync()
        {
            AdoptionRequests = await _context.AdoptionRequest
                .Include(ar => ar.Post) 
                .ToListAsync();
        }
    }
}

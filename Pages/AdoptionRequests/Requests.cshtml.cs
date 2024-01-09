using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdopt.Pages
{
    public class RequestsModel : PageModel
    {
        private readonly PetAdoptContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public List<Post> MyPosts { get; set; }

        public RequestsModel(PetAdoptContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            MyPosts = await _context.Post
                .Include(p => p.AdoptionRequests)
                .Where(p => p.UserId == currentUser.Id)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAcceptAsync(int requestId)
        {
            var request = await _context.AdoptionRequest.FindAsync(requestId);

            if (request != null)
            {
                request.IsAccepted = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/AdoptionRequests/Requests");
        }

        public async Task<IActionResult> OnPostRejectAsync(int requestId)
        {
            var request = await _context.AdoptionRequest.FindAsync(requestId);

            if (request != null)
            {
                request.IsAccepted = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/AdoptionRequests/Requests");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;

namespace PetAdopt.Pages.Posts
{

    public class DeleteModel : PageModel
    {
        private readonly PetAdopt.Data.PetAdoptContext _context;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(PetAdopt.Data.PetAdoptContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
      public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FirstOrDefaultAsync(m => m.id == id);

            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }

            if (Post.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, null, "EditOrDeletePolicy");

                if (!authorizationResult.Succeeded)
                {
                    return new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }
            var post = await _context.Post.FindAsync(id);

            if (post != null)
            {
                Post = post;
                _context.Post.Remove(Post);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

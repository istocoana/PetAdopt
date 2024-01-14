using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public DetailsModel(PetAdoptContext context)
        {
            _context = context;
        }

        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Post.Include(p => p.Animal).FirstOrDefaultAsync(m => m.id == id);

            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSendMessageAsync(int postId, string messageContent)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var post = await _context.Post.FindAsync(postId);
            var creatorUserId = post.UserId;


            if (post == null)
            {
                return NotFound();
            }

            var message = new PostMessage
            {
                SenderId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier),
                RecipientId = creatorUserId,
                MessageContent = messageContent,
                DateSent = DateTime.Now,
                PostId = postId
            };

            _context.PostMessages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = postId });
        }
    }
}

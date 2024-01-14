using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetAdopt.Pages.PostMessages
{
    public class SentMessagesModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public SentMessagesModel(PetAdoptContext context)
        {
            _context = context;
        }

        public List<PostMessage> SentMessages { get; set; }

        public async Task OnGetAsync()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            SentMessages = await _context.PostMessages
                .Include(message => message.Post)
                .Where(message => message.SenderId == currentUserId)
                .ToListAsync();
        }
    }
}

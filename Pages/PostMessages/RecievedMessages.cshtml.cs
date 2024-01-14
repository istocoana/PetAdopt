using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PetAdopt.Pages.PostMessages
{
    public class RecievedMessagesModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public RecievedMessagesModel(PetAdoptContext context)
        {
            _context = context;
        }

        public List<PostMessage> ReceivedMessages { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ReceivedMessages = await _context.PostMessages
                .Include(message => message.Post)
                .Where(message => message.RecipientId == userId)
                .ToListAsync();
        }
    }
}

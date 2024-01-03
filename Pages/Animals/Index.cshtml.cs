using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Animals
{
    public class IndexModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public IndexModel(PetAdoptContext context)
        {
            _context = context;
        }

        public IList<Animal> Animal { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null && _context.Animal != null)
            {
                Animal = await _context.Animal.Where(a => a.UserId == userId).ToListAsync();
            }
        }
    }
}

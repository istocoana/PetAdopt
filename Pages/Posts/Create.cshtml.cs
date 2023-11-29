using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetAdopt.Data;
using PetAdopt.Models;

namespace PetAdopt.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly PetAdopt.Data.PetAdoptContext _context;

        public CreateModel(PetAdopt.Data.PetAdoptContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["animalID"] = new SelectList(_context.Animal, "id", "id");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;
        public PostType SelectedTypes { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Post == null || Post == null)
            {
                return Page();
            }

            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly PetAdoptContext _context;

        [BindProperty]
        public Post Post { get; set; }


        public EditModel(PetAdoptContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Post.FirstOrDefaultAsync(m => m.id == id);

            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }


        public IFormFile Image { get; set; }
        public bool EditImage { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            var postToUpdate = await _context.Post.FindAsync(Post.id);

            if (postToUpdate == null)
            {
                return NotFound();
            }

            postToUpdate.title = Post.title;
            postToUpdate.description = Post.description;
            postToUpdate.Location = Post.Location;
            postToUpdate.Type = Post.Type;

            if (EditImage && Image != null && Image.Length > 0)
            {
                postToUpdate.ImageFile = await SaveImageAndGetURL(Image);
            }

            try
            {
                _context.Update(postToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Posts/Index");
        }


        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.id == id);
        }

        private async Task<string> SaveImageAndGetURL(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName;
        }
    }
}

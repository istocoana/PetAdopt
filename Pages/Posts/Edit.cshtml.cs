using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Posts
{
    [Authorize(Policy = "EditOrDeletePolicy")]
    public class EditModel : PageModel
    {
        private readonly PetAdoptContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [NotMapped]
        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        public EditModel(PetAdoptContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<IActionResult> OnPostAsync()
        {
            var postToUpdate = await _context.Post.FindAsync(Post.id);

            if (postToUpdate == null)
            {
                return NotFound();
            }

            postToUpdate.title = Post.title;
            postToUpdate.description = Post.description;
            postToUpdate.Type = Post.Type;
            postToUpdate.Location = Post.Location;

            if (Image != null && Image.Length > 0)
            {
                postToUpdate.ImageFile = await SaveImageAndGetURL(Image);
            }
            else
            {
                postToUpdate.ImageFile = postToUpdate.ImageFile;
            }

            try
            {
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

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
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


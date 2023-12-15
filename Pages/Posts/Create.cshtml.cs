using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetAdopt.Data;
using PetAdopt.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdopt.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly PetAdoptContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        public SelectList AnimalsSelectList { get; set; }
        public CreateModel(PetAdoptContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            Post = new Post(); // Inițializare a obiectului Post pentru a evita null reference
        }


        public IActionResult OnGet()
        {
            var animalsWithPosts = _context.Post.Select(p => p.animalID).ToList();
            var animalsWithoutPosts = _context.Animal.Where(a => !animalsWithPosts.Contains(a.id)).ToList();

            AnimalsSelectList = new SelectList(animalsWithoutPosts, "id", "animal_name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                var animalsWithPosts = _context.Post.Select(p => p.animalID).ToList();
                var animalsWithoutPosts = _context.Animal.Where(a => !animalsWithPosts.Contains(a.id)).ToList();

                AnimalsSelectList = new SelectList(animalsWithoutPosts, "id", "animal_name");
            }


            Post.ImageFile = await SaveImageAndGetURL(Image);

          
            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Posts/Index");
           
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

            return "/images/" + uniqueFileName; // Întoarce URL-ul imaginii salvate
        }
    }
}

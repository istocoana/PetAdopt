using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetAdopt.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly PetAdopt.Data.PetAdoptContext _context;

        public IndexModel(PetAdopt.Data.PetAdoptContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get; set; } = default!;
        public List<SelectListItem> BreedOptions { get; set; }
        public List<SelectListItem> LocationOptions { get; set; }
        public List<SelectListItem> TypeOptions { get; set; }
        public List<SelectListItem> SpeciesOptions { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedBreed { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedLocation { get; set; }

        [BindProperty(SupportsGet = true)]
        public PostType SelectedType { get; set; }

        [BindProperty(SupportsGet = true)]
        public AnimalSpecies SelectedSpecies { get; set; }

        public async Task OnGetAsync(string? searchString)
        {
            IQueryable<Post> postQuery =
                _context.Post
                    .Include(p => p.Animal);

            BreedOptions = await _context.Animal.Select(a => new SelectListItem
            {
                Value = a.animal_breed,
                Text = a.animal_breed
            }).Distinct().ToListAsync();

            LocationOptions = await _context.Post.Select(p => new SelectListItem
            {
                Value = p.Location,
                Text = p.Location
            }).Distinct().ToListAsync();

            TypeOptions = Enum.GetValues(typeof(PostType))
                .Cast<PostType>()
                .Select(t => new SelectListItem
                {
                    Value = t.ToString(),
                    Text = t.ToString()
                }).ToList();

            SpeciesOptions = Enum.GetValues(typeof(AnimalSpecies))
                .Cast<AnimalSpecies>()
                .Select(s => new SelectListItem
                {
                    Value = s.ToString(),
                    Text = s.ToString()
                }).ToList();

            if (!string.IsNullOrEmpty(SelectedBreed))
            {
                postQuery = postQuery.Where(p => p.Animal.animal_breed == SelectedBreed);
            }
            if (!string.IsNullOrEmpty(SelectedLocation))
            {
                postQuery = postQuery.Where(p => p.Location == SelectedLocation);
            }
            if (SelectedType != 0)
            {
                postQuery = postQuery.Where(p => p.Type == SelectedType);
            }
            if (SelectedSpecies != 0)
            {
                postQuery = postQuery.Where(p => p.Animal.animal_speacies == SelectedSpecies);
            }

            if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 3)
            {
                string searchQuery = searchString.Substring(0, 3).ToLower();

                var posts = await postQuery.ToListAsync();

                Post = posts.Where(p =>
                    EF.Functions.Like(p.title.ToLower(), $"{searchQuery}%") ||
                    EF.Functions.Like(p.description.ToLower(), $"{searchQuery}%") ||
                    EF.Functions.Like(p.Location.ToLower(), $"{searchQuery}%") ||
                    EF.Functions.Like(p.Animal.animal_breed.ToLower(), $"{searchQuery}%")
                ).ToList();
            }
            else
            {
                Post = await postQuery.AsNoTracking().ToListAsync();
            }
        }
    }
}

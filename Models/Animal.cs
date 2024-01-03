using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PetAdopt.Models
{
    public class Animal
    {
        public int id { get; set; }

        [Required]
        public AnimalSpecies animal_speacies { get; set; }

        [Required]
        public string animal_breed { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid age")]
        public int animal_age { get; set; }

        [Required]

        public string animal_name { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }

    public enum AnimalSpecies
    {
        Dog,
        Cat
    }
}

using PetAdopt.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdopt.Models
{
    public class Post
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Câmpul titlu este obligatoriu.")]
        public string title{ get; set; }
        [Required(ErrorMessage = "Câmpul descriere este obligatoriu.")]
        [MinLength(10, ErrorMessage = "Descrierea trebuie să aibă cel puțin 10 caractere.")]
        public string description { get; set; }
        public PostType Type { get; set; }
        public int animalID { get; set; }
        public Animal Animal { get; set; }

        public string ImageFile{ get; set; }

       
    }


    public enum PostType
    {
        donation,
        lost,
        found
    }
}

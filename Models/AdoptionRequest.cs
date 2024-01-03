using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdopt.Models
{
    public class AdoptionRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        [Required]
        public int PostId { get; set; } 

        public Post Post { get; set; }

        [Required]
        public string Message { get; set; } 

        public bool IsAccepted { get; set; } 
    }
}

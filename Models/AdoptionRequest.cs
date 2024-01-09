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

        [Required(ErrorMessage = "Câmpul locație este obligatoriu.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Câmpul număr de telefon este obligatoriu.")]
        [Phone(ErrorMessage = "Introduceți un număr de telefon valid.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Câmpul email este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Introduceți o adresă de email validă.")]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        [Required]
        public int PostId { get; set; } 

        public Post? Post { get; set; }

        [Required]
        public string? Message { get; set; } 

        public bool IsAccepted { get; set; } 
    }
}

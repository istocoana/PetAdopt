using Microsoft.AspNetCore.Identity;

namespace PetAdopt.Models
{
    public class PostMessage
    {
        public int Id { get; set; }

        public string SenderId { get; set; } 
        public IdentityUser Sender { get; set; }

        public string RecipientId { get; set; }
        public IdentityUser Recipient { get; set; }

        public string MessageContent { get; set; } 
        public DateTime DateSent { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }


    }
}

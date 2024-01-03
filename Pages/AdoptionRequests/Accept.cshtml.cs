using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Models;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace PetAdopt.Pages.AdoptionRequests
{
    public class AcceptModel : PageModel
    {
        private readonly PetAdoptContext _context;

        public AcceptModel(PetAdoptContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int RequestId { get; set; }

        [BindProperty]
        public string AcceptanceMessage { get; set; }

        public Animal Animal { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var adoptionRequest = await _context.AdoptionRequest
                .Include(ar => ar.Post)
                .FirstOrDefaultAsync(ar => ar.Id == RequestId);

            if (adoptionRequest == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var adoptionRequest = await _context.AdoptionRequest.FindAsync(RequestId);

            if (adoptionRequest == null)
            {
                return NotFound();
            }

            adoptionRequest.IsAccepted = true;

            _context.AdoptionRequest.Update(adoptionRequest);
            await _context.SaveChangesAsync();

            string userEmail = adoptionRequest.User.Email;
            string petName = adoptionRequest.Post.Animal.animal_name;

            string subject = "Adoption Request Accepted";
            string message = $"Hello,\n\nYour adoption request for the pet {petName} has been accepted. Thank you!";

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("internify.ie@gmail.com", "utqnjtwqxprvyiuf");
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress("internify.ie@gmail.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = false
            };

            mailMessage.To.Add(userEmail);

            await smtpClient.SendMailAsync(mailMessage);
            return RedirectToPage("/Index"); 
        }
    }
}

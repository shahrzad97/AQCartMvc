using System.ComponentModel.DataAnnotations;

namespace AQCartMvc.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        // Mandatory requirement
        public bool RequestInvoice { get; set; }

        // Mandatory requirement
        [Required(ErrorMessage = "You must accept the privacy policy")]
        public bool AcceptPrivacy { get; set; }

        // Display only
        public decimal Total { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AQCartMvc.Models
{
    public class CheckoutInput
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        // Mandatory receipt request (boolean is fine)
        public bool NeedInvoice { get; set; }

        // Privacy MUST be accepted
        public bool AcceptPrivacy { get; set; }

        // Display only
        public decimal Total { get; set; }
    }
}

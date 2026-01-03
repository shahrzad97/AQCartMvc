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

        public bool NeedInvoice { get; set; }

        [Required(ErrorMessage = "You must accept the privacy policy")]
        public bool AcceptPrivacy { get; set; }

        public decimal Total { get; set; }
    }
}

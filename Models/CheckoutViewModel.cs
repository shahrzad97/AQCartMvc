using System.ComponentModel.DataAnnotations;

namespace AQCartMvc.Models
{
    public class CheckoutViewModel
    {
        // User info
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nation { get; set; }

        public bool Newsletter { get; set; }

        // Receipt request (MANDATORY requirement from spec)
        public bool NeedInvoice { get; set; }

        public string? VatNumber { get; set; }
        public string? FiscalCode { get; set; }

        // Privacy (MANDATORY)
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the privacy policy")]
        public bool AcceptPrivacy { get; set; }
    }
}

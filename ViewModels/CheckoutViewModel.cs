using System.ComponentModel.DataAnnotations;

namespace AQCartMvc.ViewModels
{
    public class CheckoutViewModel
    {
        // ===== User input =====
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? CouponCode { get; set; }

        public bool RequestInvoice { get; set; }

        [Required(ErrorMessage = "You must accept the privacy policy")]
        [Range(typeof(bool), "true", "true",
            ErrorMessage = "You must accept the privacy policy")]
        public bool AcceptPrivacy { get; set; }

        // ===== Calculated values (display only) =====
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalTotal { get; set; }
    }
}

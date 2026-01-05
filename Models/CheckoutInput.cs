using System.ComponentModel.DataAnnotations;

namespace AQCartMvc.Models
{
    public class CheckoutInput
    {
        // =========================
        // USER INPUT
        // =========================

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? CouponCode { get; set; }

        public bool NeedInvoice { get; set; }

        // 🔴 PRIVACY — REQUIRED
        [Range(typeof(bool), "true", "true",
            ErrorMessage = "You must accept the privacy policy to continue")]
        public bool AcceptPrivacy { get; set; }

        // =========================
        // CALCULATED VALUES
        // =========================

        public decimal Total { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalTotal { get; set; }
    }
}

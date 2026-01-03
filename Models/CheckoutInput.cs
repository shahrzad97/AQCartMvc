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

        public bool AcceptPrivacy { get; set; }

        // 🔹 PART C
        public string? CouponCode { get; set; }

        // totals
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalTotal { get; set; }
    }
}

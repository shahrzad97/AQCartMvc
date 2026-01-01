using System;
using System.Collections.Generic;

namespace AQCartMvc.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // User info (required)
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Mandatory project requirements
        public bool NeedInvoice { get; set; }
        public bool AcceptPrivacy { get; set; }

        // Totals
        public decimal TotalBeforeDiscount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalFinal { get; set; }

        // Payment (sandbox / mock)
        public string PaymentType { get; set; } = "Mock";
        public string PaymentStatus { get; set; } = "Pending";

        public string? CouponCode { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}

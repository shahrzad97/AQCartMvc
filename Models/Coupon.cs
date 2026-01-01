namespace AQCartMvc.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public bool Active { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        // comma-separated product IDs (as in provided SQL)
        public string? AssociatedProductIds { get; set; }
    }
}

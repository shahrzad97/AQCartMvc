using AQCartMvc.Data;
using AQCartMvc.Helpers;
using AQCartMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AQCartMvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _db;
        private const string CART_KEY = "CART";

        public CheckoutController(AppDbContext db)
        {
            _db = db;
        }

        // =========================
        // STEP 1 — SHOW CHECKOUT
        // =========================
        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY);
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            var model = new CheckoutInput
            {
                Total = cart.Sum(i => i.UnitPrice * i.Quantity)
            };

            // 👇 IMPORTANT FIX
            return View("Index", model);
        }

        // =========================
        // STEP 2 — CONFIRM ORDER
        // =========================
        [HttpPost]
        public IActionResult Index(CheckoutInput model)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY);
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            if (!ModelState.IsValid)
            {
                model.Total = cart.Sum(i => i.UnitPrice * i.Quantity);

                // 👇 IMPORTANT FIX
                return View("Index", model);
            }

            var total = cart.Sum(i => i.UnitPrice * i.Quantity);

            var order = new Order
            {
                CreatedAt = DateTime.Now,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NeedInvoice = model.NeedInvoice,
                AcceptPrivacy = model.AcceptPrivacy,
                TotalBeforeDiscount = total,
                DiscountAmount = 0,
                TotalFinal = total,
                PaymentType = "Mock",
                PaymentStatus = "Confirmed"
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            foreach (var item in cart)
            {
                _db.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    RowTotal = item.UnitPrice * item.Quantity
                });
            }

            _db.SaveChanges();

            HttpContext.Session.Remove(CART_KEY);

            return RedirectToAction("Success");
        }

        // =========================
        // STEP 3 — SUCCESS PAGE
        // =========================
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}

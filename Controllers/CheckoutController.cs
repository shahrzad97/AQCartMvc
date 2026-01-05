using AQCartMvc.Helpers;
using AQCartMvc.Models;
using AQCartMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace AQCartMvc.Controllers
{
    public class CheckoutController : Controller
    {
        private const string CART_KEY = "CART";
        private const string INVOICE_KEY = "INVOICE_REQUESTED";

        // ===============================
        // GET: Checkout
        // ===============================
        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY);

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            var total = cart.Sum(i => i.UnitPrice * i.Quantity);

            var model = new CheckoutViewModel
            {
                Total = total,
                Discount = 0,
                FinalTotal = total
            };

            return View(model);
        }

        // ===============================
        // POST: Apply coupon OR Pay
        // ===============================
        [HttpPost]
        public IActionResult CreateStripeSession(CheckoutViewModel model)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY);

            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            decimal total = cart.Sum(i => i.UnitPrice * i.Quantity);
            decimal discount = 0;

            // =========================
            // COUPON LOGIC
            // =========================
            if (!string.IsNullOrWhiteSpace(model.CouponCode))
            {
                if (model.CouponCode.Trim().ToUpper() == "SAVE10")
                {
                    discount = total * 0.10m;
                }
                else
                {
                    ModelState.AddModelError("CouponCode", "Invalid coupon code");

                    model.Total = total;
                    model.Discount = 0;
                    model.FinalTotal = total;

                    return View("Index", model);
                }
            }

            model.Total = total;
            model.Discount = discount;
            model.FinalTotal = total - discount;

            // 👉 APPLY COUPON ONLY
            if (Request.Form["action"] == "applyCoupon")
            {
                return View("Index", model);
            }

            // =========================
            // FINAL VALIDATION
            // =========================
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // ✅ STORE INVOICE REQUEST
            HttpContext.Session.SetObject(INVOICE_KEY, model.RequestInvoice);

            // =========================
            // STRIPE
            // =========================
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "eur",
                            UnitAmount = (long)(model.FinalTotal * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Order total"
                            }
                        }
                    }
                },
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Checkout", null, Request.Scheme),
                CancelUrl = Url.Action("Index", "Checkout", null, Request.Scheme)
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        // ===============================
        // SUCCESS
        // ===============================
        public IActionResult Success()
        {
            bool invoiceRequested =
                HttpContext.Session.GetObject<bool?>(INVOICE_KEY) ?? false;

            ViewBag.InvoiceRequested = invoiceRequested;
            HttpContext.Session.Remove(INVOICE_KEY);

            return View();
        }
    }
}

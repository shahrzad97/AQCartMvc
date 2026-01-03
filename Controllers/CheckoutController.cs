using Microsoft.AspNetCore.Mvc;
using AQCartMvc.ViewModels;
using AQCartMvc.Models;
using AQCartMvc.Helpers;
using Stripe.Checkout;

public class CheckoutController : Controller
{
    private const string CART_KEY = "CART";

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

        return View(model);
    }

    // ✅ THIS is the ONLY submit action
    [HttpPost]
    public IActionResult CreateStripeSession(CheckoutInput model)
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY);

        if (cart == null || !cart.Any())
            return RedirectToAction("Index", "Cart");

        // 🔴 IMPORTANT: validation FIRST
        if (!ModelState.IsValid)
        {
            model.Total = cart.Sum(i => i.UnitPrice * i.Quantity);
            return View("Index", model); // back to checkout page
        }

        // ===============================
        // STRIPE SESSION (sandbox)
        // ===============================
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = cart.Select(item => new SessionLineItemOptions
            {
                Quantity = item.Quantity,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "eur",
                    UnitAmount = (long)(item.UnitPrice * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName
                    }
                }
            }).ToList(),
            Mode = "payment",
            SuccessUrl = Url.Action("Success", "Checkout", null, Request.Scheme),
            CancelUrl = Url.Action("Index", "Checkout", null, Request.Scheme)
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Redirect(session.Url);
    }

    public IActionResult Success()
    {
        return View();
    }
}

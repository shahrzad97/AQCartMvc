using AQCartMvc.Data;
using AQCartMvc.Helpers;
using AQCartMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AQCartMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;
        private const string CART_KEY = "CART";

        public CartController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            return View(cart);
        }

        // POST: /Cart/Add
        [HttpPost]
        public IActionResult Add(int productId)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return RedirectToAction("Index");

            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToAction("Index");
        }

        // POST: /Cart/Decrease
        [HttpPost]
        public IActionResult Decrease(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
            }

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToAction("Index");
        }

        // POST: /Cart/Remove
        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CART_KEY)
                       ?? new List<CartItem>();

            cart.RemoveAll(c => c.ProductId == productId);

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToAction("Index");
        }
    }
}

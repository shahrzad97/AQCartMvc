using AQCartMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AQCartMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Products
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products
                .Where(p => p.Active)
                .ToListAsync();

            return View(products);
        }
    }
}

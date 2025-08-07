using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // a. Display all products
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }

        // b. View product details
        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // c. Create product (GET)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // c. Create product (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }

            if (product.ImageFile != null)
            {
                string folder = "images/products/";
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                string serverFolder = Path.Combine(_env.WebRootPath, folder, imageName);

                using (var stream = new FileStream(serverFolder, FileMode.Create))
                {
                    product.ImageFile.CopyTo(stream);
                }

                product.ImagePath = "/" + folder + imageName;
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // d. Edit product (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // d. Edit product (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }

            if (product.ImageFile != null)
            {
                string folder = "images/products/";
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                string serverFolder = Path.Combine(_env.WebRootPath, folder, imageName);

                using (var stream = new FileStream(serverFolder, FileMode.Create))
                {
                    product.ImageFile.CopyTo(stream);
                }

                product.ImagePath = "/" + folder + imageName;
            }

            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // e. Delete (GET)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // e. Delete (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

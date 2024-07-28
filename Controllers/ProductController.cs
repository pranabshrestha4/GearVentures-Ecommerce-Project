using GearVentures.Models;
using Microsoft.AspNetCore.Mvc;

namespace GearVentures.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Record()
        {
            var initialModel = new ProductRecordViewModel
            {
                Name = "Default Name",
                Quantity = 1,
                Status = "In Stock",
                Price = "Rs 1",
                PhotoUrl = "/default-photo.jpg",
            };
            return View(initialModel);
        }

        [HttpPost]
        public IActionResult Record(ProductRecordViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.PhotoFile != null && product.PhotoFile.Length > 0)
                {
                    string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Photos");
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.PhotoFile.FileName);
                    string filePath = Path.Combine(uploadDirectory, fileName);
                    product.PhotoUrl = "/Photos/" + fileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        product.PhotoFile.CopyTo(stream);
                    }

                }

                if (product.PhotoUrl == null)
                {
                    ModelState.AddModelError("PhotoUrl", "Photo is required.");
                    return View(product);
                }

                var newproduct = new ProductRecordViewModel
                {
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Status = product.Status,
                    Price = product.Price,
                    PhotoUrl = product.PhotoUrl
                };

                _context.Products.Add(newproduct);
                _context.SaveChanges();
                return RedirectToAction("Confirmation");
            }

            return View(product);
        }


        public IActionResult Confirmation()
        {
            return View();
        }

    }
}

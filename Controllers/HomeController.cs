using GearVentures.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text;
using Org.BouncyCastle.Crypto.EC;
using Org.BouncyCastle.Asn1.X509;
using System.Net.Mail;
using System.Net;

namespace GearVentures.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Shop(int? page)
        {
            const int pageSize = 6;
            IQueryable<ProductRecordViewModel> products = _context.Products;
            if (TempData.ContainsKey("SearchQuery"))
            {
                var searchQuery = TempData["SearchQuery"].ToString();
                products = products.Where(p => p.Name.Contains(searchQuery));
            }
            var paginatedProducts = PaginatedList<ProductRecordViewModel>.Create(products, page ?? 1, pageSize);
            return View(paginatedProducts);
        }


        [HttpGet]
        public IActionResult Search(string searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                TempData["SearchQuery"] = searchQuery;
                return RedirectToAction("Shop");
            }
            var allProducts = _context.Products.ToList();
            return View("Shop", PaginatedList<ProductRecordViewModel>.Create(allProducts.AsQueryable(), 1, 6));
        }

        [HttpPost]
        public IActionResult AddToCart(string productName, string price, string photoUrl)
        {
            var cartItem = new CartItem
            {
                ProductName = productName,
                Price = price,
                PhotoUrl = photoUrl,
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Shop");
        }


        public IActionResult ViewCart()
        {
            var cartItems = _context.CartItems.ToList();
            var cartViewModel = new CartViewModel
            {
                CartItems = cartItems
            };
            return PartialView("_CartPartial", cartViewModel);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var cartItemToRemove = _context.CartItems.Find(cartItemId);

            if (cartItemToRemove != null)
            {
                _context.CartItems.Remove(cartItemToRemove);
                _context.SaveChanges();
            }

            return RedirectToAction("Shop");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var initialmodel = new OrderModel
            {
                OrderId = 1,
                Name = "Default Name",
                Email = "abc@gmail.com",
                Number = "12345678",
                Address = "Kathmandu Nepal",
                Notes = "Hello hi"
            };

            var cartItems = _context.CartItems.ToList();
            var cartViewModel = new OrderModel
            {
                CartItems = cartItems,
                CartItemIds = cartItems.Select(item => item.Id).ToList()
            };

            return View("Checkout", cartViewModel);
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel order)
        {
            var neworder = new OrderModel
            {
                OrderId = order.OrderId,
                Name = order.Name,
                Email = order.Email,
                Number = order.Number,
                Address = order.Address,
                Notes = order.Notes,
                CartItems = _context.CartItems.Where(item => order.CartItemIds.Contains(item.Id)).ToList()
            };
            _context.Details.Add(neworder);
            _context.SaveChanges();
            SendOrderConfirmationEmail(neworder);
            ClearCart();
            TempData["SuccessMessage"] = "Order placed successfully!";
            return View("Checkout", order);
        }


        private void ClearCart()
        {
            var cartItems = _context.CartItems.ToList();
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        private void SendOrderConfirmationEmail(OrderModel order)
        {
               // Set your SMTP server details
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "gearventuresnepal@gmail.com";
                string smtpPassword = "funo kfkr bfie vngi";

                // Set the sender and recipient email addresses
                string senderEmail = "gearventuresnepal@gmail.com";
                string recipientEmail = order.Email;

                // Build the email subject and body
                string subject = "Order Confirmation";
                string body = $"Dear {order.Name},\n\nThank you for placing your order with GearVentures. Your order ID is {order.OrderId}.\n\nShipping Details:\nName: {order.Name}\nEmail: {order.Email}\nNumber: {order.Number}\nAddress: {order.Address}\nNotes: {order.Notes}\n\nItems in your order:\n";

                // Null check for order.CartItems
                if (order.CartItems != null)
                {
                    foreach (var cartItem in order.CartItems)
                    {
                        body += $"{cartItem.ProductName} - Price: {cartItem.Price:C}\n";
                    }
                }

                body += "\n\nThank you for shopping with us!";

                // Create an instance of SmtpClient and MailMessage
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail))
                {
                    // Set SMTP credentials and enable SSL
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    // Set email subject and body
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    // Send the email
                    smtpClient.Send(mailMessage);
                }
            
        }


    }
}


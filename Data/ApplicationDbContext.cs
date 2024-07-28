using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GearVentures.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductRecordViewModel> Products { get; set; }
        public DbSet<OrderModel> Details { get; set; }

    }
}

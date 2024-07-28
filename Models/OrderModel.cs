using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearVentures.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public List<CartItem> CartItems { get; set; }
        [NotMapped]
        public List<int> CartItemIds { get; set; }
    }
}

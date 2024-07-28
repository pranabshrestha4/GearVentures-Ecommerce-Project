using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GearVentures.Models
{
    public class ProductRecordViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string PhotoUrl { get; set; }
        [NotMapped]
        public IFormFile PhotoFile { get; set; }
    }
}

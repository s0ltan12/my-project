using Project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation property (One-to-Many)
        public List<Product> Products { get; set; } = new List<Product>();
    }
}

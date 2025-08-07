using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Project.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [BindNever]
        public Category? Category { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

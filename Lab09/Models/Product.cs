using System.ComponentModel.DataAnnotations;

namespace Lab09.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        [Display(Name = "Product Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image path/URL is required.")]
        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        [Display(Name = "Image URL")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 100000000, ErrorMessage = "Price must be a non-negative number.")]
        [Display(Name = "Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, 1000000, ErrorMessage = "Quantity must be a non-negative number.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(150, ErrorMessage = "Status cannot exceed 150 characters.")]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Active";
    }
}

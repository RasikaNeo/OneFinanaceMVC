using System.ComponentModel.DataAnnotations;

namespace OneFinanaceMVC.Models
{
    public class EditProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]

        public string? ProductName { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be A positive value")]

        public int Quantity { get; set; }


        [Required(ErrorMessage = "Price is Required")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Price  positive value")]
        public double Price { get; set; }


        public int cat_Id { get; set; }
        [Required(ErrorMessage = "Select Category")]
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string? Description { get; set; }
    }
}

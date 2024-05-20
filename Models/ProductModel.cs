using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStore.Repository.Validation;

namespace BookStore.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter the Product Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter the Author Name")]
		public string Author { get; set; }

		[Required(ErrorMessage = "Please enter the Publisher Name")]
		public string Publicser { get; set; }

		[Display(Name = "Release Date")]
		[DataType(DataType.Date)]
		public DateTime ReleaseDate { get; set; }

		[Required, MinLength(4, ErrorMessage = "Hãy nhập mô tả Sản Phẩm")]
		public string Description { get; set; }
		public string Slug { get; set; }

		[Required(ErrorMessage = "Please enter Product Description")]
		[Range(0.01, double.MaxValue)]
		[Column(TypeName = "decimal(8, 2)")]
		public decimal Price { get; set; }

		[Required, Range(1, int.MaxValue, ErrorMessage = "Choose a Brand")]
		public int BrandID { get; set; }

		[Required, Range(1, int.MaxValue, ErrorMessage = "Choose a Category")]
		public int CategoryID { get; set; }
		public BrandModel Brand { get; set; }
		public CategoryModel Category { get; set; }
        public string Image { get; set; } = "noimage.jpg";

        [NotMapped]
        [FileExtension]

        [Required]
        public IFormFile ImageUpload { get; set; }
    }
}

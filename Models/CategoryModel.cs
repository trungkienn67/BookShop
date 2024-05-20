using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
	public class CategoryModel
	{
		[Key]
		public int Id { get; set; }

		[Required,MinLength(4,ErrorMessage = "Please enter the Category Name")]
		public string Name { get; set; }

		[Required, MinLength(4, ErrorMessage = "Please enter the Category Description")]
		public string Description { get; set; }
		public string Slug { get; set; }
		public int Status { get; set; }
	}
}

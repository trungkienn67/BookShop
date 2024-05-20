using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
	public class LoginViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Hãy nhập User Name")]
		public string UserName { get; set; }

		[DataType(DataType.Password), Required(ErrorMessage = "Hãy nhập Password")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}

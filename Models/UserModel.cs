using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
	public class UserModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Hãy nhập UserName của bạn")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Hãy nhập Email"), EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.Password), Required(ErrorMessage = "Hãy nhập Password")]
		public string Password { get; set; }
	}
}
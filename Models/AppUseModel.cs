using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
	public class AppUseModel : IdentityUser
	{
		public string Occupation { get; set; }
	}
}

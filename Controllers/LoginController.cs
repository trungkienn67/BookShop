using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

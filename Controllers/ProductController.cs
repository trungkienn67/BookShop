using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;

		public ProductController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
		{
			var products = _dataContext.Products.Include("Category").Include("Brand").ToList();
			return View(products);
		}


		public async Task<IActionResult> Details(int Id)
		{
			if (Id == null) return RedirectToAction("Index");

			var productsById = _dataContext.Products.Where(c => c.Id == Id).FirstOrDefault();

			return View(productsById);
		}
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _dataContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(query))
                .ToListAsync();

            return View(results);
        }


    }
}

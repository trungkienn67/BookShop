using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace JWL.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = context;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync());
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categoryes = new SelectList(_dataContext.Categories, "Id", "Name");
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductModel product)
		{
			ViewBag.Categoryes = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryID);
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandID);

			if (ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ", "-");
				var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
				if (slug != null)
				{
					ModelState.AddModelError(".", "The product already exists in the database");
					return View(product);
				}

				if (product.ImageUpload != null)
				{
					string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
					string imageName = Guid.NewGuid().ToString() + "-" + product.ImageUpload.FileName;
					string filePath = Path.Combine(uploadDir, imageName);

					using (FileStream fs = new FileStream(filePath, FileMode.Create))
					{
						await product.ImageUpload.CopyToAsync(fs);
					}
					product.Image = imageName;
				}
                // Clean up the Description field
                product.Description = product.Description.Replace("<p>", "").Replace("</p>", "<br>");

                _dataContext.Add(product);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Product added successfully";
				return RedirectToAction("Index");
			}
			else
			{
				TempData["error"] = "The model has some errors";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
				string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
			}
		}

		public async Task<IActionResult> Edit(int Id)
		{
			ProductModel product = await _dataContext.Products.FindAsync(Id);
			ViewBag.Categoryes = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryID);
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandID);
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, ProductModel product)
		{
			ViewBag.Categoryes = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryID);
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandID);

			if (ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ", "-");
				var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
				if (slug != null)
				{
					ModelState.AddModelError(".", "The product already exists in the database");
					return View(product);
				}

				if (product.ImageUpload != null)
				{
					string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
					string imageName = Guid.NewGuid().ToString() + "-" + product.ImageUpload.FileName;
					string filePath = Path.Combine(uploadDir, imageName);

					using (FileStream fs = new FileStream(filePath, FileMode.Create))
					{
						await product.ImageUpload.CopyToAsync(fs);
					}
					product.Image = imageName;
				}

                // Clean up the Description field
                product.Description = product.Description.Replace("<p>", "").Replace("</p>", "<br>");


                _dataContext.Update(product);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Product updated successfully";
				return RedirectToAction("Index");
			}
			else
			{
				TempData["error"] = "The model has some errors";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
				string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
			}
		}

		public async Task<IActionResult> Delete(int Id)
		{
			ProductModel product = await _dataContext.Products.FindAsync(Id);
			if (!string.Equals(product.Image, "noname.jpg"))
			{
				string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
				string oldFilePath = Path.Combine(uploadDir, product.Image);
				if (System.IO.File.Exists(oldFilePath))
				{
					System.IO.File.Delete(oldFilePath);
				}
			}

			_dataContext.Products.Remove(product);
			await _dataContext.SaveChangesAsync();
			TempData["error"] = "Product deleted successfully";
			return RedirectToAction("Index");
		}
	}
}

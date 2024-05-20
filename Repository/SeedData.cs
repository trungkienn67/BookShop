using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is Large Product in the world", Status =1 };
				CategoryModel pc = new CategoryModel { Name = "Pc", Slug = "pc", Description = "Pc is Large Product in the world", Status =1 };

				BrandModel apple = new BrandModel { Name = "Apple", Slug = "apple", Description = "Pc is Large Product in the world", Status =1 };
				BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "samsung", Description = "Samsung is Large Product in the world", Status =1 };


				_context.Products.AddRange(

				     new ProductModel { Name = "Vì sao thế nhỉ", Author = "Vì sao thế nhỉ", Publicser = "Vì sao thế nhỉ", ReleaseDate = DateTime.Parse("2022-2-12"), Description = "Hay lắm", Slug = "Vì sao thế nhỉ", Price = 1234, Category = macbook, Brand = apple, Image = "1.jpg" },
					  new ProductModel { Name = "Cây cam ngọt của tôi", Author = "José Mauro de Vasconcelos", Publicser = "Nguyễn Bích Lan - Tô Yến Ly dịch", ReleaseDate = DateTime.Parse("1968-3-9"), Description = "Sách Cây cam ngọt của tôi là tự truyện về thời thơ ấu của cậu bé Zézé, mang bài học về sự thấu cảm và lòng trắc ẩn", Slug = "Vì sao thế nhỉ", Price = 456, Category = pc, Brand = samsung, Image = "2.jpg" }
					);
				_context.SaveChanges();
			}
		}
	}
}

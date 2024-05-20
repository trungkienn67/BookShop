using BookStore.Models.ViewModels;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWL.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _datacontext;
        public CheckoutController(DataContext context)
        {
            _datacontext = context;
        }

        public IActionResult Checkout()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (cartItems.Any(item => item == null))
            {
                throw new InvalidOperationException("Giỏ hàng chứa các phần tử không hợp lệ.");
            }

            decimal grandTotal = cartItems.Sum(x => x.Quantity * x.Price);

            var model = new CheckoutViewModel
            {
                CartItems = cartItems,
                GrandTotal = grandTotal
            };

            return View("~/Views/Checkout/Checkout.cshtml", model);
        }


        public async Task<IActionResult> ProcessCheckout(CheckoutViewModel model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var ordercode = Guid.NewGuid().ToString();
                var orderItem = new OrderModel
                {
                    OrderCode = ordercode,
                    UserName = userEmail,
                    CreateDate = DateTime.Now,
                    Status = 1
                };
                _datacontext.Add(orderItem);
                _datacontext.SaveChanges();

                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                foreach (var cart in cartItems)
                {
                    var orderdetails = new OrderDetails
                    {
                        UserName = userEmail,
                        OrderCode = ordercode,
                        ProductId = cart.ProductId,
                        Price = cart.Price,
                        Quantity = cart.Quantity
                    };
                    _datacontext.Add(orderdetails);
                    _datacontext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Checkout thành công, vui lòng đợi đơn hàng của bạn được duyệt.";
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}

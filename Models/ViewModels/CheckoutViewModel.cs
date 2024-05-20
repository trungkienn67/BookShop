namespace BookStore.Models.ViewModels
{
	public class CheckoutViewModel
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string Phone { get; set; }
		public string PaymentMethod { get; set; }
        public List<CartItemModel> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}

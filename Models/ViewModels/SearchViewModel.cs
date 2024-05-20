namespace BookStore.Models.ViewModels
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public List<ProductModel> Results { get; set; }
    }
}

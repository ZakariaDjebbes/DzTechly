namespace Core.Specifications.SpecificationParams
{
	public class ProductSpecificationParams
	{
		private const int MAX_PAGE_SIZE = 50;

		public int PageIndex { get; set; } = 1;
		public int? CategoryId { get; set; }
		public int? TypeId { get; set; }
		public string Sort { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public bool InStock { get; set; }
		public string Search
		{
			get => _search; 
			set => _search = value.ToLower();
		}
		private string _search;

		public int PageSize
		{
			get { return _pageSie; }
			set { _pageSie = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value; }
		}
		private int _pageSie = 6;
	}
}
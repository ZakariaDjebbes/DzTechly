namespace Core.Specifications
{
    public class UsersForAdministrationParams
    {
        private const int MAX_PAGE_SIZE = 50;
		public int PageIndex { get; set; } = 1;
		public int PageSize
		{
			get { return _pageSie; }
			set { _pageSie = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value; }
		}
		private int _pageSie = 10;
    }
}
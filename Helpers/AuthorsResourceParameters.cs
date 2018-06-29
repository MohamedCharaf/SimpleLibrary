namespace SimpleLibrary.API.Helpers
{
    public class AuthorsResourceParameters
    {
        private const int _maxPageSize = 20;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;


        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
        }

    }
}

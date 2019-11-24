namespace ApiBoilerPlate.Data
{
    public class UrlQueryParameters
    {
        const int maxPageSize = 50;
        private int _pageSize = 20;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public bool IncludeCount { get; set; } = false;
    }
}

namespace SIVIGILA.Commons.DTOs.Search
{
    public class SearchBaseDTO
    {
        private int _page = 1;
        public int page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value > 0)
                {
                    _page = value;
                }
            }
        }
        private int _take = 20;
        public int take
        {
            get
            {
                return _take;
            }
            set
            {
                if (value > 0)
                {
                    _take = value;
                }
            }
        }
        public string? columna { get; set; }
        public bool ascending { get; set; } = true;
    }
}

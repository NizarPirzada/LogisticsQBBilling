namespace FTDTO
{
    public abstract class PagingParameterDTO
    {
        const int maxLimit = 100;
        public int Offset { get; set; } = 0;
        private int _limit = 10;
        public int Limit
        {
            get
            {
                return _limit;
            }
            set
            {
                _limit = (value > maxLimit) ? maxLimit : value;
            }
        }
    }
}

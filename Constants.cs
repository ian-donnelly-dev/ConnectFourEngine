namespace ConnectFourEngine
{
    public static class Constants
    {
        public const int COLUMNS = 7;
        public const int ROWS = 6;
        
        public const int MAX_VALUE = 19;
        public const int MAX_DEPTH = 10;
        
        public const ulong FIRST_COLUMN_MASK = (1UL << ROWS) - 1;
    }
}

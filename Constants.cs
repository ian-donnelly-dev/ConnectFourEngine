namespace ConnectFourEngine
{
    public static class Constants
    {
        public const int COLS = 7;
        public const int ROWS = 6;
        
        public const int BOARD_SIZE = ROWS * COLS;
        public const ulong FIRST_COLUMN_MASK = (1UL << ROWS) - 1;
        
        public const int MAX_SCORE = 19;
        public const int MINIMAX_DEPTH_LIMIT = 10;
    }
}

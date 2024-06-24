namespace ConnectFourEngine
{
    public static class Constants
    {
        public const int ROWS = 6;
        public const int COLS = 7;

        public const int TRANSPOSITION_TABLE_NUM_ENTRIES = 10_000_000;
        public const int TRANSPOSITION_DEPTH_OFFSET = 1;
        
        public const int BOARD_SIZE = ROWS * COLS;
        public const ulong FIRST_COLUMN_MASK = (1UL << ROWS) - 1;
        
        public const int BASELINE_SCORE = BOARD_SIZE / 2 + 1;
        public const int MAX_SCORE = BASELINE_SCORE - 4 + 1;
        public const int MIN_SCORE = -MAX_SCORE;
    }
}

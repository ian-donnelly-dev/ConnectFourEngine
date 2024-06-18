using System.Numerics;

namespace ConnectFourEngine
{
    public class Board
    {
        public ulong Player1Bitboard { get; private set; }
        public ulong Player2Bitboard{ get; private set; }
        
        public bool IsPlayer1Turn()
        {
            return BitOperations.PopCount(Player1Bitboard) == BitOperations.PopCount(Player2Bitboard);
        }
        
        public int GetMovesCount(bool isPlayer1)
        {
            return isPlayer1 ? BitOperations.PopCount(Player1Bitboard) : BitOperations.PopCount(Player2Bitboard);
        }
        
        public int GetColumnHeight(int column)
        {
            return BitOperations.PopCount((Player1Bitboard | Player2Bitboard) & GetColumnMask(column));
        }
        
        private static ulong GetColumnMask(int column)
        {
            return Constants.FIRST_COLUMN_MASK << (column * Constants.ROWS);
        }
    }
}

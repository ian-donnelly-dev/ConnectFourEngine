using System.Numerics;

namespace ConnectFourEngine
{
    public class Board
    {
        public ulong Player1Bitboard { get; private set; }
        public ulong Player2Bitboard{ get; private set; }
        
        public Board()
        {
            Player1Bitboard = 0UL;
            Player2Bitboard = 0UL;
        }
        
        public bool IsPlayer1Turn()
        {
            return BitOperations.PopCount(Player1Bitboard) == BitOperations.PopCount(Player2Bitboard);
        }
        
        public int GetMovesCount(bool isPlayer1)
        {
            return isPlayer1 ? BitOperations.PopCount(Player1Bitboard) : BitOperations.PopCount(Player2Bitboard);
        }
        
        public bool IsColumnPlayable(int column)
        {
            return GetColumnHeight(column) < Constants.ROWS;
        }
        
        public void MakeMove(int column)
        {
            ulong moveBit = 1UL << GetColumnHeight(column) + column * Constants.ROWS;

            if (IsPlayer1Turn())
            {
                Player1Bitboard |= moveBit;
            }
            else
            {
                Player2Bitboard |= moveBit;
            }
        }

        public void UnmakeMove(int column)
        {
            ulong moveBit = 1UL << GetColumnHeight(column) - 1 + column * Constants.ROWS;

            if (IsPlayer1Turn())
            {
                Player2Bitboard &= ~moveBit;
            }
            else
            {
                Player1Bitboard &= ~moveBit;
            }
        }
        
        private int GetColumnHeight(int column)
        {
            return BitOperations.PopCount((Player1Bitboard | Player2Bitboard) & GetColumnMask(column));
        }
        
        private static ulong GetColumnMask(int column)
        {
            return Constants.FIRST_COLUMN_MASK << (column * Constants.ROWS);
        }
    }
}

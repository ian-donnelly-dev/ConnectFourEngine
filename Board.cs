using System.Numerics;
using System.Text;

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
        
        public void LoadBoardStateString(string stateString)
        {
            Player1Bitboard = 0UL;
            Player2Bitboard = 0UL;
            int bitIndex = 0;

            foreach (char cellState in stateString)
            {
                switch (cellState)
                {
                    case '0':
                        bitIndex++;
                        break;
                    case '1':
                        Player1Bitboard |= 1UL << bitIndex;
                        bitIndex++;
                        break;
                    case '2':
                        Player2Bitboard |= 1UL << bitIndex;
                        bitIndex++;
                        break;
                }
            }
        }
        
        public string ExportBoardStateString()
        {
            StringBuilder sb = new StringBuilder();

            for (int col = 0; col < Constants.COLS; col++)
            {
                for (int row = 0; row < Constants.ROWS; row++)
                {
                    ulong mask = 1UL << (col * Constants.ROWS + row);
                    
                    if ((Player1Bitboard & mask) != 0)
                    {
                        sb.Append('1');
                    }
                    else if ((Player2Bitboard & mask) != 0)
                    {
                        sb.Append('2');
                    }
                    else
                    {
                        sb.Append('0');
                    }
                }
                if (col < Constants.COLS - 1)
                {
                    sb.Append('_');
                }
            }

            return sb.ToString();
        }
        
        public bool IsPlayer1Turn()
        {
            return BitOperations.PopCount(Player1Bitboard) == BitOperations.PopCount(Player2Bitboard);
        }
        
        public bool IsColumnPlayable(int column)
        {
            return GetColumnHeight(column) < Constants.ROWS;
        }
        
        public void MakeMove(int column)
        {
            ulong moveBit = 1UL << column * Constants.ROWS + GetColumnHeight(column);

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
            ulong moveBit = 1UL << column * Constants.ROWS + GetColumnHeight(column) - 1;

            if (IsPlayer1Turn())
            {
                Player2Bitboard &= ~moveBit;
            }
            else
            {
                Player1Bitboard &= ~moveBit;
            }
        }
        
        public bool IsBoardFull()
        {
            return BitOperations.PopCount(Player1Bitboard | Player2Bitboard) == Constants.BOARD_SIZE;
        }
        
        public int GetMovesCount(bool isPlayer1)
        {
            return isPlayer1 ? BitOperations.PopCount(Player1Bitboard) : BitOperations.PopCount(Player2Bitboard);
        }
        
        private int GetColumnHeight(int column)
        {
            ulong columnMask = Constants.FIRST_COLUMN_MASK << (column * Constants.ROWS);
            return BitOperations.PopCount((Player1Bitboard | Player2Bitboard) & columnMask);
        }
    }
}

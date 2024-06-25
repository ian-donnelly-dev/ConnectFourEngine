using System.Text;
using System.Numerics;

namespace ConnectFourEngine
{
    public class Board
    {
        private ulong player1Bitboard = 0UL;
        private ulong player2Bitboard = 0UL;
        
        public void ImportBoardState(string stateString)
        {
            player1Bitboard = 0UL;
            player2Bitboard = 0UL;
            int bitIndex = 0;
            
            foreach (char cellState in stateString)
            {
                switch (cellState)
                {
                    case '0':
                        bitIndex++;
                        break;
                    case '1':
                        player1Bitboard |= 1UL << bitIndex;
                        bitIndex++;
                        break;
                    case '2':
                        player2Bitboard |= 1UL << bitIndex;
                        bitIndex++;
                        break;
                }
                
                if ((bitIndex + 1) % (Constants.ROWS + 1) == 0)
                {
                    bitIndex++;
                }
            }
        }
        
        public string ExportBoardState()
        {
            StringBuilder encodedBoard = new StringBuilder();
            int bitIndex = 0;
            
            for (int col = 0; col < Constants.COLS; col++)
            {
                for (int row = 0; row < Constants.ROWS; row++)
                {
                    ulong mask = 1UL << bitIndex;
                    
                    if ((player1Bitboard & mask) != 0)
                    {
                        encodedBoard.Append('1');
                    }
                    else if ((player2Bitboard & mask) != 0)
                    {
                        encodedBoard.Append('2');
                    }
                    else
                    {
                        encodedBoard.Append('0');
                    }
                    
                    bitIndex++;
                    
                    if ((bitIndex + 1) % (Constants.ROWS + 1) == 0)
                    {
                        bitIndex++;
                    }
                }
                
                if (col < Constants.COLS - 1)
                {
                    encodedBoard.Append('_');
                }
            }
            
            return encodedBoard.ToString();
        }
        
        public string StringifyBoard()
        {
            StringBuilder displayBoard = new StringBuilder();
            
            for (int row = Constants.ROWS - 1; row >= 0; row--)
            {
                for (int col = 0; col < Constants.COLS; col++)
                {
                    ulong mask = 1UL << col * (Constants.ROWS + 1) + row;
                    
                    if ((player1Bitboard & mask) != 0)
                    {
                        displayBoard.Append('x');
                    }
                    else if ((player2Bitboard & mask) != 0)
                    {
                        displayBoard.Append('o');
                    }
                    else
                    {
                        displayBoard.Append('.');
                    }
                    
                    if (col < Constants.COLS - 1)
                    {
                        displayBoard.Append(' ');
                    }
                }
                
                if (row > 0)
                {
                    displayBoard.AppendLine();
                }
            }
            
            return displayBoard.ToString();
        }
        
        public bool IsPlayer1Turn()
        {
            return BitOperations.PopCount(player1Bitboard) == BitOperations.PopCount(player2Bitboard);
        }
        
        public bool IsColumnPlayable(int column)
        {
            return GetColumnHeight(column) < Constants.ROWS;
        }
        
        public void MakeMove(int column)
        {
            ulong moveBit = 1UL << (column * (Constants.ROWS + 1) + GetColumnHeight(column));
            
            if (IsPlayer1Turn())
            {
                player1Bitboard |= moveBit;
            }
            else
            {
                player2Bitboard |= moveBit;
            }
        }
        
        public void UnmakeMove(int column)
        {
            ulong moveBit = 1UL << (column * (Constants.ROWS + 1) + GetColumnHeight(column) - 1);
            
            if (IsPlayer1Turn())
            {
                player2Bitboard &= ~moveBit;
            }
            else
            {
                player1Bitboard &= ~moveBit;
            }
        }
        
        public bool IsWinner(bool isPlayer1)
        {
            ulong bitboard = isPlayer1 ? player1Bitboard : player2Bitboard;
            
            ulong diag1 = bitboard & (bitboard >> (Constants.ROWS + 0));
            if ((diag1 & (diag1 >> 2 * (Constants.ROWS + 0))) != 0)
            {
                return true;
            }
            
            ulong diag2 = bitboard & (bitboard >> (Constants.ROWS + 2));
            if ((diag2 & (diag2 >> 2 * (Constants.ROWS + 2))) != 0)
            {
                return true;
            }
            
            ulong horizontal = bitboard & (bitboard >> (Constants.ROWS + 1));
            if ((horizontal & (horizontal >> 2 * (Constants.ROWS + 1))) != 0)
            {
                return true;
            }
            
            ulong vertical = bitboard & (bitboard >> 1);
            if ((vertical & (vertical >> 2)) != 0)
            {
                return true;
            }
            
            return false;
        }
        
        public bool IsBoardFull()
        {
            return BitOperations.PopCount(player1Bitboard | player2Bitboard) == Constants.BOARD_SIZE;
        }
        
        public int GetMovesCount(bool isPlayer1)
        {
            return isPlayer1 ? BitOperations.PopCount(player1Bitboard) : BitOperations.PopCount(player2Bitboard);
        }
        
        public ulong GetBoardKey()
        {
            return player1Bitboard + (player1Bitboard | player2Bitboard);
        }
        
        private int GetColumnHeight(int column)
        {
            ulong columnMask = Constants.FIRST_COLUMN_MASK << (column * (Constants.ROWS + 1));
            return BitOperations.PopCount((player1Bitboard | player2Bitboard) & columnMask);
        }
    }
}

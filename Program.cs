using System;
using System.Text;

namespace ConnectFourEngine
{
    static class Program
    {
        private static string BoardStateToString(Board board)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = Constants.ROWS - 1; row >= 0; row--)
            {
                for (int col = 0; col < Constants.COLS; col++)
                {
                    ulong mask = 1UL << (col * Constants.ROWS + row);
                    
                    if ((board.Player1Bitboard & mask) != 0)
                    {
                        sb.Append('x');
                    }
                    else if ((board.Player2Bitboard & mask) != 0)
                    {
                        sb.Append('o');
                    }
                    else
                    {
                        sb.Append('.');
                    }

                    if (col < Constants.COLS - 1)
                    {
                        sb.Append(' ');
                    }
                }
                if (row > 0)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
        
        private static string BitboardToString(ulong bitboard)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int row = Constants.ROWS - 1; row >= 0; row--)
            {
                for (int col = 0; col < Constants.COLS; col++)
                {
                    sb.Append((bitboard & (1UL << (col * Constants.ROWS + row))) != 0 ? '1' : '0');
                    
                    if (col < Constants.COLS - 1)
                    {
                        sb.Append(' ');
                    }
                }
                if (row > 0)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
        
        static void Main()
        {
            Board board = new Board();
            
            board.LoadBoardStateString("000000_000000_000000_121200_000000_000000_000000");
            
            Console.WriteLine("Board after loading state string:");
            Console.WriteLine(BoardStateToString(board));
        }
    }
}

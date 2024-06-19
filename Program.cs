using System;
using System.Text;

namespace ConnectFourEngine
{
    static class Program
    {
        private static string BoardToString(Board board)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = Constants.ROWS - 1; row >= 0; row--)
            {
                for (int col = 0; col < Constants.COLS; col++)
                {
                    ulong mask = 1UL << col * Constants.ROWS + row;
                    
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
                    sb.Append((bitboard & (1UL << col * Constants.ROWS + row)) != 0 ? '1' : '0');
                    
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
        
        private static string UlongToString(ulong bitboard, bool cropToBoard = true)
        {
            int length = cropToBoard ? Constants.BOARD_SIZE : 64;
            return Convert.ToString((long)bitboard, 2).PadLeft(length, '0');
        }
        
        static void Main()
        {
            Board board = new Board();

            board.LoadBitboards(0b01, 0b10);
            Console.WriteLine(BoardToString(board));
            Console.WriteLine();
            
            board.LoadDropSequence("33443");
            Console.WriteLine(BoardToString(board));
            Console.WriteLine();
            
            board.LoadStateString("121200000000000000000000000000000000121212");
            Console.WriteLine(BoardToString(board));
        }
    }
}

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
                for (int col = 0; col < Constants.COLUMNS; col++)
                {
                    int bitIndex = col * Constants.ROWS + row;
                    ulong mask = 1UL << bitIndex;
                    
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

                    if (col < Constants.COLUMNS - 1)
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
                for (int col = 0; col < Constants.COLUMNS; col++)
                {
                    int bitIndex = col * Constants.ROWS + row;
                    sb.Append((bitboard & (1UL << bitIndex)) != 0 ? '1' : '0');
                    
                    if (col < Constants.COLUMNS - 1)
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
            int length = cropToBoard ? Constants.ROWS * Constants.COLUMNS : 64;
            return Convert.ToString((long)bitboard, 2).PadLeft(length, '0');
        }
        
        static void Main()
        {
            Board board = new Board();
            
            board.MakeMove(3);
            board.MakeMove(3);
            board.MakeMove(4);
            
            Console.WriteLine(BoardToString(board));
            Console.WriteLine();
            
            Console.WriteLine(BitboardToString(board.Player1Bitboard));
            Console.WriteLine();
            
            Console.WriteLine(BitboardToString(board.Player2Bitboard));
        }
    }
}

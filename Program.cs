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
        
        private static string ExportBitboardsString(Board board)
        {
            string player1BitboardString = ConvertToBinaryString(board.Player1Bitboard);
            string player2BitboardString = ConvertToBinaryString(board.Player2Bitboard);

            return $"({player1BitboardString}, {player2BitboardString})";

            string ConvertToBinaryString(ulong bitboard)
            {
                StringBuilder sb = new StringBuilder("0b");
                for (int col = Constants.COLS - 1; col >= 0; col--)
                {
                    for (int row = Constants.ROWS - 1; row >= 0; row--)
                    {
                        int bitIndex = col * Constants.ROWS + row;
                        sb.Append((bitboard & (1UL << bitIndex)) != 0 ? '1' : '0');
                    }
                    if (col > 0)
                    {
                        sb.Append('_');
                    }
                }
                return sb.ToString();
            }
        }

        private static string ExportStateString(Board board)
        {
            StringBuilder stateString = new StringBuilder(Constants.BOARD_SIZE);
            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                ulong mask = 1UL << i;
                if ((board.Player1Bitboard & mask) != 0)
                {
                    stateString.Append('1');
                }
                else if ((board.Player2Bitboard & mask) != 0)
                {
                    stateString.Append('2');
                }
                else
                {
                    stateString.Append('0');
                }
            }
            return stateString.ToString();
        }
        
        static void Main()
        {
            Board board = new Board();

            board.LoadBitboards(0b01, 0b10);
            Console.WriteLine(ExportBitboardsString(board));
            Console.WriteLine(BoardToString(board));
            Console.WriteLine();
            
            board.LoadDropSequence("33443");
            Console.WriteLine(BoardToString(board));
            Console.WriteLine();
            
            board.LoadStateString("121200000000000000000000000000000000121212");
            Console.WriteLine(ExportStateString(board));
            Console.WriteLine(BoardToString(board));
        }
    }
}

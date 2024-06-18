using System;
using System.Text;

namespace ConnectFourEngine
{
    static class Program
    {
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
            const ulong bitboard = 0b000000_000000_000000_000000_000000_000000_000001;
            
            Console.WriteLine(BitboardToString(bitboard));
            Console.WriteLine();
            
            Console.WriteLine(UlongToString(bitboard));
            Console.WriteLine();
            
            Console.WriteLine(UlongToString(bitboard, false));
        }
    }
}

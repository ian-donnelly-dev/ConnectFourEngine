using System;
using System.Text;

namespace ConnectFourEngine
{
    static class Program
    {
        private static string BitboardToString(ulong bitboard, bool multiline)
        {
            StringBuilder sb = new StringBuilder();
        
            if (multiline)
            {
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
            }
            else
            {
                string bitString = Convert.ToString((long)bitboard, 2).PadLeft(Constants.ROWS * Constants.COLUMNS, '0');
                sb.Append(bitString);
            }
        
            return sb.ToString();
        }
        
        static void Main()
        {
            const ulong bitboard = 0b010000000000000000000000000000000000001110;
            
            Console.WriteLine("Multiline Output:");
            Console.WriteLine(BitboardToString(bitboard, true));
            Console.WriteLine();
            
            Console.WriteLine("Single-line Output:");
            Console.WriteLine(BitboardToString(bitboard, false));
        }
    }
}

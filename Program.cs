using System;

namespace ConnectFourEngine
{
    static class Program
    {
        static void Main()
        {
            Board board = new Board();
            
            board.ImportBoardState("000000_000000_000000_121200_000000_000000_000000");
            
            Console.WriteLine("Board after loading state string:");
            Console.WriteLine(board.StringifyBoard());
        }
    }
}

using System;

namespace ConnectFourEngine
{
    static class Program
    {
        static void Main()
        {
            Board board = new Board();
            
            board.ImportBoardState("121211_110000_000000_000000_000000_200000_220000");
            
            Console.WriteLine(board.StringifyBoard());
            Console.WriteLine(board.CheckWin(true) || board.CheckWin(false));
        }
    }
}

using System;

namespace ConnectFourEngine
{
    static class Program
    {
        static void Main()
        {
            Board board = new Board();
            
            board.ImportBoardState("000000_000000_100000_120000_000000_200000_000000");
            
            Console.WriteLine(board.StringifyBoard());
            Console.WriteLine();
            
            for (int col = 0; col < Constants.COLS; col++)
            {
                if (!board.IsColumnPlayable(col))
                {
                    continue;
                }

                board.MakeMove(col);
                int score = Solvers.Minimax(board, board.IsPlayer1Turn(), Constants.MINIMAX_DEPTH_LIMIT, -Constants.MAX_SCORE, +Constants.MAX_SCORE);
                board.UnmakeMove(col);

                Console.WriteLine($"Column: {col}, Score: {score}");
            }
        }
    }
}

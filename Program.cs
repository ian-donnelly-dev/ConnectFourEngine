using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFourEngine
{
    static class Program
    {
        static void Main()
        {
            Board board = new Board();
            string boardState = "000000_000000_000000_121200_000000_000000_000000";
            board.ImportBoardState(boardState);
            
            Console.WriteLine($"Loaded board state string {board.ExportBoardState()}:");
            Console.WriteLine(board.StringifyBoard());
            Console.WriteLine();
            
            Console.WriteLine(board.GetBoardKey());
            
            Solver solver = new Solver(board);
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            int[] scores = solver.RootMinimax();
            stopwatch.Stop();
            
            Console.WriteLine("Raw scores:");
            for (int col = 0; col < scores.Length; col++)
            {
                Console.WriteLine($"Column {col} score: {scores[col]}");
            }
            Console.WriteLine();
            
            bool isPlayer1Turn = board.IsPlayer1Turn();
            List<int> bestColumns = GetBestColumns(scores, isPlayer1Turn);
            
            Console.WriteLine($"Player {(isPlayer1Turn ? "1 (maximizer)" : "2 (minimizer)")} should play in column(s): [{string.Join(", ", bestColumns)}].");
            Console.WriteLine($"Minimax processing completed at depth {Constants.MINIMAX_DEPTH_LIMIT} in {stopwatch.ElapsedMilliseconds}ms.");
        }

        private static List<int> GetBestColumns(int[] scores, bool isPlayer1Turn)
        {
            List<int> bestColumns = new List<int>();
            int bestScore = isPlayer1Turn ? Constants.MIN_SCORE : Constants.MAX_SCORE;
            
            for (int col = 0; col < scores.Length; col++)
            {
                if ((isPlayer1Turn && scores[col] > bestScore) || (!isPlayer1Turn && scores[col] < bestScore))
                {
                    bestScore = scores[col];
                    bestColumns.Clear();
                    bestColumns.Add(col);
                }
                else if (scores[col] == bestScore)
                {
                    bestColumns.Add(col);
                }
            }
            
            return bestColumns;
        }
    }
}

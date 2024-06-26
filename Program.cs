using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConnectFourEngine
{
    static class Program
    {
        static void Main()
        {
            const string boardState = "000000_000000_000000_100000_000000_000000_000000";
            const int maxSearchDepth = 16;
            
            Board board = new Board();
            board.ImportBoardState(boardState);
            
            Console.WriteLine($"Loaded board state string {board.ExportBoardState()}:");
            Console.WriteLine(board.StringifyBoard());
            Console.WriteLine();
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            Solver solver = new Solver(board, maxSearchDepth);
            int[] scores = solver.RootMinimax();
            stopwatch.Stop();
            
            for (int col = 0; col < scores.Length; col++)
            {
                Console.WriteLine($"Column {col} score: {(board.IsColumnPlayable(col) ? scores[col].ToString() : "full")}.");
            }
            Console.WriteLine();
            
            Console.WriteLine($"Transposition Table Load Factor: {solver.GetTranspositionTableLoadFactor():P2}");
            Console.WriteLine();
            
            List<int> bestColumns = GetBestColumns(scores, board);
            Console.WriteLine($"Player {(board.IsPlayer1Turn() ? "1 (maximizer)" : "2 (minimizer)")} should play in column(s): [{string.Join(", ", bestColumns)}].");
            
            Random random = new Random();
            int randomBestMove = bestColumns[random.Next(bestColumns.Count)];
            Console.WriteLine($"Random best move: column {randomBestMove}.");
            Console.WriteLine();
            
            Console.WriteLine($"Minimax processing completed at depth {maxSearchDepth} in {stopwatch.ElapsedMilliseconds}ms.");
        }

        private static List<int> GetBestColumns(int[] scores, Board board)
        {
            bool isPlayer1Turn = board.IsPlayer1Turn();
            List<int> bestColumns = new List<int>();
            int bestScore = isPlayer1Turn ? Constants.MIN_SCORE : Constants.MAX_SCORE;
            
            for (int col = 0; col < Constants.COLS; col++)
            {
                if (!board.IsColumnPlayable(col))
                {
                    continue;
                }
                
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

using System;

namespace ConnectFourEngine
{
    public class Solver
    {
        private readonly Board board;
        private readonly TranspositionTable transpositionTable;
        private readonly int searchDepthLimit;
        
        public Solver(Board board, int searchDepthLimit)
        {
            this.board = board;
            this.searchDepthLimit = searchDepthLimit;
            
            transpositionTable = new TranspositionTable(Constants.TRANSPOSITION_TABLE_SIZE_MB);
        }
        
        public int[] RootMinimax()
        {
            int[] scores = new int[Constants.COLS];
            
            for (int col = 0; col < Constants.COLS; col++)
            {
                board.MakeMove(col);
                scores[col] = RecursiveMinimax(board.IsPlayer1Turn(), 0, Constants.MIN_SCORE, Constants.MAX_SCORE);
                board.UnmakeMove(col);
            }
            
            return scores;
        }
        
        private int RecursiveMinimax(bool maximizingPlayer, int depth, int alpha, int beta)
        {
            if (depth == searchDepthLimit)
            {
                return 0;
            }
            
            if (board.IsBoardFull())
            {
                return 0;
            }
            
            ulong key = board.GetBoardKey();
            if (transpositionTable.TryGetValue(key, out int storedScore))
            {
                return storedScore;
            }
            
            if (maximizingPlayer)
            {
                if (board.IsWinner(false))
                {
                    return board.GetMovesCount(false) - Constants.BASELINE_SCORE;
                }
            }
            else
            {
                if (board.IsWinner(true))
                {
                    return Constants.BASELINE_SCORE - board.GetMovesCount(true);
                }
            }
            
            int bestScore = maximizingPlayer ? Constants.MIN_SCORE : Constants.MAX_SCORE;
            
            for (int i = 0; i < Constants.COLS; i++)
            {
                int col = GetColumnOrder(i);
                
                if (!board.IsColumnPlayable(col))
                {
                    continue;
                }
                
                board.MakeMove(col);
                int score = RecursiveMinimax(!maximizingPlayer, depth + 1, alpha, beta);
                board.UnmakeMove(col);
                
                if (maximizingPlayer)
                {
                    bestScore = Math.Max(bestScore, score);
                    alpha = Math.Max(alpha, score);
                }
                else
                {
                    bestScore = Math.Min(bestScore, score);
                    beta = Math.Min(beta, score);
                }
                
                if (beta <= alpha)
                {
                    break;
                }
            }

            if (depth + Constants.TRANSPOSITION_DEPTH_OFFSET < searchDepthLimit)
            {
                transpositionTable.AddEntry(key, bestScore);
            }

            return bestScore;
        }
        
        private static int GetColumnOrder(int index)
        {
            switch (index)
            {
                case 0: return 3;
                case 1: return 2;
                case 2: return 4;
                case 3: return 1;
                case 4: return 5;
                case 5: return 0;
                default: return 6;
            }
        }
    }
}

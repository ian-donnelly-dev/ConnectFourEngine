using System;

namespace ConnectFourEngine
{
    public class Solver
    {
        private readonly Board board;
        
        public Solver(Board board)
        {
            this.board = board;
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
            if (depth == Constants.MINIMAX_DEPTH_LIMIT)
            {
                return 0;
            }
            
            if (board.IsBoardFull())
            {
                return 0;
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
                int col;
                switch (i)
                {
                    case 0: col = 3; break;
                    case 1: col = 2; break;
                    case 2: col = 4; break;
                    case 3: col = 1; break;
                    case 4: col = 5; break;
                    case 5: col = 0; break;
                    case 6: col = 6; break;
                    default: continue;
                }
                
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
            
            return bestScore;
        }
    }
}

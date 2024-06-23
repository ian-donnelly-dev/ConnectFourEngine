using System;

namespace ConnectFourEngine
{
    public static class Solvers
    {
        public static int Minimax(Board board, bool maximizingPlayer, int depth, int alpha, int beta)
        {
            bool player1Won = !maximizingPlayer && board.IsWinner(true);
            bool player2Won = maximizingPlayer && board.IsWinner(false);
            bool tie = board.IsBoardFull();
            
            if (tie || player1Won || player2Won || depth == 0)
            {
                if (player1Won)
                {
                    return 22 - board.GetMovesCount(true);
                }

                if (player2Won)
                {
                    return board.GetMovesCount(false) - 22;
                }
    
                return 0;
            }
            
            int bestScore = maximizingPlayer ? -Constants.MAX_SCORE : +Constants.MAX_SCORE;
            
            for (int col = 0; col < Constants.COLS; col++)
            {
                if (!board.IsColumnPlayable(col))
                {
                    continue;
                }
            
                board.MakeMove(col);
                int score = Minimax(board, !maximizingPlayer, depth - 1, alpha, beta);
                board.UnmakeMove(col);
            
                if (maximizingPlayer)
                {
                    bestScore = Math.Max(bestScore, score);
                    alpha = Math.Max(alpha, bestScore);
                }
                else
                {
                    bestScore = Math.Min(bestScore, score);
                    beta = Math.Min(beta, bestScore);
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

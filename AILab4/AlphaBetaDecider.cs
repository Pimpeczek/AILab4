using System.Collections.Generic;
using System.Threading;
using PiwotToolsLib.PMath;
//https://github.com/Pimpeczek/PiwotToolsLib

namespace AI
{
    public class AlphaBetaDecider: DeciderBase
    {

        override public List<Int2> Decide(IBoard board, int player, int depth)
        {
            nodesChecked = 0;
            List<List<Int2>> moves = board.DetectAllMoves(board.State, player);
            if(moves.Count == 1)
            {
                return moves[0];
            }

            int deciderFav = -1;
            AlpBet(board.State, player, depth, ref deciderFav, board);

            if (deciderFav < 0)
                return null;
            return moves[deciderFav];
        }

        protected virtual void AlpBet(int[,] state, int player, int depth, ref int deciderFav, IBoard board)
        {
            int tempScore;
            List<List<Int2>> moves = board.DetectAllMoves(state, player);
            if (moves == null || moves.Count == 0)
                return;
            Thread[] threads = new Thread[moves.Count];
            int[] scores = new int[moves.Count];
            int[,] copyState;
            int score = (player % 2 == 0 ? int.MaxValue : int.MinValue);
            int[] moveStates = new int[moves[0].Count];
            int alpha = int.MinValue;
            int beta = int.MaxValue;


            for (int m = 0; m < moves.Count; m++)
            {
                copyState = (int[,])state.Clone();
                board.MakeMove(copyState, moves[m]);
                tempScore = AlpBet(copyState, (player + 1) % 2, depth - 1, ref alpha, ref beta, board);
                
                if (player % 2 == 1)
                {
                    if (tempScore > score)
                    {
                        deciderFav = m;
                        score = tempScore;
                    }
                    else if (tempScore == score && rng.Next(moves.Count) == 0)
                    {
                        deciderFav = m;
                    }

                    if (alpha < score)
                    {
                        alpha = score;
                        if (beta <= alpha)
                            break;
                    }
                }
                else
                {
                    if (tempScore < score)
                    {
                        deciderFav = m;
                        score = tempScore;
                    }
                    else if (tempScore == score && rng.Next(moves.Count) == 0)
                    {
                        deciderFav = m;
                    }
                    if (beta > score)
                    {
                        beta = score;
                        if (beta <= alpha)
                            break;
                    }
                }
            }
        }

        protected virtual int AlpBet(int[,] state, int player, int depth, ref int alpha, ref int beta, IBoard board)
        {
            nodesChecked++;
            List<List<Int2>> moves = board.DetectAllMoves(state, player);
            if (depth == 0)
            {
                if (moves.Count == 0)
                    return player % 2 == 0 ? int.MaxValue : int.MinValue;
                return board.EvaluateBoard(state);
            }
            int tempScore;

            
            int score = (player % 2 == 0 ? int.MaxValue : int.MinValue);
            if (moves == null || moves.Count == 0)
                return score;
            int[] moveStates = new int[moves[0].Count];
            for (int m = 0; m < moves.Count; m++)
            {

                board.MakeMoveAndSaveStates(state, moves[m], moveStates);
                tempScore = AlpBet(state, (player + 1) % 2, depth - 1, ref alpha, ref beta, board);
                if (player % 2 == 1)
                {
                    if (tempScore > score)
                    {
                        score = tempScore;
                        if (alpha < score)
                        {
                            alpha = score;
                            if (beta <= alpha)
                                break;
                        }
                    }
                    

                }
                else
                {
                    if (tempScore < score)
                    {
                        score = tempScore;
                        if (beta > score)
                        {
                            beta = score;
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                board.UndoMove(state, moves[m], moveStates);
            }

            return score;
        }
    }
}

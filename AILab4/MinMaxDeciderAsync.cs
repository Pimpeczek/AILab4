using System.Collections.Generic;
using System.Threading;
using PiwotToolsLib.PMath;
//https://github.com/Pimpeczek/PiwotToolsLib

namespace AI
{
    public class MinMaxDeciderAsync: MinMaxDecider
    {
        protected override void MinMax(int[,] state, int player, int depth, ref int deciderFav, IBoard board)
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
            for (int m = 0; m < moves.Count; m++)
            {
                
                copyState = (int[,])state.Clone();
                board.MakeMove(copyState, moves[m]);
                threads[m] = new Thread(()=> { scores[m] = MinMax(copyState, (player + 1) % 2, depth - 1, board); });
                threads[m].Start();
                Thread.Sleep(20);
            }


            for (int m = 0; m < moves.Count; m++)
            {
                threads[m].Join();
                tempScore = scores[m];
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
                }
            }
        }
    }
}

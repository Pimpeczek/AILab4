using System.Collections.Generic;
using PiwotToolsLib.PMath;
//https://github.com/Pimpeczek/PiwotToolsLib

namespace AI
{
    public interface IBoard
    {
        int[,] State { get; }

        List<List<Int2>> DetectMoves(int[,] state, int x, int y);
        List<List<Int2>> DetectAllMoves(int[,] state, int player);

        void MakeMove(int[,] state, List<Int2> move);

        void MakeMoveAndSaveStates(int[,] state, List<Int2> move, int[] moveStates);

        void UndoMove(int[,] state, List<Int2> move, int[] moveStates);

        int EvaluateBoard(int[,] state);
        int GetWinner(int[,] state);
    }
}

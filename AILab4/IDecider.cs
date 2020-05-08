using System.Collections.Generic;
using PiwotToolsLib.PMath;
//https://github.com/Pimpeczek/PiwotToolsLib

namespace AI
{
    public interface IDecider
    {
        int NodesChecked { get; }
        List<Int2> Decide(IBoard board, int player, int depth);
    }
}

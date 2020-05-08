using System;
using System.Collections.Generic;
using PiwotToolsLib.PMath;
//https://github.com/Pimpeczek/PiwotToolsLib

namespace AI
{
    public abstract class DeciderBase : IDecider
    {
        protected int nodesChecked = 0;
        public int NodesChecked
        {
            get
            {
                return nodesChecked;
            }
        }

        protected static Random rng;
        static DeciderBase()
        {
            rng = new Random((int)DateTime.UtcNow.Ticks);
        }

        public virtual List<Int2> Decide(IBoard board, int player, int depth)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong
{
    public interface IWaitingDistanceFinder
    {
        int GetWaitingDistance(IList<Tile> tiles);
    }
}

using System.Collections.Generic;

namespace Fraser.Mahjong
{
    public interface IWaitingDistanceFinder
    {
        int GetWaitingDistance(IList<Tile> tiles);
    }
}

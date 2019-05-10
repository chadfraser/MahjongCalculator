using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong
{
    public interface ITileSorter
    {
        IList<Tile> SortTiles(IEnumerable<Tile> tiles);
    }
}

using System.Collections.Generic;

namespace Fraser.Mahjong
{
    public interface ITileSorter
    {
        IList<Tile> SortTiles(IEnumerable<Tile> tiles);
    }
}

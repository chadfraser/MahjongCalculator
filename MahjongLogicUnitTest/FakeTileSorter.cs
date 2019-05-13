using System.Collections.Generic;
using Fraser.Mahjong;

namespace MahjongLogicUnitTest
{
    class FakeTileSorter : ITileSorter
    {
        public IList<Tile> SortTiles(IEnumerable<Tile> tiles)
        {
            // The passed tiles must already be in the expected sorted order for this fake to work without side effects
            return (List<Tile>)tiles;
        }
    }
}

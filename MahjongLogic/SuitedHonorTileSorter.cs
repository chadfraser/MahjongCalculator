using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    public class SuitedHonorTileSorter : ITileSorter
    {
        public IList<Tile> SortTiles(IEnumerable<Tile> tiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>();
            var honorTiles = tiles.OfType<HonorTile>();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).Cast<Tile>();
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType).Cast<Tile>();

            return orderedSuitedTiles.Concat(orderedHonorTiles).ToList();
        }
    }
}

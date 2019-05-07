using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    public class SuitedHonorTileSorter : ITileSorter
    {
        public IEnumerable<Tile> SortTiles(IEnumerable<Tile> tiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>();
            var honorTiles = tiles.OfType<HonorTile>();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank);
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType);
            List<Tile> orderedCastedSuitedTiles = new List<Tile>(orderedSuitedTiles);
            List<Tile> orderedCastedHonorTiles = new List<Tile>(orderedHonorTiles);

            return orderedCastedSuitedTiles.Concat(orderedCastedHonorTiles).ToList();
        }
    }
}

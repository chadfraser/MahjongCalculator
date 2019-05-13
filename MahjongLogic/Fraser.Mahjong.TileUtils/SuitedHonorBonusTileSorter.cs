using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    public class SuitedHonorBonusTileSorter : ITileSorter
    {
        public IList<Tile> SortTiles(IEnumerable<Tile> tiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>();
            var honorTiles = tiles.OfType<HonorTile>();
            var bonusTiles = tiles.OfType<BonusTile>();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).Cast<Tile>();
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType).Cast<Tile>();
            var orderedBonusTiles = bonusTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).Cast<Tile>();

            return orderedSuitedTiles.Concat(orderedHonorTiles).Concat(orderedBonusTiles).ToList();
        }
    }
}

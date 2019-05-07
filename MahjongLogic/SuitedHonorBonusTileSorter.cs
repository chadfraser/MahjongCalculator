using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    public class SuitedHonorBonusTileSorter : ITileSorter
    {
        public IEnumerable<Tile> SortTiles(IEnumerable<Tile> tiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>();
            var honorTiles = tiles.OfType<HonorTile>();
            var bonusTiles = tiles.OfType<BonusTile>();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank);
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType);
            var orderedBonusTiles = bonusTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank);
            List<Tile> orderedCastedSuitedTiles = new List<Tile>(orderedSuitedTiles);
            List<Tile> orderedCastedHonorTiles = new List<Tile>(orderedHonorTiles);
            List<Tile> orderedCastedBonusTiles = new List<Tile>(orderedBonusTiles);

            return orderedCastedSuitedTiles.Concat(orderedCastedHonorTiles).Concat(orderedCastedBonusTiles).ToList();
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class AppliedHKOSEfficientDrawsFinder : HKOSEfficientDrawsFinder
    {
        public AppliedHKOSEfficientDrawsFinder(IEnumerable<Tile> allDistinctTiles) : base(allDistinctTiles)
        {
            AllAvailableTiles = TileInstance.AllMainTileInstancesFourOfEachTile;
        }

        public AppliedHKOSEfficientDrawsFinder() : base()
        {
            AllAvailableTiles = TileInstance.AllMainTileInstancesFourOfEachTile;
        }

        protected IEnumerable<Tile> AllAvailableTiles { get; set; }

        public override int GetEfficientDrawCountWithSeenTiles(IList<Tile> tilesInHand, IList<Tile> seenTiles)
        {
            var remainingTiles = AllAvailableTiles.Except(seenTiles);

            var efficientDrawCount = 0;
            var currentWaitingDistince = WaitingDistanceFinder.GetWaitingDistance(tilesInHand);

            IDictionary<Tile, int> countOfTilesUsedInHand = tilesInHand.GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());
            IDictionary<Tile, int> countOfRemainingTiles = remainingTiles.GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var tile in AllDistinctTiles)
            {
                if (!countOfRemainingTiles.ContainsKey(tile))
                {
                    continue;
                }
                var tilesWithNewTileAdded = new List<Tile>(tilesInHand)
                {
                    tile
                };
                var newWaitingDistance = WaitingDistanceFinder.GetWaitingDistance(tilesWithNewTileAdded);
                if (newWaitingDistance < currentWaitingDistince)
                {
                    efficientDrawCount += countOfRemainingTiles[tile];
                }
            }
            return efficientDrawCount;
        }
    }
}

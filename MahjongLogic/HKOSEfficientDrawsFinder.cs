using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class HKOSEfficientDrawsFinder : IEfficientDrawsFinder
    {
        public HKOSEfficientDrawsFinder(IEnumerable<Tile> allDistinctTiles)
        {
            WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            AllDistinctTiles = new List<Tile>(allDistinctTiles);
        }

        public HKOSEfficientDrawsFinder() : this(TileInstance.AllMainTileInstances)
        {
        }

        protected IWaitingDistanceFinder WaitingDistanceFinder { get; }
        protected IEnumerable<Tile> AllDistinctTiles { get; set; }

        public int GetEfficientDrawCount(IList<Tile> tiles)
        {
            var efficientDrawCount = 0;
            var currentWaitingDistince = WaitingDistanceFinder.GetWaitingDistance(tiles);

            IDictionary<Tile, int> countOfTilesUsedInHand = tiles.GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var tile in AllDistinctTiles)
            {
                if (countOfTilesUsedInHand.ContainsKey(tile) && countOfTilesUsedInHand[tile] == 4)
                {
                    continue;
                }
                var tilesWithNewTileAdded = new List<Tile>(tiles)
                {
                    tile
                };
                var newWaitingDistance = WaitingDistanceFinder.GetWaitingDistance(tilesWithNewTileAdded);
                if (newWaitingDistance < currentWaitingDistince)
                {
                    efficientDrawCount += countOfTilesUsedInHand.ContainsKey(tile) ? 4 - countOfTilesUsedInHand[tile] : 4;
                }
            }
            return efficientDrawCount;
        }

        public virtual int GetEfficientDrawCountWithSeenTiles(IList<Tile> tilesInHand, IList<Tile> seenTiles)
        {
            return GetEfficientDrawCount(tilesInHand);
        }
    }
}
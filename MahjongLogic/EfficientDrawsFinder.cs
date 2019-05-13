using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    class EfficientDrawsFinder : IEfficientDrawsFinder
    {
        public EfficientDrawsFinder()
        {
            WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            AllAvailableTiles = TileInstance.AllMainTileInstances.ToList();
        }

        private IWaitingDistanceFinder WaitingDistanceFinder { get; }
        private IEnumerable<Tile> AllAvailableTiles { get; set; }

        public int GetEfficientDrawCount(IList<Tile> tiles)
        {
            var efficientDrawCount = 0;
            var currentWaitingDistince = WaitingDistanceFinder.GetWaitingDistance(tiles);

            IDictionary<Tile, int> countOfTilesUsedInHand = tiles.GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var tile in AllAvailableTiles)
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
    }
}

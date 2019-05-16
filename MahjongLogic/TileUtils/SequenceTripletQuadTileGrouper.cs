using System.Collections.Generic;

namespace Fraser.Mahjong
{
    class SequenceTripletQuadTileGrouper : SequenceTripletTileGrouper, ITileGrouper
    {
        public readonly int maximumGroupSize = 4;

        public SequenceTripletQuadTileGrouper(ITileSorter tileSorter) : base(tileSorter)
        {
        }

        protected override int GetMaximumGroupSize()
        {
            return 4;
        }

        protected bool CanGroupAllTilesUsingQuadStartingAtIndexN(IList<Tile> remainingTiles, int n)
        {
            if (remainingTiles.Count == 0)
            {
                return true;
            }
            if (remainingTiles.Count < n + 4)
            {
                return false;
            }

            if (remainingTiles[n].Equals(remainingTiles[n + 1]) && remainingTiles[n + 1].Equals(remainingTiles[n + 2]) &&
                remainingTiles[n + 2].Equals(remainingTiles[n + 3]))
            {
                var tilesAfterRemovingTriplet = GetTilesWithConsecutiveNTilesRemoved(remainingTiles, n, 4);
                return CanGroupAllTilesAtOnce(tilesAfterRemovingTriplet);
            }
            return false;
        }
    }
}
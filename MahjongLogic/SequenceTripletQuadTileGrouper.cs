using System.Collections.Generic;

namespace Mahjong
{
    class SequenceTripletQuadTileGrouper : SequenceTripletTileGrouper, ITileGrouper
    {
        public new static readonly int maximumGroupSize = 4;

        public SequenceTripletQuadTileGrouper(ITileSorter tileSorter) : base(tileSorter)
        {
        }

        protected override bool CanGroupAllTilesAtOnce(IList<Tile> remainingTiles)
        {
            if (remainingTiles.Count == 0)
            {
                return true;
            }
            if (remainingTiles.Count < 3)
            {
                return false;
            }

            if (CanGroupAllTilesUsingSequenceStartingAtIndexN(remainingTiles, 0) ||
                CanGroupAllTilesUsingTripletStartingAtIndexN(remainingTiles, 0) ||
                CanGroupAllTilesUsingQuadStartingAtIndexN(remainingTiles, 0))
            {
                return true;
            }
            return false;
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
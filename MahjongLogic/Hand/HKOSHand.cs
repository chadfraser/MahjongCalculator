using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class HKOSHand : Hand
    {
        public HKOSHand() : base()
        {
            BonusSets = new List<TileGrouping>();
            HandScorer = new HKOSHandScorer(this);
            TileSorter = new SuitedHonorBonusTileSorter();
            TileGrouper = new SequenceTripletQuadTileGrouper(TileSorter);
        }

        public IList<TileGrouping> BonusSets { get; set; }
        public HKOSHandScorer HandScorer { get; set; }

        public override bool IsWinningHand()
        {
            var uncalledTilesWithoutBonusTiles = new List<Tile>();
            var bonusTileSets = new List<TileGrouping>(BonusSets);
            foreach (var tile in UncalledTiles)
            {
                if (tile.GetType() == typeof(BonusTile))
                {
                    bonusTileSets.Add(new TileGrouping(tile));
                }
                else
                {
                    uncalledTilesWithoutBonusTiles.Add(tile);
                }
            }

            if (GetAdjustedCountOfPassedTiles(uncalledTilesWithoutBonusTiles, CalledSets) != WinningHandBaseTileCount)
            {
                return false;
            }

            return IsThirteenOrphans(uncalledTilesWithoutBonusTiles, CalledSets) ||
                IsSevenPairs(uncalledTilesWithoutBonusTiles, CalledSets) ||
                TileGrouper.CanGroupTilesIntoLegalHand(uncalledTilesWithoutBonusTiles);
        }

        public int GetAdjustedCountOfPassedTiles(IList<Tile> uncalledTiles, IList<TileGrouping> calledSets)
        {
            return uncalledTiles.Count + (3 * calledSets.Count);
        }

        public IList<IList<TileGrouping>> FindAllWaysToParseWinningHand()
        {
            var allWaysToSplitTiles = TileGrouper.FindAllWaysToGroupTilesAfterRemovingAPair(UncalledTiles);
            if (IsThirteenOrphans(UncalledTiles, CalledSets))
            {
                allWaysToSplitTiles.Add(new List<TileGrouping> { new TileGrouping(UncalledTiles.ToArray()) });
            }
            if (IsSevenPairs(UncalledTiles, CalledSets))
            {
                allWaysToSplitTiles.Add(new List<TileGrouping> { new TileGrouping(UncalledTiles.ToArray()) });
            }

            var waysToSplitTilesThatUseAllTiles = allWaysToSplitTiles.Where(
                group => group.Sum(t => t.Count()) == UncalledTiles.Count()).ToList();
            return waysToSplitTilesThatUseAllTiles;
        }

        public IList<TileGrouping> FindMostValuableWayToParseWinningHand()
        {
            var maxScore = 0;
            IList<TileGrouping> bestWayToParseHand = null;

            var waysToParseWinningHand = FindAllWaysToParseWinningHand();
            foreach (var wayToParse in waysToParseWinningHand)
            {
                var tilesPlusCombinedSetsAndBonus = wayToParse.Concat(CalledSets).Concat(BonusSets).ToList();
                var newScore = HandScorer.ScoreHand(tilesPlusCombinedSetsAndBonus);
                if (newScore > maxScore)
                {
                    maxScore = newScore;
                    bestWayToParseHand = tilesPlusCombinedSetsAndBonus;
                }
            }
            return bestWayToParseHand;
        }

        public IList<TileGrouping> ParseHandAsSevenPairs(IList<Tile> tiles)
        {
            var groupsOfPairs = new List<TileGrouping>();
            tiles = TileSorter.SortTiles(tiles);
            for (int i = 0; i < tiles.Count - 2; i += 2)
            {
                if (tiles[i].Equals(tiles[i + 1]))
                {
                    groupsOfPairs.Add(new TileGrouping(tiles[i], tiles[i + 1]));
                }
            }
            return groupsOfPairs;
        }

        public int FindScoreOfMostValuableHand()
        {
            var mostValuableHand = FindMostValuableWayToParseWinningHand();
            if (mostValuableHand == null)
            {
                return 0;
            }
            return HandScorer.ScoreHand(mostValuableHand);
        }
    }
}

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
        public IList<TileGrouping> BestWayToParseHand { get; set; }

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
                allGroups => allGroups.Sum(t => t.Count()) == UncalledTiles.Count() ||
                allGroups.All(group => group.IsBonus())).ToList();
            return waysToSplitTilesThatUseAllTiles;
        }

        private void CheckAndUpdateBestWayToParseWinningHand(IList<IList<TileGrouping>> allWaysToParseWinningHand)
        {
            var maxScore = 0;
            foreach (var wayToParse in allWaysToParseWinningHand)
            {
                var tilesPlusCombinedSetsAndBonus = wayToParse.Concat(CalledSets).Concat(BonusSets).ToList();
                var newScore = HandScorer.ScoreHand(tilesPlusCombinedSetsAndBonus);
                if (newScore >= maxScore)
                {
                    maxScore = newScore;
                    BestWayToParseHand = tilesPlusCombinedSetsAndBonus;
                }
            }
        }

        public void FindMostValuableWayToParseWinningHand()
        {
            if (BestWayToParseHand != null)
            {
                return;
            }

            var allWaysToParseWinningHand = FindAllWaysToParseWinningHand();
            if (allWaysToParseWinningHand.Count == 0)
            {
                if (BonusSets.Count > 6)
                {
                    BestWayToParseHand = CalledSets.Concat(BonusSets).ToList();
                }
                return;
            }

            CheckAndUpdateBestWayToParseWinningHand(allWaysToParseWinningHand);
        }
        
        public void FindMostValuableWayToParseWinningHand(Tile winningDiscardedTile)
        {
            if (BestWayToParseHand != null)
            {
                return;
            }

            var allWaysToParseWinningHand = FindAllWaysToParseWinningHand();
            if (allWaysToParseWinningHand.Count == 0)
            {
                return;
            }

            IList<IList<TileGrouping>> allWaysToParseWinningHandWithOpenSet = new List<IList<TileGrouping>>();
            foreach (var wayToParse in allWaysToParseWinningHand)
            {
                foreach (var group in wayToParse.Where(g => !g.IsOpenGroup && g.Contains(winningDiscardedTile)))
                {
                    var temp = new List<TileGrouping>(wayToParse)
                    {
                        new TileGrouping(group.ToArray()) { IsOpenGroup = true }
                    };
                    temp.Remove(group);
                    allWaysToParseWinningHandWithOpenSet.Add(temp);
                }
            }

            CheckAndUpdateBestWayToParseWinningHand(allWaysToParseWinningHandWithOpenSet);
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
            FindMostValuableWayToParseWinningHand();
            if (BestWayToParseHand == null)
            {
                return 0;
            }
            return HandScorer.ScoreHand(BestWayToParseHand);
        }

        public int FindScoreOfMostValuableHand(Tile winningDiscardedTile)
        {
            FindMostValuableWayToParseWinningHand(winningDiscardedTile);
            if (BestWayToParseHand == null)
            {
                return 0;
            }
            return HandScorer.ScoreHand(BestWayToParseHand);
        }
    }
}

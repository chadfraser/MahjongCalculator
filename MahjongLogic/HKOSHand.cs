using System.Collections.Generic;
using System.Linq;

namespace Mahjong
{
    public class HKOSHand : Hand
    {
        public HKOSHand() : base()
        {
            BonusSets = new List<TileGrouping>();
            HandScorer = new HKOSHandScorer(this);
        }

        public List<TileGrouping> BonusSets { get; set; }
        private HKOSHandScorer HandScorer { get; set; }

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
                CanRemovePairAndSplitRemainingTilesIntoGroups(uncalledTilesWithoutBonusTiles);
        }

        public int GetAdjustedCountOfPassedTiles(List<Tile> uncalledTiles, List<TileGrouping> calledSets)
        {
            return uncalledTiles.Count + (3 * CalledSets.Count);
        }

        public override List<Tile> SortTiles(List<Tile> tiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>().ToList();
            var honorTiles = tiles.OfType<HonorTile>().ToList();
            var bonusTiles = tiles.OfType<BonusTile>().ToList();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).ToList();
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType).ToList();
            var orderedBonusTiles = bonusTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).ToList();
            List<Tile> orderedCastedSuitedTiles = new List<Tile>(orderedSuitedTiles.ToArray());
            List<Tile> orderedCastedHonorTiles = new List<Tile>(orderedHonorTiles.ToArray());
            List<Tile> orderedCastedBonusTiles = new List<Tile>(orderedBonusTiles.ToArray());

            tiles = orderedCastedSuitedTiles.Concat(orderedCastedHonorTiles).Concat(orderedCastedBonusTiles).ToList();
            return tiles;
        }

        public List<List<TileGrouping>> FindAllWaysToParseWinningHand()
        {
            var allWaysToSplitTiles = FindAllPossibleWaysToSplitTiles(UncalledTiles);
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

        public List<TileGrouping> FindMostValuableWayToParseWinningHand()
        {
            var maxScore = 0;
            List<TileGrouping> bestWayToParseHand = null;

            var waysToParseWinningHand = FindAllWaysToParseWinningHand();
            foreach (var wayToParse in waysToParseWinningHand)
            {
                var tilesPlusCombinedSetsAndBonus = wayToParse.Concat(CalledSets).Concat(BonusSets).ToList();
                var newScore = HandScorer.ScoreHand(wayToParse);
                if (newScore > maxScore)
                {
                    maxScore = newScore;
                    bestWayToParseHand = wayToParse;
                }
            }
            return bestWayToParseHand;
        }

        public List<TileGrouping> ParseHandAsSevenPairs(List<Tile> tiles)
        {
            var groupsOfPairs = new List<TileGrouping>();
            tiles = SortTiles(tiles);
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
            return HandScorer.ScoreHand(mostValuableHand);
        }
    }
}

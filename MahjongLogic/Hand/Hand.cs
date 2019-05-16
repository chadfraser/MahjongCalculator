using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class Hand
    {
        protected static readonly int WinningHandBaseTileCount = 14;

        public Hand()
        {
            UncalledTiles = new List<Tile>();
            CalledSets = new List<TileGrouping>();
            IsOpen = false;
            TileSorter = new SuitedHonorTileSorter();
            TileGrouper = new SequenceTripletQuadTileGrouper(TileSorter);
            RoundWind = HonorType.East;
            SeatWind = HonorType.East;
        }

        public IList<Tile> UncalledTiles { get; set; }
        public IList<TileGrouping> CalledSets { get; set; }
        public bool IsOpen { get; set; }
        public HonorType RoundWind { get; set; }
        public HonorType SeatWind { get; set; }
        protected ITileSorter TileSorter { get; set; }
        protected ITileGrouper TileGrouper { get; set; }

        public virtual bool IsWinningHand()
        {
            if (GetAdjustedCountOfHandTiles() != WinningHandBaseTileCount)
            {
                return false;
            }

            return IsThirteenOrphans(UncalledTiles, CalledSets) || IsSevenPairs(UncalledTiles, CalledSets) ||
                TileGrouper.CanGroupTilesIntoLegalHand(UncalledTiles);
        }

        public void SortHand()
        {
            UncalledTiles = TileSorter.SortTiles(UncalledTiles);
        }

        public bool ContainsQuad()
        {
            var allGroups = TileGrouper.FindAllGroupsInTiles(UncalledTiles);
            return allGroups.Any(group => group.IsQuad()) ||
                (CalledSets.Any(group => group.IsTriplet() && UncalledTiles.Contains(group.First())));
        }

        public IList<IList<TileGrouping>> FindAllWaysToGroupUncalledTiles()
        {
            var copyOfUncalledTiles = new List<Tile>(UncalledTiles);
            return TileGrouper.FindAllWaysToGroupTiles(copyOfUncalledTiles);
        }

        public IList<IList<TileGrouping>> FindAllWaysToGroupUncalledTilesIntoHand()
        {
            var copyOfUncalledTiles = new List<Tile>(UncalledTiles);
            return TileGrouper.FindAllWaysToFullyGroupTilesAfterRemovingAPair(copyOfUncalledTiles);
        }

        protected IList<IList<TileGrouping>> GetSortedListOfTileGroupings(IList<IList<TileGrouping>> listOfTileGroupings)
        {
            var sortedList = new List<IList<TileGrouping>>();
            // Order the groups with pairs first, then by the first tile in the group with sequences before triplets/quads
            // e.g., 6-6 comes before 2-3-4, which comes before 2-2-2
            foreach (var grouping in listOfTileGroupings)
            {
                sortedList.Add(grouping
                    .OrderBy(group => group.Count)
                    .ThenBy(group => group.ElementAt(0))
                    .ThenByDescending(group => group.ElementAt(1)).ToList());
            }
            return sortedList;
        }

        protected int GetAdjustedCountOfHandTiles()
        {
            return UncalledTiles.Count + (3 * CalledSets.Count);
        }

        protected bool IsThirteenOrphans(IList<Tile> uncalledTiles, IList<TileGrouping> calledSets)
        {
            if (uncalledTiles.Count != WinningHandBaseTileCount || calledSets.Any())
            {
                return false;
            }

            return uncalledTiles.ToHashSet().SetEquals(GetThirteenOrphansSet());
        }

        protected bool IsSevenPairs(IList<Tile> uncalledTiles, IList<TileGrouping> calledSets)
        {
            if (uncalledTiles.Count != WinningHandBaseTileCount || calledSets.Any() || 
                uncalledTiles.ToHashSet().Count != WinningHandBaseTileCount / 2)
            {
                return false;
            }

            uncalledTiles = TileSorter.SortTiles(uncalledTiles);
            for (int i = 0; i < uncalledTiles.Count - 2; i += 2)
            {
                if (!uncalledTiles[i].Equals(uncalledTiles[i + 1]))
                {
                    return false;
                }
            }
            return true;
        }

        protected static HashSet<Tile> GetThirteenOrphansSet()
        {
            return new HashSet<Tile>
            {
                TileInstance.OneOfDots,
                TileInstance.NineOfDots,
                TileInstance.OneOfBamboo,
                TileInstance.NineOfBamboo,
                TileInstance.OneOfCharacters,
                TileInstance.NineOfCharacters,
                TileInstance.EastWind,
                TileInstance.SouthWind,
                TileInstance.WestWind,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.GreenDragon,
                TileInstance.RedDragon
            };
        }

        protected static IList<IList<T>> GetAllCombinationsOfNestedLists<T>(IList<IList<IList<T>>> nestedList)
        {
            var flattenedCombinationsList = new List<IList<T>>(nestedList[0]);
            for (int i = 1; i < nestedList.Count; i++)
            {
                var sublist = nestedList[i];
                if (sublist.Count == 0)
                {
                    continue;
                }
                if (flattenedCombinationsList.Count == 0)
                {
                    flattenedCombinationsList = new List<IList<T>>(sublist);
                    continue;
                }

                var tempCombinationsList = flattenedCombinationsList.SelectMany(flatListValue => sublist.Select(
                    sublistValue => new List<IList<T>> {flatListValue, sublistValue})).ToList();

                flattenedCombinationsList.Clear();
                foreach (var nonFlattenedCombinations in tempCombinationsList)
                {
                    flattenedCombinationsList.Add(nonFlattenedCombinations.SelectMany(x => x).ToList());
                }
            }

            return flattenedCombinationsList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    public class Hand
    {
        static readonly int WinningHandBaseTileCount = 14;

        public Hand()
        {
            UncalledTiles = new List<Tile>();
            CalledSets = new List<TileGrouping>();
        }

        public List<Tile> UncalledTiles { get; set; }
        public List<TileGrouping> CalledSets { get; set; }

        public bool IsWinningHand()
        {
            if (GetAdjustedCountOfHandTiles() != WinningHandBaseTileCount)
            {
                return false;
            }

            return IsThirteenOrphans(UncalledTiles, CalledSets) || IsSevenPairs(UncalledTiles, CalledSets) ||
                CanRemovePairAndSplitRemainingTilesIntoGroups(UncalledTiles);
        }

        public void SortHand()
        {
            UncalledTiles = SortTiles(UncalledTiles);
        }

        public static List<Tile> SortTiles(List<Tile> tiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>().ToList();
            var honorTiles = tiles.OfType<HonorTile>().ToList();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).ToList();
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType).ToList();
            List<Tile> orderedCastedSuitedTiles = new List<Tile>(orderedSuitedTiles.ToArray());
            List<Tile> orderedCastedHonorTiles = new List<Tile>(orderedHonorTiles.ToArray());

            tiles = orderedCastedSuitedTiles.Concat(orderedCastedHonorTiles).ToList();
            return tiles;
        }

        public List<List<TileGrouping>> FindAllPossibleWaysToSplitTiles(List<Tile> tiles)
        {
            List<Tile> uncheckedTiles;
            List<List<TileGrouping>> currentWaysToSplitTiles;
            List<List<TileGrouping>> waysToSplitTilesWithoutPair;
            var allWaysToSplitTiles = new List<List<TileGrouping>>();

            tiles = SortTiles(tiles);

            for (int i = 0; i < tiles.Count - 1; i++)
            {
                if (tiles[i].Equals(tiles[i + 1]))
                {
                    var pair = new TileGrouping(tiles[i], tiles[i + 1]);

                    // If we've already seen all ways of splitting these tiles starting with an identical pair to this one,
                    // we won't find any new ways to split the tiles by starting with this pair
                    if (IsValueAlreadyContainedInNestedList(pair, allWaysToSplitTiles))
                    {
                        i++;
                        continue;
                    }

                    uncheckedTiles = GetListWithConsecutiveNTilesRemoved(tiles, i, 2);
                    currentWaysToSplitTiles = FindAllWaysToSplitTilesIntoGroups(uncheckedTiles);
                    foreach (var listOfSplits in currentWaysToSplitTiles)
                    {
                        listOfSplits.Add(pair);
                    }
                    allWaysToSplitTiles.AddRange(GetSortedListOfTileGroupings(currentWaysToSplitTiles));

                    // If the tiles at index i and i+1 make a pair, and we find all ways to split the remaining tiles of
                    // that hand, there's no point to see if we can remove a pair using tile i+1, since that will give us
                    // the same result.
                    // For this purpose, we increment i a second time here after finding a pair.
                    i++;
                }
            }
            waysToSplitTilesWithoutPair = FindAllWaysToSplitTilesIntoGroups(tiles);
            allWaysToSplitTiles.AddRange(GetSortedListOfTileGroupings(waysToSplitTilesWithoutPair));
            return allWaysToSplitTiles;
        }

        private List<List<TileGrouping>> FindAllWaysToSplitTilesIntoGroups(List<Tile> tiles)
        {
            List<List<TileGrouping>> waysToSplitTiles;

            OutputTileListsGroupedBySuit(tiles, out List<List<Tile>> tilesGroupedBySuit);
            waysToSplitTiles = FindAllWaysToSplitNestedTileList(tilesGroupedBySuit);

            return GetSortedListOfTileGroupings(waysToSplitTiles);
        }

        private bool CanRemovePairAndSplitRemainingTilesIntoGroups(List<Tile> tiles)
        {
            List<Tile> uncheckedTiles;
            tiles = SortTiles(tiles);
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                if (tiles[i].Equals(tiles[i + 1]))
                {
                    uncheckedTiles = GetListWithConsecutiveNTilesRemoved(tiles, i, 2);
                    OutputTileListsGroupedBySuit(uncheckedTiles, out List<List<Tile>> tilesGroupedBySuit);

                    if (tilesGroupedBySuit.All(tilesOfSuitX => CanSplitIntoTripletsAndSequences(tilesOfSuitX)))
                    {
                        return true;
                    }
                    // If the tiles at index i and i+1 make a pair, and removing that pair does not give us a hand that can
                    // be split into sequences and triplets, there's no point to see if we can remove a pair using tile
                    // i+1, since that will give us the same result.
                    // For this purpose, we increment i a second time here if we fail to fully split the hand after
                    // removing the pair.
                    i++;
                    continue;
                }
            }
            return false;
        }

        private List<List<TileGrouping>> FindAllWaysToSplitNestedTileList(List<List<Tile>> nestedTileLists)
        {
            List<List<TileGrouping>> currentWaysToSplitTiles;
            var waysToSplitNestedTilesList = new List<List<List<TileGrouping>>>();

            foreach (var currentTileList in nestedTileLists)
            {
                waysToSplitNestedTilesList.Add(FindAllGroupsToSplitFromTiles(currentTileList));
            }
            currentWaysToSplitTiles = GetAllCombinationsOfNestedLists(waysToSplitNestedTilesList);
            return currentWaysToSplitTiles;
        }

        private List<List<TileGrouping>> GetSortedListOfTileGroupings(List<List<TileGrouping>> listOfTileGroupings)
        {
            var sortedList = new List<List<TileGrouping>>();
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

        private List<Tile> GetListWithConsecutiveNTilesRemoved(List<Tile> tiles, int indexOfFirstTileToRemove, int n)
        {
            List<Tile> leftSubList = new List<Tile>();
            List<Tile> rightSubList = new List<Tile>();

            if (indexOfFirstTileToRemove != 0)
            {
                leftSubList = tiles.GetRange(0, indexOfFirstTileToRemove);
            }
            if (indexOfFirstTileToRemove != tiles.Count - n)
            {
                rightSubList = tiles.GetRange(indexOfFirstTileToRemove + n, tiles.Count - (indexOfFirstTileToRemove + n));
            }

            return leftSubList.Concat(rightSubList).ToList();
        }

        private void OutputTileListsGroupedBySuit(List<Tile> tiles, out List<List<Tile>> castedTilesGroupedBySuit)
        {
            castedTilesGroupedBySuit = new List<List<Tile>>();
            var remainingTiles = new List<Tile>(tiles);

            while (remainingTiles.Count > 0)
            {
                var suitOfFirstTile = remainingTiles[0].Suit;
                var tilesOfSpecificSuit = remainingTiles.Where(tile => tile.Suit == suitOfFirstTile).ToList();
                castedTilesGroupedBySuit.Add(tilesOfSpecificSuit);
                remainingTiles = remainingTiles.Where(tile => tile.Suit != suitOfFirstTile).ToList();
            }
        }

        // Find some way to make this work for groups of larger sizes, to potentially include quads
        private Dictionary<TileGrouping, List<int>> FindAllDistinctGroupsAndTheirLocationsInTiles(List<Tile> tiles)
        {
            var uniqueGroupsInTiles = new Dictionary<TileGrouping, List<int>>();

            for (int indexOfFirstTile = 0; indexOfFirstTile < tiles.Count - 2; indexOfFirstTile++)
            {
                var firstTile = tiles[indexOfFirstTile];

                for (int indexOfSecondTile = indexOfFirstTile + 1; indexOfSecondTile < tiles.Count - 1;
                    indexOfSecondTile++)
                {
                    var secondTile = tiles[indexOfSecondTile];

                    if (!firstTile.CanBelongToSameGroup(secondTile))
                    {
                        break;
                    }

                    for (int indexOfThirdTile = indexOfSecondTile + 1; indexOfThirdTile < tiles.Count;
                        indexOfThirdTile++)
                    {
                        var thirdTile = tiles[indexOfThirdTile];

                        if (!secondTile.CanBelongToSameGroup(thirdTile))
                        {
                            break;
                        }

                        var currentTiles = new TileGrouping() { firstTile, secondTile, thirdTile };

                        if (currentTiles.IsGroup())
                        {
                            uniqueGroupsInTiles[currentTiles] = new List<int> { indexOfFirstTile, indexOfSecondTile,
                                indexOfThirdTile };
                        }
                    }
                }
            }
            return uniqueGroupsInTiles;
        }

        private List<List<TileGrouping>> FindAllGroupsToSplitFromTiles(List<Tile> tiles)
        {
            return FindAllGroupsToSplitFromTiles(tiles, new List<List<TileGrouping>>());
        }

        private List<List<TileGrouping>> FindAllGroupsToSplitFromTiles(List<Tile> tiles,
            List<List<TileGrouping>> currentWaysToSplitTiles)
        {
            if (tiles.Count < 3)
            {
                return currentWaysToSplitTiles;
            }

            var listOfAllPossibleTileGroupings = new List<List<TileGrouping>>();
            var dictOfTileGroupsAndLocations = FindAllDistinctGroupsAndTheirLocationsInTiles(tiles);
            foreach (var groupData in dictOfTileGroupsAndLocations)
            {
                var remainingTiles = GetListOfTilesWithPassedIndexesRemoved(tiles, groupData.Value.ToArray());

                var newGroups = FindAllGroupsToSplitFromTiles(remainingTiles, currentWaysToSplitTiles);
                foreach (var tileGroups in newGroups)
                {
                    tileGroups.Add(groupData.Key);
                    AddCurrentGroupsToNestedListIfNotAlreadyIn(tileGroups, listOfAllPossibleTileGroupings);
                }

                if (newGroups.Count == 0)
                {
                    listOfAllPossibleTileGroupings.Add(new List<TileGrouping> { groupData.Key });
                }
            }
            return listOfAllPossibleTileGroupings;
        }

        private void AddCurrentGroupsToNestedListIfNotAlreadyIn(List<TileGrouping> currentTileGroups,
            List<List<TileGrouping>> nestedListOfTileGroups)
        {
            foreach (var possibleTileGrouping in nestedListOfTileGroups)
            {
                if (AreNestedEnumerablesSequenceEqualUnordered(currentTileGroups, possibleTileGrouping))
                {
                    return;
                }
            }

            nestedListOfTileGroups.Add(currentTileGroups);
        }

        private List<Tile> GetTilesWithFirstSequenceFromIndexNRemoved(List<Tile> tiles, int n)
        {
            List<SuitedTile> suitedTiles = tiles.OfType<SuitedTile>().ToList();

            for (int indexOfFirstTile = n; indexOfFirstTile < suitedTiles.Count - 2; indexOfFirstTile++)
            {
                for (int indexOfSecondTile = indexOfFirstTile + 1; indexOfSecondTile < suitedTiles.Count - 1;
                    indexOfSecondTile++)
                {
                    if (!suitedTiles[indexOfFirstTile].IsNextInSequence(suitedTiles[indexOfSecondTile]))
                    {
                        break;
                    }

                    for (int indexOfThirdTile = indexOfSecondTile + 1; indexOfThirdTile < suitedTiles.Count;
                        indexOfThirdTile++)
                    {
                        if (!suitedTiles[indexOfSecondTile].IsNextInSequence(suitedTiles[indexOfThirdTile]))
                        {
                            break;
                        }

                        if (SuitedTile.IsSequence(suitedTiles[indexOfFirstTile], suitedTiles[indexOfSecondTile],
                            suitedTiles[indexOfThirdTile]))
                        {
                            return GetListOfTilesWithPassedIndexesRemoved(suitedTiles, indexOfFirstTile,
                                indexOfSecondTile, indexOfThirdTile);
                        }
                    }
                }
            }
            return tiles;
        }

        private List<Tile> GetTilesWithFirstTripletFromIndexNRemoved(List<Tile> tiles, int n)
        {
            for (int i = n; i < tiles.Count - 2; i++)
            {
                if (tiles[i].Equals(tiles[i + 1]) && tiles[i + 1].Equals(tiles[i + 2]))
                {
                    return GetListWithConsecutiveNTilesRemoved(tiles, i, 3);
                }
            }
            return tiles;
        }

        private List<Tile> GetListOfTilesWithPassedIndexesRemoved<T>(List<T> tiles, params int[] indexesToRemove)
            where T : Tile
        {
            Array.Sort(indexesToRemove);
            Array.Reverse(indexesToRemove);
            var listWithIndexesRemoved = new List<Tile>(tiles.ToArray());
            foreach (var index in indexesToRemove)
            {
                listWithIndexesRemoved.RemoveAt(index);
            }
            return listWithIndexesRemoved;
        }

        private bool CanSplitIntoTripletsAndSequences(List<Tile> uncheckedTiles)
        {
            if (uncheckedTiles.Count == 0)
            {
                return true;
            }
            if (uncheckedTiles.Count < 3)
            {
                return false;
            }

            if (CanSplitByRemovingSequenceAtIndex0(uncheckedTiles) || CanSplitByRemovingTripletAtIndex0(uncheckedTiles))
            {
                return true;
            }

            return false;
        }

        private bool CanSplitByRemovingSequenceAtIndex0(List<Tile> uncheckedTiles)
        {
            if (uncheckedTiles.Count == 0)
            {
                return true;
            }
            if (uncheckedTiles.Count < 3 || uncheckedTiles[0].GetType() != typeof(SuitedTile))
            {
                return false;
            }

            List<SuitedTile> suitedUncheckedTiles = uncheckedTiles.OfType<SuitedTile>().ToList();

            for (int i = 1; i <= suitedUncheckedTiles.Count - 2; i++)
            {
                if (!suitedUncheckedTiles[0].IsWithinBoundsOfSameSequence(suitedUncheckedTiles[i]))
                {
                    break;
                }

                for (int j = i + 1; j <= uncheckedTiles.Count - 1; j++)
                {
                    if (!suitedUncheckedTiles[0].IsWithinBoundsOfSameSequence(suitedUncheckedTiles[j]))
                    {
                        break;
                    }
                    if (SuitedTile.IsSequence(suitedUncheckedTiles[0], suitedUncheckedTiles[i], suitedUncheckedTiles[j]))
                    {
                        var remainingTiles = GetListOfTilesWithPassedIndexesRemoved(suitedUncheckedTiles, j, i, 0);
                        return CanSplitIntoTripletsAndSequences(remainingTiles);
                    }
                }
            }
            return false;
        }

        private bool CanSplitByRemovingTripletAtIndex0(List<Tile> uncheckedTiles)
        {
            if (uncheckedTiles.Count == 0)
            {
                return true;
            }
            if (uncheckedTiles.Count < 3)
            {
                return false;
            }

            if (uncheckedTiles[0].Equals(uncheckedTiles[1]) && uncheckedTiles[1].Equals(uncheckedTiles[2]))
            {
                // See if you can simplify this line
                return uncheckedTiles.Count == 3 || 
                    CanSplitIntoTripletsAndSequences(uncheckedTiles.GetRange(3, uncheckedTiles.Count - 3));
            }
            return false;
        }

        private int GetAdjustedCountOfHandTiles()
        {
            return UncalledTiles.Count + (3 * CalledSets.Count);
        }

        private static bool IsThirteenOrphans(List<Tile> uncalledTiles, List<TileGrouping> calledSets)
        {
            if (uncalledTiles.Count != WinningHandBaseTileCount || calledSets.Any())
            {
                return false;
            }

            return uncalledTiles.ToHashSet().SetEquals(GetThirteenOrphansSet());
        }

        private static bool IsSevenPairs(List<Tile> uncalledTiles, List<TileGrouping> calledSets)
        {
            if (uncalledTiles.Count != WinningHandBaseTileCount || calledSets.Any() || 
                uncalledTiles.ToHashSet().Count != WinningHandBaseTileCount / 2)
            {
                return false;
            }

            uncalledTiles = SortTiles(uncalledTiles);
            for (int i = 0; i < uncalledTiles.Count - 2; i += 2)
            {
                if (!uncalledTiles[i].Equals(uncalledTiles[i + 1]))
                {
                    return false;
                }
            }
            return true;
        }

        private static HashSet<Tile> GetThirteenOrphansSet()
        {
            return new HashSet<Tile> {
                new SuitedTile(Suit.Dots, 1),
                new SuitedTile(Suit.Dots, 9),
                new SuitedTile(Suit.Bamboo, 1),
                new SuitedTile(Suit.Bamboo, 9),
                new SuitedTile(Suit.Characters, 1),
                new SuitedTile(Suit.Characters, 9),
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Wind, HonorType.South),
                new HonorTile(Suit.Wind, HonorType.West),
                new HonorTile(Suit.Wind, HonorType.North),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.Green),
                new HonorTile(Suit.Dragon, HonorType.Red)};
        }

        private static List<List<T>> GetAllCombinationsOfNestedLists<T>(List<List<List<T>>> nestedList)
        {
            var flattenedCombinationsList = new List<List<T>>(nestedList[0]);
            for (int i = 1; i < nestedList.Count; i++)
            {
                var sublist = nestedList[i];
                if (sublist.Count == 0)
                {
                    continue;
                }
                if (flattenedCombinationsList.Count == 0)
                {
                    flattenedCombinationsList = new List<List<T>>(sublist);
                    continue;
                }

                var tempCombinationsList = flattenedCombinationsList.SelectMany(flatListValue => sublist.Select(
                    sublistValue => new List<List<T>> {flatListValue, sublistValue})).ToList();

                flattenedCombinationsList.Clear();
                foreach (var nonFlattenedCombinations in tempCombinationsList)
                {
                    flattenedCombinationsList.Add(nonFlattenedCombinations.SelectMany(x => x).ToList());
                }
            }

            return flattenedCombinationsList;
        }

        private static bool IsValueAlreadyContainedInNestedList<T>(T value, List<List<T>> nestedList)
        {
            foreach (var sublist in nestedList)
            {
                if (sublist.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool AreNestedEnumerablesSequenceEqualUnordered<T>(
            IEnumerable<IEnumerable<T>> currentNestedEnumerable, IEnumerable<IEnumerable<T>> otherNestedEnumerable)
        {
            return currentNestedEnumerable.All(
                currentSubEnumerable => otherNestedEnumerable.Any(
                    otherSubEnumerable => otherSubEnumerable.SequenceEqual(currentSubEnumerable)));
        }
    }
}

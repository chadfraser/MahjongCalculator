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
                CanRemovePairAndSplitRemainingTilesIntoSequencesAndTriplets(UncalledTiles);
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

        private void FindAllWaysToSplitHandIntoSequencesAndTriplets()
        {
        }

        private bool CanRemovePairAndSplitRemainingTilesIntoSequencesAndTriplets(List<Tile> tiles)
        {
            List<Tile> uncheckedTiles;
            SortHand();
            for (int i = 0; i < tiles.Count - 2; i++)
            {
                if (tiles[i].Equals(tiles[i + 1]))
                {
                    uncheckedTiles = GetListWithConsecutiveNTilesRemoved(tiles, i, 2);
                    OutputDistinctListsOfTiles(uncheckedTiles, out List<List<Tile>> castedSuitedTilesGroupedBySuit, out List<Tile> castedHonorTiles);

                    if (CanSplitIntoTripletsAndSequences(castedHonorTiles) &&
                        castedSuitedTilesGroupedBySuit.All(tilesOfSuitX => CanSplitIntoTripletsAndSequences(tilesOfSuitX)))
                    {
                        return true;
                    }
                    // If the tiles at index i and i+1 make a pair, and removing that pair does not give us a hand that can be split into
                    // sequences and triplets, there's no point to see if we can remove a pair using tile i+1, since that will give us the
                    // same result.
                    // For this purpose, we increment i a second time here if we fail to fully split the hand after removing the pair.
                    i++;
                    continue;
                }
            }
            return false;
        }

        public List<List<TileGrouping>> FindAllPossibleWaysToSplitHandIntoOnePairWithSequencesAndTriplets(List<Tile> tiles)
        {
            List<Tile> uncheckedTiles;
            var allWaysToSplitTiles = new List<List<TileGrouping>>();

            tiles = SortTiles(tiles);

            for (int i = 0; i < tiles.Count - 2; i++)
            {
                if (tiles[i].Equals(tiles[i + 1]))
                {
                    var pair = new TileGrouping(tiles[i], tiles[i + 1]);

                    // If we've already seen all ways of splitting these tiles starting with an identical pair to this one, we won't find any
                    // new ways to split the tiles by starting with this pair
                    if (IsTileGroupingAlreadyContainedInSeenWaysToSplitGroups(pair, allWaysToSplitTiles))
                    {
                        i++;
                        continue;
                    }

                    uncheckedTiles = GetListWithConsecutiveNTilesRemoved(tiles, i, 2);
                    OutputDistinctListsOfTiles(uncheckedTiles, out List<List<Tile>> castedSuitedTilesGroupedBySuit, out List<Tile> castedHonorTiles);

                    var waysToSplitSuitedTilesBySuit = new List<List<List<TileGrouping>>>();
                    var waysToSplitHonorTiles = FindAllTripletsToSplitFromTiles(castedHonorTiles);
                    waysToSplitHonorTiles.Add(pair);
                    foreach (var suitedTileGroup in castedSuitedTilesGroupedBySuit) {
                        waysToSplitSuitedTilesBySuit.Add(FindAllGroupsToSplitFromTiles(suitedTileGroup));
                    }

                    allWaysToSplitTiles = GetAllCombinationsOfNestedLists(waysToSplitSuitedTilesBySuit);

                    foreach (var currentWayToSplitSuited in waysToSplitSuitedTilesBySuit[0])
                    {
                        currentWayToSplitSuited.AddRange(waysToSplitHonorTiles);
                        // Order the groups by the first tile in the group with sequences coming before triplets/quads
                        // e.g., 2-3-4 comes before 2-2-2
                        var a = currentWayToSplitSuited.OrderBy(group => group.Count).ThenBy(group => group.ElementAt(0)).ThenByDescending(group => group.ElementAt(1)).ToList();
                        allWaysToSplitTiles.Add(a);
                    }

                    // In this loop, we know that the tile at index i+1 is the same as the tile at index i.
                    // We increment i twice at the end of the loop (including the afterthought of the loop) to skip over that tile,
                    // since finding pairs with tile i+1 will give the same result as finding pairs with tile i.
                    i++;
                }
            }
            return allWaysToSplitTiles;
            //foreach (var a in allWaysToSplitTiles)
            //{
            //    foreach (var group in a)
            //    {
            //        Console.Write("[");
            //        foreach (var t in group)
            //        {
            //            Console.Write(t + ", ");
            //        }
            //        Console.Write("],   ");
            //    }
            //    Console.WriteLine();
            //    }
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

        private void OutputDistinctListsOfTiles(List<Tile> tiles, out List<List<Tile>> castedSuitedTilesGroupedBySuit,
            out List<Tile> castedHonorTiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>().ToList();
            var honorTiles = tiles.OfType<HonorTile>().ToList();
            castedSuitedTilesGroupedBySuit = new List<List<Tile>>();

            while (suitedTiles.Count > 0)
            {
                var suitOfFirstTile = suitedTiles[0].Suit;
                var tilesOfSpecificSuit = suitedTiles.Where(tile => tile.Suit == suitOfFirstTile).ToList();
                List<Tile> castedTilesOfSpecificSuit = new List<Tile>(tilesOfSpecificSuit.ToArray());
                castedSuitedTilesGroupedBySuit.Add(castedTilesOfSpecificSuit);
                suitedTiles = suitedTiles.Where(tile => tile.Suit != suitOfFirstTile).ToList();
            }

            castedHonorTiles = new List<Tile>(honorTiles.ToArray());
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

            if (CanSplitByRemovingSequence(uncheckedTiles) || CanSplitByRemovingTriplet(uncheckedTiles))
            {
                return true;
            }

            return false;
        }

        private Dictionary<TileGrouping, List<int>> FindAllDistinctGroupsAndLocationsInTiles(List<Tile> tiles)
        {
            var uniqueGroupsInTiles = new Dictionary<TileGrouping, List<int>>();
            List<SuitedTile> suitedTiles = tiles.OfType<SuitedTile>().ToList();

            for (int indexOfFirstTile = 0; indexOfFirstTile <= suitedTiles.Count - 3; indexOfFirstTile++)
            {
                for (int indexOfSecondTile = indexOfFirstTile + 1; indexOfSecondTile <= suitedTiles.Count - 2; indexOfSecondTile++)
                {
                    if (!suitedTiles[indexOfFirstTile].IsNextInSequence(suitedTiles[indexOfSecondTile]) &&
                        !suitedTiles[indexOfFirstTile].Equals(suitedTiles[indexOfSecondTile]))
                    {
                        break;
                    }

                    for (int indexOfThirdTile = indexOfSecondTile + 1; indexOfThirdTile <= suitedTiles.Count - 1; indexOfThirdTile++)
                    {
                        if (!suitedTiles[indexOfSecondTile].IsNextInSequence(suitedTiles[indexOfThirdTile]) &&
                            !suitedTiles[indexOfSecondTile].Equals(suitedTiles[indexOfThirdTile]))
                        {
                            break;
                        }

                        var currentTiles = new SuitedTile[] { suitedTiles[indexOfFirstTile], suitedTiles[indexOfSecondTile], suitedTiles[indexOfThirdTile] };

                        if (SuitedTile.IsSequence(currentTiles) || SuitedTile.IsTriplet(currentTiles))
                        {
                            var removedSequence = new TileGrouping(currentTiles);
                            uniqueGroupsInTiles[removedSequence] = new List<int> { indexOfFirstTile, indexOfSecondTile, indexOfThirdTile };
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

        private List<List<TileGrouping>> FindAllGroupsToSplitFromTiles(List<Tile> tiles, List<List<TileGrouping>> currentWaysToSplitTiles)
        {
            if (tiles.Count < 3)
            {
                return currentWaysToSplitTiles;
            }

            var listOfAllPossibleTileGroupings = new List<List<TileGrouping>>();
            var dictOfTileGroupsAndLocations = FindAllDistinctGroupsAndLocationsInTiles(tiles);
            foreach (var groupData in dictOfTileGroupsAndLocations)
            {
                var reversedLocationArray = Enumerable.Reverse(groupData.Value).ToArray();
                var remainingTiles = GetListOfTilesWithPassedIndexesRemoved(tiles, reversedLocationArray);

                var newGroups = FindAllGroupsToSplitFromTiles(remainingTiles, currentWaysToSplitTiles);
                foreach (var tileGroups in newGroups)
                {
                    tileGroups.Add(groupData.Key);
                    var isInListAlready = false;

                    foreach (var possibleTileGrouping in listOfAllPossibleTileGroupings)
                    {
                        if (AreNestedEnumerablesSequenceEqualUnordered(tileGroups, possibleTileGrouping))
                        {
                            isInListAlready = true;
                            break;
                        }
                    }

                    if (!isInListAlready)
                    {
                        listOfAllPossibleTileGroupings.Add(tileGroups);
                    }
                }

                if (newGroups.Count == 0)
                {
                    listOfAllPossibleTileGroupings.Add(new List<TileGrouping> { groupData.Key });
                }
            }
            return listOfAllPossibleTileGroupings;
        }

        private List<TileGrouping> FindAllSequencesAndTripletsToSplitFromTiles(List<Tile> tiles, List<TileGrouping> currentWayToSplitTiles)
        {
            var groupsSplitFromTiles = new List<TileGrouping>();

            groupsSplitFromTiles.AddRange(FindAllSequencesToSplitFromTiles(tiles));
            groupsSplitFromTiles.AddRange(FindAllTripletsToSplitFromTiles(tiles));

            return groupsSplitFromTiles;
        }

        private List<TileGrouping> FindAllSequencesToSplitFromTiles(List<Tile> tiles)
        {
            var sequencesSplitFromTiles = new List<TileGrouping>();
            int previousCountOfTiles;

            do
            {
                previousCountOfTiles = tiles.Count;
                tiles = GetTilesWithFirstSequenceFromIndexNRemoved(tiles, 0, sequencesSplitFromTiles);
            }
            while (tiles.Count < previousCountOfTiles);

            return sequencesSplitFromTiles;
        }

        private List<TileGrouping> FindAllTripletsToSplitFromTiles(List<Tile> tiles)
        {
            var tripletsSplitFromTiles = new List<TileGrouping>();
            int previousCountOfTiles;

            do
            {
                previousCountOfTiles = tiles.Count;
                tiles = GetTilesWithFirstTripletFromIndexNRemoved(tiles, 0, tripletsSplitFromTiles);
            }
            while (tiles.Count < previousCountOfTiles);

            return tripletsSplitFromTiles;
        }

        private List<Tile> GetTilesWithFirstSequenceFromIndexNRemoved(List<Tile> tiles, int n, List<TileGrouping> splitTilesGroups)
        {
            List<SuitedTile> suitedTiles = tiles.OfType<SuitedTile>().ToList();

            for (int indexOfFirstTile = n; indexOfFirstTile <= suitedTiles.Count - 3; indexOfFirstTile++)
            {
                for (int indexOfSecondTile = indexOfFirstTile + 1; indexOfSecondTile <= suitedTiles.Count - 2; indexOfSecondTile++)
                {
                    // As the tiles are sorted, if the second tile we're checking is one rank higher than the first, it's impossible to make a sequence
                    // using any of the later tiles combined with the first (unless the first and second are equal in rank, in which case nothing is lost by
                    // marking the second tile as the first to begin searching from)
                    if (!suitedTiles[indexOfFirstTile].IsNextInSequence(suitedTiles[indexOfSecondTile]))
                    {
                        indexOfFirstTile = indexOfSecondTile - 1;
                        break;
                    }

                    for (int indexOfThirdTile = indexOfSecondTile + 1; indexOfThirdTile <= suitedTiles.Count - 1; indexOfThirdTile++)
                    {
                        // As the tiles are sorted, if the third tile we're checking is one rank higher than the second, it's impossible to make a
                        // sequence using any of the later tiles combined with the first and second (unless the second and third are equal in rank, in which
                        // case nothing is lost by marking the third tile as the second to begin searching from)
                        if (!suitedTiles[indexOfSecondTile].IsNextInSequence(suitedTiles[indexOfThirdTile]))
                        {
                            indexOfSecondTile = indexOfThirdTile - 1;
                            break;
                        }

                        if (SuitedTile.IsSequence(new SuitedTile[] { suitedTiles[indexOfFirstTile], suitedTiles[indexOfSecondTile], suitedTiles[indexOfThirdTile] }))
                        {
                            var listWithSequenceRemoved = GetListOfTilesWithPassedIndexesRemoved(suitedTiles, indexOfFirstTile, indexOfSecondTile, indexOfThirdTile);
                            
                            var removedSequence = new TileGrouping(suitedTiles[indexOfFirstTile], suitedTiles[indexOfSecondTile], suitedTiles[indexOfThirdTile]);
                            splitTilesGroups.Add(removedSequence);
                            return listWithSequenceRemoved;
                        }
                    }
                }
            }
            return tiles;
        }

        private List<Tile> GetListOfTilesWithPassedIndexesRemoved<T>(List<T> tiles, params int[] indexesToRemoveInDescendingOrder) where T : Tile
        {
            List<Tile> listWithSequenceRemoved = new List<Tile>(tiles.ToArray());
            foreach (var index in indexesToRemoveInDescendingOrder)
            {
                listWithSequenceRemoved.RemoveAt(index);
            }
            return listWithSequenceRemoved;
        }

        private List<Tile> GetTilesWithFirstTripletFromIndexNRemoved(List<Tile> tiles, int n, List<TileGrouping> splitTilesGroups)
        {
            for (int i = n; i < tiles.Count - 2; i++)
            {
                if (tiles[i].Equals(tiles[i + 1]) && tiles[i + 1].Equals(tiles[i + 2]))
                {
                    var removedTriplet = new TileGrouping(tiles[i], tiles[i + 1], tiles[i + 2]);
                    splitTilesGroups.Add(removedTriplet);

                    List<Tile> remainingTiles = GetListWithConsecutiveNTilesRemoved(tiles, i, 3);
                    return remainingTiles;
                }
            }
            return tiles;
        }

        private bool CanSplitByRemovingSequence(List<Tile> uncheckedTiles)
        {
            if (uncheckedTiles[0].GetType() != typeof(SuitedTile))
            {
                return false;
            }

            List<SuitedTile> suitedUncheckedTiles = uncheckedTiles.Cast<SuitedTile>().ToList();

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
                    if (SuitedTile.IsSequence(new SuitedTile[] { suitedUncheckedTiles[0], suitedUncheckedTiles[i], suitedUncheckedTiles[j] }))
                    {
                        List<Tile> copyList = new List<Tile>(uncheckedTiles.ToArray());
                        copyList.RemoveAt(j);
                        copyList.RemoveAt(i);
                        copyList.RemoveAt(0);
                        return (uncheckedTiles.Count == 3 || CanSplitIntoTripletsAndSequences(copyList));
                    }
                }
            }
            return false;
        }

        private bool CanSplitByRemovingTriplet(List<Tile> uncheckedTiles)
        {
            if (uncheckedTiles[0].Equals(uncheckedTiles[1]) && uncheckedTiles[1].Equals(uncheckedTiles[2]))
            {
                return (uncheckedTiles.Count == 3 || CanSplitIntoTripletsAndSequences(uncheckedTiles.GetRange(3, uncheckedTiles.Count - 3)));
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
            if (uncalledTiles.Count != WinningHandBaseTileCount || calledSets.Any() || uncalledTiles.ToHashSet().Count != WinningHandBaseTileCount / 2)
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
            var combinationsList = new List<List<T>>(nestedList[0]);
            for (int i = 1; i < nestedList.Count; i++)
            {
                var sublist = nestedList[i];
                var c = combinationsList.SelectMany(a => sublist.Select(b => new List<List<T>> {a, b})).ToList();

                combinationsList.Clear();
                for (int j = 0; j < c.Count; j++)
                {
                    combinationsList.Add(c[j].SelectMany(x => x).ToList());
                }
            }

            return combinationsList;
        }

        private static bool IsTileGroupingAlreadyContainedInSeenWaysToSplitGroups(TileGrouping grouping, List<List<TileGrouping>> seenWaysToSplitGroups)
        {
            foreach (var previousWayToSplitGroups in seenWaysToSplitGroups)
            {
                if (previousWayToSplitGroups.Contains(grouping))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool AreNestedEnumerablesSequenceEqualUnordered<T>(IEnumerable<IEnumerable<T>> currentNestedEnumerable, IEnumerable<IEnumerable<T>> otherNestedEnumerable)
        {
            return currentNestedEnumerable.All(currentSubEnumerable => otherNestedEnumerable.Any(otherSubEnumerable => otherSubEnumerable.SequenceEqual(currentSubEnumerable)));
        }
    }
}

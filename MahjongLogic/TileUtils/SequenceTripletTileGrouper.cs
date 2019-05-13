using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class SequenceTripletTileGrouper : ITileGrouper
    {
        protected readonly ITileSorter tileSorter;
        public static readonly int minimumGroupSize = 3;
        public static readonly int maximumGroupSize = 3;

        public SequenceTripletTileGrouper(ITileSorter tileSorter)
        {
            this.tileSorter = tileSorter;
        }

        public bool CanGroupTilesIntoLegalHand(IList<Tile> tiles)
        {
            tiles = tileSorter.SortTiles(tiles);
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                if (!tiles[i].Equals(tiles[i + 1]))
                {
                    continue;
                }

                var remainingTiles = GetTilesWithConsecutiveNTilesRemoved(tiles, i, 2);
                var nestedTilesGroupedBySuit = GetNestedTileListsGroupedBySuit(remainingTiles);
                if (nestedTilesGroupedBySuit.All(tilesOfSuitX => CanGroupAllTilesAtOnce(tilesOfSuitX)))
                {
                    return true;
                }

                // If the tiles at index i and i+1 make a pair, and removing that pair does not give us a hand that can
                // be split into sequences and triplets, there's no point to see if we can remove a pair using tile
                // i+1, since that will give us the same result.
                // For this purpose, we increment i a second time here if we fail to fully split the hand after
                // removing the pair.
                i++;
            }
            return false;
        }

        public IList<IList<TileGrouping>> FindAllWaysToGroupTiles(IList<Tile> tiles)
        {
            tiles = tileSorter.SortTiles(tiles);
            return FindAllWaysToGroupTiles(tiles, new List<IList<TileGrouping>>());
        }

        protected IList<IList<TileGrouping>> FindAllWaysToGroupTiles(IList<Tile> tiles,
            IList<IList<TileGrouping>> currentWaysToGroupTiles)
        {
            //  We cannot do anything more if we have fewer tiles remaining than the minimum group size.
            if (tiles.Count < minimumGroupSize)
            {
                return currentWaysToGroupTiles;
            }

            IList<IList<TileGrouping>> listOfAllPossibleWaysToGroupTiles = new List<IList<TileGrouping>>();
            var listOfTileGroups = FindAllGroupsInTiles(tiles);
            foreach (var group in listOfTileGroups)
            {
                var remainingTiles = GetListOfTilesWithPassedItemsRemoved(tiles, group.ToArray());

                var newGroups = FindAllWaysToGroupTiles(remainingTiles, currentWaysToGroupTiles);
                foreach (var tileGroups in newGroups)
                {
                    tileGroups.Add(group);
                    AddCurrentGroupsToNestedListIfNotAlreadyIn(tileGroups, listOfAllPossibleWaysToGroupTiles);
                }

                // We want to add the current group to our list, even if there are no ways to group the remaining tiles
                // after removing this group.
                if (newGroups.Count == 0)
                {
                    listOfAllPossibleWaysToGroupTiles.Add(new List<TileGrouping> { group });
                }
            }
            return listOfAllPossibleWaysToGroupTiles;
        }

        public IList<IList<TileGrouping>> FindAllWaysToGroupTilesAfterRemovingAPair(IList<Tile> tiles)
        {
            var allWaysToGroupTiles = new List<IList<TileGrouping>>();

            tiles = tileSorter.SortTiles(tiles);
            for (int i = 0; i < tiles.Count - 1; i++)
            {

                var pair = new TileGrouping(tiles[i], tiles[i + 1]);
                if (!tiles[i].Equals(tiles[i + 1]))
                {
                    continue;
                }

                // If we've already seen all ways of splitting these tiles starting with an identical pair to this one,
                // we won't find any new ways to split the tiles by starting with this pair
                if (IsValueAlreadyContainedInNestedEnumerable(pair, allWaysToGroupTiles))
                {
                    i++;
                    continue;
                }

                var remainingTiles = GetTilesWithConsecutiveNTilesRemoved(tiles, i, 2);
                var currentWaysToSplitTiles = FindAllWaysToGroupTiles(remainingTiles);
                foreach (var listOfGroups in currentWaysToSplitTiles)
                {
                    listOfGroups.Add(pair);
                    allWaysToGroupTiles.Add(listOfGroups);
                }
            }
            return allWaysToGroupTiles;
        }

        public IList<IList<TileGrouping>> FindAllWaysToFullyGroupTilesAfterRemovingAPair(IList<Tile> tiles)
        {
            var tileCount = tiles.Count;
            var allWaysToGroupTiles = FindAllWaysToGroupTilesAfterRemovingAPair(tiles);
            var allWaysToFullyGroupTiles = allWaysToGroupTiles.Where(
                sublist => sublist.Sum(group => group.Count) == tileCount).ToList();

            return allWaysToFullyGroupTiles;
        }

        public IList<TileGrouping> FindAllGroupsInTiles(IList<Tile> tiles)
        {
            tiles = tileSorter.SortTiles(tiles);
            IList<TileGrouping> allGroupsInTiles = new List<TileGrouping>();

            for (int firstTileIndex = 0; firstTileIndex < tiles.Count - (maximumGroupSize - 1); firstTileIndex++)
            {
                RecursivelyFindAllGroupsInTiles(tiles, firstTileIndex + 1, maximumGroupSize - 2, allGroupsInTiles,
                    tiles[firstTileIndex]);
            }
            return allGroupsInTiles;
        }

        protected void RecursivelyFindAllGroupsInTiles(IList<Tile> tiles, int currentIndex, int maxDepth,
            IList<TileGrouping> allGroups, params Tile[] currentTiles)
        {
            if (!currentTiles[0].CanBelongToSameGroup(tiles[currentIndex]))
            {
                return;
            }

            Tile[] potentialGroup = new List<Tile>(currentTiles)
            {
                tiles[currentIndex]
            }.ToArray();
            var groupingOfTiles = new TileGrouping(potentialGroup);

            if (Tile.IsGroup(potentialGroup) && !allGroups.Contains(groupingOfTiles)) // || Tile.IsPair(potentialGroup))
            {
                allGroups.Add(groupingOfTiles);
            }

            if (maxDepth > 0)
            {
                for (int nextTileIndex = currentIndex; nextTileIndex < tiles.Count - maxDepth; nextTileIndex++)
                {
                    RecursivelyFindAllGroupsInTiles(tiles, nextTileIndex + 1, maxDepth - 1, allGroups, potentialGroup);
                }
            }
        }

        protected IList<Tile> GetTilesWithConsecutiveNTilesRemoved(IEnumerable<Tile> tiles,
            int indexOfFirstTileToRemove, int n)
        {
            IEnumerable<Tile> leftSubEnumerable = new List<Tile>();
            IEnumerable<Tile> rightSubEnumerable = new List<Tile>();

            if (indexOfFirstTileToRemove != 0)
            {
                leftSubEnumerable = tiles.Take(indexOfFirstTileToRemove);
            }
            if (indexOfFirstTileToRemove != tiles.Count() - n)
            {
                rightSubEnumerable = tiles.Skip(indexOfFirstTileToRemove + n).Take(
                    tiles.Count() - (indexOfFirstTileToRemove + n));
            }

            return leftSubEnumerable.Concat(rightSubEnumerable).ToList();
        }

        protected IList<IList<Tile>> GetNestedTileListsGroupedBySuit(IEnumerable<Tile> tiles)
        {
            IList<IList<Tile>> tilesGroupedBySuit = new List<IList<Tile>>();
            IList<Tile> remainingTiles = new List<Tile>(tiles);

            while (remainingTiles.Count > 0)
            {
                var suitOfFirstTile = remainingTiles[0].Suit;
                var tilesOfSpecificSuit = remainingTiles.Where(tile => tile.Suit == suitOfFirstTile).ToList();
                tilesGroupedBySuit.Add(tilesOfSpecificSuit);
                remainingTiles = remainingTiles.Where(tile => tile.Suit != suitOfFirstTile).ToList();
            }
            return tilesGroupedBySuit;
        }

        protected void AddCurrentGroupsToNestedListIfNotAlreadyIn(IList<TileGrouping> currentTileGroups,
            IList<IList<TileGrouping>> nestedListOfTileGroups)
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

        protected static bool AreNestedEnumerablesSequenceEqualUnordered<T>(
            IEnumerable<IEnumerable<T>> currentNestedEnumerable, IEnumerable<IEnumerable<T>> otherNestedEnumerable)
        {
            return currentNestedEnumerable.All(
                currentSubEnumerable => otherNestedEnumerable.Any(
                    otherSubEnumerable => otherSubEnumerable.SequenceEqual(currentSubEnumerable)));
        }

        protected virtual bool CanGroupAllTilesAtOnce(IList<Tile> remainingTiles)
        {
            if (remainingTiles.Count == 0)
            {
                return true;
            }
            if (remainingTiles.Count < minimumGroupSize)
            {
                return false;
            }

            return CanGroupAllTilesUsingSequenceStartingAtIndexN(remainingTiles, 0) ||
                CanGroupAllTilesUsingTripletStartingAtIndexN(remainingTiles, 0);
        }

        protected bool CanGroupAllTilesUsingSequenceStartingAtIndexN(IList<Tile> remainingTiles, int n)
        {
            if (remainingTiles.Count == 0)
            {
                return true;
            }
            if (remainingTiles.Count < n + 3 || !remainingTiles[n].CanMakeSequence())
            {
                return false;
            }

            for (int secondTileIndex = n + 1; secondTileIndex < remainingTiles.Count - 1; secondTileIndex++)
            {
                if (!remainingTiles[n].CanBelongToSameGroup(remainingTiles[secondTileIndex]))
                {
                    break;
                }

                for (int thirdTileIndex = secondTileIndex + 1; thirdTileIndex < remainingTiles.Count; thirdTileIndex++)
                {
                    if (!remainingTiles[n].CanBelongToSameGroup(remainingTiles[thirdTileIndex]))
                    {
                        break;
                    }
                    if (Tile.IsSequence(remainingTiles[n], remainingTiles[secondTileIndex],
                        remainingTiles[thirdTileIndex]))
                    {
                        var tilesAfterRemovingSequence = GetListOfTilesWithPassedIndexesRemoved(
                            remainingTiles, n, secondTileIndex, thirdTileIndex);
                        return CanGroupAllTilesAtOnce(tilesAfterRemovingSequence);
                    }
                }
            }
            return false;
        }

        protected bool CanGroupAllTilesUsingTripletStartingAtIndexN(IList<Tile> remainingTiles, int n)
        {
            if (remainingTiles.Count == 0)
            {
                return true;
            }
            if (remainingTiles.Count < n + 3)
            {
                return false;
            }

            if (remainingTiles[n].Equals(remainingTiles[n + 1]) && remainingTiles[n + 1].Equals(remainingTiles[n + 2]))
            {
                var tilesAfterRemovingTriplet = GetTilesWithConsecutiveNTilesRemoved(remainingTiles, n, 3);
                return CanGroupAllTilesAtOnce(tilesAfterRemovingTriplet);
            }
            return false;
        }

        protected IList<Tile> GetListOfTilesWithPassedIndexesRemoved<T>(IEnumerable<T> tiles, params int[] indexesToRemove)
            where T : Tile
        {
            Array.Sort(indexesToRemove);
            Array.Reverse(indexesToRemove);
            IList<Tile> listWithIndexesRemoved = new List<Tile>(tiles);

            foreach (var index in indexesToRemove)
            {
                listWithIndexesRemoved.RemoveAt(index);
            }
            return listWithIndexesRemoved;
        }

        protected IList<Tile> GetListOfTilesWithPassedItemsRemoved<T>(IEnumerable<T> tiles, params T[] itemsToRemove)
            where T : Tile
        {
            IList<Tile> listWithItemsRemoved = new List<Tile>(tiles);

            foreach (var item in itemsToRemove)
            {
                listWithItemsRemoved.Remove(item);
            }
            return listWithItemsRemoved;
        }

        protected static bool IsValueAlreadyContainedInNestedEnumerable<T>(T value,
            IEnumerable<IEnumerable<T>> nestedEnumerable)
        {
            foreach (var innerEnumerable in nestedEnumerable)
            {
                if (innerEnumerable.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
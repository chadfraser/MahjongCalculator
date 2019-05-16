using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder : IWaitingDistanceFinder
    {
        public RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder()
        {
            TileSorter = new SuitedHonorBonusTileSorter();
        }

        private ITileSorter TileSorter { get; }

        public int GetWaitingDistance(IList<Tile> tiles)
        {
            tiles = TileSorter.SortTiles(tiles);
            var alreadySeparatedGroupCount = (14 - tiles.Count) / 3;
            return GetWaitingDistance(tiles, alreadySeparatedGroupCount);
        }

        private int GetWaitingDistance(IList<Tile> tiles, int foundGroupCount)
        {
            var maximumWaitingDistance = 8;
            var minimumFoundWaitingDistance = maximumWaitingDistance;

            if (tiles.Count >= 13)
            {
                minimumFoundWaitingDistance = Math.Min(minimumFoundWaitingDistance,
                    GetThirteenOrphansWaitingDistance(tiles));
                minimumFoundWaitingDistance = Math.Min(minimumFoundWaitingDistance,
                    GetSevenPairsWaitingDistance(tiles));
            }

            if (tiles.Count > 3)
            {
                var waitingDistanceByRemovingGroups = RecursivelyGetWaitingDistanceByRemovingGroups(tiles,
                    minimumFoundWaitingDistance, foundGroupCount);
                minimumFoundWaitingDistance = Math.Min(minimumFoundWaitingDistance,
                    waitingDistanceByRemovingGroups);
            }

            var drawsAwayFromWaitingByCountingGroups = GetWaitingDistanceByCountingPartialGroups(tiles,
                maximumWaitingDistance, foundGroupCount);

            minimumFoundWaitingDistance = Math.Min(minimumFoundWaitingDistance,
                drawsAwayFromWaitingByCountingGroups);

            // check for waiting: returns 0 if waiting, 1+ else
            //if (minimumFoundDrawsAwayFromWaiting == 0 && tiles.Count == 13)
            //{
            //    foreach (var tile in TileInstance.GetAllMainTilesSorted())
            //    {
            //        var checkHand = new List<Tile>(tiles);
            //        IDictionary<Tile, int> countOfTilesUsedInHand = checkHand.GroupBy(x => x)
            //            .ToDictionary(g => g.Key, g=> g.Count());
            //        var checkRes = 0;
            //        if (countOfTilesUsedInHand.ContainsKey(tile) && countOfTilesUsedInHand[tile] == 4)
            //        {
            //            continue;
            //        }
            //        checkHand.Add(tile);
            //        // sort hand
            //        checkRes = GetDrawsAwayFromWaitingNumber(checkHand, 0);
            //        minimumFoundDrawsAwayFromWaiting = Math.Min(minimumFoundDrawsAwayFromWaiting, checkRes) + 1;
            //    }
            //}

            return minimumFoundWaitingDistance;
        }

        private int RecursivelyGetWaitingDistanceByRemovingGroups(IList<Tile> tiles, int minimumWaitingDistance,
            int foundGroupCount)
        {
            for (int firstTileIndex = 0; firstTileIndex < tiles.Count - 2; firstTileIndex++)
            {
                var firstTile = tiles[firstTileIndex];
                for (int secondTileIndex = firstTileIndex + 1; secondTileIndex < tiles.Count - 1; secondTileIndex++)
                {
                    var secondTile = tiles[secondTileIndex];
                    if (!firstTile.CanBelongToSameGroup(secondTile))
                    {
                        break;
                    }

                    for (int thirdTileIndex = secondTileIndex + 1; thirdTileIndex < tiles.Count; thirdTileIndex++)
                    {
                        var thirdTile = tiles[thirdTileIndex];
                        if (!firstTile.CanBelongToSameGroup(thirdTile))
                        {
                            break;
                        }

                        if (Tile.IsGroup(firstTile, secondTile, thirdTile))
                        {
                            var tilesWithGroupRemoved = new List<Tile>(tiles);
                            tilesWithGroupRemoved.RemoveAt(thirdTileIndex);
                            tilesWithGroupRemoved.RemoveAt(secondTileIndex);
                            tilesWithGroupRemoved.RemoveAt(firstTileIndex);

                            minimumWaitingDistance = Math.Min(minimumWaitingDistance,
                                GetWaitingDistance(tilesWithGroupRemoved, foundGroupCount + 1));

                            if (Tile.IsTriplet(firstTile, secondTile, thirdTile))
                            {
                                firstTileIndex++;
                                secondTileIndex = tiles.Count;
                                break;
                            }
                        }
                    }
                }
            }
            return minimumWaitingDistance;
        }

        private int GetWaitingDistanceByCountingPartialGroups(IList<Tile> tiles, int maximumWaitingDistance,
            int foundGroupCount)
        {
            OutputPairAndPartialSequenceCountsFromTiles(tiles, out int pairCount, out int partialSequenceCount);

            // Given three unconnected tiles, it takes a minimum of two draws to turn them into a group
            // e.g., ABC -> AAC -> AAA
            // Therefore, in the worst case scenario we need eight draws to turn our hand of tiles into four groups,
            // and we subtract two from this value for every distinct group we already have in our hand
            var currentWaitingDistance = maximumWaitingDistance - 2 * foundGroupCount;
            var remainingGroupsNeededToFind = tiles.Count / 3;

            // If we have enough pairs/partial sequences to complete the hand (given we transform them into groups),
            // we subtract (tiles.Count)/3 from our currentWaitingDistance
            // This represents that, instead of needing to spend two draws to turn three unconnected tiles into a group,
            // we only need one draw to turn a pair/partial sequence into a group, and caps this value at four groups
            // in total
            // (e.g., since a hand can only use four groups, having five partial sequences is no better than having four)

            // If our hand contains any pairs, we can subtract one more from this result to represent that we do not need
            // to pair the last tile in our hand. If our hand only contains partial sequences and no pairs, we will need
            // to spend one more draw to pair that last tile, and so we can't subtract one in this case
            if (pairCount + partialSequenceCount > remainingGroupsNeededToFind)
            {
                var valueIfAlreadyHasPairForLastTile = pairCount > 0 ? 1 : 0;
                currentWaitingDistance -= (remainingGroupsNeededToFind + valueIfAlreadyHasPairForLastTile);
            }

            // If we don't have enough pairs/partial sequences to complete the hand, we simply take the count of pairs
            // and partial sequences we do have, and subtract it from our currentWaitingDistance
            // This represents that, instead of needing to spend two draws to turn three unconnected tiles into a group,
            // we only need one draw to turn a pair/partial sequence into a group
            else
            {
                currentWaitingDistance -= (pairCount + partialSequenceCount);
            }
            return currentWaitingDistance;
        }

        private void OutputPairAndPartialSequenceCountsFromTiles(IList<Tile> tiles, out int pairCount,
            out int partialSequenceCount)
        {
            pairCount = 0;
            partialSequenceCount = 0;

            // Because the hand is sorted, we only have to look at adjacent tiles to determine if they form a pair or
            // partial sequence
            for (int firstTileIndex = 0; firstTileIndex < tiles.Count - 1; firstTileIndex++)
            {
                var firstTile = tiles[firstTileIndex];
                var secondTile = tiles[firstTileIndex + 1];

                // If we do find a pair or partial sequence with two adjacent tiles, increment firstTileIndex again so
                // it will skip over the second tile in that pair/partial sequence
                // This prevents us from using the same tile in two pairs/partial sequences at once
                if (Tile.IsPair(firstTile, secondTile))
                {
                    pairCount++;
                    firstTileIndex++;
                }
                else if (firstTileIndex < tiles.Count - 2 && Tile.IsPair(secondTile, tiles[firstTileIndex + 2]))
                {
                    pairCount++;
                    firstTileIndex += 2;
                }
                else if (firstTile.CanBelongToSameGroup(secondTile))
                {
                    partialSequenceCount++;
                    firstTileIndex++;
                }
            }
        }

        public int GetThirteenOrphansWaitingDistance(IList<Tile> tiles)
        {
            var maximumDrawsAwayFromThirteenOrphans = 13;
            if (tiles.Count < 13)
            {
                return int.MaxValue;
            }
            var distinctTerminalOrHonorCount = tiles.Where(x => x.IsTerminalOrHonor()).ToHashSet().Count();
            var terminalOrHonorPairExists = tiles.GroupBy(t => t).Where(g => g.Count() > 1)
                .Any(g => g.First().IsTerminalOrHonor());

            return terminalOrHonorPairExists ? maximumDrawsAwayFromThirteenOrphans - distinctTerminalOrHonorCount - 1 :
                maximumDrawsAwayFromThirteenOrphans - distinctTerminalOrHonorCount;
        }

        public int GetSevenPairsWaitingDistance(IList<Tile> tiles)
        {
            var minimumTilesRequiredInHand = 13;
            if (tiles.Count < minimumTilesRequiredInHand)
            {
                return int.MaxValue;
            }
            var distinctPairCount = tiles.GroupBy(t => t).Where(g => g.Count() >= 2).Count();
            var singleTileCount = tiles.GroupBy(t => t).Where(g => g.Count() == 1).Count();
            // Our hand will have at most 7 distinct tiles in it, one for each pair
            var singleTilesUsuableInSevenPairsHand = Math.Min(singleTileCount, 7 - distinctPairCount);

            // We must replace all unusuable tiles in our hand: Leftover tiles from triplets/quads, and single tiles
            // that would still be leftover after pairing seven tiles
            var unusableTilesCount = minimumTilesRequiredInHand - 2 * distinctPairCount - 
                singleTilesUsuableInSevenPairsHand;

            return unusableTilesCount;
        }
    }
}
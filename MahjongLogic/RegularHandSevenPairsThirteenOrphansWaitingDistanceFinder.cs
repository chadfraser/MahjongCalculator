using System;
using System.Collections.Generic;
using System.Linq;

namespace Mahjong
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
            return GetWaitingDistance(tiles, 0);
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

    /* Make a clone of your closed tiles to 'winningTiles', then do the same with open tiles noting they're open
     * and tracking how many open seqs/trips exist
     * 
     * Run v3 with 0 depth, 0 pairs, empty dict of int-List<TileGrouping>, cloned tiles
     *      Return if winning hand
     *      If pairs <2 and melds <4:
     *              Build empty rejected list
     *              Iterate building all sequences logically (rank k > rank j > rank i) only looking at unused tiles
     *                 If the sequence has not been rejected yet:
     *                      - mark these tiles as used and their meld ID as N
     *                      - recurse with depth + 1, sequences + 1
     *                      - add them to the rejected list
     *                      - mark these tiles as unused with no meld ID
     *      If still pairs <2 and melds <4:
     *              Build empty rejected list
     *              Iterate building all triplets logically (rank k > rank j > rank i) only looking at unused tiles
     *                 If the triplet has not been rejected yet:
     *                      - mark these tiles as used and their meld ID as N
     *                      - recurse with depth + 1, triplets + 1
     *                      - add them to the rejected list
     *                      - mark these tiles as unused with no meld ID
     *      If pairs <1 || and melds == 0:
     *              Build empty rejected list
     *              Iterate building all pairs logically (rank j > rank i) only looking at unused tiles
     *                 If the triplet has not been rejected yet:
     *                      - mark these tiles as used and their meld ID as N
     *                      - recurse with depth + 1, pairs + 1
     *                      - add them to the rejected list
     *                      - mark these tiles as unused with no meld ID
     *                      
     *      Run calc with remaining tiles, whether triplets first or second, whether backwards or forwards
     *          Clone unused tiles and mark them all as 'shantenFiller'
     *          if we have 14 unused, shanten = 7 (7 pairs)
     *              if triplets first:
     *                  build a list of Grouping<int, trackedTile> with the int as their value (unique identifier per tile)
     *                  (if backwards, iterate the following in reverse)
     *                      iterate through the list of groupings
     *                          if we have 4+ melds or any pairs, break this iteration
     *                          if we have 2 in this group
     *                              mark the group as used and not shantenFiller
     *                              subtract 3 from unusedCount, add 1 to triplets and to shanten
     *              (if backwards, iterate the following in reverse)
     *                  iterate through the unused tiles
     *                      if we have 4+ melds or any pairs, break this iteration
     *                      nested iterate through the unused tiles
     *                          if AB or AxB:
     *                              mark A and B as used and not shantenFiller
     *                              subtract 3 from unusedCount, add 1 to sequences and to shanten
     *              if not triplets first, do the triplets stuff here
     *              remove two from unusedCount and increase shanten by 1 to account for a random pair
     *              if still have all tiles unused in hand, shanten = 7
     *              if we have no melds
     *                  needed pairs = unusedCount / 2
     *                  shanten 
     *              if we have melds, shanten += unusedCount * 2 / 3
     *                              
     *                              
     *                              
     *                              
     */
}
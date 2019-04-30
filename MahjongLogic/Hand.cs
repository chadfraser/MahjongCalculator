using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    class Hand
    {
        List<Tile> uncalledTiles;
        List<List<Tile>> calledSets;
        static readonly int WinningHandBaseTileCount = 14;

        public bool IsWinningHand()
        {
            if (IsKokushiMusou() || IsChiitoitsu())
            {
                return true;
            }
            SortHand();
            return false;
        }

        public void SortHand()
        {
            var suitedTiles = uncalledTiles.OfType<SuitedTile>().ToList();
            var honorTiles = uncalledTiles.OfType<HonorTile>().ToList();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank);
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType);
            uncalledTiles = ((List<Tile>)orderedSuitedTiles).Concat(((List<Tile>)orderedHonorTiles)).ToList();
        }

        public bool FindAndRemovePair()
        {
            List<Tile> remainingTiles;
            for (int i = 0; i < uncalledTiles.Count - 2; i += 2)
            {
                if (uncalledTiles[i] == uncalledTiles[i + 1])
                {
                    List<Tile> leftSubList = new List<Tile>();
                    List<Tile> rightSubList = new List<Tile>();

                    if (i != 0)
                    {
                        leftSubList = uncalledTiles.GetRange(0, i);
                    }
                    if (i != uncalledTiles.Count - 2)
                    {
                        rightSubList = uncalledTiles.GetRange(i + 2, uncalledTiles.Count - i - 2);
                    }

                    remainingTiles = leftSubList.Concat(rightSubList).ToList();

                    if (CanBreakIntoKoutsuAndShuntsu(remainingTiles))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanBreakIntoKoutsuAndShuntsu(List<Tile> remainingTiles)
        {
            if (remainingTiles[0] == remainingTiles[1])
            {
                if (remainingTiles[1] == remainingTiles[2])
                {
                    // Remove tiles and call CanBreakInto...
                }
                else if (((SuitedTile)remainingTiles[0]).IsNextInShuntsu(((SuitedTile)remainingTiles[2])))
                {
                    // Check for third tile in run

                }
            }

            return true;
        }

        private bool IsKokushiMusou()
        {
            var kokushiSet = new HashSet<Tile> { };

            if (uncalledTiles.Count != WinningHandBaseTileCount || calledSets.Any())
            {
                return false;
            }

            return uncalledTiles.ToHashSet().SetEquals(kokushiSet);
        }

        private bool IsChiitoitsu()
        {
            if (uncalledTiles.Count != WinningHandBaseTileCount || calledSets.Any())
            {
                return false;
            }
            SortHand();

            for (int i = 0; i < uncalledTiles.Count - 2; i += 2)
            {
                if (uncalledTiles[i] != uncalledTiles[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

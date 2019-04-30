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
            CalledSets = new List<List<Tile>>();
        }

        public bool IsWinningHand()
        {
            if (IsKokushiMusou() || IsChiitoitsu())
            {
                return true;
            }
            SortHand();
            return false;
        }

        public List<Tile> UncalledTiles { get; set; }
        public List<List<Tile>> CalledSets { get; set; }

        public void SortHand()
        {
            var suitedTiles = UncalledTiles.OfType<SuitedTile>().ToList();
            Console.WriteLine();
            foreach (var t in suitedTiles.OfType<SuitedTile>())
            {
                System.Console.WriteLine($"{t.Rank} {t.Suit}");
            }
            var honorTiles = UncalledTiles.OfType<HonorTile>().ToList();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).ToList();
            Console.WriteLine();
            foreach (var t in orderedSuitedTiles.OfType<SuitedTile>())
            {
                System.Console.WriteLine($"{t.Rank} {t.Suit}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType).ToList();
            List<Tile> orderedCastedSuitedTiles = new List<Tile>(orderedSuitedTiles.ToArray());
            List<Tile> orderedCastedHonorTiles = new List<Tile>(orderedHonorTiles.ToArray());

            UncalledTiles = orderedCastedSuitedTiles.Concat(orderedCastedHonorTiles).ToList();

            foreach (var t in UncalledTiles.OfType<SuitedTile>())
            {
                System.Console.WriteLine($"{t.Rank} {t.Suit}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        public bool FindAndRemovePair()
        {
            List<Tile> remainingTiles;
            for (int i = 0; i < UncalledTiles.Count - 2; i += 2)
            {
                if (UncalledTiles[i] == UncalledTiles[i + 1])
                {
                    List<Tile> leftSubList = new List<Tile>();
                    List<Tile> rightSubList = new List<Tile>();

                    if (i != 0)
                    {
                        leftSubList = UncalledTiles.GetRange(0, i);
                    }
                    if (i != UncalledTiles.Count - 2)
                    {
                        rightSubList = UncalledTiles.GetRange(i + 2, UncalledTiles.Count - i - 2);
                    }

                    remainingTiles = leftSubList.Concat(rightSubList).ToList();

                    if (CanBreakIntoTripletsAndSequences(remainingTiles))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanBreakIntoTripletsAndSequences(List<Tile> remainingTiles)
        {
            if (remainingTiles[0] == remainingTiles[1])
            {
                if (remainingTiles[1] == remainingTiles[2])
                {
                    // Remove tiles and call CanBreakInto...
                }
                else if (((SuitedTile)remainingTiles[0]).IsNextInSequence(((SuitedTile)remainingTiles[2])))
                {
                    // Check for third tile in run

                }
            }

            return true;
        }

        private bool IsKokushiMusou()
        {
            var kokushiSet = new HashSet<Tile> { };

            if (UncalledTiles.Count != WinningHandBaseTileCount || CalledSets.Any())
            {
                return false;
            }

            return UncalledTiles.ToHashSet().SetEquals(kokushiSet);
        }

        private bool IsChiitoitsu()
        {
            if (UncalledTiles.Count != WinningHandBaseTileCount || CalledSets.Any())
            {
                return false;
            }
            SortHand();

            for (int i = 0; i < UncalledTiles.Count - 2; i += 2)
            {
                if (UncalledTiles[i] != UncalledTiles[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

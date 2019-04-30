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
            if (IsThirteenOrphans() || IsSevenPairs())
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
            var honorTiles = UncalledTiles.OfType<HonorTile>().ToList();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).ToList();
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType).ToList();
            List<Tile> orderedCastedSuitedTiles = new List<Tile>(orderedSuitedTiles.ToArray());
            List<Tile> orderedCastedHonorTiles = new List<Tile>(orderedHonorTiles.ToArray());

            UncalledTiles = orderedCastedSuitedTiles.Concat(orderedCastedHonorTiles).ToList();

            foreach (var t in UncalledTiles.OfType<SuitedTile>())
            {
                Console.WriteLine(t);
            }
            Console.WriteLine();
        }

        public bool FindAndRemovePair()
        {
            List<Tile> remainingTiles;
            for (int i = 0; i < UncalledTiles.Count - 2; i += 2)
            {
                if (UncalledTiles[i].Equals(UncalledTiles[i + 1]))
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
            if (remainingTiles[0].Equals(remainingTiles[1]))
            {
                if (remainingTiles[1].Equals(remainingTiles[2]))
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

        private bool IsThirteenOrphans()
        {
            if (UncalledTiles.Count != WinningHandBaseTileCount || CalledSets.Any())
            {
                return false;
            }

            return UncalledTiles.ToHashSet().SetEquals(GetThirteenOrphansSet());
        }

        private bool IsSevenPairs()
        {
            if (UncalledTiles.Count != WinningHandBaseTileCount || CalledSets.Any())
            {
                return false;
            }
            if (UncalledTiles.ToHashSet().Count != WinningHandBaseTileCount / 2)
            {
                Console.WriteLine("HERE");
                return false;
            }

            SortHand();
            for (int i = 0; i < UncalledTiles.Count - 2; i += 2)
            {
                Console.WriteLine(i);
                if (!UncalledTiles[i].Equals(UncalledTiles[i + 1]))
                {
                    Console.WriteLine(">>" + i);
                    return false;
                }
            }

            return true;
        }

        private HashSet<Tile> GetThirteenOrphansSet()
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
    }
}

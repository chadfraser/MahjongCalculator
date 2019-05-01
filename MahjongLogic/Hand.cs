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

        public List<Tile> UncalledTiles { get; set; }
        public List<List<Tile>> CalledSets { get; set; }

        public bool IsWinningHand()
        {
            if (GetAdjustedCountOfHandTiles() != WinningHandBaseTileCount)
            {
                return false;
            }

            if (IsThirteenOrphans() || IsSevenPairs())
            {
                return true;
            }
            SortHand();
            return CanRemovePairAndSplitRemainingTilesIntoSequencesAndTriplets();
        }

        public void SortHand()
        {
            var suitedTiles = UncalledTiles.OfType<SuitedTile>().ToList();
            var honorTiles = UncalledTiles.OfType<HonorTile>().ToList();
            var orderedSuitedTiles = suitedTiles.OrderBy(t => t.Suit).ThenBy(t => t.Rank).ToList();
            var orderedHonorTiles = honorTiles.OrderBy(t => t.Suit).ThenBy(t => t.HonorType).ToList();
            List<Tile> orderedCastedSuitedTiles = new List<Tile>(orderedSuitedTiles.ToArray());
            List<Tile> orderedCastedHonorTiles = new List<Tile>(orderedHonorTiles.ToArray());

            UncalledTiles = orderedCastedSuitedTiles.Concat(orderedCastedHonorTiles).ToList();
        }

        private bool CanRemovePairAndSplitRemainingTilesIntoSequencesAndTriplets()
        {
            List<Tile> uncheckedTiles;
            for (int i = 0; i < UncalledTiles.Count - 1; i++)
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

                    uncheckedTiles = leftSubList.Concat(rightSubList).ToList();
                    var suitedTiles = uncheckedTiles.OfType<SuitedTile>().ToList();
                    var honorTiles = uncheckedTiles.OfType<HonorTile>().ToList();
                    List<Tile> castedSuitedTiles = new List<Tile>(suitedTiles.ToArray());
                    List<Tile> castedHonorTiles = new List<Tile>(honorTiles.ToArray());

                    if (CanSplitIntoTripletsAndSequences(castedSuitedTiles) && CanSplitIntoTripletsAndSequences(castedHonorTiles))
                    {
                        return true;
                    }
                    i++;
                }
            }
            return false;
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
                return false;
            }

            SortHand();
            for (int i = 0; i < UncalledTiles.Count - 2; i += 2)
            {
                if (!UncalledTiles[i].Equals(UncalledTiles[i + 1]))
                {
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

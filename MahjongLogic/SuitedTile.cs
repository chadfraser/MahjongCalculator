using System;
using System.Collections.Generic;
using System.Linq;

namespace Mahjong
{
    public class SuitedTile : Tile, IComparable<SuitedTile>
    {
        public SuitedTile(Suit suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int Rank { get; set; }

        public bool IsNextInShuntsu(SuitedTile other)
        {
            return (Suit == other.Suit && Rank + 1 == other.Rank);
        }

        public static bool IsShuntsu(SuitedTile[] tiles)
        {
            if (tiles.Length != 3)
            {
                return false;
            }

            if (new HashSet<Suit>(from a in tiles select a.Suit).Count != 1)
            {
                return false;
            }

            Array.Sort(tiles);
            for (int i = 0; i < tiles.Length - 1; i++)
            {
                if (tiles[i].Rank + 1 != tiles[i + 1].Rank)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool IsTerminal()
        {
            return (Rank == 1 || Rank == 9);
        }

        public override bool IsTerminalOrHonor()
        {
            return IsTerminal();
        }

        public int CompareTo(SuitedTile other)
        {
            return Rank.CompareTo(other.Rank);
        }

        public override bool CanMakeShuntsu()
        {
            return true;
        }

        public static bool operator <(SuitedTile a, SuitedTile b)
        {
            return a.Rank < b.Rank;
        }

        public static bool operator >(SuitedTile a, SuitedTile b)
        {
            return a.Rank > b.Rank;
        }

        public override bool Equals(Object obj)
        {
            if ((obj is null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                SuitedTile t = (SuitedTile)obj;
                return (Rank == t.Rank) && (Suit == t.Suit);
            }
        }

        public override int GetHashCode()
        {
            const int baseHash = 7673;
            const int hashFactor = 95651;

            int hash = baseHash;
            hash = (hash * hashFactor) ^ Suit.GetHashCode();
            hash = (hash * hashFactor) ^ Rank.GetHashCode();
            return hash;
        }
    }
}
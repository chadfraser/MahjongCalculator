using System;
using System.Collections.Generic;
using System.Linq;

namespace Mahjong
{
    public class SuitedTile : Tile, IComparable, IComparable<SuitedTile>
    {
        public SuitedTile(Suit suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int Rank { get; set; }

        public override bool CanMakeSequence()
        {
            return true;
        }

        public override bool IsTerminal()
        {
            return Rank == 1 || Rank == 9;
        }

        public override bool IsTerminalOrHonor()
        {
            return IsTerminal();
        }

        public override bool CanBelongToSameGroup(params Tile[] otherTiles)
        {
            if (otherTiles.Any(t => !(t is SuitedTile)))
            {
                return false;
            }
            if (otherTiles.Length < 4 && otherTiles.All(t => Equals(t)))
            {
                return true;
            }
            var tempList = new List<Tile>(otherTiles)
            {
                this
            };

            foreach (var t in tempList)
            {
                foreach (var t2 in tempList)
                {
                    if (!((SuitedTile)t2).IsWithinBoundsOfSameSequence((SuitedTile)t))
                    {
                        return false;
                    }
                }
            }
            return otherTiles.Length < 3;
        }

        public bool IsNextInSequence(SuitedTile other)
        {
            return Suit == other.Suit && Rank + 1 == other.Rank;
        }

        public bool IsWithinBoundsOfSameSequence(SuitedTile other)
        {
            var lowerRank = (Rank < other.Rank) ? Rank : other.Rank;
            var higherRank = (Rank > other.Rank) ? Rank : other.Rank;

            return Suit == other.Suit && !(higherRank > lowerRank + 2);
        }

        public override bool IsGroup(params Tile[] tiles)
        {
            return IsSequence(tiles) || IsTriplet(tiles) || IsQuad(tiles);
        }

        public override bool IsSequence(params Tile[] tiles)
        {
            var suitedTiles = tiles.OfType<SuitedTile>().ToArray();
            return suitedTiles.Length == tiles.Length && IsSequence(suitedTiles);
        }

        public static bool IsSequence(params SuitedTile[] tiles)
        {
            if (tiles.Length != 3 || new HashSet<Suit>(from t in tiles select t.Suit).Count != 1)
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

        public override string ToString()
        {
            return $"{Rank} of {Suit.Name}";
        }

        public override bool Equals(Object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                SuitedTile t = (SuitedTile)obj;
                return Rank == t.Rank && Suit == t.Suit;
            }
        }

        public override int GetHashCode()
        {
            const int baseHash = 8737;
            const int hashFactor = 74933;

            int hash = baseHash;
            hash = (hash * hashFactor) ^ Suit.GetHashCode();
            hash = (hash * hashFactor) ^ Rank.GetHashCode();
            return hash;
        }

        public static bool operator <(SuitedTile a, SuitedTile b)
        {
            return a.Rank < b.Rank;
        }

        public static bool operator >(SuitedTile a, SuitedTile b)
        {
            return a.Rank > b.Rank;
        }

        public int CompareTo(Tile other)
        {
            if (other.GetType() == typeof(SuitedTile))
            {
                SuitedTile t = (SuitedTile)other;
                return CompareTo(t);
            }
            return -1;
        }

        public int CompareTo(SuitedTile other)
        {
            var comparator = Suit.Equals(other.Suit) ? Rank.CompareTo(other.Rank) : Suit.CompareTo(other.Suit);
            return comparator;
        }

        public int CompareTo(object obj)
        {
            if (obj is null || 
                !(obj.GetType().Equals(typeof(Tile)) || obj.GetType().IsAssignableFrom(typeof(Tile))))
            {
                throw new Exception($"Cannot compare objects of type {obj.GetType()} and type {GetType()}.");
            }
            else
            {
                Tile t = (Tile)obj;
                return CompareTo(t);
            }
        }
    }
}
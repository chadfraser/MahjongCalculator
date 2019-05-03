using System;
using System.Collections.Generic;
using System.Linq;

namespace Mahjong
{
    public class HonorTile : Tile, IComparable, IComparable<HonorTile>
    {
        public HonorTile(Suit suit, HonorType honorType)
        {
            Suit = suit;
            HonorType = honorType;
        }

        public HonorType HonorType { get; set; }

        public override bool CanMakeSequence()
        {
            return false;
        }

        public override bool IsTerminal()
        {
            return false;
        }

        public override bool IsTerminalOrHonor()
        {
            return true;
        }

        public override bool CanBelongToSameGroup(params Tile[] otherTiles)
        {
            if (otherTiles.Any(t => !(t is HonorTile)))
            {
                return false;
            }
            return otherTiles.Length <= 4 && otherTiles.All(t => Equals(t));
        }

        public static new bool IsGroup(params Tile[] tiles)
        {
            HonorTile[] honorTiles = tiles.OfType<HonorTile>().ToArray();
            if (honorTiles.Length != tiles.Length)
            {
                return Tile.IsGroup(tiles);
            }
            return IsTriplet(honorTiles) || IsQuad(honorTiles);
        }

        public override string ToString()
        {
            return $"{HonorType.Name} {Suit.Name}";
        }

        public override bool Equals(Object obj)
        {
            if ((obj is null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                HonorTile t = (HonorTile)obj;
                return (HonorType == t.HonorType) && (Suit == t.Suit);
            }
        }

        public override int GetHashCode()
        {
            const int baseHash = 26209;
            const int hashFactor = 71809;

            int hash = baseHash;
            hash = (hash * hashFactor) ^ Suit.GetHashCode();
            hash = (hash * hashFactor) ^ HonorType.GetHashCode();
            return hash;
        }

        public int CompareTo(HonorTile other)
        {
            var comparator = Suit.Equals(other.Suit) ? HonorType.CompareTo(other.HonorType) : Suit.CompareTo(other.Suit);
            return comparator;
        }

        public int CompareTo(object obj)
        {
            if ((obj is null) || !GetType().Equals(obj.GetType()))
            {
                return 1;
            }
            else
            {
                HonorTile t = (HonorTile)obj;
                return CompareTo(t);
            }
        }
    }
}

using System;

namespace Mahjong
{
    public class BonusTile : Tile, IComparable, IComparable<BonusTile>
    {
        public BonusTile(Suit suit, int rank, string title)
        {
            Suit = suit;
            Rank = rank;
            Title = title;
        }

        public int Rank { get; set; }

        public string Title { get; set; }

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
            return false;
        }

        public override bool CanBelongToSameGroup(params Tile[] otherTiles)
        {
            return false;
        }

        public override bool IsGroup(params Tile[] tiles)
        {
            return false;
        }

        public override bool IsSequence(params Tile[] tiles)
        {
            return false;
        }

        public override bool IsTriplet(params Tile[] tiles)
        {
            return false;
        }

        public override bool IsQuad(params Tile[] tiles)
        {
            return false;
        }

        public override bool IsPair(params Tile[] tiles)
        {
            return false;
        }

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(Object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                BonusTile t = (BonusTile)obj;
                return (Rank == t.Rank) && (Suit == t.Suit);
            }
        }

        public override int GetHashCode()
        {
            const int baseHash = 24281;
            const int hashFactor = 84377;

            int hash = baseHash;
            hash = (hash * hashFactor) ^ Suit.GetHashCode();
            hash = (hash * hashFactor) ^ Rank.GetHashCode();
            return hash;
        }

        public int CompareTo(Tile other)
        {
            if (other.GetType() == typeof(BonusTile))
            {
                BonusTile t = (BonusTile)other;
                return CompareTo(t);
            }
            return 1;
        }

        public int CompareTo(BonusTile other)
        {
            var comparator = Suit.Equals(other.Suit) ? Rank.CompareTo(other.Rank) : Suit.CompareTo(other.Suit);
            return comparator;
        }

        public int CompareTo(object obj)
        {
            if (obj is null ||
                !(obj.GetType().Equals(typeof(Tile)) || typeof(Tile).IsAssignableFrom(obj.GetType())))
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
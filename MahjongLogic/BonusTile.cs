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

        public static new bool IsGroup(params Tile[] tiles)
        {
            return false;
        }

        public static new bool IsTriplet(params Tile[] tiles)
        {
            return false;
        }

        public static new bool IsQuad(params Tile[] tiles)
        {
            return false;
        }

        public static new bool IsPair(params Tile[] tiles)
        {
            return false;
        }

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(Object obj)
        {
            if ((obj is null) || !this.GetType().Equals(obj.GetType()))
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

        public int CompareTo(BonusTile other)
        {
            var comparator = Suit.Equals(other.Suit) ? Rank.CompareTo(other.Rank) : Suit.CompareTo(other.Suit);
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
                BonusTile t = (BonusTile)obj;
                return CompareTo(t);
            }
        }
    }
}

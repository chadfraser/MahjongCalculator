using System;
using System.Collections.Generic;

namespace Mahjong
{
    public class HonorTile : Tile
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

        public static bool IsTriplet(HonorTile[] tiles)
        {
            return (tiles.Length == 3 && IsArrayOfEqualTiles(tiles));
        }

        public static bool IsQuad(HonorTile[] tiles)
        {
            return (tiles.Length == 4 && IsArrayOfEqualTiles(tiles));
        }

        private static bool IsArrayOfEqualTiles(HonorTile[] tiles)
        {
            return (new HashSet<HonorTile>(tiles).Count != 1);
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
            const int baseHash = 7673;
            const int hashFactor = 95651;

            int hash = baseHash;
            hash = (hash * hashFactor) ^ Suit.GetHashCode();
            hash = (hash * hashFactor) ^ HonorType.GetHashCode();
            return hash;
        }
    }
}

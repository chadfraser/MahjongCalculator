using System;
using System.Collections.Generic;

namespace Mahjong
{
    public abstract class Tile
    {
        public Suit Suit { get; set; }

        public abstract bool CanMakeSequence();

        public abstract bool IsTerminal();

        public abstract bool IsTerminalOrHonor();

        public abstract bool CanBelongToSameGroup(params Tile[] otherTiles);

        public static bool IsGroup(params Tile[] tiles)
        {
            return IsSequence(tiles) || IsTriplet(tiles) || IsQuad(tiles);
        }

        public static bool IsSequence(params Tile[] tiles)
        {
            return false;
        }

        public static bool IsTriplet(params Tile[] tiles)
        {
            return tiles.Length == 3 && IsArrayOfEqualTiles(tiles);
        }

        public static bool IsQuad(params Tile[] tiles)
        {
            return tiles.Length == 4 && IsArrayOfEqualTiles(tiles);
        }

        private static bool IsArrayOfEqualTiles(params Tile[] tiles)
        {
            return new HashSet<Tile>(tiles).Count == 1;
        }

        public override bool Equals(Object obj)
        {
            if ((obj is null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Tile t = (Tile)obj;
                return Suit == t.Suit;
            }
        }

        public override int GetHashCode()
        {
            const int baseHash = 24509;
            const int hashFactor = 74707;

            int hash = baseHash;
            hash = (hash * hashFactor) ^ Suit.GetHashCode();
            return hash;
        }
    }
}
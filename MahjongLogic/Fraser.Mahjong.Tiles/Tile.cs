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

        public abstract bool IsGroup(params Tile[] tiles);

        public abstract bool IsSequence(params Tile[] tiles);

        public virtual bool IsPair(params Tile[] tiles)
        {
            return tiles.Length == 2 && IsArrayOfEqualTiles(tiles);
        }

        public virtual bool IsTriplet(params Tile[] tiles)
        {
            return tiles.Length == 3 && IsArrayOfEqualTiles(tiles);
        }

        public virtual bool IsQuad(params Tile[] tiles)
        {
            return tiles.Length == 4 && IsArrayOfEqualTiles(tiles);
        }

        public static bool IsGroup<T>(params T[] tiles) where T: Tile
        {
            return tiles[0].IsGroup(tiles);
        }

        public static bool IsSequence<T>(params T[] tiles) where T : Tile
        {
            return tiles[0].IsSequence(tiles);
        }

        public static bool IsTriplet<T>(params T[] tiles) where T : Tile
        {
            return tiles[0].IsTriplet(tiles);
        }

        public static bool IsQuad<T>(params T[] tiles) where T : Tile
        {
            return tiles[0].IsQuad(tiles);
        }

        public static bool IsPair<T>(params T[] tiles) where T : Tile
        {
            return tiles[0].IsPair(tiles);
            //return tiles.Length == 2 && IsArrayOfEqualTiles(tiles);
            //return tiles.Length == 2 && IsArrayOfEqualTiles(tiles);
        }

        protected static bool IsArrayOfEqualTiles(params Tile[] tiles)
        {
            return new HashSet<Tile>(tiles).Count == 1;
        }

        public override bool Equals(Object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
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
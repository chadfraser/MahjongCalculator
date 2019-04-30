using System;
using System.Collections.Generic;

namespace Mahjong
{
    public abstract class Tile
    {
        public Suit Suit { get; set; }

        public abstract bool CanMakeShuntsu();

        public abstract bool IsTerminal();

        public abstract bool IsTerminalOrHonor();

        public static bool IsKoutsu(Tile[] tiles)
        {
            return (tiles.Length == 3 && IsArrayOfEqualTiles(tiles));
        }

        public static bool IsKantsu(Tile[] tiles)
        {
            return (tiles.Length == 4 && IsArrayOfEqualTiles(tiles));
        }

        private static bool IsArrayOfEqualTiles(Tile[] tiles)
        {
            return (new HashSet<Tile>(tiles).Count == 1);
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
                return (Suit == t.Suit);
            }
        }

        public override int GetHashCode()
        {
            const int baseHash = 7673;
            const int hashFactor = 95651;

            int hash = baseHash;
            hash = (hash * hashFactor) ^ Suit.GetHashCode();
            return hash;
        }
    }

    public enum Suit
    {
        Pin,
        Sou,
        Man,
        Wind,
        Dragon
    }

    public enum HonorType
    {
        Haku,
        Hatsu,
        Chun,
        Ton,
        Nan,
        Xia,
        Pei
    }
}
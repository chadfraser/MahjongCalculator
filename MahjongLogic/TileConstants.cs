using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong
{
    public class Suit
    {
        private Suit(string name, string japaneseName)
        {
            Name = name;
            JapaneseName = japaneseName;
        }

        public string Name { get; }
        public string JapaneseName { get; }

        public static readonly Suit Dots = new Suit("Dots", "Pin");
        public static readonly Suit Bamboo = new Suit("Bamboo", "Sou");
        public static readonly Suit Characters = new Suit("Characters", "Man");
        public static readonly Suit Wind = new Suit("Wind", "");
        public static readonly Suit Dragon = new Suit("Dragon", "");
    }

    public class HonorType
    {
        private HonorType(string name, string japaneseName)
        {
            Name = name;
            JapaneseName = japaneseName;
        }

        public string Name { get; }
        public string JapaneseName { get; }

        public static readonly HonorType East = new HonorType("East", "Ton");
        public static readonly HonorType South = new HonorType("South", "Nan");
        public static readonly HonorType West = new HonorType("West", "Xia");
        public static readonly HonorType North = new HonorType("North", "Pei");
        public static readonly HonorType White = new HonorType("White", "Haku");
        public static readonly HonorType Green = new HonorType("Green", "Hatsu");
        public static readonly HonorType Red = new HonorType("Red", "Chun");
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong
{
    public class Suit : IComparable<Suit>
    {
        private Suit(string name, string japaneseName, int sortOrder)
        {
            Name = name;
            JapaneseName = japaneseName;
            SortOrder = sortOrder;
        }

        public string Name { get; }
        public string JapaneseName { get; }
        public int SortOrder { get; }

        public static readonly Suit Dots = new Suit("Dots", "Pin", 0);
        public static readonly Suit Bamboo = new Suit("Bamboo", "Sou", 1);
        public static readonly Suit Characters = new Suit("Characters", "Man", 2);
        public static readonly Suit Wind = new Suit("Wind", "", 3);
        public static readonly Suit Dragon = new Suit("Dragon", "", 4);

        public int CompareTo(Suit other)
        {
            return SortOrder.CompareTo(other.SortOrder);
        }
    }

    public class HonorType: IComparable<HonorType>
    {
        private HonorType(string name, string japaneseName, int sortOrder)
        {
            Name = name;
            JapaneseName = japaneseName;
            SortOrder = sortOrder;
        }

        public string Name { get; }
        public string JapaneseName { get; }
        public int SortOrder { get; }

        public static readonly HonorType East = new HonorType("East", "Ton", 0);
        public static readonly HonorType South = new HonorType("South", "Nan", 1);
        public static readonly HonorType West = new HonorType("West", "Xia", 2);
        public static readonly HonorType North = new HonorType("North", "Pei", 3);
        public static readonly HonorType White = new HonorType("White", "Haku", 4);
        public static readonly HonorType Green = new HonorType("Green", "Hatsu", 5);
        public static readonly HonorType Red = new HonorType("Red", "Chun", 6);

        public int CompareTo(HonorType other)
        {
            return SortOrder.CompareTo(other.SortOrder);
        }
    }
}

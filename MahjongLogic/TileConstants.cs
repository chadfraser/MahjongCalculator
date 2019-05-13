using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fraser.Mahjong
{
    public class Suit : IComparable<Suit>
    {
        private Suit(string name, string japaneseName, string shortName, ConsoleColor color, int sortOrder)
        {
            Name = name;
            JapaneseName = japaneseName;
            ShortName = shortName;
            Color = color;
            SortOrder = sortOrder;
        }

        public string Name { get; }
        public string JapaneseName { get; }
        public string ShortName { get; }
        public ConsoleColor Color { get; }
        public int SortOrder { get; }

        public static readonly Suit Dots = new Suit("Dots", "Pin", "D", ConsoleColor.Blue, 0);
        public static readonly Suit Bamboo = new Suit("Bamboo", "Sou", "B", ConsoleColor.Green, 1);
        public static readonly Suit Characters = new Suit("Characters", "Man", "C", ConsoleColor.Red, 2);
        public static readonly Suit Wind = new Suit("Wind", "", "W", ConsoleColor.White, 3);
        public static readonly Suit Dragon = new Suit("Dragon", "", "", ConsoleColor.Gray, 4);
        public static readonly Suit Flower = new Suit("Flower", "", "F", ConsoleColor.Magenta, 5);
        public static readonly Suit Season = new Suit("Season", "", "S", ConsoleColor.Yellow, 6);

        public static readonly Suit[] allSuits = new Suit[] { Dots, Bamboo, Characters, Wind, Dragon };
        public static readonly Suit[] allSuitsWithBonus = new Suit[] { Dots, Bamboo, Characters, Wind, Dragon,
            Flower, Season };

        public int CompareTo(Suit other)
        {
            return SortOrder.CompareTo(other.SortOrder);
        }
    }

    public class HonorType: IComparable<HonorType>
    {
        private HonorType(string name, string japaneseName, string shortName, ConsoleColor color, int sortOrder)
        {
            Name = name;
            JapaneseName = japaneseName;
            ShortName = shortName;
            Color = color;
            SortOrder = sortOrder;
        }

        public string Name { get; }
        public string JapaneseName { get; }
        public string ShortName { get; }
        public ConsoleColor Color { get; }
        public int SortOrder { get; }

        public static readonly HonorType East = new HonorType("East", "Ton", "E", ConsoleColor.Black, 0);
        public static readonly HonorType South = new HonorType("South", "Nan", "S", ConsoleColor.Black, 1);
        public static readonly HonorType West = new HonorType("West", "Xia", "W", ConsoleColor.Black, 2);
        public static readonly HonorType North = new HonorType("North", "Pei", "N", ConsoleColor.Black, 3);
        public static readonly HonorType White = new HonorType("White", "Haku", "Wh", ConsoleColor.DarkBlue, 4);
        public static readonly HonorType Green = new HonorType("Green", "Hatsu", "Gr", ConsoleColor.DarkGreen, 5);
        public static readonly HonorType Red = new HonorType("Red", "Chun", "Rd", ConsoleColor.DarkRed, 6);

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(HonorType other)
        {
            return SortOrder.CompareTo(other.SortOrder);
        }
    }

    public static class TileInstance
    {
        public static readonly SuitedTile OneOfDots = new SuitedTile(Suit.Dots, 1);
        public static readonly SuitedTile TwoOfDots = new SuitedTile(Suit.Dots, 2);
        public static readonly SuitedTile ThreeOfDots = new SuitedTile(Suit.Dots, 3);
        public static readonly SuitedTile FourOfDots = new SuitedTile(Suit.Dots, 4);
        public static readonly SuitedTile FiveOfDots = new SuitedTile(Suit.Dots, 5);
        public static readonly SuitedTile SixOfDots = new SuitedTile(Suit.Dots, 6);
        public static readonly SuitedTile SevenOfDots = new SuitedTile(Suit.Dots, 7);
        public static readonly SuitedTile EightOfDots = new SuitedTile(Suit.Dots, 8);
        public static readonly SuitedTile NineOfDots = new SuitedTile(Suit.Dots, 9);

        public static readonly SuitedTile OneOfBamboo = new SuitedTile(Suit.Bamboo, 1);
        public static readonly SuitedTile TwoOfBamboo = new SuitedTile(Suit.Bamboo, 2);
        public static readonly SuitedTile ThreeOfBamboo = new SuitedTile(Suit.Bamboo, 3);
        public static readonly SuitedTile FourOfBamboo = new SuitedTile(Suit.Bamboo, 4);
        public static readonly SuitedTile FiveOfBamboo = new SuitedTile(Suit.Bamboo, 5);
        public static readonly SuitedTile SixOfBamboo = new SuitedTile(Suit.Bamboo, 6);
        public static readonly SuitedTile SevenOfBamboo = new SuitedTile(Suit.Bamboo, 7);
        public static readonly SuitedTile EightOfBamboo = new SuitedTile(Suit.Bamboo, 8);
        public static readonly SuitedTile NineOfBamboo = new SuitedTile(Suit.Bamboo, 9);

        public static readonly SuitedTile OneOfCharacters = new SuitedTile(Suit.Characters, 1);
        public static readonly SuitedTile TwoOfCharacters = new SuitedTile(Suit.Characters, 2);
        public static readonly SuitedTile ThreeOfCharacters = new SuitedTile(Suit.Characters, 3);
        public static readonly SuitedTile FourOfCharacters = new SuitedTile(Suit.Characters, 4);
        public static readonly SuitedTile FiveOfCharacters = new SuitedTile(Suit.Characters, 5);
        public static readonly SuitedTile SixOfCharacters = new SuitedTile(Suit.Characters, 6);
        public static readonly SuitedTile SevenOfCharacters = new SuitedTile(Suit.Characters, 7);
        public static readonly SuitedTile EightOfCharacters = new SuitedTile(Suit.Characters, 8);
        public static readonly SuitedTile NineOfCharacters = new SuitedTile(Suit.Characters, 9);

        public static readonly HonorTile EastWind = new HonorTile(Suit.Wind, HonorType.East);
        public static readonly HonorTile SouthWind = new HonorTile(Suit.Wind, HonorType.South);
        public static readonly HonorTile WestWind = new HonorTile(Suit.Wind, HonorType.West);
        public static readonly HonorTile NorthWind = new HonorTile(Suit.Wind, HonorType.North);

        public static readonly HonorTile WhiteDragon = new HonorTile(Suit.Dragon, HonorType.White);
        public static readonly HonorTile GreenDragon = new HonorTile(Suit.Dragon, HonorType.Green);
        public static readonly HonorTile RedDragon = new HonorTile(Suit.Dragon, HonorType.Red);

        public static readonly BonusTile Spring = new BonusTile(Suit.Season, 1, "Spring");
        public static readonly BonusTile Summer = new BonusTile(Suit.Season, 2, "Summer");
        public static readonly BonusTile Autumn = new BonusTile(Suit.Season, 3, "Autumn");
        public static readonly BonusTile Winter = new BonusTile(Suit.Season, 4, "Winter");

        public static readonly BonusTile PlumBlossom = new BonusTile(Suit.Flower, 1, "Plum Blossom");
        public static readonly BonusTile Orchid = new BonusTile(Suit.Flower, 2, "Orchid");
        public static readonly BonusTile Chrysanthemum = new BonusTile(Suit.Flower, 3, "Chrysanthemum");
        public static readonly BonusTile BambooPlant = new BonusTile(Suit.Flower, 4, "Bamboo Plant");

        public static readonly ReadOnlyCollection<Tile> AllDotsTileInstances = new ReadOnlyCollection<Tile>(
            new Tile[] {
                OneOfDots,
                TwoOfDots,
                ThreeOfDots,
                FourOfDots,
                FiveOfDots,
                SixOfDots,
                SevenOfDots,
                EightOfDots,
                NineOfDots
            }
        );

        public static readonly ReadOnlyCollection<Tile> AllBambooTileInstances = new ReadOnlyCollection<Tile>(
            new Tile[] {
                OneOfBamboo,
                TwoOfBamboo,
                ThreeOfBamboo,
                FourOfBamboo,
                FiveOfBamboo,
                SixOfBamboo,
                SevenOfBamboo,
                EightOfBamboo,
                NineOfBamboo
            }
        );

        public static readonly ReadOnlyCollection<Tile> AllCharactersTileInstances = new ReadOnlyCollection<Tile>(
            new Tile[] {
                OneOfCharacters,
                TwoOfCharacters,
                ThreeOfCharacters,
                FourOfCharacters,
                FiveOfCharacters,
                SixOfCharacters,
                SevenOfCharacters,
                EightOfCharacters,
                NineOfCharacters
            }
        );

        public static readonly ReadOnlyCollection<Tile> AllWindTileInstances = new ReadOnlyCollection<Tile>(
            new Tile[] {
                EastWind,
                SouthWind,
                WestWind,
                NorthWind
            }
        );

        public static readonly ReadOnlyCollection<Tile> AllDragonTileInstances = new ReadOnlyCollection<Tile>(
            new Tile[] {
                WhiteDragon,
                GreenDragon,
                RedDragon
            }
        );

        public static readonly ReadOnlyCollection<Tile> AllSeasonTileInstances = new ReadOnlyCollection<Tile>(
            new Tile[] {
                Spring,
                Summer,
                Autumn,
                Winter
            }
        );

        public static readonly ReadOnlyCollection<Tile> AllFlowerTileInstances = new ReadOnlyCollection<Tile>(
            new Tile[] {
                PlumBlossom,
                Orchid,
                Chrysanthemum,
                BambooPlant
            }
        );

        public static void InitializeAllTileArrays()
        {

        }

        public static readonly ReadOnlyCollection<Tile> AllBonusTileInstances = new ReadOnlyCollection<Tile>(
            AllSeasonTileInstances.Concat(AllFlowerTileInstances).ToArray()
        );

        public static readonly ReadOnlyCollection<Tile> AllSuitedTileInstances = new ReadOnlyCollection<Tile>(
            AllDotsTileInstances.Concat(AllBambooTileInstances).Concat(AllCharactersTileInstances).ToArray()
        );

        public static readonly ReadOnlyCollection<Tile> AllHonorTileInstances = new ReadOnlyCollection<Tile>(
            AllWindTileInstances.Concat(AllDragonTileInstances).ToArray()
        );

        public static readonly ReadOnlyCollection<Tile> AllMainTileInstances = new ReadOnlyCollection<Tile>(
            AllSuitedTileInstances.Concat(AllHonorTileInstances).ToArray()
        );

        public static readonly ReadOnlyCollection<Tile> AllMainTileInstancesFourOfEachTile = new ReadOnlyCollection<Tile>(
            AllMainTileInstances.Concat(AllMainTileInstances).Concat(AllMainTileInstances)
                .Concat(AllMainTileInstances).ToArray()
        );

        public static readonly ReadOnlyCollection<Tile> AllMainTileInstancesFourOfEachTilePlusBonusTiles = new
            ReadOnlyCollection<Tile>(AllMainTileInstancesFourOfEachTile.Concat(AllBonusTileInstances).ToArray()
        );

        public static ReadOnlyCollection<Tile> GetAllMainTilesSorted()
        {
            var allMainTiles = AllMainTileInstancesFourOfEachTile.ToList();
            allMainTiles.Sort();
            return new ReadOnlyCollection<Tile>(allMainTiles);
        }
    }
}
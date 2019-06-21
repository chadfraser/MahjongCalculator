using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong
{
    public class HKOSScoringPatternConstants
    {
        public static HKOSScoringPattern WhiteDragonTriplet = new HKOSScoringPattern("White Dragon Triplet", 1,
            "A triplet/quad of the White Dragon.");

        public static HKOSScoringPattern GreenDragonTriplet = new HKOSScoringPattern("Green Dragon Triplet", 1,
            "A triplet/quad of the Green Dragon.");

        public static HKOSScoringPattern RedDragonTriplet = new HKOSScoringPattern("Red Dragon Triplet", 1,
            "A triplet/quad of the Red Dragon.");

        public static HKOSScoringPattern SeatWindTriplet = new HKOSScoringPattern("Seat Wind Triplet", 1,
            "A triplet/quad of your seat wind.");

        public static HKOSScoringPattern RoundWindTriplet = new HKOSScoringPattern("Round Wind Triplet", 1,
            "A triplet/quad of the prevailing round wind.");

        public static HKOSScoringPattern LittleThreeDragons = new HKOSScoringPattern("Little Three Dragons", 2,
            "Two triplets/quads of dragons, and a pair of the third dragon tile.");

        public static HKOSScoringPattern NoBonusTiles = new HKOSScoringPattern("No Bonus Tiles", 1,
            "Having no bonus tiles.");

        public static HKOSScoringPattern SeatFlower = new HKOSScoringPattern("Seat Flower", 1,
            "Having the flower bonus tile corresponding to your seat wind.\n" +
                "\t- East: Plum Blossom (F1)\n" +
                "\t- South: Orchid (F2)\n" +
                "\t- West: Chrysanthemum (F3)\n" +
                "\t- North: Bamboo Plant (F4)");

        public static HKOSScoringPattern SeatSeason = new HKOSScoringPattern("Seat Season", 1,
            "Having the season bonus tile corresponding to your seat wind.\n" +
                "\t- East: Spring (S1)\n" +
                "\t- South: Summer (S2)\n" +
                "\t- West: Autumn (S3)\n" +
                "\t- North: Winter (S4)");

        public static HKOSScoringPattern AllFlowers = new HKOSScoringPattern("All Flowers", 1,
            "Having all four flower bonus tiles.");

        public static HKOSScoringPattern AllSeasons = new HKOSScoringPattern("All Seasons", 1,
            "Having all four season bonus tiles.");

        public static HKOSScoringPattern SixBonusTiles = new HKOSScoringPattern("Six Bonus Tiles", 1,
            "Having exactly six bonus tiles.");

        public static HKOSScoringPattern SevenBonusTiles = new HKOSScoringPattern("Seven Bonus Tiles", 3,
            "Having exactly seven bonus tiles.\n" +
                "\t- When you draw the seventh bonus tile, you can declare an immediate win worth 3 doubles, " +
                "regardless of the tiles in your hand.\n" +
                "\t- Taking this win is an exception to the 'four sets and a pair' rule.");

        public static HKOSScoringPattern AllSequences = new HKOSScoringPattern("All Sequences", 1,
            "Four sequences and one pair.");

        public static HKOSScoringPattern AllTriplets = new HKOSScoringPattern("All Triplets", 3,
            "Four triplets/quads and one pair.");

        public static HKOSScoringPattern SevenPairs = new HKOSScoringPattern("Seven Pairs", 3,
            "Seven distinct pairs in a closed hand.\n" +
                "\t- This is an exception to the 'four sets and a pair' rule.");

        public static HKOSScoringPattern HalfFlush = new HKOSScoringPattern("Half Flush", 3,
            "Having only one suit, winds, dragons, and bonus tiles in your hand.");

        public static HKOSScoringPattern FullFlush = new HKOSScoringPattern("Full Flush", 3,
            "Having only one suit and bonus tiles in your hand.");

        public static HKOSScoringPattern SelfDrawn = new HKOSScoringPattern("Self Drawn", 1,
            "Drawing the winning tile yourself.");

        public static HKOSScoringPattern ConcealedHand = new HKOSScoringPattern("Concealed Hand", 1,
            "Winning without any open sequences, triplets, or quads.");

        public static HKOSScoringPattern LastTileDrawn = new HKOSScoringPattern("Last Tile Drawn", 1,
            "Winning by drawing the last tile in the deal.");

        public static HKOSScoringPattern LastTileDiscarded = new HKOSScoringPattern("Last Tile Discarded", 1,
            "Winning with the last discarded tile, when there are no tiles left to draw.");

        public static HKOSScoringPattern WinOffAReplacementTile = new HKOSScoringPattern("Win Off A Replacement Tile", 1,
            "Winning with the tile drawn to replace a bonus tile or a quad.");

        public static HKOSScoringPattern QuadOnQuad = new HKOSScoringPattern("Quad on Quad", 2,
            "Winning with the replacement tile drawn, after making two or more quads in the same turn.");

        public static HKOSScoringPattern RobAQuad = new HKOSScoringPattern("Rob A Quad", 1,
            "Winning with the tile another player uses to make a promoted quad.");

        public static HKOSScoringPattern FourConcealedTriplets = new HKOSScoringPattern("Four Concealed Triplets", 13,
            "A concealed hand of four triplets/quads.\n" +
                "\t- If you win with a discarded tile, that tile must complete the pair, not a triplet.");

        public static HKOSScoringPattern BigThreeDragons = new HKOSScoringPattern("Big Three Dragons", 13,
            "Three triplets/quads of dragons.");

        public static HKOSScoringPattern LittleFourWinds = new HKOSScoringPattern("Little Four Winds", 13,
            "Three triplets/quads of winds, and a pair of the fourth wind tile.");

        public static HKOSScoringPattern BigFourWinds = new HKOSScoringPattern("Big Four Winds", 13,
            "Four triplets/quads of winds.");

        public static HKOSScoringPattern AllHonors = new HKOSScoringPattern("All Honors", 13,
            "All tiles in your hand are winds, dragons, or bonus tiles.");

        public static HKOSScoringPattern AllTerminals = new HKOSScoringPattern("All Terminals", 13,
            "All tiles in your hand are 1's, 9's, or bonus tiles.");

        public static HKOSScoringPattern NineGates = new HKOSScoringPattern("Nine Gates", 13,
            "A concealed hand containing 1-1-1-2-3-4-5-6-7-8-9-9-9 in the same suit, plus any other tile in that suit.");

        public static HKOSScoringPattern EightBonusTiles = new HKOSScoringPattern("Eight Bonus Tiles", 13,
            "Having all eight bonus tiles.\n" +
                "\t- When you draw the eight bonus tile, you immediately score this pattern as if you had a " +
                "winning hand.\n" +
                "\t- This win is an exception to the 'four sets and a pair' rule.");

        public static HKOSScoringPattern ThirteenOrphans = new HKOSScoringPattern("Thirteen Orphans", 13,
            "Having one of each 1, 9, dragon, and wind, plus any copy of any of these tiles.\n" +
                "\t- This win is an exception to the 'four sets and a pair' rule.");

        public static HKOSScoringPattern PerfectGreen = new HKOSScoringPattern("Perfect Green", 13,
            "Every tile in your hand is one of the following:\n" +
                "\t- 2 of Bamboo\n" +
                "\t- 3 of Bamboo\n" +
                "\t- 4 of Bamboo\n" +
                "\t- 6 of Bamboo\n" +
                "\t- 8 of Bamboo\n" +
                "\t- Green Dragon");

        public static HKOSScoringPattern TheChariot = new HKOSScoringPattern("The Chariot", 13,
            "A concealed hand containing 2-2-3-3-4-4-5-5-6-6-7-7-8-8 of dots.");

        public static HKOSScoringPattern RubyDragon = new HKOSScoringPattern("Ruby Dragon", 13,
            "A triplet/quad of the Red Dragon, three triplets/quads of characters, and a pair of characters.");

        public static HKOSScoringPattern FourQuads = new HKOSScoringPattern("Ruby Dragon", 13,
            "Four quads and one pair.");

        public static HKOSScoringPattern GiftOfHeaven = new HKOSScoringPattern("Gift of Heaven", 13,
            "The dealer's initial draw is a winning hand.");

        public static HKOSScoringPattern GiftOfEarth = new HKOSScoringPattern("Gift of Earth", 13,
            "A non-dealer's first draw is a winning hand.");

        public static HKOSScoringPattern GiftOfMan = new HKOSScoringPattern("Gift of Man", 13,
            "A non-dealer wins with a discard tile before their first turn.");

        public static HKOSScoringPattern EightDealerKeeps = new HKOSScoringPattern("Eight Dealer Keeps", 13,
            "The dealer winning after winning/drawing eight or more times in a row.");

        public static HKOSScoringPattern[] AllBaseScoringPatterns = new HKOSScoringPattern[]
        {
            WhiteDragonTriplet,
            GreenDragonTriplet,
            RedDragonTriplet,
            SeatWindTriplet,
            RoundWindTriplet,
            LittleThreeDragons,
            NoBonusTiles,
            SeatFlower,
            SeatSeason,
            AllFlowers,
            AllSeasons,
            SixBonusTiles,
            SevenBonusTiles,
            AllSequences,
            AllTriplets,
            SevenPairs,
            HalfFlush,
            FullFlush,
            SelfDrawn,
            ConcealedHand,
            LastTileDrawn,
            LastTileDiscarded,
            WinOffAReplacementTile,
            QuadOnQuad,
            RobAQuad
        };

        public static HKOSScoringPattern[] AllLimitScoringPatterns = new HKOSScoringPattern[]
        {
            FourConcealedTriplets,
            BigThreeDragons,
            LittleFourWinds,
            BigFourWinds,
            AllHonors,
            AllTerminals,
            NineGates,
            EightBonusTiles,
            ThirteenOrphans,
            PerfectGreen,
            TheChariot,
            RubyDragon,
            FourQuads,
            GiftOfHeaven,
            GiftOfEarth,
            GiftOfMan,
            EightDealerKeeps
        };

        public static void WriteAllBaseScoringData()
        {
            foreach (var pattern in AllBaseScoringPatterns)
            {
                Console.WriteLine($"{pattern.Name.ToUpper()} -- {pattern.Value} Double{(pattern.Value != 1 ? "s" : "")}");
                Console.WriteLine(pattern.Description);
                Console.WriteLine();
            }
        }

        public static void WriteAllLimitScoringData()
        {
            foreach (var pattern in AllLimitScoringPatterns)
            {
                Console.WriteLine($"{pattern.Name.ToUpper()} -- {pattern.Value} Doubles");
                Console.WriteLine(pattern.Description);
                Console.WriteLine();
            }
        }

        public static void WriteAllScoringData()
        {
            WriteAllBaseScoringData();
            Console.ReadKey();
            Console.WriteLine("\n==============================================\n\n");
            WriteAllLimitScoringData();
            Console.ReadKey();
        }
    }
}

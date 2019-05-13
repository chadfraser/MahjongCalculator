using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fraser.Mahjong
{

    public class HandParser
    {
        public HandParser()
        {
        }

        public Dictionary<char, IReadOnlyCollection<Tile>> mappingDict;

        public void FillDict()
        {
            mappingDict = new Dictionary<char, IReadOnlyCollection<Tile>>
            {
                ['d'] = TileInstance.AllDotsTileInstances,
                ['b'] = TileInstance.AllBambooTileInstances,
                ['c'] = TileInstance.AllCharactersTileInstances,
                ['z'] = TileInstance.AllHonorTileInstances,
                ['f'] = TileInstance.AllFlowerTileInstances,
                ['s'] = TileInstance.AllSeasonTileInstances
            };
        }

        public void GetHandString()
        {
            FillDict();
            var tiles = new List<Tile>();
            var digitsGroupBeforeLetterRegex = new Regex(@"(\d+)([a-zA-z])");

            //var hand = new List<Tile>
            //{
            //    TileInstance.TwoOfDots,
            //    TileInstance.TwoOfDots,
            //    TileInstance.FourOfDots,
            //    TileInstance.FourOfDots,
            //    TileInstance.FourOfDots,
            //    TileInstance.EightOfDots,
            //    TileInstance.ThreeOfBamboo,
            //    TileInstance.SevenOfBamboo,
            //    TileInstance.OneOfCharacters,
            //    TileInstance.TwoOfCharacters,
            //    TileInstance.TwoOfCharacters,
            //    TileInstance.FourOfCharacters,
            //    TileInstance.SevenOfCharacters,
            //    TileInstance.EightOfCharacters
            //};

            foreach (var tile in TileInstance.AllMainTileInstances)
            {
                tile.WriteShortColoredString();
            }
            Console.WriteLine("TEST");

            foreach (var tile in TileInstance.AllBonusTileInstances)
            {
                tile.WriteShortColoredString();
            }
            Console.WriteLine();

            var shantenFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var ukeireFinder = new EfficientDrawsFinder();

            while (true)
            {
                var g = new Game();
                g.CurrentDeal.DealInitialHands();
                foreach (var p in g.Players)
                {
                    p.Hand.SortHand();
                }
                g.CurrentDeal.WriteGameState();
                Console.ReadKey();
            }

            while (true)
            {
                var handString = Console.ReadLine();
                tiles.Clear();
                foreach (Match match in digitsGroupBeforeLetterRegex.Matches(handString))
                {
                    var digitString = match.Groups[1].Captures[0].ToString();
                    char letterChar = match.Groups[2].Captures[0].ToString().ToCharArray()[0];
                    //Console.WriteLine(match.Groups[1].Captures[0]);
                    //Console.WriteLine(match.Groups[2].Captures[0]);
                    foreach (var digit in digitString)
                    {
                        int index = (int)char.GetNumericValue(digit);
                        try
                        {
                            tiles.Add(mappingDict[letterChar].ElementAt(index - 1));
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
                var a = shantenFinder.GetWaitingDistance(tiles);
                var b = ukeireFinder.GetEfficientDrawCount(tiles);
                Console.WriteLine(a);
                Console.WriteLine(b);
                Console.WriteLine();
            }
        }

        //private void AddSpecialTile(IList<Tile> tiles, char passedChar)
        //{
        //    Tile tileToAdd = null;
        //    switch (passedChar)
        //    {
        //        case 'w':
        //            tileToAdd = TileInstance.WhiteDragon;
        //            break;
        //        case 'g':
        //            tileToAdd = TileInstance.GreenDragon;
        //            break;
        //        case 'r':
        //            tileToAdd = TileInstance.RedDragon;
        //            break;
        //        case 'E':
        //            tileToAdd = TileInstance.EastWind;
        //            break;
        //        case 'S':
        //            tileToAdd = TileInstance.SouthWind;
        //            break;
        //        case 'W':
        //            tileToAdd = TileInstance.WestWind;
        //            break;
        //        case 'N':
        //            tileToAdd = TileInstance.NorthWind;
        //            break;
        //        default:
        //            break;
        //    }

        //    if (tileToAdd != null)
        //    {
        //        tiles.Add(tileToAdd);
        //    }
        //}

        public bool ValidateHandString(string handString)
        {
            var handStringWithoutWhiteSpace = Regex.Replace(handString, @"\s", "");
            var twoConsecutiveLettersRegex = new Regex("[a-zA-Z]{2,}", RegexOptions.ECMAScript);
            var unapprovedLettersRegex = new Regex("[^b-dfhszB-DFHSZ1-9]", RegexOptions.ECMAScript);
            var endsWithDigitRegex = new Regex(@"\d$", RegexOptions.ECMAScript);
            var startsWithLetterRegex = new Regex(@"^[a-zA-Z]", RegexOptions.ECMAScript);
            var illegalDigitForHonorTilesRegex = new Regex(@"[89\d*][zZ]", RegexOptions.ECMAScript);
            var illegalDigitForBonusTilesRegex = new Regex(@"[5-9\d*][fFsS]", RegexOptions.ECMAScript);

            if (twoConsecutiveLettersRegex.IsMatch(handStringWithoutWhiteSpace) ||
                unapprovedLettersRegex.IsMatch(handStringWithoutWhiteSpace) ||
                endsWithDigitRegex.IsMatch(handStringWithoutWhiteSpace) ||
                startsWithLetterRegex.IsMatch(handStringWithoutWhiteSpace))
            {
                return false;
            }
            return true;
        }

        //public List<string> ParseHand(string hand)
        //{
        //    var currentSubString = "";
        //    //for (int i = 0; i < hand )
        //}

    }
}
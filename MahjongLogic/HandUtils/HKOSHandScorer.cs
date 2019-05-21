using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class HKOSHandScorer : IHandScorer
    {
        private static Deal currentDeal;
        private static Player winningPlayer;
        private static Player sourceOfWinningTile;
        private static int replacementDrawsBeforeWinning;
        private static bool isRobAQuad;
        public static bool isBonusWin;

        public HKOSHandScorer(HKOSHand hand)
        {
            Hand = hand;
            InitializeDictsOfScoringMethodsWithPoints();
            InitializeDictOfText();
        }

        public HKOSHandScorer() : this(null)
        {
        }

        public HKOSHand Hand { get; set; }

        //private int ReplacementTilesDrawnBeforeWinning { get; set; }

        //private Deal CurrentDeal { get; set; }

        //private Player WinningPlayer { get; set; }

        //private Player PlayerSourceOfWinningTile { get; set; }

        private IDictionary<Func<IList<TileGrouping>, bool>, int> PatternPoints { get; set; }

        private IDictionary<Func<IList<TileGrouping>, bool>, int> LimitHandPatternPoints { get; set; }

        private IDictionary<Func<IList<TileGrouping>, bool>, string> PatternText { get; set; }

        protected static readonly IDictionary<HonorType, int> SeatWindRanks = new Dictionary<HonorType, int>
        {
            [HonorType.East] = 1,
            [HonorType.South] = 2,
            [HonorType.West] = 3,
            [HonorType.North] = 4,
        };

        private void InitializeDictsOfScoringMethodsWithPoints()
        {
            PatternPoints = new Dictionary<Func<IList<TileGrouping>, bool>, int>
            {
                [x => ScoresWhiteDragonTriplet(x)] = 1,
                [x => ScoresGreenDragonTriplet(x)] = 1,
                [x => ScoresRedDragonTriplet(x)] = 1,
                [x => ScoresSeatWindTriplet(x)] = 1,
                [x => ScoresRoundWindTriplet(x)] = 1,
                [x => ScoresLittleThreeDragons(x)] = 2,
                [x => ScoresHalfFlush(x)] = 3,
                [x => ScoresFullFlush(x)] = 6,
                [x => ScoresAllSequences(x)] = 1,
                [x => ScoresAllTriplets(x)] = 3,
                [x => ScoresSevenPairs(x)] = 3,
                [x => ScoresNoBonusTiles(x)] = 1,
                [x => ScoresOwnFlower(x)] = 1,
                [x => ScoresOwnSeason(x)] = 1,
                [x => ScoresAllFlowers(x)] = 1,
                [x => ScoresAllSeasons(x)] = 1,
                [x => ScoresSixBonusTiles(x)] = 1,
                [x => ScoresSevenBonusTiles(x)] = 3,
                [x => ScoresConcealedHand()] = 1,
                [x => ScoresSelfDrawn()] = 1,
                [x => ScoresLastTileDrawn()] = 1,
                [x => ScoresLastTileDiscarded()] = 1,
                [x => ScoresWinOffAReplacementTile()] = 1,
                [x => ScoresQuadOnQuad()] = 2,
                [x => ScoresRobAQuad()] = 1
            };

            LimitHandPatternPoints = new Dictionary<Func<IList<TileGrouping>, bool>, int>
            {
                [x => ScoresFourConcealedTriplets(x)] = 13,
                [x => ScoresBigThreeDragons(x)] = 13,
                [x => ScoresLittleFourWinds(x)] = 13,
                [x => ScoresBigFourWinds(x)] = 13,
                [x => ScoresAllHonors(x)] = 13,
                [x => ScoresAllTerminals(x)] = 13,
                [x => ScoresNineGates(x)] = 13,
                [x => ScoresEightBonusTiles(x)] = 13,
                [x => ScoresThirteenOrphans(x)] = 13,
                [x => ScoresPerfectGreen(x)] = 13,
                [x => ScoresTheChariot(x)] = 13,
                [x => ScoresRubyDragon(x)] = 13,
                [x => ScoresFourQuads(x)] = 13,
                [x => ScoresGiftOfHeaven()] = 13,
                [x => ScoresGiftOfEarth()] = 13,
                [x => ScoresGiftOfMan()] = 13,
                [x => ScoresEightDealerKeeps()] = 13
            };
        }

        private void InitializeDictOfText()
        {
            PatternText = new Dictionary<Func<IList<TileGrouping>, bool>, string>
            {
                [x => ScoresWhiteDragonTriplet(x)] = "White Dragon",
                [x => ScoresGreenDragonTriplet(x)] = "Green Dragon",
                [x => ScoresRedDragonTriplet(x)] = "Red Dragon",
                [x => ScoresSeatWindTriplet(x)] = "Seat Wind",
                [x => ScoresRoundWindTriplet(x)] = "Round Wind",
                [x => ScoresLittleThreeDragons(x)] = "Little Three Dragons",
                [x => ScoresHalfFlush(x)] = "Half Flush",
                [x => ScoresFullFlush(x)] = "Full Flush",
                [x => ScoresAllSequences(x)] = "All Sequences",
                [x => ScoresAllTriplets(x)] = "All Triplets",
                [x => ScoresSevenPairs(x)] = "Seven Pairs",
                [x => ScoresNoBonusTiles(x)] = "No Bonus Tiles",
                [x => ScoresOwnFlower(x)] = "Seat Flower",
                [x => ScoresOwnSeason(x)] = "Seat Season",
                [x => ScoresAllFlowers(x)] = "All Flowers",
                [x => ScoresAllSeasons(x)] = "All Seasons",
                [x => ScoresSixBonusTiles(x)] = "Six Bonus Tiles",
                [x => ScoresSevenBonusTiles(x)] = "Seven Bonus Tiles",
                [x => ScoresConcealedHand()] = "Concealed Hand",
                [x => ScoresSelfDrawn()] = "Self Drawn",
                [x => ScoresLastTileDrawn()] = "Last Tile Drawn",
                [x => ScoresLastTileDiscarded()] = "Last Tile Discarded",
                [x => ScoresWinOffAReplacementTile()] = "Win Off a Replacement Tile",
                [x => ScoresQuadOnQuad()] = "Quad-On-Quad",
                [x => ScoresRobAQuad()] = "Rob a Quad",
                [x => ScoresFourConcealedTriplets(x)] = "Four Concealed Triplets",
                [x => ScoresBigThreeDragons(x)] = "Big Three Dragons",
                [x => ScoresLittleFourWinds(x)] = "Little Four Winds",
                [x => ScoresBigFourWinds(x)] = "Big Four Winds",
                [x => ScoresAllHonors(x)] = "All Honors",
                [x => ScoresAllTerminals(x)] = "All Terminals",
                [x => ScoresNineGates(x)] = "Nine Gates",
                [x => ScoresEightBonusTiles(x)] = "Eight Bonus Tiles",
                [x => ScoresThirteenOrphans(x)] = "Thirteen Orphans",
                [x => ScoresPerfectGreen(x)] = "Perfect Green",
                [x => ScoresTheChariot(x)] = "The Chariot",
                [x => ScoresRubyDragon(x)] = "Ruby Dragon",
                [x => ScoresFourQuads(x)] = "Four Quads",
                [x => ScoresGiftOfHeaven()] = "Gift of Heaven",
                [x => ScoresGiftOfEarth()] = "Gift of Earth",
                [x => ScoresGiftOfMan()] = "Gift of Man",
                [x => ScoresEightDealerKeeps()] = "Eight Dealer Keeps"
            };
        }

        public void SetCircumstantialValues(Player winningPlayer, Player sourceOfWinningTile, Deal deal,
            int replacementTilesDrawn)
        {
            HKOSHandScorer.winningPlayer = winningPlayer;
            HKOSHandScorer.sourceOfWinningTile = sourceOfWinningTile;
            HKOSHandScorer.currentDeal = deal;
            HKOSHandScorer.replacementDrawsBeforeWinning = replacementTilesDrawn;
        }

        public void ClearCircumstantialValues()
        {
            winningPlayer = null;
            sourceOfWinningTile = null;
            currentDeal = null;
            replacementDrawsBeforeWinning = 0;
            isRobAQuad = false;
            isBonusWin = false;
        }

        public void WriteHandScoringPatterns()
        {
            var tileGroups = Hand.FindMostValuableWayToParseWinningHand();
            if (isBonusWin)
            {
                if (ScoresSevenBonusTiles(tileGroups))
                {
                    Console.WriteLine("Seven Bonus Tiles - Aborted Hand");
                }
                else if (ScoresEightBonusTiles(tileGroups))
                {
                    Console.WriteLine("Eight Bonus Tiles");
                }
                return;
            }
            foreach (var patternFunction in PatternText)
            {
                if (patternFunction.Key(tileGroups))
                {
                    Console.WriteLine(patternFunction.Value);
                }
            }
        }

        public int ScoreHand(IList<TileGrouping> tileGroups)
        {
            if (isBonusWin)
            {
                return ScoreHandAsBonusWin(tileGroups);
            }

            var score = 0;
            foreach (var patternFunction in LimitHandPatternPoints)
            {
                if (patternFunction.Key(tileGroups))
                {
                    score += patternFunction.Value;
                }
            }
            if (score > 0)
            {
                return score;
            }

            foreach (var patternFunction in PatternPoints)
            {
                if (patternFunction.Key(tileGroups))
                {
                    score += patternFunction.Value;
                }
            }
            return score;
        }

        private int ScoreHandAsBonusWin(IList<TileGrouping> tileGroups)
        {
            if (ScoresSevenBonusTiles(tileGroups))
            {
                return 3;
            }
            else if (ScoresEightBonusTiles(tileGroups))
            {
                return 13;
            }
            return 0;
        }

        public int ScoreHand()
        {
            return Hand.FindScoreOfMostValuableHand();
        }

        public int GetPointsFromHand()
        {
            return ConvertScoreToPoints(ScoreHand());
        }

        private int ConvertScoreToPoints(int score)
        {
            switch (score)
            {
                case 0:
                    return 0;
                case 1:
                case 2:
                    return 8;
                case 3:
                    return 16;
                case 4:
                case 5:
                    return 24;
                case 6:
                case 7:
                    return 32;
                case 8:
                case 9:
                    return 48;
                case 11:
                case 10:
                    return 64;
                case 12:
                    return 96;
                default:
                    return 128 * (score / 13);
            }
        }

        public bool ScoresWhiteDragonTriplet(IList<TileGrouping> tileGroups)
        {
            return ScoresSpecificTileTripet(tileGroups, TileInstance.WhiteDragon);
        }

        public bool ScoresGreenDragonTriplet(IList<TileGrouping> tileGroups)
        {
            return ScoresSpecificTileTripet(tileGroups, TileInstance.GreenDragon);
        }

        public bool ScoresRedDragonTriplet(IList<TileGrouping> tileGroups)
        {
            return ScoresSpecificTileTripet(tileGroups, TileInstance.RedDragon);
        }

        public bool ScoresSeatWindTriplet(IList<TileGrouping> tileGroups)
        {
            if (winningPlayer is null)
            {
                return false;
            }
            var targetTile = new HonorTile(Suit.Wind, winningPlayer.SeatWind);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }

        public bool ScoresRoundWindTriplet(IList<TileGrouping> tileGroups)
        {
            if (currentDeal is null)
            {
                return false;
            }
            var targetTile = new HonorTile(Suit.Wind, currentDeal.Round.RoundWind);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }

        public bool ScoresLittleThreeDragons(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Dragon, out _, out int triplets, out int quads, out int pairs);
            return triplets + quads == 2 && pairs == 1;
        }

        public bool ScoresHalfFlush(IList<TileGrouping> tileGroups)
        {
            var suitedTileGroups = GetAllGroupsOfSpecificType<SuitedTile>(tileGroups);
            if (suitedTileGroups.Count == 0 ||
                suitedTileGroups.Count + GetCountOfBonusTilesGroups(tileGroups) == tileGroups.Count)
            {
                return false;
            }
            var givenSuit = suitedTileGroups.First().First().Suit;
            return !AnyTilesFitCriteria(suitedTileGroups, t => t.Suit != givenSuit);
        }

        public bool ScoresFullFlush(IList<TileGrouping> tileGroups)
        {
            var suitedTileGroups = GetAllGroupsOfSpecificType<SuitedTile>(tileGroups);
            if (suitedTileGroups.Count + GetCountOfBonusTilesGroups(tileGroups) != tileGroups.Count)
            {
                return false;
            }
            var givenSuit = suitedTileGroups.First().First().Suit;
            return !AnyTilesFitCriteria(suitedTileGroups, t => t.Suit != givenSuit);
        }

        public bool ScoresAllSequences(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out int sequences, out _, out _, out _);
            return sequences == 4;
        }

        public bool ScoresAllTriplets(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out int triplets, out int quads, out _);
            return triplets + quads == 4;
        }

        public bool ScoresSevenPairs(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out _, out int _, out int pairs);
            var uniqueTileSet = new HashSet<Tile>(tileGroups.SelectMany(
                group => group.Where(t => t.GetType() != typeof(BonusTile))));
            return pairs == 7 && uniqueTileSet.Count == 7;
        }

        public bool ScoresNoBonusTiles(IList<TileGrouping> tileGroups)
        {
            return GetCountOfBonusTilesGroups(tileGroups) == 0;
        }

        public bool ScoresOwnFlower(IList<TileGrouping> tileGroups)
        {
            if (winningPlayer is null)
            {
                return false;
            }
            var targetRank = SeatWindRanks[winningPlayer.SeatWind];
            return tileGroups.Any(group => group.IsBonus() &&
                group.First().Suit == Suit.Flower && ((BonusTile)group.First()).Rank == targetRank);
        }

        public bool ScoresOwnSeason(IList<TileGrouping> tileGroups)
        {
            if (winningPlayer is null)
            {
                return false;
            }
            var targetRank = SeatWindRanks[winningPlayer.SeatWind];
            return tileGroups.Any(group => group.IsBonus() &&
                group.First().Suit == Suit.Season && ((BonusTile)group.First()).Rank == targetRank);
        }

        public bool ScoresAllFlowers(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Flower, out _, out _, out _, out int bonus);
            return bonus == 4;
        }

        public bool ScoresAllSeasons(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Season, out _, out _, out _, out int bonus);
            return bonus == 4;
        }

        public bool ScoresSixBonusTiles(IList<TileGrouping> tileGroups)
        {
            return GetCountOfBonusTilesGroups(tileGroups) == 6;
        }

        public bool ScoresSevenBonusTiles(IList<TileGrouping> tileGroups)
        {
            return GetCountOfBonusTilesGroups(tileGroups) == 7;
        }

        public bool ScoresConcealedHand()
        {
            return !Hand.IsOpen;
        }

        public bool ScoresSelfDrawn()
        {
            if (winningPlayer is null || sourceOfWinningTile is null)
            {
                return false;
            }
            return sourceOfWinningTile.Equals(winningPlayer);
        }

        public bool ScoresLastTileDrawn()
        {
            if (winningPlayer is null || sourceOfWinningTile is null || currentDeal is null)
            {
                return false;
            }
            return sourceOfWinningTile.Equals(winningPlayer) && replacementDrawsBeforeWinning == 0 &&
                currentDeal.GetRemainingTilesCount() == 0;
        }

        public bool ScoresLastTileDiscarded()
        {
            if (winningPlayer is null || sourceOfWinningTile is null || currentDeal is null)
            {
                return false;
            }
            return !sourceOfWinningTile.Equals(winningPlayer) && replacementDrawsBeforeWinning == 0 &&
                currentDeal.GetRemainingTilesCount() == 0;
        }

        public bool ScoresWinOffAReplacementTile()
        {
            if (winningPlayer is null || sourceOfWinningTile is null)
            {
                return false;
            }
            return replacementDrawsBeforeWinning == 1;
        }

        public bool ScoresQuadOnQuad()
        {
            if (winningPlayer is null || sourceOfWinningTile is null)
            {
                return false;
            }
            return replacementDrawsBeforeWinning > 1;
        }

        public bool ScoresRobAQuad()
        {
            return isRobAQuad;
        }

        public bool ScoresFourConcealedTriplets(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out int triplets, out int quads, out _);
            return triplets + quads == 4 &&
                !AnyGroupsFitCriteria(tileGroups, group => group.IsOpenGroup && (group.IsTriplet() || group.IsQuad()));
        }

        public bool ScoresBigThreeDragons(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Dragon, out _, out int triplets, out int quads, out _);
            return triplets + quads == 3;
        }

        public bool ScoresLittleFourWinds(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Wind, out _, out int triplets, out int quads, out int pairs);
            return triplets + quads == 3 && pairs == 1;
        }

        public bool ScoresBigFourWinds(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Wind, out _, out int triplets, out int quads, out _);
            return triplets + quads == 4;
        }

        public bool ScoresAllHonors(IList<TileGrouping> tileGroups)
        {
            var tileGroupsWithoutBonusTiles = GetAllGroupsWithoutBonusTiles(tileGroups);
            return !AnyTilesFitCriteria(tileGroupsWithoutBonusTiles, t => t.GetType() != typeof(HonorTile));
        }

        public bool ScoresAllTerminals(IList<TileGrouping> tileGroups)
        {
            var tileGroupsWithoutBonusTiles = GetAllGroupsWithoutBonusTiles(tileGroups);
            return !AnyTilesFitCriteria(tileGroupsWithoutBonusTiles, t => !t.IsTerminal());
        }

        public bool ScoresNineGates(IList<TileGrouping> tileGroups)
        {
            var tileGroupsWithoutBonusTiles = GetAllGroupsWithoutBonusTiles(tileGroups);
            var givenSuit = tileGroups.First().First().Suit;
            if (Hand.IsOpen || Hand.CalledSets.Count > 0 ||
                AnyTilesFitCriteria(tileGroupsWithoutBonusTiles, t => t.Suit != givenSuit))
            {

                return false;
            }

            IList<Tile> listOfAllTiles = tileGroupsWithoutBonusTiles.SelectMany(group => group).ToList();
            var dictOfTileCounts = listOfAllTiles.GroupBy(t => t).ToDictionary(g => g.Key, g => g.Count());
            HashSet<Tile> setOfAllTilesOfGivenSuit;
            Tile firstTileInSuit;
            Tile lastTileInSuit;

            if (givenSuit == Suit.Dots)
            {
                setOfAllTilesOfGivenSuit = TileInstance.AllDotsTileInstances.ToHashSet();
                firstTileInSuit = TileInstance.OneOfDots;
                lastTileInSuit = TileInstance.NineOfDots;
            }
            else if (givenSuit == Suit.Bamboo)
            {
                setOfAllTilesOfGivenSuit = TileInstance.AllBambooTileInstances.ToHashSet();
                firstTileInSuit = TileInstance.OneOfBamboo;
                lastTileInSuit = TileInstance.NineOfBamboo;
            }
            else if (givenSuit == Suit.Characters)
            {
                setOfAllTilesOfGivenSuit = TileInstance.AllCharactersTileInstances.ToHashSet();
                firstTileInSuit = TileInstance.OneOfCharacters;
                lastTileInSuit = TileInstance.NineOfCharacters;
            }
            else
            {
                return false;
            }

            return dictOfTileCounts.Keys.ToHashSet().SetEquals(setOfAllTilesOfGivenSuit) &&
                (dictOfTileCounts[firstTileInSuit] == 3 || dictOfTileCounts[firstTileInSuit] == 4) &&
                (dictOfTileCounts[lastTileInSuit] == 3 || dictOfTileCounts[lastTileInSuit] == 4);
        }

        public bool ScoresEightBonusTiles(IList<TileGrouping> tileGroups)
        {
            return GetCountOfBonusTilesGroups(tileGroups) >= 8;
        }

        public bool ScoresThirteenOrphans(IList<TileGrouping> tileGroups)
        {
            var tileGroupsWithoutBonusTiles = GetAllGroupsWithoutBonusTiles(tileGroups);
            var uniqueTileSet = new HashSet<Tile>(tileGroupsWithoutBonusTiles.SelectMany(group => group));
            
            return !AnyTilesFitCriteria(tileGroupsWithoutBonusTiles, t => !t.IsTerminalOrHonor()) &&
                uniqueTileSet.Count == 13;
        }

        public bool ScoresPerfectGreen(IList<TileGrouping> tileGroups)
        {
            var tileGroupsWithoutBonusTiles = GetAllGroupsWithoutBonusTiles(tileGroups);
            var greenTiles = new Tile[]
            {
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.GreenDragon
            };
            return !AnyTilesFitCriteria(tileGroupsWithoutBonusTiles, t => !greenTiles.Contains(t));
        }

        public bool ScoresTheChariot(IList<TileGrouping> tileGroups)
        {
            var tileGroupsWithoutBonusTiles = GetAllGroupsWithoutBonusTiles(tileGroups);
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Dots, out _, out _, out _, out int pairs);
            var uniqueTileSet = new HashSet<Tile>(tileGroupsWithoutBonusTiles.SelectMany(group => group));
            return pairs == 7 && uniqueTileSet.Count == 7 &&
                !AnyTilesFitCriteria(tileGroups, t => t.IsTerminalOrHonor());
        }

        public bool ScoresRubyDragon(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Characters, out _, out int triplets, out int quads, out int pairs);
            return triplets + quads == 3 && pairs == 1 && ScoresSpecificTileTripet(tileGroups, TileInstance.RedDragon);
        }

        public bool ScoresFourQuads(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out _, out int quads, out _);
            return quads == 4;
        }

        public bool ScoresGiftOfHeaven()
        {
            if (winningPlayer is null || sourceOfWinningTile is null || currentDeal is null)
            {
                return false;
            }
            return sourceOfWinningTile.Equals(winningPlayer) &&
                currentDeal.DiscardedTiles.Count() == 0 &&
                currentDeal.Round.GetPlayers().All(x => x.Hand.CalledSets.SequenceEqual(new List<TileGrouping>()));
        }

        public bool ScoresGiftOfEarth()
        {
            if (winningPlayer is null || sourceOfWinningTile is null || currentDeal is null)
            {
                return false;
            }
            return sourceOfWinningTile.Equals(winningPlayer) &&
                currentDeal.DiscardedTiles.Count() != 0 &&
                currentDeal.DiscardedTilesPerPlayer[winningPlayer].Count() == 0 &&
                currentDeal.Round.GetPlayers().All(x => x.Hand.CalledSets.SequenceEqual(new List<TileGrouping>()));
        }

        public bool ScoresGiftOfMan()
        {
            if (winningPlayer is null || sourceOfWinningTile is null || currentDeal is null)
            {
                return false;
            }
            return !sourceOfWinningTile.Equals(winningPlayer) &&
                currentDeal.DiscardedTilesPerPlayer[winningPlayer].Count() == 0 &&
                currentDeal.Round.GetPlayers().All(x => x.Hand.CalledSets.SequenceEqual(new List<TileGrouping>()));
        }

        public bool ScoresEightDealerKeeps()
        {
            if (currentDeal is null)
            {
                return false;
            }
            return currentDeal.Round.DealerKeepCount >= 8 && winningPlayer.SeatWind == HonorType.East;
        }

        public bool AnyTilesFitCriteria(IList<TileGrouping> tileGroups, Func<Tile, bool> criteria)
        {
            foreach (var group in tileGroups)
            {
                if (group.Any(criteria))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AnyGroupsFitCriteria(IList<TileGrouping> tileGroups, Func<TileGrouping, bool> criteria)
        {
            if (tileGroups.Any(criteria))
            {
                return true;
            }
            return false;
        }

        protected bool ScoresSpecificTileTripet(IList<TileGrouping> tileGroups, Tile specificTile)
        {
            return tileGroups.Any(group => (group.IsTriplet() || group.IsQuad()) &&
                group.All(tile => tile == specificTile));
        }

        protected int GetCountOfBonusTilesGroups(IList<TileGrouping> tileGroups)
        {
            return GetAllGroupsOfSpecificType<BonusTile>(tileGroups).Count();
        }

        protected IList<TileGrouping> GetAllGroupsWithoutBonusTiles(IList<TileGrouping> tileGroups)
        {
            var bonusTiles = GetAllGroupsOfSpecificType<BonusTile>(tileGroups);
            return tileGroups.Except(bonusTiles).ToList();
        }

        protected IList<TileGrouping> GetAllGroupsOfSpecificType<T>(IList<TileGrouping> tileGroups) where T : Tile
        {
            var typedTileGroups = new List<TileGrouping>();
            foreach (var group in tileGroups)
            {
                if (group.First().GetType() == typeof(T))
                {
                    typedTileGroups.Add(group);
                }
            }
            return typedTileGroups;
        }

        public void OutputCountOfGroupsOfSuit(IList<TileGrouping> tileGroups, Suit givenSuit, out int sequences,
            out int triplets, out int quads, out int pairs)
        {
            OutputCountOfGroupsOfSuit(tileGroups, new Suit[] { givenSuit }, out sequences, out triplets, out quads,
                out pairs);
        }

        public void OutputCountOfGroupsOfSuit(IList<TileGrouping> tileGroups, Suit givenSuit, out int sequences,
            out int triplets, out int quads, out int pairs, out int bonus)
        {
            OutputCountOfGroupsOfSuit(tileGroups, new Suit[] { givenSuit }, out sequences, out triplets, out quads,
                out pairs, out bonus);
        }

        public void OutputCountOfGroupsOfSuit(IList<TileGrouping> tileGroups, Suit[] givenSuits, out int sequences,
        out int triplets, out int quads, out int pairs)
        {
            OutputCountOfGroupsOfSuit(tileGroups, givenSuits, out sequences, out triplets, out quads,
                out pairs, out _);
        }

        public void OutputCountOfGroupsOfSuit(IList<TileGrouping> tileGroups, Suit[] givenSuits, out int sequences,
        out int triplets, out int quads, out int pairs, out int bonus)
        {
            sequences = triplets = quads = pairs = bonus = 0;

            foreach (var group in tileGroups)
            {
                var firstTile = group.FirstOrDefault();
                if (givenSuits.Contains(firstTile.Suit))
                {
                    if (group.IsSequence())
                    {
                        sequences++;
                    }
                    else if (group.IsTriplet())
                    {
                        triplets++;
                    }
                    else if (group.IsQuad())
                    {
                        quads++;
                    }
                    else if (group.IsPair())
                    {
                        pairs++;
                    }
                    else if (group.First().GetType() == typeof(BonusTile))
                    {
                        bonus++;
                    }
                }
            }
        }
    }
}

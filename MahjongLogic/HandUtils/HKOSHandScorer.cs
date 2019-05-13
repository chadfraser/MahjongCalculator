using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class HKOSHandScorer : IHandScorer
    {
        public HKOSHandScorer(HKOSHand hand)
        {
            Hand = hand;
            InitializeDictOfScoringMethodsWithPoints();
        }

        public HKOSHand Hand { get; set; }

        private IDictionary<Func<IList<TileGrouping>, bool>, int> PatternPoints { get; set; }

        private static readonly IDictionary<HonorType, int> SeatWindRanks = new Dictionary<HonorType, int>
        {
            [HonorType.East] = 1,
            [HonorType.South] = 2,
            [HonorType.West] = 3,
            [HonorType.East] = 4,
        };

        private void InitializeDictOfScoringMethodsWithPoints()
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
                [x => ScoresConcealedHand(x)] = 1,
                // tsumo, (haitei houtei rinshan kanshankan chankan)

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
                [x => ScoresFourQuads(x)] = 13
            };
        }

        public int ScoreHand(IList<TileGrouping> tileGroups)
        {
            var score = 0;
            foreach (var patternFunction in PatternPoints)
            {
                if (patternFunction.Key(tileGroups))
                {
                    Console.WriteLine(">>>");
                    score += patternFunction.Value;
                }
                Console.WriteLine(patternFunction);
            }
            return score;
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
            var targetTile = new HonorTile(Suit.Wind, Hand.SeatWind);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }

        public bool ScoresRoundWindTriplet(IList<TileGrouping> tileGroups)
        {
            var targetTile = new HonorTile(Suit.Wind, Hand.RoundWind);
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
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out int triplets, out _, out _);
            return triplets == 4;
        }

        public bool ScoresSevenPairs(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out _, out int _, out int pairs);
            var uniqueTileSet = new HashSet<Tile>(tileGroups.SelectMany(group => group));
            return pairs == 7 && uniqueTileSet.Count == 7;
        }

        public bool ScoresNoBonusTiles(IList<TileGrouping> tileGroups)
        {
            return GetCountOfBonusTilesGroups(tileGroups) == 0;
        }

        public bool ScoresOwnFlower(IList<TileGrouping> tileGroups)
        {
            var targetRank = SeatWindRanks[Hand.SeatWind];
            return tileGroups.Any(group => group.IsBonus() &&
                group.First().Suit == Suit.Flower && ((BonusTile)group.First()).Rank == targetRank);
        }

        public bool ScoresOwnSeason(IList<TileGrouping> tileGroups)
        {
            var targetRank = SeatWindRanks[Hand.SeatWind];
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

        public bool ScoresConcealedHand(IList<TileGrouping> tileGroups)
        {
            return !Hand.IsOpen;
        }

        public bool ScoresFourConcealedTriplets(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out int triplets, out int _, out _);
            return triplets == 4 && !AnyGroupsFitCriteria(tileGroups, group => group.IsOpenGroup && group.IsTriplet());
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
            return !AnyTilesFitCriteria(tileGroups, t => t.GetType() != typeof(HonorTile));
        }

        public bool ScoresAllTerminals(IList<TileGrouping> tileGroups)
        {
            return !AnyTilesFitCriteria(tileGroups, t => !t.IsTerminal());
        }




        public bool ScoresNineGates(IList<TileGrouping> tileGroups)
        {
            var givenSuit = tileGroups.First().First().Suit;
            if (Hand.IsOpen || AnyTilesFitCriteria(tileGroups, t => t.Suit != givenSuit))
            {
                return false;
            }

            IList<Tile> listOfAllTiles = tileGroups.SelectMany(group => group).ToList();
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
            return GetCountOfBonusTilesGroups(tileGroups) == 8;
        }

        public bool ScoresThirteenOrphans(IList<TileGrouping> tileGroups)
        {
            var uniqueTileSet = new HashSet<Tile>(tileGroups.SelectMany(group => group));
            return !AnyTilesFitCriteria(tileGroups, t => !t.IsTerminalOrHonor()) &&
                tileGroups.Any(t => t.IsPair()) && uniqueTileSet.Count == 13;
        }

        public bool ScoresPerfectGreen(IList<TileGrouping> tileGroups)
        {
            var greenTiles = new Tile[]
            {
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.GreenDragon
            };
            return !AnyTilesFitCriteria(tileGroups, t => !greenTiles.Contains(t));
        }

        public bool ScoresTheChariot(IList<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Dots, out _, out _, out _, out int pairs);
            var uniqueTileSet = new HashSet<Tile>(tileGroups.SelectMany(group => group));
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

        private bool ScoresSpecificTileTripet(IList<TileGrouping> tileGroups, Tile specificTile)
        {
            return tileGroups.Any(group => (group.IsTriplet() || group.IsQuad()) &&
                group.All(tile => tile == specificTile));
        }

        public int GetCountOfBonusTilesGroups(IList<TileGrouping> tileGroups)
        {
            return GetAllGroupsOfSpecificType<BonusTile>(tileGroups).Count();
        }

        public List<TileGrouping> GetAllGroupsOfSpecificType<T>(IList<TileGrouping> tileGroups) where T : Tile
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

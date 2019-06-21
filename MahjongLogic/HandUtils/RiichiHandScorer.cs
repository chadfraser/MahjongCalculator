using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong
{
    class RiichiHandScorer
    {
        /*
         * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    class HKOSHandScorer : IHandScorer
    {
        public Hand Hand { get; set; }

        private Dictionary<Func<List<TileGrouping>, bool>, int> PatternPoints;

        private void InitializeDictOfScoringMethodsWithPoints()
        {
            PatternPoints = new Dictionary<Func<List<TileGrouping>, bool>, int>
            {
                [x => ScoresDragonTriplet(x)] = 1,
                // Seat Wind, Round Wind
                [x => ScoresLittleThreeDragons(x)] = 2,
                [x => ScoresBigThreeDragons(x)] = 13,
                [x => ScoresLittleFourWinds(x)] = 13,
                [x => ScoresBigFourWinds(x)] = 26,
                [x => ScoresNoTerminalsAndHonors(x)] = 2,
                [x => ScoresAllGroupsHaveTerminalsAndHonors(x)] = 2,
                [x => ScoresAllGroupsHaveTerminals(x)] = 2,
                [x => ScoresAllTerminalsAndHonors(x)] = 2,
                [x => ScoresAllTerminals(x)] = 2,
                [x => ScoresAllHonors(x)] = 2,
                [x => ScoresHalfFlush(x)] = 3,
                [x => ScoresFullFlush(x)] = 6,
                [x => ScoresPerfectGreen(x)] = 13,
                [x => ScoresTheChariot(x)] = 13,
                [x => ScoresAllTriplets(x)] = 2,
                [x => ScoresThreeQuads(x)] = 2,
                [x => ScoresFourQuads(x)] = 13,
                [x => ScoresSevenPairs(x)] = 2,
            };
        }

        public bool ScoresWhiteDragonTriplet(List<TileGrouping> tileGroups)
        {
            var targetTile = new HonorTile(Suit.Dragon, HonorType.White);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }
        public bool ScoresGreenDragonTriplet(List<TileGrouping> tileGroups)
        {
            var targetTile = new HonorTile(Suit.Dragon, HonorType.Green);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }

        public bool ScoresRedDragonTriplet(List<TileGrouping> tileGroups)
        {
            var targetTile = new HonorTile(Suit.Dragon, HonorType.Red);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }

        public bool ScoresSeatWindTriplet(List<TileGrouping> tileGroups)
        {
            var targetTile = new HonorTile(Suit.Wind, Hand.SeatWind);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }

        public bool ScoresRoundWindTriplet(List<TileGrouping> tileGroups)
        {
            var targetTile = new HonorTile(Suit.Wind, Hand.RoundWind);
            return ScoresSpecificTileTripet(tileGroups, targetTile);
        }

        private bool ScoresSpecificTileTripet(List<TileGrouping> tileGroups, Tile specificTile)
        {
            return tileGroups.Any(group => (group.IsTriplet() || group.IsQuad()) &&
                group.All(tile => tile == specificTile));
        }

        public bool ScoresLittleThreeDragons(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Dragon, out _, out int triplets, out int quads, out int pairs);
            return (triplets + quads == 2 && pairs == 1);
        }

        //public int GetDragonTripletCount(List<TileGrouping> tileGroups)
        //{
        //    OutputCountOfGroupsOfSuit(tileGroups, Suit.Dragon, out _, out int triplets, out int quads, out _);
        //    return triplets + quads;
        //}

        // Seat Wind Triplet or Round Wind Triplet, Peace, Pure Double Sequence, Twice Pure Double Sequence,
        // Large Straight, Three Colors One Sequence, Three Colors One Pon, Three Concealed Triplets
        // Four Concealed Triplets (Pair Wait), Big Seven Stars, Nine Gates (Pure), 13 Orphans (13-Way)

        public bool ScoresBigThreeDragons(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Dragon, out _, out int triplets, out int quads, out _);
            return (triplets + quads == 3);
        }

        public bool ScoresLittleFourWinds(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Wind, out _, out int triplets, out int quads, out int pairs);
            return (triplets + quads == 3 && pairs == 1);
        }

        public bool ScoresBigFourWinds(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.Wind, out _, out int triplets, out int quads, out _);
            return (triplets + quads == 4);
        }

        public bool ScoresNoTerminalsAndHonors(List<TileGrouping> tileGroups)
        {
            return NoTilesFitProhibition(tileGroups, t => t.IsTerminalOrHonor());
        }

        public bool ScoresAllGroupsHaveTerminalsAndHonors(List<TileGrouping> tileGroups)
        {
            return NoGroupsFitProhibition(tileGroups, group => !group.Any(t => t.IsTerminalOrHonor()));
        }

        public bool ScoresAllGroupsHaveTerminals(List<TileGrouping> tileGroups)
        {
            return NoGroupsFitProhibition(tileGroups, group => !group.Any(t => t.IsTerminal()));
        }

        public bool ScoresAllTerminalsAndHonors(List<TileGrouping> tileGroups)
        {
            return NoTilesFitProhibition(tileGroups, t => !t.IsTerminalOrHonor()) &&
                !(ScoresAllTerminals(tileGroups) || ScoresAllHonors(tileGroups));
        }

        public bool ScoresAllTerminals(List<TileGrouping> tileGroups)
        {
            return NoTilesFitProhibition(tileGroups, t => !t.IsTerminal());
        }

        public bool ScoresAllHonors(List<TileGrouping> tileGroups)
        {
            return NoTilesFitProhibition(tileGroups, t => t.GetType() != typeof(HonorTile));
        }

        public bool ScoresHalfFlush(List<TileGrouping> tileGroups)
        {
            var suitedTileGroups = new List<TileGrouping>();
            foreach (var group in tileGroups)
            {
                if (group.First().GetType() == typeof(SuitedTile))
                {
                    suitedTileGroups.Add(group);
                }
            }
            if (suitedTileGroups.Count == 0 || suitedTileGroups.Count == tileGroups.Count)
            {
                return false;
            }
            var givenSuit = suitedTileGroups.First().First().Suit;
            return NoTilesFitProhibition(suitedTileGroups, t => t.Suit != givenSuit);
        }

        public bool ScoresFullFlush(List<TileGrouping> tileGroups)
        {
            var suitedTileGroups = new List<TileGrouping>();
            foreach (var group in tileGroups)
            {
                if (group.First().GetType() == typeof(SuitedTile))
                {
                    suitedTileGroups.Add(group);
                }
            }
            if (suitedTileGroups.Count != tileGroups.Count)
            {
                return false;
            }
            var givenSuit = suitedTileGroups.First().First().Suit;
            return NoTilesFitProhibition(suitedTileGroups, t => t.Suit != givenSuit);
        }

        public bool ScoresPerfectGreen(List<TileGrouping> tileGroups)
        {
            var greenTiles = new Tile[] { new SuitedTile(Suit.Bamboo, 2), new SuitedTile(Suit.Bamboo, 3),
                new SuitedTile(Suit.Bamboo, 4), new SuitedTile(Suit.Bamboo, 6), new SuitedTile(Suit.Bamboo, 8),
                new HonorTile(Suit.Dragon, HonorType.Green)};
            return NoTilesFitProhibition(tileGroups, t => !greenTiles.Contains(t));
        }

        public bool ScoresTheChariot(List<TileGrouping> tileGroups)
        {
            var suitOfFirstTile = tileGroups.First().First().Suit;
            return NoGroupsFitProhibition(tileGroups, group => group.IsOpenGroup) &&
                NoTilesFitProhibition(tileGroups, t => t.Suit != suitOfFirstTile || t.IsTerminalOrHonor()) &&
                ScoresSevenPairs(tileGroups);
        }

        public bool ScoresAllTriplets(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out int triplets, out _, out _);
            return (triplets == 4);
        }

        public bool ScoresThreeQuads(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out _, out int quads, out _);
            return (quads == 3);
        }

        public bool ScoresFourQuads(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out _, out int quads, out _);
            return (quads == 4);
        }

        public bool ScoresSevenPairs(List<TileGrouping> tileGroups)
        {
            OutputCountOfGroupsOfSuit(tileGroups, Suit.allSuits, out _, out _, out int _, out int pairs);
            return (pairs == 7);
        }

        public bool NoTilesFitProhibition(List<TileGrouping> tileGroups, Func<Tile, bool> prohibition)
        {
            foreach (var group in tileGroups)
            {
                if (group.Any(prohibition))
                {
                    return false;
                }
            }
            return true;
        }

        public bool NoGroupsFitProhibition(List<TileGrouping> tileGroups, Func<TileGrouping, bool> prohibition)
        {
            if (tileGroups.Any(prohibition))
            {
                return false;
            }
            return true;
        }

        public int GetCountOfBonusTilesGroups(List<TileGrouping> tileGroups)
        {
            return tileGroups.Count(g => g.FirstOrDefault().GetType() == typeof(BonusTile));
        }

        public void OutputCountOfGroupsOfSuit(List<TileGrouping> tileGroups, Suit givenSuit, out int sequences,
            out int triplets, out int quads, out int pairs)
        {
            OutputCountOfGroupsOfSuit(tileGroups, new Suit[] { givenSuit }, out sequences, out triplets, out quads,
                out pairs);
        }

        public void OutputCountOfGroupsOfSuit(List<TileGrouping> tileGroups, Suit[] givenSuits, out int sequences,
        out int triplets, out int quads, out int pairs)
        {
            sequences = triplets = quads = pairs = 0;

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
                }
            }
        }

    }
}
*/
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mahjong;
using System.Collections.Generic;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderUnitTest
    {
        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetThirteenOrphansWaitingDistance_PairlessThirteenOrphans_IsZero()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetPairlessThirteenOrphans();

            var actual = WaitingDistanceFinder.GetThirteenOrphansWaitingDistance(tiles);
            var expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetThirteenOrphansWaitingDistance_CompleteThirteenOrphans_IsNegativeOne()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetCompleteThirteenOrphans();

            var actual = WaitingDistanceFinder.GetThirteenOrphansWaitingDistance(tiles);
            var expected = -1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetThirteenOrphansWaitingDistance_SingleTile_IsMaxInteger()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = new List<Tile> { TileInstance.SouthWind };

            var actual = WaitingDistanceFinder.GetThirteenOrphansWaitingDistance(tiles);
            var expected = int.MaxValue;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetThirteenOrphansWaitingDistance_NoTerminalOrHonorTiles_IsThirteen()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetHandWithNoTerminalOrHonorTiles();

            var actual = WaitingDistanceFinder.GetThirteenOrphansWaitingDistance(tiles);
            var expected = 13;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetThirteenOrphansWaitingDistance_SixTerminalOrHonorTilesTwoPaired_IsSix()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetHandWithSixTerminalOrHonorTilesTwoPaired();

            var actual = WaitingDistanceFinder.GetThirteenOrphansWaitingDistance(tiles);
            var expected = 6;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetSevenPairsWaitingDistance_SixPairsPlusSingleTile_IsZero()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetSixPairsPlusSingleTile();

            var actual = WaitingDistanceFinder.GetSevenPairsWaitingDistance(tiles);
            var expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetSevenPairsWaitingDistance_SevenPairs_IsNegativeOne()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetCompleteSevenPairs();

            var actual = WaitingDistanceFinder.GetSevenPairsWaitingDistance(tiles);
            var expected = -1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetSevenPairsWaitingDistance_SingleTile_IsMaxInteger()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = new List<Tile> { TileInstance.SouthWind };

            var actual = WaitingDistanceFinder.GetSevenPairsWaitingDistance(tiles);
            var expected = int.MaxValue;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetSevenPairsWaitingDistance_NoPairs_IsSix()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetPairlessThirteenOrphans();

            var actual = WaitingDistanceFinder.GetSevenPairsWaitingDistance(tiles);
            var expected = 6;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetSevenPairsWaitingDistance_FourTripletsPlusSingleTile_IsFour()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetFourTripletsPlusSingleTile();

            var actual = WaitingDistanceFinder.GetSevenPairsWaitingDistance(tiles);
            var expected = 4;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetSevenPairsWaitingDistance_FivePairsAndOneQuad_IsOne()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetFivePairsAndOneQuad();

            var actual = WaitingDistanceFinder.GetSevenPairsWaitingDistance(tiles);
            var expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegularHandSevenPairsThirteenOrphansWaitingDistanceFinderGetDrawsAwayFromWaitingNumber_TestHand_IsTwo()
        {
            var WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            var tiles = GetTestHand();

            var actual = WaitingDistanceFinder.GetWaitingDistance(tiles);
            var expected = 2;

            Assert.AreEqual(expected, actual);
        }

        private IList<Tile> GetPairlessThirteenOrphans()
        {
            return new List<Tile> {
                TileInstance.OneOfDots,
                TileInstance.NineOfDots,
                TileInstance.OneOfBamboo,
                TileInstance.NineOfBamboo,
                TileInstance.OneOfCharacters,
                TileInstance.NineOfCharacters,
                TileInstance.EastWind,
                TileInstance.SouthWind,
                TileInstance.WestWind,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.GreenDragon,
                TileInstance.RedDragon
            };
        }

        private IList<Tile> GetCompleteThirteenOrphans()
        {
            return new List<Tile> {
                TileInstance.OneOfDots,
                TileInstance.NineOfDots,
                TileInstance.OneOfBamboo,
                TileInstance.NineOfBamboo,
                TileInstance.OneOfCharacters,
                TileInstance.NineOfCharacters,
                TileInstance.EastWind,
                TileInstance.SouthWind,
                TileInstance.WestWind,
                TileInstance.WestWind,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.GreenDragon,
                TileInstance.RedDragon
            };
        }

        private IList<Tile> GetHandWithNoTerminalOrHonorTiles()
        {
            return new List<Tile> {
                TileInstance.FiveOfBamboo,
                TileInstance.FiveOfBamboo,
                TileInstance.FiveOfBamboo,
                TileInstance.SevenOfDots,
                TileInstance.SevenOfDots,
                TileInstance.SevenOfDots,
                TileInstance.SevenOfDots,
                TileInstance.FourOfCharacters,
                TileInstance.FourOfCharacters,
                TileInstance.FourOfCharacters,
                TileInstance.FiveOfCharacters,
                TileInstance.SixOfCharacters,
                TileInstance.SevenOfCharacters
            };
        }

        private IList<Tile> GetHandWithSixTerminalOrHonorTilesTwoPaired()
        {
            return new List<Tile> {
                TileInstance.FiveOfBamboo,
                TileInstance.FiveOfBamboo,
                TileInstance.FiveOfBamboo,
                TileInstance.NineOfBamboo,
                TileInstance.OneOfCharacters,
                TileInstance.OneOfCharacters,
                TileInstance.NineOfCharacters,
                TileInstance.EastWind,
                TileInstance.SouthWind,
                TileInstance.WestWind,
                TileInstance.WestWind,
                TileInstance.SixOfCharacters,
                TileInstance.SevenOfCharacters
            };
        }

        private List<Tile> GetSixPairsPlusSingleTile()
        {
            // 22 D, 44 B, 88 B, 33 C, 77 C, SS, r
            return new List<Tile> {
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.FourOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.ThreeOfCharacters,
                TileInstance.SevenOfCharacters,
                TileInstance.SevenOfCharacters,
                TileInstance.SouthWind,
                TileInstance.SouthWind,
                TileInstance.RedDragon
            };
        }

        private List<Tile> GetCompleteSevenPairs()
        {
            // 22 D, 44 B, 88 B, 33 C, 77 C, SS, rr
            return new List<Tile> {
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.FourOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.ThreeOfCharacters,
                TileInstance.SevenOfCharacters,
                TileInstance.SevenOfCharacters,
                TileInstance.SouthWind,
                TileInstance.SouthWind,
                TileInstance.RedDragon,
                TileInstance.RedDragon
            };
        }

        private List<Tile> GetFourTripletsPlusSingleTile()
        {
            // 222 D, 888 B, 333 C, SSS, r
            return new List<Tile> {
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.EightOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.ThreeOfCharacters,
                TileInstance.ThreeOfCharacters,
                TileInstance.SouthWind,
                TileInstance.SouthWind,
                TileInstance.SouthWind,
                TileInstance.RedDragon
            };
        }

        private List<Tile> GetFivePairsAndOneQuad()
        {
            // 22 D, 4488 B, 3377 C, SSSS
            return new List<Tile> {
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.FourOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.ThreeOfCharacters,
                TileInstance.SevenOfCharacters,
                TileInstance.SevenOfCharacters,
                TileInstance.SouthWind,
                TileInstance.SouthWind,
                TileInstance.SouthWind,
                TileInstance.SouthWind
            };
        }

        private List<Tile> GetTestHand()
        {
            // 388 D, 1223478 B, 1 C, E, RR
            return new List<Tile> {
                TileInstance.ThreeOfDots,
                TileInstance.EightOfDots,
                TileInstance.EightOfDots,
                TileInstance.OneOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.SevenOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.OneOfCharacters,
                TileInstance.EastWind,
                TileInstance.RedDragon,
                TileInstance.RedDragon
            };
        }
    }
}
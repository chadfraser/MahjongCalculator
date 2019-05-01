using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mahjong;
using System.Collections.Generic;
using System.Linq;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class HandUnitTest
    {
        [TestMethod]
        public void HandSortHandMethodTest_SimpleSuitedTilesEqualsSortedTiles_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetSimpleHandOfSuitedTiles();
            handA.SortHand();
            var result = handA.UncalledTiles.SequenceEqual(GetSortedSimpleHandOfSuitedTiles());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandSortHandMethodTest_AlreadySortedSuitedTilesEqualsSortedTiles_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetSortedSimpleHandOfSuitedTiles();
            handA.SortHand();
            var result = handA.UncalledTiles.SequenceEqual(GetSortedSimpleHandOfSuitedTiles());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandSortHandMethodTest_SimpleHonorTilesEqualsSortedTiles_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetSimpleHandOfHonorTiles();
            handA.SortHand();
            var result = handA.UncalledTiles.SequenceEqual(GetSortedSimpleHandOfHonorTiles());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandSortHandMethodTest_AlreadySortedHonorTilesEqualsSortedTiles_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetSortedSimpleHandOfHonorTiles();
            handA.SortHand();
            var result = handA.UncalledTiles.SequenceEqual(GetSortedSimpleHandOfHonorTiles());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandSortHandMethodTest_SimpleMixedTilesEqualsSortedTiles_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetSimpleHandOfMixedTiles();
            handA.SortHand();
            var result = handA.UncalledTiles.SequenceEqual(GetSortedSimpleHandOfMixedTiles());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandSortHandMethodTest_AlreadySortedMixedTilesEqualsSortedTiles_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetSortedSimpleHandOfMixedTiles();
            handA.SortHand();
            var result = handA.UncalledTiles.SequenceEqual(GetSortedSimpleHandOfMixedTiles());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_ProperThirteenOrphansDataSuitedPair_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
            handA.UncalledTiles.Add(new SuitedTile(Suit.Characters, 1));

            var result = handA.IsWinningHand();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_ProperThirteenOrphansDataHonorPair_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
            handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));

            var result = handA.IsWinningHand();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_TooFewTilesThirteenOrphansData_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetPairlessThirteenOrphansTiles();

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_TooManyPairsThirteenOrphansData_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
            handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));
            handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.White));

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_NonPairThirteenOrphansData_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
            handA.UncalledTiles.Add(new SuitedTile(Suit.Bamboo, 5));

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_CalledSetsProperThirteenOrphansData_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
            handA.UncalledTiles.Add(new SuitedTile(Suit.Bamboo, 1));
            handA.CalledSets.Add(new List<Tile>() {
                new SuitedTile(Suit.Bamboo, 5),
                new SuitedTile(Suit.Bamboo, 5),
                new SuitedTile(Suit.Bamboo, 5)
            });

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_ProperSevenPairsData_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetUnsortedSevenPairsTiles();
            var result = handA.IsWinningHand();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_SevenPairsTooManyPairs_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetUnsortedSevenPairsTiles();
            handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));
            handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_SevenPairsTooFewPairs_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetUnsortedSevenPairsTiles();
            var firstTileData = handA.UncalledTiles[0];
            handA.UncalledTiles.Remove(firstTileData);
            handA.UncalledTiles.Remove(firstTileData);

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_SevenPairsWithQuads_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetUnsortedSevenPairsTilesWithQuads();

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_CalledSetsProperSevenPairsData_IsFalse()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetUnsortedSevenPairsTiles();
            handA.CalledSets.Add(new List<Tile>() {
                new SuitedTile(Suit.Bamboo, 5),
                new SuitedTile(Suit.Bamboo, 5),
                new SuitedTile(Suit.Bamboo, 5)
            });

            var result = handA.IsWinningHand();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandIsWinningHandMethodTest_ProperSuitedTiles_IsTrue()
        {
            var handA = new Hand();
            handA.UncalledTiles = GetSimpleHandOfSuitedTiles();
            handA.CalledSets.Add(new List<Tile>() {
                new SuitedTile(Suit.Bamboo, 5),
                new SuitedTile(Suit.Bamboo, 5),
                new SuitedTile(Suit.Bamboo, 5)
            });

            var result = handA.IsWinningHand();
            Assert.IsTrue(result);
        }

        private List<Tile> GetSimpleHandOfSuitedTiles()
        {
            var tiles = new List<Tile> {
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Dots, 3),
                new SuitedTile(Suit.Dots, 4),
                new SuitedTile(Suit.Dots, 5),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Bamboo, 6),
                new SuitedTile(Suit.Bamboo, 7),
                new SuitedTile(Suit.Bamboo, 8),
                new SuitedTile(Suit.Characters, 3),
                new SuitedTile(Suit.Characters, 4),
                new SuitedTile(Suit.Characters, 5),
                new SuitedTile(Suit.Dots, 6),
                new SuitedTile(Suit.Dots, 6)
            };

            return tiles;
        }

        private List<Tile> GetSortedSimpleHandOfSuitedTiles()
        {
            var tiles = new List<Tile> {
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Dots, 3),
                new SuitedTile(Suit.Dots, 4),
                new SuitedTile(Suit.Dots, 5),
                new SuitedTile(Suit.Dots, 6),
                new SuitedTile(Suit.Dots, 6),
                new SuitedTile(Suit.Bamboo, 6),
                new SuitedTile(Suit.Bamboo, 7),
                new SuitedTile(Suit.Bamboo, 8),
                new SuitedTile(Suit.Characters, 3),
                new SuitedTile(Suit.Characters, 4),
                new SuitedTile(Suit.Characters, 5)
            };

            return tiles;
        }

        private List<Tile> GetSimpleHandOfHonorTiles()
        {
            var tiles = new List<Tile> {
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Dragon, HonorType.Red),
                new HonorTile(Suit.Wind, HonorType.South),
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Wind, HonorType.West),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Wind, HonorType.North),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Wind, HonorType.South),
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Dragon, HonorType.Green),
                new HonorTile(Suit.Dragon, HonorType.Green)
            };

            return tiles;
        }

        private List<Tile> GetSortedSimpleHandOfHonorTiles()
        {
            var tiles = new List<Tile> {
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Wind, HonorType.South),
                new HonorTile(Suit.Wind, HonorType.South),
                new HonorTile(Suit.Wind, HonorType.West),
                new HonorTile(Suit.Wind, HonorType.North),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.Green),
                new HonorTile(Suit.Dragon, HonorType.Green),
                new HonorTile(Suit.Dragon, HonorType.Red)
            };

            return tiles;
        }

        private List<Tile> GetSimpleHandOfMixedTiles()
        {
            var tiles = new List<Tile> {
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Dragon, HonorType.Red),
                new SuitedTile(Suit.Dots, 5),
                new SuitedTile(Suit.Bamboo, 9),
                new HonorTile(Suit.Wind, HonorType.West),
                new HonorTile(Suit.Dragon, HonorType.White),
                new SuitedTile(Suit.Bamboo, 2),
                new HonorTile(Suit.Wind, HonorType.North),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new SuitedTile(Suit.Characters, 1),
                new SuitedTile(Suit.Dots, 6),
                new SuitedTile(Suit.Dots, 6),
                new HonorTile(Suit.Dragon, HonorType.Green)
            };

            return tiles;
        }

        private List<Tile> GetSortedSimpleHandOfMixedTiles()
        {
            var tiles = new List<Tile> {
                new SuitedTile(Suit.Dots, 5),
                new SuitedTile(Suit.Dots, 6),
                new SuitedTile(Suit.Dots, 6),
                new SuitedTile(Suit.Bamboo, 2),
                new SuitedTile(Suit.Bamboo, 9),
                new SuitedTile(Suit.Characters, 1),
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Wind, HonorType.West),
                new HonorTile(Suit.Wind, HonorType.North),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.Green),
                new HonorTile(Suit.Dragon, HonorType.Red)
            };

            return tiles;
        }

        private List<Tile> GetPairlessThirteenOrphansTiles()
        {
            var tiles = new List<Tile> {
                new SuitedTile(Suit.Dots, 1),
                new SuitedTile(Suit.Dots, 9),
                new SuitedTile(Suit.Bamboo, 1),
                new SuitedTile(Suit.Bamboo, 9),
                new SuitedTile(Suit.Characters, 1),
                new SuitedTile(Suit.Characters, 9),
                new HonorTile(Suit.Wind, HonorType.East),
                new HonorTile(Suit.Wind, HonorType.South),
                new HonorTile(Suit.Wind, HonorType.West),
                new HonorTile(Suit.Wind, HonorType.North),
                new HonorTile(Suit.Dragon, HonorType.White),
                new HonorTile(Suit.Dragon, HonorType.Green),
                new HonorTile(Suit.Dragon, HonorType.Red)
            };

            return tiles;
        }

        private List<Tile> GetUnsortedSevenPairsTiles()
        {
            var tiles = new List<Tile> {
                new SuitedTile(Suit.Bamboo, 4),
                new SuitedTile(Suit.Bamboo, 8),
                new SuitedTile(Suit.Dots, 2),
                new HonorTile(Suit.Wind, HonorType.South),
                new HonorTile(Suit.Dragon, HonorType.Red),
                new SuitedTile(Suit.Bamboo, 4),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Characters, 3),
                new SuitedTile(Suit.Characters, 7),
                new SuitedTile(Suit.Characters, 3),
                new SuitedTile(Suit.Bamboo, 8),
                new HonorTile(Suit.Dragon, HonorType.Red),
                new SuitedTile(Suit.Characters, 7),
                new HonorTile(Suit.Wind, HonorType.South)
            };

            return tiles;
        }

        private List<Tile> GetUnsortedSevenPairsTilesWithQuads()
        {
            var tiles = new List<Tile> {
                new SuitedTile(Suit.Bamboo, 4),
                new SuitedTile(Suit.Bamboo, 8),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Bamboo, 8),
                new HonorTile(Suit.Dragon, HonorType.Red),
                new SuitedTile(Suit.Bamboo, 4),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Characters, 3),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Characters, 3),
                new SuitedTile(Suit.Bamboo, 8),
                new HonorTile(Suit.Dragon, HonorType.Red),
                new SuitedTile(Suit.Dots, 2),
                new SuitedTile(Suit.Bamboo, 8),
            };

            return tiles;
        }
    }
}

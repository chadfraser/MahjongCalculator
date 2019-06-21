using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fraser.Mahjong;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class BonusTileUnitTest
    {
        [TestMethod]
        public void BonusTileEqualityTest_SameData_AreEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Spring");

            Assert.AreEqual(tileA, tileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_SameDataTileCast_AreEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Spring");

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_DifferentName_AreEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Summer");

            Assert.AreEqual(tileA, tileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_DifferentNameTileCast_AreEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Summer");

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_DifferentRank_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 2, "Spring");

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_DifferentRankTileCast_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 2, "Spring");

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_DifferentSuit_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Flower, 1, "Spring");

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_DifferentSuitTileCast_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Flower, 1, "Spring");

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_SuitedTile_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new SuitedTile(Suit.Dots, 9);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_SuitedTileTileCast_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new SuitedTile(Suit.Dots, 1);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_HonorTile_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void BonusTileEqualityTest_HonorTileTileCast_AreNotEqual()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new HonorTile(Suit.Dragon, HonorType.Red);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void BonusTileIsTerminalMethodTest_BonusTile_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");

            Assert.IsFalse(tileA.IsTerminal());
        }

        [TestMethod]
        public void BonusTileIsTerminalOrHonorMethodTest_BonusTile_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");

            Assert.IsFalse(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void BonusTileIsSequenceTest_ThreeDistinctTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 2, "Summer");
            var tileC = new BonusTile(Suit.Season, 3, "Autumn");

            Assert.IsFalse(BonusTile.IsSequence(tileA, tileB, tileC));
        }

        [TestMethod]
        public void BonusTileIsTripletTest_ThreeIdenticalTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Spring");
            var tileC = new BonusTile(Suit.Season, 1, "Spring");

            Assert.IsFalse(BonusTile.IsTriplet(tileA, tileB, tileC));
        }

        [TestMethod]
        public void BonusTileIsGroupTest_ThreeDistinctTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 2, "Summer");
            var tileC = new BonusTile(Suit.Season, 3, "Autumn");

            Assert.IsFalse(BonusTile.IsGroup(tileA, tileB, tileC));
        }

        [TestMethod]
        public void BonusTileIsGroupTest_TwoIdenticalTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Spring");

            Assert.IsFalse(BonusTile.IsGroup(tileA, tileB));
        }

        [TestMethod]
        public void BonusTileIsGroupTest_ThreeIdenticalTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Spring");
            var tileC = new BonusTile(Suit.Season, 1, "Spring");

            Assert.IsFalse(BonusTile.IsGroup(tileA, tileB, tileC));
        }

        [TestMethod]
        public void BonusTileCanBelongToSameGroupTest_TwoDistinctTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 2, "Summer");

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB));
        }

        [TestMethod]
        public void BonusTileCanBelongToSameGroupTest_ThreeDistinctTilesOfDifferentSuits_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Flower, 1, "Plum Blossom");

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB));
        }

        [TestMethod]
        public void BonusTileCanBelongToSameGroupTest_ThreeDistinctTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 2, "Summer");
            var tileC = new BonusTile(Suit.Season, 3, "Autumn");

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB, tileC));
        }

        [TestMethod]
        public void BonusTileCanBelongToSameGroupTest_TwoIdenticalTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Spring");

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB));
        }

        [TestMethod]
        public void BonusTileCanBelongToSameGroupTest_ThreeIdenticalTiles_IsFalse()
        {
            var tileA = new BonusTile(Suit.Season, 1, "Spring");
            var tileB = new BonusTile(Suit.Season, 1, "Spring");
            var tileC = new BonusTile(Suit.Season, 1, "Spring");

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB, tileC));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fraser.Mahjong;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class HonorTileUnitTest
    {
        [TestMethod]
        public void HonorTileEqualityTest_SameData_AreEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);

            Assert.AreEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_SameDataTileCast_AreEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentHonorType_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.South);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentHonorTypeTileCast_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.South);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentSuit_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentSuitTileCast_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Red);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentSuitSameHonorType_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Dragon, HonorType.East);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_SuitedTile_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Dragon, HonorType.Red);
            var tileB = new SuitedTile(Suit.Dots, 9);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_SuitedTileTileCast_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Dragon, HonorType.Red);
            var tileB = new SuitedTile(Suit.Dots, 9);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileIsTerminalMethodTest_HonorTile_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(tileA.IsTerminal());
        }

        [TestMethod]
        public void HonorTileIsTerminalOrHonorMethodTest_HonorTile_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void HonorTileIsTripletTest_ProperTripletData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(HonorTile.IsTriplet(tileA, tileB, tileC));
        }

        [TestMethod]
        public void HonorTileIsTripletTest_ProperTripletDataTooFewTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(HonorTile.IsTriplet(tileA, tileB));
        }

        [TestMethod]
        public void HonorTileIsTripletTest_ProperTripletDataTooManyTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(HonorTile.IsTriplet(tileA, tileB, tileC, tileD));
        }

        [TestMethod]
        public void HonorTileIsTripletTest_WrongSuit_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.IsFalse(HonorTile.IsTriplet(tileA, tileB, tileC));
        }

        [TestMethod]
        public void HonorTileIsIsTripletTest_WrongHonorTypes_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.South);

            Assert.IsFalse(HonorTile.IsTriplet(tileA, tileB, tileC));
        }

        [TestMethod]
        public void HonorTileIsQuadTest_ProperQuadData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(HonorTile.IsQuad(tileA, tileB, tileC, tileD));
        }

        [TestMethod]
        public void HonorTileIsQuadTest_ProperQuadDataTooFewTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(HonorTile.IsQuad(tileA, tileB, tileC));
        }

        [TestMethod]
        public void HonorTileIsQuadTest_ProperQuadDataTooManyTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);
            var tileE = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(HonorTile.IsQuad(tileA, tileB, tileC, tileD, tileE));
        }

        [TestMethod]
        public void HonorTileIsQuadTest_WrongSuit_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Dragon, HonorType.Red);
            var tileD = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.IsFalse(HonorTile.IsQuad(tileA, tileB, tileC, tileD));
        }

        [TestMethod]
        public void HonorTileIsQuadTest_WrongHonorTypes_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.South);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(HonorTile.IsQuad(tileA, tileB, tileC, tileD));
        }

        [TestMethod]
        public void HonorTileCanBelongToSameGroup_SameData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(tileA.CanBelongToSameGroup(tileB));
        }

        [TestMethod]
        public void HonorTileCanBelongToSameGroup_DifferentHonorType_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.West);

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB));
        }

        [TestMethod]
        public void HonorTileCanBelongToSameGroup_DifferentSuit_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB));
        }
        [TestMethod]
        public void HonorTileCanBelongToSameGroup_ThreeTilesOfSameData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(tileA.CanBelongToSameGroup(tileB, tileC));
        }

        [TestMethod]
        public void HonorTileCanBelongToSameGroup_FourTilesOfSameData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(tileA.CanBelongToSameGroup(tileB, tileC, tileD));
        }

        [TestMethod]
        public void HonorTileCanBelongToSameGroup_ThreeTilesOfSameDataPlusSuitedTile_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new SuitedTile(Suit.Bamboo, 9);

            Assert.IsFalse(tileA.CanBelongToSameGroup(tileB, tileC, tileD));
        }

        [TestMethod]
        public void HonorTileCanBelongToSameGroup_FiveTilesOfSameData_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);
            var tileE = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(tileA.CanBelongToSameGroup(tileB, tileC, tileD, tileE));
        }
    }
}

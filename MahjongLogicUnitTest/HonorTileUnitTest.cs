using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mahjong;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class HonorTileUnitTest
    {
        [TestMethod]
        public void HonorTileEqualityTest_SameData_AreEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.AreEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_SameDataTileCast_AreEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentHonorType_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Nan);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentHonorTypeTileCast_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Nan);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentSuit_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Chun);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_DifferentSuitTileCast_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Chun);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_HonorTile_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Dragon, HonorType.Chun);
            var tileB = new SuitedTile(Suit.Pin, 9);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_HonorTileTileCast_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Dragon, HonorType.Chun);
            var tileB = new SuitedTile(Suit.Pin, 9);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void HonorTileIsTerminalMethodTest_HonorTile_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsFalse(tileA.IsTerminal());
        }

        [TestMethod]
        public void HonorTileIsTerminalOrHonorMethodTest_HonorTile_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsTrue(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void HonorTileIsKoutsuTest_ProperKoutsuData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsTrue(Tile.IsKoutsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsKoutsuTest_ProperKoutsuDataTooFewTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[] { tileA, tileB }));
        }

        [TestMethod]
        public void HonorTileIsKoutsuTest_ProperKoutsuDataTooManyTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileD = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void HonorTileIsKoutsuTest_WrongSuit_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Dragon, HonorType.Chun);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsIsKoutsuTest_WrongHonorTypes_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Wind, HonorType.Nan);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_ProperKantsuData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileD = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsTrue(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_ProperKantsuDataTooFewTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_ProperKantsuDataTooManyTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileD = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileE = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD, tileE }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_WrongSuit_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Dragon, HonorType.Chun);
            var tileD = new HonorTile(Suit.Dragon, HonorType.Chun);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_WrongRanks_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileB = new HonorTile(Suit.Wind, HonorType.Ton);
            var tileC = new HonorTile(Suit.Wind, HonorType.Nan);
            var tileD = new HonorTile(Suit.Wind, HonorType.Ton);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }
    }
}

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
        public void HonorTileEqualityTest_HonorTile_AreNotEqual()
        {
            var tileA = new HonorTile(Suit.Dragon, HonorType.Red);
            var tileB = new SuitedTile(Suit.Dots, 9);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void HonorTileEqualityTest_HonorTileTileCast_AreNotEqual()
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
        public void HonorTileIsKoutsuTest_ProperKoutsuData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(Tile.IsKoutsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsKoutsuTest_ProperKoutsuDataTooFewTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[] { tileA, tileB }));
        }

        [TestMethod]
        public void HonorTileIsKoutsuTest_ProperKoutsuDataTooManyTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void HonorTileIsKoutsuTest_WrongSuit_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsIsKoutsuTest_WrongHonorTypes_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.South);

            Assert.IsFalse(Tile.IsKoutsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_ProperKantsuData_IsTrue()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsTrue(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_ProperKantsuDataTooFewTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_ProperKantsuDataTooManyTiles_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.East);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);
            var tileE = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD, tileE }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_WrongSuit_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Dragon, HonorType.Red);
            var tileD = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void HonorTileIsKantsuTest_WrongRanks_IsFalse()
        {
            var tileA = new HonorTile(Suit.Wind, HonorType.East);
            var tileB = new HonorTile(Suit.Wind, HonorType.East);
            var tileC = new HonorTile(Suit.Wind, HonorType.South);
            var tileD = new HonorTile(Suit.Wind, HonorType.East);

            Assert.IsFalse(SuitedTile.IsKantsu(new HonorTile[]
                { tileA, tileB, tileC, tileD }));
        }
    }
}

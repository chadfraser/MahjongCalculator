using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mahjong;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class SuitedTileUnitTest
    {
        [TestMethod]
        public void SuitedTileEqualityTest_SameData_AreEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Pin, 9);

            Assert.AreEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_SameDataTileCast_AreEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Pin, 9);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentRank_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Pin, 8);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentRankTileCast_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Pin, 8);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentSuit_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Sou, 9);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentSuitTileCast_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Sou, 9);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_HonorTile_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Chun);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_HonorTileTileCast_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Chun);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileIsTerminalMethodTest_Rank1_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 1);

            Assert.IsTrue(tileA.IsTerminal());
        }

        [TestMethod]
        public void SuitedTileIsTerminalMethodTest_Rank9_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);

            Assert.IsTrue(tileA.IsTerminal());
        }

        [TestMethod]
        public void SuitedTileIsTerminalMethodTest_Rank3_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);

            Assert.IsFalse(tileA.IsTerminal());
        }

        [TestMethod]
        public void SuitedTileIsTerminalOrHonorMethodTest_Rank1_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 1);

            Assert.IsTrue(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void SuitedTileIsTerminalOrHonorMethodTest_Rank9_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);

            Assert.IsTrue(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void SuitedTileIsTerminalOrHonorMethodTest_Rank3_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);

            Assert.IsFalse(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void SuitedTileIsNextInShuntsuTest_SucceedingTiles_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 4);

            Assert.IsTrue(tileA.IsNextInShuntsu(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInShuntsuTest_IdenticalTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 3);

            Assert.IsFalse(tileA.IsNextInShuntsu(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInShuntsuTest_PreceedingTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 2);

            Assert.IsFalse(tileA.IsNextInShuntsu(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInShuntsuTest_SucceedingWrongSuitTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 2);
            var tileB = new SuitedTile(Suit.Sou, 3);

            Assert.IsFalse(tileA.IsNextInShuntsu(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInShuntsuTest_SkipsARank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 2);
            var tileB = new SuitedTile(Suit.Pin, 5);

            Assert.IsFalse(tileA.IsNextInShuntsu(tileB));
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_GreaterRank_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Pin, 8);

            Assert.IsTrue(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_GreaterRankDifferentSuit_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Man, 8);

            Assert.IsTrue(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_SameRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Man, 9);

            Assert.IsFalse(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_LesserRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 9);

            Assert.IsFalse(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_LesserRank_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 2);
            var tileB = new SuitedTile(Suit.Pin, 9);

            Assert.IsTrue(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_LesserRankDifferentSuit_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 2);
            var tileB = new SuitedTile(Suit.Man, 8);

            Assert.IsTrue(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_SameRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 9);
            var tileB = new SuitedTile(Suit.Sou, 9);

            Assert.IsFalse(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_GreaterRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 7);
            var tileB = new SuitedTile(Suit.Pin, 2);

            Assert.IsFalse(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileIsShuntsuTest_ProperShuntsuData_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 4);
            var tileC = new SuitedTile(Suit.Pin, 5);

            Assert.IsTrue(SuitedTile.IsShuntsu(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsShuntsuTest_ProperShuntsuDataTooFewTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 4);

            Assert.IsFalse(SuitedTile.IsShuntsu(new SuitedTile[] { tileA, tileB }));
        }

        [TestMethod]
        public void SuitedTileIsShuntsuTest_ProperShuntsuDataTooManyTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 4);
            var tileC = new SuitedTile(Suit.Pin, 5);
            var tileD = new SuitedTile(Suit.Pin, 6);

            Assert.IsFalse(SuitedTile.IsShuntsu(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsShuntsuTest_WrongSuit_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Sou, 4);
            var tileC = new SuitedTile(Suit.Man, 5);

            Assert.IsFalse(SuitedTile.IsShuntsu(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsShuntsuTest_WrongRanks_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 4);
            var tileC = new SuitedTile(Suit.Pin, 6);

            Assert.IsFalse(SuitedTile.IsShuntsu(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsKoutsuTest_ProperKoutsuData_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 3);
            var tileC = new SuitedTile(Suit.Pin, 3);

            Assert.IsTrue(Tile.IsKoutsu(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsKoutsuTest_ProperKoutsuDataTooFewTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 3);

            Assert.IsFalse(Tile.IsKoutsu(new SuitedTile[] { tileA, tileB }));
        }

        [TestMethod]
        public void SuitedTileIsKoutsuTest_ProperKoutsuDataTooManyTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 3);
            var tileC = new SuitedTile(Suit.Pin, 3);
            var tileD = new SuitedTile(Suit.Pin, 3);

            Assert.IsFalse(Tile.IsKoutsu(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsKoutsuTest_WrongSuit_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Sou, 3);
            var tileC = new SuitedTile(Suit.Man, 3);

            Assert.IsFalse(Tile.IsKoutsu(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsIsKoutsuTest_WrongRanks_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 3);
            var tileC = new SuitedTile(Suit.Pin, 4);

            Assert.IsFalse(Tile.IsKoutsu(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsKantsuTest_ProperKantsuData_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Pin, 4);
            var tileB = new SuitedTile(Suit.Pin, 4);
            var tileC = new SuitedTile(Suit.Pin, 4);
            var tileD = new SuitedTile(Suit.Pin, 4);

            Assert.IsTrue(Tile.IsKantsu(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsKantsuTest_ProperKantsuDataTooFewTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 4);
            var tileB = new SuitedTile(Suit.Pin, 4);
            var tileC = new SuitedTile(Suit.Pin, 4);

            Assert.IsFalse(Tile.IsKantsu(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsKantsuTest_ProperKantsuDataTooManyTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 4);
            var tileB = new SuitedTile(Suit.Pin, 4);
            var tileC = new SuitedTile(Suit.Pin, 4);
            var tileD = new SuitedTile(Suit.Pin, 4);
            var tileE = new SuitedTile(Suit.Pin, 4);

            Assert.IsFalse(Tile.IsKantsu(new SuitedTile[]
                { tileA, tileB, tileC, tileD, tileE }));
        }

        [TestMethod]
        public void SuitedTileIsKantsuTest_WrongSuit_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 4);
            var tileB = new SuitedTile(Suit.Sou, 4);
            var tileC = new SuitedTile(Suit.Man, 4);
            var tileD = new SuitedTile(Suit.Pin, 4);

            Assert.IsFalse(Tile.IsKantsu(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsKantsuTest_WrongRanks_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Pin, 3);
            var tileB = new SuitedTile(Suit.Pin, 4);
            var tileC = new SuitedTile(Suit.Pin, 4);
            var tileD = new SuitedTile(Suit.Pin, 4);

            Assert.IsFalse(Tile.IsKantsu(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }
    }
}

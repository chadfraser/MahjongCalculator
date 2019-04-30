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
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Dots, 9);

            Assert.AreEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_SameDataTileCast_AreEqual()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Dots, 9);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentRank_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Dots, 8);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentRankTileCast_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Dots, 8);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentSuit_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Bamboo, 9);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_DifferentSuitTileCast_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Bamboo, 9);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_HonorTile_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Red);

            Assert.AreNotEqual(tileA, tileB);
        }

        [TestMethod]
        public void SuitedTileEqualityTest_HonorTileTileCast_AreNotEqual()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new HonorTile(Suit.Dragon, HonorType.Red);

            var castedTileA = (Tile)tileA;
            var castedTileB = (Tile)tileB;

            Assert.AreNotEqual(castedTileA, castedTileB);
        }

        [TestMethod]
        public void SuitedTileIsTerminalMethodTest_Rank1_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 1);

            Assert.IsTrue(tileA.IsTerminal());
        }

        [TestMethod]
        public void SuitedTileIsTerminalMethodTest_Rank9_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);

            Assert.IsTrue(tileA.IsTerminal());
        }

        [TestMethod]
        public void SuitedTileIsTerminalMethodTest_Rank3_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);

            Assert.IsFalse(tileA.IsTerminal());
        }

        [TestMethod]
        public void SuitedTileIsTerminalOrHonorMethodTest_Rank1_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 1);

            Assert.IsTrue(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void SuitedTileIsTerminalOrHonorMethodTest_Rank9_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);

            Assert.IsTrue(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void SuitedTileIsTerminalOrHonorMethodTest_Rank3_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);

            Assert.IsFalse(tileA.IsTerminalOrHonor());
        }

        [TestMethod]
        public void SuitedTileIsNextInSequenceTest_SucceedingTiles_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 4);

            Assert.IsTrue(tileA.IsNextInSequence(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInSequenceTest_IdenticalTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 3);

            Assert.IsFalse(tileA.IsNextInSequence(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInSequenceTest_PreceedingTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 2);

            Assert.IsFalse(tileA.IsNextInSequence(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInSequenceTest_SucceedingWrongSuitTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 2);
            var tileB = new SuitedTile(Suit.Bamboo, 3);

            Assert.IsFalse(tileA.IsNextInSequence(tileB));
        }

        [TestMethod]
        public void SuitedTileIsNextInSequenceTest_SkipsARank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 2);
            var tileB = new SuitedTile(Suit.Dots, 5);

            Assert.IsFalse(tileA.IsNextInSequence(tileB));
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_GreaterRank_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Dots, 8);

            Assert.IsTrue(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_GreaterRankDifferentSuit_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Characters, 8);

            Assert.IsTrue(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_SameRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Characters, 9);

            Assert.IsFalse(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileGreaterThanComparisonTest_LesserRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 9);

            Assert.IsFalse(tileA > tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_LesserRank_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 2);
            var tileB = new SuitedTile(Suit.Dots, 9);

            Assert.IsTrue(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_LesserRankDifferentSuit_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 2);
            var tileB = new SuitedTile(Suit.Characters, 8);

            Assert.IsTrue(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_SameRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 9);
            var tileB = new SuitedTile(Suit.Bamboo, 9);

            Assert.IsFalse(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileLesserThanComparisonTest_GreaterRank_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 7);
            var tileB = new SuitedTile(Suit.Dots, 2);

            Assert.IsFalse(tileA < tileB);
        }

        [TestMethod]
        public void SuitedTileIsSequenceTest_ProperSequenceData_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 4);
            var tileC = new SuitedTile(Suit.Dots, 5);

            Assert.IsTrue(SuitedTile.IsSequence(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsSequenceTest_ProperSequenceDataTooFewTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 4);

            Assert.IsFalse(SuitedTile.IsSequence(new SuitedTile[] { tileA, tileB }));
        }

        [TestMethod]
        public void SuitedTileIsSequenceTest_ProperSequenceDataTooCharactersyTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 4);
            var tileC = new SuitedTile(Suit.Dots, 5);
            var tileD = new SuitedTile(Suit.Dots, 6);

            Assert.IsFalse(SuitedTile.IsSequence(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsSequenceTest_WrongSuit_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Bamboo, 4);
            var tileC = new SuitedTile(Suit.Characters, 5);

            Assert.IsFalse(SuitedTile.IsSequence(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsSequenceTest_WrongRanks_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 4);
            var tileC = new SuitedTile(Suit.Dots, 6);

            Assert.IsFalse(SuitedTile.IsSequence(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsTripletTest_ProperTripletData_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 3);
            var tileC = new SuitedTile(Suit.Dots, 3);

            Assert.IsTrue(Tile.IsTriplet(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsTripletTest_ProperTripletDataTooFewTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 3);

            Assert.IsFalse(Tile.IsTriplet(new SuitedTile[] { tileA, tileB }));
        }

        [TestMethod]
        public void SuitedTileIsTripletTest_ProperTripletDataTooCharactersyTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 3);
            var tileC = new SuitedTile(Suit.Dots, 3);
            var tileD = new SuitedTile(Suit.Dots, 3);

            Assert.IsFalse(Tile.IsTriplet(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsTripletTest_WrongSuit_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Bamboo, 3);
            var tileC = new SuitedTile(Suit.Characters, 3);

            Assert.IsFalse(Tile.IsTriplet(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsIsTripletTest_WrongRanks_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 3);
            var tileC = new SuitedTile(Suit.Dots, 4);

            Assert.IsFalse(Tile.IsTriplet(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsQuadTest_ProperQuadData_IsTrue()
        {
            var tileA = new SuitedTile(Suit.Dots, 4);
            var tileB = new SuitedTile(Suit.Dots, 4);
            var tileC = new SuitedTile(Suit.Dots, 4);
            var tileD = new SuitedTile(Suit.Dots, 4);

            Assert.IsTrue(Tile.IsQuad(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsQuadTest_ProperQuadDataTooFewTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 4);
            var tileB = new SuitedTile(Suit.Dots, 4);
            var tileC = new SuitedTile(Suit.Dots, 4);

            Assert.IsFalse(Tile.IsQuad(new SuitedTile[] { tileA, tileB, tileC }));
        }

        [TestMethod]
        public void SuitedTileIsQuadTest_ProperQuadDataTooCharactersyTiles_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 4);
            var tileB = new SuitedTile(Suit.Dots, 4);
            var tileC = new SuitedTile(Suit.Dots, 4);
            var tileD = new SuitedTile(Suit.Dots, 4);
            var tileE = new SuitedTile(Suit.Dots, 4);

            Assert.IsFalse(Tile.IsQuad(new SuitedTile[]
                { tileA, tileB, tileC, tileD, tileE }));
        }

        [TestMethod]
        public void SuitedTileIsQuadTest_WrongSuit_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 4);
            var tileB = new SuitedTile(Suit.Bamboo, 4);
            var tileC = new SuitedTile(Suit.Characters, 4);
            var tileD = new SuitedTile(Suit.Dots, 4);

            Assert.IsFalse(Tile.IsQuad(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }

        [TestMethod]
        public void SuitedTileIsQuadTest_WrongRanks_IsFalse()
        {
            var tileA = new SuitedTile(Suit.Dots, 3);
            var tileB = new SuitedTile(Suit.Dots, 4);
            var tileC = new SuitedTile(Suit.Dots, 4);
            var tileD = new SuitedTile(Suit.Dots, 4);

            Assert.IsFalse(Tile.IsQuad(new SuitedTile[]
                { tileA, tileB, tileC, tileD }));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mahjong;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class TileGroupingUnitTest
    {
        [TestMethod]
        public void TileGroupingEqualityTest_SameSequenceData_AreEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo);
            var tileGroupingB = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo);

            Assert.AreEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_SameSequenceDataDifferentOrder_AreEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo);
            var tileGroupingB = new TileGrouping(
                TileInstance.ThreeOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.FourOfBamboo);

            Assert.AreEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_SamePairData_AreEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo);
            var tileGroupingB = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo);

            Assert.AreEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_SameTripletData_AreEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon);
            var tileGroupingB = new TileGrouping(
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon);

            Assert.AreEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_SameQuadData_AreEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo);
            var tileGroupingB = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo);

            Assert.AreEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_SameBonusTileData_AreEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Spring);
            var tileGroupingB = new TileGrouping(
                TileInstance.Spring);

            Assert.AreEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_EmptyData_AreEqual()
        {
            var tileGroupingA = new TileGrouping();
            var tileGroupingB = new TileGrouping();

            Assert.AreEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_DifferentAmountOfIdenticalTileData_AreNotEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo);
            var tileGroupingB = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo,
                TileInstance.TwoOfBamboo);

            Assert.AreNotEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingEqualityTest_SameAmountOfDifferentTileData_AreNotEqual()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo);
            var tileGroupingB = new TileGrouping(
                TileInstance.TwoOfCharacters,
                TileInstance.ThreeOfCharacters,
                TileInstance.FourOfCharacters);

            Assert.AreNotEqual(tileGroupingA, tileGroupingB);
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_ProperData_IsTrue()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo);

            Assert.IsTrue(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_ProperSequenceDataTooFewTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_ProperSequenceDataTooManyTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.FiveOfBamboo);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_WrongSuit_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.FourOfDots);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_WrongRanks_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FiveOfBamboo);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_HonorTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.WhiteDragon,
                TileInstance.GreenDragon,
                TileInstance.RedDragon);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_TripletOfHonorTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_BonusTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Spring);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_TripletOfBonusTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Spring,
                TileInstance.Spring,
                TileInstance.Spring);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsSequenceTest_ThreeDifferentlyTypedTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.NineOfCharacters,
                TileInstance.GreenDragon,
                TileInstance.Spring);

            Assert.IsFalse(tileGroupingA.IsSequence());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_ProperSuitedTileTripletData_IsTrue()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots);

            Assert.IsTrue(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_ProperHonorTileTripletData_IsTrue()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon);

            Assert.IsTrue(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_ProperTripletDataTooFewTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots);

            Assert.IsFalse(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_ProperTripletDataTooManyTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots);

            Assert.IsFalse(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_WrongSuit_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfBamboo,
                TileInstance.ThreeOfDots);

            Assert.IsFalse(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_WrongRanks_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.ThreeOfDots);

            Assert.IsFalse(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_BonusTileTripletData_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.PlumBlossom,
                TileInstance.PlumBlossom,
                TileInstance.PlumBlossom);

            Assert.IsFalse(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_ThreeDifferentlyTypedTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.NineOfCharacters,
                TileInstance.GreenDragon,
                TileInstance.Spring);

            Assert.IsFalse(tileGroupingA.IsTriplet());
        }

        [TestMethod]
        public void TileGroupingIsQuadTest_ProperSuitedTileQuadData_IsTrue()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo);

            Assert.IsTrue(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsTripletTest_ProperHonorTileQuadData_IsTrue()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.EastWind);

            Assert.IsTrue(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsQuadTest_ProperQuadDataTooFewTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo);

            Assert.IsFalse(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsQuadTest_ProperQuadDataTooManyTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo);

            Assert.IsFalse(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsQuadTest_WrongSuit_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.SixOfBamboo,
                TileInstance.SixOfDots,
                TileInstance.SixOfBamboo,
                TileInstance.SixOfCharacters);

            Assert.IsFalse(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsQuadTest_WrongRanks_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.SixOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.NineOfBamboo,
                TileInstance.SixOfBamboo);

            Assert.IsFalse(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsQuadTest_BonusTileQuadData_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Winter,
                TileInstance.Winter,
                TileInstance.Winter,
                TileInstance.Winter);

            Assert.IsFalse(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsQuadTest_ThreeDifferentlyTypedTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.NineOfCharacters,
                TileInstance.GreenDragon,
                TileInstance.Spring);

            Assert.IsFalse(tileGroupingA.IsQuad());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_BonusTile_IsTrue()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Winter);

            Assert.IsTrue(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_BonusTilePair_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Winter,
                TileInstance.Winter);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_BonusTileTwoUnrelatedBonusTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Winter,
                TileInstance.PlumBlossom);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_BonusTileTwoUnrelatedDifferentlyTypedTiles_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.Winter,
                TileInstance.FourOfBamboo);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_SuitedTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.FourOfBamboo);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_SuitedTilePair_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.FourOfBamboo,
                TileInstance.FourOfBamboo);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_SuitedTileSequence_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.FourOfBamboo,
                TileInstance.FiveOfBamboo,
                TileInstance.SixOfBamboo);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_HonorTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.WestWind);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_HonorTilePair_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.RedDragon,
                TileInstance.RedDragon);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }

        [TestMethod]
        public void TileGroupingIsBonusTest_ThreeDifferentlyTypedTile_IsFalse()
        {
            var tileGroupingA = new TileGrouping(
                TileInstance.NineOfCharacters,
                TileInstance.GreenDragon,
                TileInstance.Spring);

            Assert.IsFalse(tileGroupingA.IsBonus());
        }
    }
}

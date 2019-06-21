//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Fraser.Mahjong;
//using System.Collections.Generic;
//using System.Linq;

//namespace MahjongLogicUnitTest
//{
//    [TestClass]
//    public class HandUnitTest
//    {

/*  Is Winning Hand
 *  Find All Possible Ways to Group Uncalled Tiles
 *  Sort Hand
 */
 

//        //[TestMethod]
//        //public void HandSortHandMethodTest_NonWinningSuitedTilesEqualsSortedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetWinningHandOfSuitedTiles();
//        //    handA.SortHand();
//        //    var result = handA.UncalledTiles.SequenceEqual(GetSortedWinningHandOfSuitedTiles());
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandSortHandMethodTest_AlreadySortedSuitedTilesEqualsSortedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetSortedWinningHandOfSuitedTiles();
//        //    handA.SortHand();
//        //    var result = handA.UncalledTiles.SequenceEqual(GetSortedWinningHandOfSuitedTiles());
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandSortHandMethodTest_NonWinningHonorTilesEqualsSortedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetNonWinningHandOfHonorTiles();
//        //    handA.SortHand();
//        //    var result = handA.UncalledTiles.SequenceEqual(GetSortedNonWinningHandOfHonorTiles());
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandSortHandMethodTest_AlreadySortedHonorTilesEqualsSortedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetSortedNonWinningHandOfHonorTiles();
//        //    handA.SortHand();
//        //    var result = handA.UncalledTiles.SequenceEqual(GetSortedNonWinningHandOfHonorTiles());
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandSortHandMethodTest_NonWinningMixedTilesEqualsSortedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetNonWinningHandOfMixedTiles();
//        //    handA.SortHand();
//        //    var result = handA.UncalledTiles.SequenceEqual(GetSortedNonWinningHandOfMixedTiles());
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandSortHandMethodTest_AlreadySortedMixedTilesEqualsSortedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetSortedNonWinningHandOfMixedTiles();
//        //    handA.SortHand();
//        //    var result = handA.UncalledTiles.SequenceEqual(GetSortedNonWinningHandOfMixedTiles());
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperThirteenOrphansDataSuitedPair_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
//        //    handA.UncalledTiles.Add(new SuitedTile(Suit.Characters, 1));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperThirteenOrphansDataHonorPair_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
//        //    handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_TooFewTilesThirteenOrphansData_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetPairlessThirteenOrphansTiles();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_TooManyPairsThirteenOrphansData_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
//        //    handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));
//        //    handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.White));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_NonPairThirteenOrphansData_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
//        //    handA.UncalledTiles.Add(new SuitedTile(Suit.Bamboo, 5));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_CalledSetsProperThirteenOrphansData_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetPairlessThirteenOrphansTiles();
//        //    handA.UncalledTiles.Add(new SuitedTile(Suit.Bamboo, 1));
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperSevenPairsData_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetUnsortedSevenPairsTiles();
//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_SevenPairsTooManyPairs_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetUnsortedSevenPairsTiles();
//        //    handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));
//        //    handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.Red));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_SevenPairsTooFewPairs_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetUnsortedSevenPairsTiles();
//        //    var firstTileData = handA.UncalledTiles[0];
//        //    handA.UncalledTiles.Remove(firstTileData);
//        //    handA.UncalledTiles.Remove(firstTileData);

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_SevenPairsWithQuads_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetUnsortedSevenPairsTilesWithQuads();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_CalledSetsProperSevenPairsData_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetUnsortedSevenPairsTiles();
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperSuitedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetWinningHandOfSuitedTiles();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperSuitedTilesWithExtraCalledSet_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetWinningHandOfSuitedTiles();
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_OneSetShortSuitedTilesWithExtraCalledSet_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetHandOfSuitedTilesWithTooFewTiles();
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_IncompleteSuitedTiles_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetNonWinningHandOfSuitedTiles();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperHonorTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetWinningHandOfHonorTiles();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperHonorTilesWithTwoExtraCalledSets_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetWinningHandOfHonorTiles();
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_TwoSetsShortHonorTilesWithExtraCalledSet_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetHandOfHonorTilesWithTooFewTiles();
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 4),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 6)
//        //    ));
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new HonorTile(Suit.Dragon, HonorType.Green),
//        //        new HonorTile(Suit.Dragon, HonorType.Green),
//        //        new HonorTile(Suit.Dragon, HonorType.Green),
//        //        new HonorTile(Suit.Dragon, HonorType.Green)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_IncompleteHandOfHonorTiles_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetNonWinningHandOfHonorTiles();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperMixedTiles_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetWinningHandOfMixedTiles();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ProperMixedTilesWithTwoExtraCalledSets_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetWinningHandOfMixedTiles();
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Characters, 7),
//        //        new SuitedTile(Suit.Characters, 8),
//        //        new SuitedTile(Suit.Characters, 9)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_OneSetShortMixedTilesWithExtraCalledSet_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetHandOfMixedTilesWithTooFewTiles();
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 4),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 6)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_IncompleteHandOfMixedTiles_IsFalse()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetNonWinningHandOfMixedTiles();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsFalse(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_PairWithFourExtraCalledSets_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.White));
//        //    handA.UncalledTiles.Add(new HonorTile(Suit.Dragon, HonorType.White));

//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Characters, 7),
//        //        new SuitedTile(Suit.Characters, 8),
//        //        new SuitedTile(Suit.Characters, 9)
//        //    ));
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5),
//        //        new SuitedTile(Suit.Bamboo, 5)
//        //    ));
//        //    handA.CalledSets.Add(new TileGrouping(
//        //        new SuitedTile(Suit.Characters, 7),
//        //        new SuitedTile(Suit.Characters, 8),
//        //        new SuitedTile(Suit.Characters, 9)
//        //    ));

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        //[TestMethod]
//        //public void HandIsWinningHandMethodTest_ComplicatedNineGatesHand_IsTrue()
//        //{
//        //    var handA = new Hand();
//        //    handA.UncalledTiles = GetNineGatesHand();

//        //    var result = handA.IsWinningHand();
//        //    Assert.IsTrue(result);
//        //}

//        [TestMethod]
//        public void HAAAAAAND()
//        {
//            var handA = new Hand();
//            handA.UncalledTiles = GetWinningHandOfMixedTiles();
//            //handA.UncalledTiles = GetNineGatesHand();
//            //handA.UncalledTiles = GetWinningHandOfSuitedTiles();

//            var a = new HonorTile(Suit.Dragon, HonorType.White);
//            var b = new HonorTile(Suit.Dragon, HonorType.White);
//            var c = new HonorTile(Suit.Dragon, HonorType.Green);
//            System.Console.WriteLine(SuitedTile.IsGroup(a, b, c));
//            handA.FindAllPossibleWaysToSplitTiles(handA.UncalledTiles);
//            //Assert.IsTrue(result);
//        }

//        private List<Tile> GetWinningHandOfSuitedTiles()
//        {
//            // 222 D, 345 D, 678 B, 345 C, 66 D
//            return new List<Tile>
//            {
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfDots,
//                TileInstance.FourOfDots,
//                TileInstance.FiveOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.SixOfBamboo,
//                TileInstance.SevenOfBamboo,
//                TileInstance.EightOfBamboo,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.FourOfCharacters,
//                TileInstance.FiveOfCharacters,
//                TileInstance.SixOfDots,
//                TileInstance.SixOfDots,
//            };
//        }

//        private List<Tile> GetNonWinningHandOfSuitedTiles()
//        {
//            // 222 D, 345 D, 678 B, 66 D. Remaining 34 C, 5 D
//            return new List<Tile>
//            {
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfDots,
//                TileInstance.FourOfDots,
//                TileInstance.FiveOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.SixOfBamboo,
//                TileInstance.SevenOfBamboo,
//                TileInstance.EightOfBamboo,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.FourOfCharacters,
//                TileInstance.FiveOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.SixOfDots,
//            };
//        }

//        private List<Tile> GetHandOfSuitedTilesWithTooFewTiles()
//        {
//            // 222 D, 345 D, 345 C, 66 D
//            return new List<Tile>
//            {
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfDots,
//                TileInstance.FourOfDots,
//                TileInstance.FiveOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.FourOfCharacters,
//                TileInstance.FiveOfCharacters,
//                TileInstance.SixOfDots,
//                TileInstance.SixOfDots,
//            };
//        }

//        private List<Tile> GetSortedWinningHandOfSuitedTiles()
//        {
//            // 222 D, 345 D, 678 B, 345 C, 66 D
//            return new List<Tile>
//            {
//                TileInstance.TwoOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfDots,
//                TileInstance.FourOfDots,
//                TileInstance.FiveOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.SixOfBamboo,
//                TileInstance.SevenOfBamboo,
//                TileInstance.EightOfBamboo,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.FourOfCharacters,
//                TileInstance.FiveOfCharacters,
//            };
//        }

//        private List<Tile> GetNonWinningHandOfHonorTiles()
//        {
//            // EEE, SS, W, N, wwww, gg, r
//            return new List<Tile>
//            {
//                TileInstance.EastWind,
//                TileInstance.RedDragon,
//                TileInstance.SouthWind,
//                TileInstance.EastWind,
//                TileInstance.WestWind,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.NorthWind,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.SouthWind,
//                TileInstance.EastWind,
//                TileInstance.GreenDragon,
//                TileInstance.GreenDragon
//            };
//        }

//        private List<Tile> GetWinningHandOfHonorTiles()
//        {
//            // EEE, SSS, NNN, www, gg
//            return new List<Tile>
//            {
//                TileInstance.EastWind,
//                TileInstance.NorthWind,
//                TileInstance.SouthWind,
//                TileInstance.EastWind,
//                TileInstance.NorthWind,
//                TileInstance.SouthWind,
//                TileInstance.WhiteDragon,
//                TileInstance.NorthWind,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.SouthWind,
//                TileInstance.EastWind,
//                TileInstance.GreenDragon,
//                TileInstance.GreenDragon,
//            };
//        }

//        private List<Tile> GetHandOfHonorTilesWithTooFewTiles()
//        {
//            // SSS, WWW, rr
//            return new List<Tile>
//            {
//                TileInstance.WestWind,
//                TileInstance.SouthWind,
//                TileInstance.RedDragon,
//                TileInstance.WestWind,
//                TileInstance.WestWind,
//                TileInstance.SouthWind,
//                TileInstance.SouthWind,
//                TileInstance.RedDragon
//            };
//        }

//        private List<Tile> GetSortedNonWinningHandOfHonorTiles()
//        {
//            // EEE, SS, W, N, wwww, gg, r
//            return new List<Tile>
//            {
//                TileInstance.EastWind,
//                TileInstance.EastWind,
//                TileInstance.EastWind,
//                TileInstance.SouthWind,
//                TileInstance.SouthWind,
//                TileInstance.WestWind,
//                TileInstance.NorthWind,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.GreenDragon,
//                TileInstance.GreenDragon,
//                TileInstance.RedDragon
//            };
//        }

//        private List<Tile> GetNonWinningHandOfMixedTiles()
//        {
//            // 566D, 29B, 1C, EWN, wwwgr
//            return new List<Tile>
//            {
//                TileInstance.EastWind,
//                TileInstance.RedDragon,
//                TileInstance.FiveOfDots,
//                TileInstance.NineOfBamboo,
//                TileInstance.WestWind,
//                TileInstance.WhiteDragon,
//                TileInstance.TwoOfBamboo,
//                TileInstance.NorthWind,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.OneOfCharacters,
//                TileInstance.SixOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.GreenDragon,
//            };
//        }

//        private List<Tile> GetWinningHandOfMixedTiles()
//        {
//            // 567 D, 123 C, WWW, www, rr
//            return new List<Tile>
//            {
//                TileInstance.WestWind,
//                TileInstance.RedDragon,
//                TileInstance.FiveOfDots,
//                TileInstance.TwoOfCharacters,
//                TileInstance.WestWind,
//                TileInstance.WhiteDragon,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.WestWind,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.OneOfCharacters,
//                TileInstance.SevenOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.RedDragon,
//            };
//        }

//        private List<Tile> GetHandOfMixedTilesWithTooFewTiles()
//        {
//            // 567 D, 123 C, WWW, rr
//            return new List<Tile> {
//                TileInstance.WestWind,
//                TileInstance.RedDragon,
//                TileInstance.FiveOfDots,
//                TileInstance.TwoOfCharacters,
//                TileInstance.WestWind,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.WestWind,
//                TileInstance.OneOfCharacters,
//                TileInstance.SevenOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.RedDragon,
//            };
//        }

//        private List<Tile> GetSortedNonWinningHandOfMixedTiles()
//        {
//            // 566 D, 29 B, 1 C, EWN, wwwgr
//            return new List<Tile> {
//                TileInstance.FiveOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.TwoOfBamboo,
//                TileInstance.NineOfBamboo,
//                TileInstance.OneOfCharacters,
//                TileInstance.EastWind,
//                TileInstance.WestWind,
//                TileInstance.NorthWind,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.WhiteDragon,
//                TileInstance.GreenDragon,
//                TileInstance.RedDragon
//            };
//        }

//        private List<Tile> GetPairlessThirteenOrphansTiles()
//        {
//            return new List<Tile> {
//                TileInstance.OneOfDots,
//                TileInstance.NineOfDots,
//                TileInstance.OneOfBamboo,
//                TileInstance.NineOfBamboo,
//                TileInstance.OneOfCharacters,
//                TileInstance.NineOfCharacters,
//                TileInstance.EastWind,
//                TileInstance.SouthWind,
//                TileInstance.WestWind,
//                TileInstance.NorthWind,
//                TileInstance.WhiteDragon,
//                TileInstance.GreenDragon,
//                TileInstance.RedDragon
//            };
//        }

//        private List<Tile> GetUnsortedSevenPairsTiles()
//        {
//            // 22 D, 44 B, 88 B, 33 C, 77 C, SS, rr
//            return new List<Tile> {
//                TileInstance.FourOfBamboo,
//                TileInstance.EightOfBamboo,
//                TileInstance.TwoOfDots,
//                TileInstance.SouthWind,
//                TileInstance.RedDragon,
//                TileInstance.FourOfBamboo,
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.SevenOfCharacters,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.EightOfBamboo,
//                TileInstance.RedDragon,
//                TileInstance.SevenOfCharacters,
//                TileInstance.SouthWind
//            };
//        }

//        private List<Tile> GetUnsortedSevenPairsTilesWithQuads()
//        {
//            // 2222 D, 44 B, 8888 B, 33 C, rr
//            return new List<Tile> {
//                TileInstance.FourOfBamboo,
//                TileInstance.EightOfBamboo,
//                TileInstance.TwoOfDots,
//                TileInstance.EightOfBamboo,
//                TileInstance.RedDragon,
//                TileInstance.FourOfBamboo,
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfCharacters,
//                TileInstance.EightOfBamboo,
//                TileInstance.RedDragon,
//                TileInstance.TwoOfDots,
//                TileInstance.EightOfBamboo
//            };
//        }

//        private List<Tile> GetNineGatesHand()
//        {
//            // 111 D, 234 D, 55 D, 678 D, 999 D
//            return new List<Tile> {
//                TileInstance.OneOfDots,
//                TileInstance.OneOfDots,
//                TileInstance.OneOfDots,
//                TileInstance.TwoOfDots,
//                TileInstance.ThreeOfDots,
//                TileInstance.FourOfDots,
//                TileInstance.FiveOfDots,
//                TileInstance.FiveOfDots,
//                TileInstance.SixOfDots,
//                TileInstance.SevenOfDots,
//                TileInstance.EightOfDots,
//                TileInstance.NineOfDots,
//                TileInstance.NineOfDots,
//                TileInstance.NineOfDots
//            };
//        }
//    }
//}

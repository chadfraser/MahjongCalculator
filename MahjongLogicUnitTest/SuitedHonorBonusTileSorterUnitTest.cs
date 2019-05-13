using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fraser.Mahjong;
using System.Collections.Generic;
using System.Linq;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class SuitedHonorBonusTileSorterUnitTest
    {
        [TestMethod]
        public void SuitedHonorBonusTileSorterSortHandMethodTest_SuitedAndBonusTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorBonusTileSorter();
            var tiles = GetUnsortedHandOfSuitedAndBonusTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfSuitedAndBonusTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterSortHandMethodTest_AlreadySortedSuitedAndBonusTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorBonusTileSorter();
            var tiles = GetSortedHandOfSuitedAndBonusTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfSuitedAndBonusTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterSortHandMethodTest_HonorAndBonusTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorBonusTileSorter();
            var tiles = GetUnsortedHandOfHonorAndBonusTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfHonorAndBonusTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterSortHandMethodTest_AlreadySortedHonorAndBonusTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorBonusTileSorter();
            var tiles = GetSortedHandOfHonorAndBonusTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfHonorAndBonusTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterSortHandMethodTest_MixedAndBonusTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorBonusTileSorter();
            var tiles = GetUnsortedHandOfMixedAndBonusTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfMixedAndBonusTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterSortHandMethodTest_AlreadySortedMixedAndBonusTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorBonusTileSorter();
            var tiles = GetSortedHandOfMixedAndBonusTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfMixedAndBonusTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterEqualityControlTest_UnsortedSuitedAndBonusTilesEqualsSortedTiles_IsFalse()
        {
            var tiles = GetUnsortedHandOfSuitedAndBonusTiles();
            var result = tiles.SequenceEqual(GetSortedHandOfSuitedAndBonusTiles());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterEqualityControlTest_UnsortedHonorAndBonusTilesEqualsSortedTiles_IsFalse()
        {
            var tiles = GetUnsortedHandOfHonorAndBonusTiles();
            var result = tiles.SequenceEqual(GetSortedHandOfHonorAndBonusTiles());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SuitedHonorBonusTileSorterEqualityControlTest_UnsortedMixedAndBonusTilesEqualsSortedTiles_IsFalse()
        {
            var tiles = GetUnsortedHandOfMixedAndBonusTiles();
            var result = tiles.SequenceEqual(GetSortedHandOfMixedAndBonusTiles());

            Assert.IsFalse(result);
        }

        private IList<Tile> GetUnsortedHandOfSuitedAndBonusTiles()
        {
            // 22234566 D, 678 B, 345 C, 3F, 24 S
            return new List<Tile>
            {
                TileInstance.Winter,
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.FiveOfDots,
                TileInstance.TwoOfDots,
                TileInstance.Summer,
                TileInstance.TwoOfDots,
                TileInstance.SixOfBamboo,
                TileInstance.SevenOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.FourOfCharacters,
                TileInstance.Chrysanthemum,
                TileInstance.FiveOfCharacters,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots
            };
        }

        private IList<Tile> GetSortedHandOfSuitedAndBonusTiles()
        {
            // 22234566 D, 678 B, 345 C, 3F, 24 S
            return new List<Tile>
            {
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.FiveOfDots,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots,
                TileInstance.SixOfBamboo,
                TileInstance.SevenOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.FourOfCharacters,
                TileInstance.FiveOfCharacters,
                TileInstance.Chrysanthemum,
                TileInstance.Summer,
                TileInstance.Winter
            };
        }

        private IList<Tile> GetUnsortedHandOfHonorAndBonusTiles()
        {
            // EEESSWN, wwwwggr, 4F, 1S
            return new List<Tile>
            {
                TileInstance.EastWind,
                TileInstance.RedDragon,
                TileInstance.BambooPlant,
                TileInstance.SouthWind,
                TileInstance.EastWind,
                TileInstance.WestWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.NorthWind,
                TileInstance.Spring,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.SouthWind,
                TileInstance.EastWind,
                TileInstance.GreenDragon,
                TileInstance.GreenDragon
            };
        }

        private IList<Tile> GetSortedHandOfHonorAndBonusTiles()
        {
            // EEESSWN, wwwwggr, 4F, 1S
            return new List<Tile>
            {
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.SouthWind,
                TileInstance.SouthWind,
                TileInstance.WestWind,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.GreenDragon,
                TileInstance.GreenDragon,
                TileInstance.RedDragon,
                TileInstance.BambooPlant,
                TileInstance.Spring,
            };
        }

        private IList<Tile> GetUnsortedHandOfMixedAndBonusTiles()
        {
            // 566 D, 29 B, 1C, EWN, wwwgr, 134 S
            return new List<Tile>
            {
                TileInstance.EastWind,
                TileInstance.RedDragon,
                TileInstance.FiveOfDots,
                TileInstance.Autumn,
                TileInstance.NineOfBamboo,
                TileInstance.WestWind,
                TileInstance.WhiteDragon,
                TileInstance.Spring,
                TileInstance.TwoOfBamboo,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.OneOfCharacters,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots,
                TileInstance.Winter,
                TileInstance.GreenDragon,
            };
        }

        private IList<Tile> GetSortedHandOfMixedAndBonusTiles()
        {
            // 566 D, 29 B, 1C, EWN, wwwgr, 134 S
            return new List<Tile> {
                TileInstance.FiveOfDots,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots,
                TileInstance.TwoOfBamboo,
                TileInstance.NineOfBamboo,
                TileInstance.OneOfCharacters,
                TileInstance.EastWind,
                TileInstance.WestWind,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.GreenDragon,
                TileInstance.RedDragon,
                TileInstance.Spring,
                TileInstance.Autumn,
                TileInstance.Winter
            };
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fraser.Mahjong;
using System.Collections.Generic;
using System.Linq;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class SuitedHonorTileSorterUnitTest
    {
        [TestMethod]
        public void SuitedHonorTileSorterSortHandMethodTest_SuitedTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorTileSorter();
            var tiles = GetUnsortedHandOfSuitedTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfSuitedTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterSortHandMethodTest_AlreadySortedSuitedTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorTileSorter();
            var tiles = GetSortedHandOfSuitedTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfSuitedTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterSortHandMethodTest_HonorTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorTileSorter();
            var tiles = GetUnsortedHandOfHonorTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfHonorTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterSortHandMethodTest_AlreadySortedHonorTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorTileSorter();
            var tiles = GetSortedHandOfHonorTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfHonorTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterSortHandMethodTest_MixedTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorTileSorter();
            var tiles = GetUnsortedHandOfMixedTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfMixedTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterSortHandMethodTest_AlreadySortedMixedTilesEqualsSortedTiles_IsTrue()
        {
            var sorter = new SuitedHonorTileSorter();
            var tiles = GetSortedHandOfMixedTiles();

            tiles = sorter.SortTiles(tiles);
            var result = tiles.SequenceEqual(GetSortedHandOfMixedTiles());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterEqualityControlTest_UnsortedSuitedTilesEqualsSortedTiles_IsFalse()
        {
            var tiles = GetUnsortedHandOfSuitedTiles();
            var result = tiles.SequenceEqual(GetSortedHandOfSuitedTiles());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterEqualityControlTest_UnsortedHonorTilesEqualsSortedTiles_IsFalse()
        {
            var tiles = GetUnsortedHandOfHonorTiles();
            var result = tiles.SequenceEqual(GetSortedHandOfHonorTiles());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SuitedHonorTileSorterEqualityControlTest_UnsortedMixedTilesEqualsSortedTiles_IsFalse()
        {
            var tiles = GetUnsortedHandOfMixedTiles();
            var result = tiles.SequenceEqual(GetSortedHandOfMixedTiles());

            Assert.IsFalse(result);
        }

        private IList<Tile> GetUnsortedHandOfSuitedTiles()
        {
            // 22234566 D, 678 B, 345 C
            return new List<Tile>
            {
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.FiveOfDots,
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.SixOfBamboo,
                TileInstance.SevenOfBamboo,
                TileInstance.EightOfBamboo,
                TileInstance.ThreeOfCharacters,
                TileInstance.FourOfCharacters,
                TileInstance.FiveOfCharacters,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots,
            };
        }

        private IList<Tile> GetSortedHandOfSuitedTiles()
        {
            // 22234566 D, 678 B, 345 C
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
            };
        }

        private IList<Tile> GetUnsortedHandOfHonorTiles()
        {
            // EEESSWN, wwwwggr
            return new List<Tile>
            {
                TileInstance.EastWind,
                TileInstance.RedDragon,
                TileInstance.SouthWind,
                TileInstance.EastWind,
                TileInstance.WestWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.SouthWind,
                TileInstance.EastWind,
                TileInstance.GreenDragon,
                TileInstance.GreenDragon
            };
        }

        private IList<Tile> GetSortedHandOfHonorTiles()
        {
            // EEESSWN, wwwwggr
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
                TileInstance.RedDragon
            };
        }

        private IList<Tile> GetUnsortedHandOfMixedTiles()
        {
            // 566 D, 29 B, 1 C, EWN, wwwgr
            return new List<Tile>
            {
                TileInstance.EastWind,
                TileInstance.RedDragon,
                TileInstance.FiveOfDots,
                TileInstance.NineOfBamboo,
                TileInstance.WestWind,
                TileInstance.WhiteDragon,
                TileInstance.TwoOfBamboo,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.OneOfCharacters,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots,
                TileInstance.GreenDragon,
            };
        }

        private IList<Tile> GetSortedHandOfMixedTiles()
        {
            // 566 D, 29 B, 1 C, EWN, wwwgr
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
                TileInstance.RedDragon
            };
        }
    }
}
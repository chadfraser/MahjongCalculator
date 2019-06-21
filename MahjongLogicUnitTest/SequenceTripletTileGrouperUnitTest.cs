using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fraser.Mahjong;
using System.Collections.Generic;
using System.Linq;

namespace MahjongLogicUnitTest
{
    [TestClass]
    public class SequenceTripletTileGrouperUnitTest
    {
        ///*
        // Find All Ways to Group Tiles After Removing a Pair (IList IList TileGrouping)
        // Find All Ways to Fully Group Tiles After Removing a Pair (IList IList TileGrouping)
        // */
        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleSuitedTilesAllSequences_IsTrue()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesAllSequences();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleSuitedTilesAllTriplets_IsTrue()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesAllTriplets();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleSuitedTilesAllGroups_IsTrue()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesAllGroups();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleSuitedTilesNotGroupable_IsFalse()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesNotFullyGroupable();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleHonorTilesAllTriplets_IsTrue()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfHonorTilesAllTriplets();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleHonorTilesNotGroupable_IsFalse()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfHonorTilesNotFullyGroupable();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleMixedTilesAllTriplets_IsTrue()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfMixedTilesAllTriplets();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleMixedTilesAllGroups_IsTrue()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfMixedTilesAllGroups();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_SimpleMixedTilesNotGroupable_IsFalse()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfMixedTilesNotFullyGroupable();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperCanGroupTilesIntoLegalHandMethodTest_ComplexSuitedTilesGroupable_IsTrue()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetComplexGroupableHandOfSuitedTiles();

            var result = grouper.CanGroupTilesIntoLegalHand(tiles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllGroupsInTilesMethodTest_SimpleSuitedTilesAllSequences()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesAllSequences();

            var actual = (List<TileGrouping>)grouper.FindAllGroupsInTiles(tiles);
            var expected = (List<TileGrouping>)GetAllGroupsFromSimpleHandOfSuitedTilesSequences();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllGroupsInTilesMethodTest_SimpleSuitedTilesAllTriplets()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesAllTriplets();

            var actual = (List<TileGrouping>)grouper.FindAllGroupsInTiles(tiles);
            var expected = (List<TileGrouping>)GetAllGroupsFromSimpleHandOfSuitedTilesTriplets();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllGroupsInTilesMethodTest_SimpleSuitedTilesAllGroups()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesAllGroups();

            var actual = (List<TileGrouping>)grouper.FindAllGroupsInTiles(tiles);
            var expected = (List<TileGrouping>)GetAllGroupsFromSimpleHandOfSuitedTilesGroups();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllGroupsInTilesMethodTest_SimpleMixedTilesAllGroups()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfMixedTilesAllGroups();

            var actual = (List<TileGrouping>)grouper.FindAllGroupsInTiles(tiles);
            var expected = (List<TileGrouping>)GetAllGroupsFromSimpleHandOfMixedTilesGroups();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllGroupsInTilesMethodTest_ComplexSuitedTiles()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetComplexGroupableHandOfSuitedTiles();

            var actual = (List<TileGrouping>)grouper.FindAllGroupsInTiles(tiles);
            var expected = (List<TileGrouping>)GetAllGroupsFromComplexHandOfSuitedTiles();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllWaysToGroupTilesMethodTest_SimpleSuitedTilesAllGroups()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesAllGroups();

            var actual = (List<IList<TileGrouping>>)grouper.FindAllWaysToGroupTiles(tiles);
            var expected = (List<IList<TileGrouping>>)GetAllWaysToGroupTilesFromSimpleHandOfSuitedTilesGroups();

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var sublist in actual)
            {
                var result = expected.Any(x => x.Count == sublist.Count && x.ToHashSet().SetEquals(sublist.ToHashSet()));
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllWaysToGroupTilesMethodTest_SimpleMixedTilesAllGroups()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfMixedTilesAllGroups();

            var actual = (List<IList<TileGrouping>>)grouper.FindAllWaysToGroupTiles(tiles);
            var expected = (List<IList<TileGrouping>>)GetAllWaysToGroupTilesFromSimpleHandOfMixedTilesGroups();

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var sublist in actual)
            {
                //var result = expected.Any(x => x.OrderBy(t => t).SequenceEqual(sublist.OrderBy(t => t)));
                var result = expected.Any(x => x.Count == sublist.Count && x.ToHashSet().SetEquals(sublist.ToHashSet()));
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllWaysToGroupTilesMethodTest_ComplexSuitedTiles()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetComplexGroupableHandOfSuitedTiles();

            var actual = (List<IList<TileGrouping>>)grouper.FindAllWaysToGroupTiles(tiles);
            var expected = (List<IList<TileGrouping>>)GetAllWaysToGroupTilesFromComplexHandOfSuitedTiles();

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var sublist in actual)
            {
                //var result = expected.Any(x => x.OrderBy(t => t).SequenceEqual(sublist.OrderBy(t => t)));
                var result = expected.Any(x => x.Count == sublist.Count && x.ToHashSet().SetEquals(sublist.ToHashSet()));
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void SequenceTripletTileGrouperFindAllWaysToGroupTilesMethodTest_SimpleSuitedTilesSequencesOrTriplets()
        {
            var tileSorterStub = new FakeTileSorter();
            var grouper = new SequenceTripletTileGrouper(tileSorterStub);
            var tiles = GetSimpleHandOfSuitedTilesSequencesOrTriplets();

            var actual = (List<IList<TileGrouping>>)grouper.FindAllWaysToGroupTiles(tiles);
            var expected = (List<IList<TileGrouping>>)GetAllWaysToGroupTilesFromSimpleHandOfSuitedTilesSequencesOrTriplets();

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var sublist in actual)
            {
                //var result = expected.Any(x => x.OrderBy(t => t).SequenceEqual(sublist.OrderBy(t => t)));
                var result = expected.Any(x => x.Count == sublist.Count && x.ToHashSet().SetEquals(sublist.ToHashSet()));
                Assert.IsTrue(result);
            }
        }

        private IList<Tile> GetSimpleHandOfSuitedTilesAllSequences()
        {
            // 11 D, 234 678 B
            return new List<Tile>
            {
                TileInstance.OneOfDots,
                TileInstance.OneOfDots,
                TileInstance.TwoOfBamboo,
                TileInstance.ThreeOfBamboo,
                TileInstance.FourOfBamboo,
                TileInstance.SixOfBamboo,
                TileInstance.SevenOfBamboo,
                TileInstance.EightOfBamboo
            };
        }

        private IList<Tile> GetSimpleHandOfSuitedTilesAllTriplets()
        {
            // 11 222 666 D
            return new List<Tile>
            {
                TileInstance.OneOfDots,
                TileInstance.OneOfDots,
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots,
                TileInstance.SixOfDots
            };
        }

        private IList<Tile> GetSimpleHandOfSuitedTilesAllGroups()
        {
            // 11 234 D, 111 B
            return new List<Tile>
            {
                TileInstance.OneOfDots,
                TileInstance.OneOfDots,
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.OneOfBamboo,
                TileInstance.OneOfBamboo,
                TileInstance.OneOfBamboo
            };
        }

        private IList<Tile> GetSimpleHandOfSuitedTilesNotFullyGroupable()
        {
            // 11 234 D, 113 B
            return new List<Tile>
            {
                TileInstance.OneOfDots,
                TileInstance.OneOfDots,
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.OneOfBamboo,
                TileInstance.OneOfBamboo,
                TileInstance.ThreeOfBamboo
            };
        }

        private IList<Tile> GetSimpleHandOfHonorTilesAllTriplets()
        {
            // EE NNN rrr
            return new List<Tile>
            {
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.NorthWind,
                TileInstance.NorthWind,
                TileInstance.NorthWind,
                TileInstance.RedDragon,
                TileInstance.RedDragon,
                TileInstance.RedDragon
            };
        }

        private IList<Tile> GetSimpleHandOfHonorTilesNotFullyGroupable()
        {
            // EE NNN w
            return new List<Tile>
            {
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.NorthWind,
                TileInstance.NorthWind,
                TileInstance.NorthWind,
                TileInstance.WhiteDragon
            };
        }

        private IList<Tile> GetSimpleHandOfMixedTilesAllTriplets()
        {
            // 222 D, EE www
            return new List<Tile>
            {
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon,
                TileInstance.WhiteDragon
            };
        }

        private IList<Tile> GetSimpleHandOfMixedTilesAllGroups()
        {
            // 11 234 D, EEE
            return new List<Tile>
            {
                TileInstance.OneOfDots,
                TileInstance.OneOfDots,
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.EastWind,
                TileInstance.EastWind,
                TileInstance.EastWind
            };
        }

        private IList<Tile> GetSimpleHandOfMixedTilesNotFullyGroupable()
        {
            // 11 234 D, 3 B, WWW
            return new List<Tile>
            {
                TileInstance.OneOfDots,
                TileInstance.OneOfDots,
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.ThreeOfBamboo,
                TileInstance.WestWind,
                TileInstance.WestWind,
                TileInstance.WestWind
            };
        }

        private IList<Tile> GetComplexGroupableHandOfSuitedTiles()
        {
            // 222 345 66 789 D
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
                TileInstance.SevenOfDots,
                TileInstance.EightOfDots,
                TileInstance.NineOfDots
            };
        }

        private IList<Tile> GetSimpleHandOfSuitedTilesSequencesOrTriplets()
        {
            // 222 333 444 D
            return new List<Tile>
            {
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.TwoOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.ThreeOfDots,
                TileInstance.FourOfDots,
                TileInstance.FourOfDots,
                TileInstance.FourOfDots
            };
        }

        private IList<TileGrouping> GetAllGroupsFromSimpleHandOfSuitedTilesSequences()
        {
            // 11 D, 234678 B
            return new List<TileGrouping>
            {
                new TileGrouping
                {
                    TileInstance.TwoOfBamboo,
                    TileInstance.ThreeOfBamboo,
                    TileInstance.FourOfBamboo
                },
                new TileGrouping
                {
                    TileInstance.SixOfBamboo,
                    TileInstance.SevenOfBamboo,
                    TileInstance.EightOfBamboo
                }
            };
        }

        private IList<TileGrouping> GetAllGroupsFromSimpleHandOfSuitedTilesTriplets()
        {
            // 11222666 D
            return new List<TileGrouping>
            {
                new TileGrouping
                {
                    TileInstance.TwoOfDots,
                    TileInstance.TwoOfDots,
                    TileInstance.TwoOfDots
                },
                new TileGrouping
                {
                    TileInstance.SixOfDots,
                    TileInstance.SixOfDots,
                    TileInstance.SixOfDots
                }
            };
        }

        private IList<TileGrouping> GetAllGroupsFromSimpleHandOfSuitedTilesGroups()
        {
            // 11234 D, 111 B
            return new List<TileGrouping>
            {
                new TileGrouping
                {
                    TileInstance.OneOfDots,
                    TileInstance.TwoOfDots,
                    TileInstance.ThreeOfDots
                },
                new TileGrouping
                {
                    TileInstance.TwoOfDots,
                    TileInstance.ThreeOfDots,
                    TileInstance.FourOfDots
                },
                new TileGrouping
                {
                    TileInstance.OneOfBamboo,
                    TileInstance.OneOfBamboo,
                    TileInstance.OneOfBamboo
                }
            };
        }

        private IList<TileGrouping> GetAllGroupsFromSimpleHandOfMixedTilesGroups()
        {
            // 11234 D, EEE
            return new List<TileGrouping>
            {
                new TileGrouping
                {
                    TileInstance.OneOfDots,
                    TileInstance.TwoOfDots,
                    TileInstance.ThreeOfDots
                },
                new TileGrouping
                {
                    TileInstance.TwoOfDots,
                    TileInstance.ThreeOfDots,
                    TileInstance.FourOfDots
                },
                new TileGrouping
                {
                    TileInstance.EastWind,
                    TileInstance.EastWind,
                    TileInstance.EastWind
                }
            };
        }

        private IList<TileGrouping> GetAllGroupsFromComplexHandOfSuitedTiles()
        {
            // 22234566789 D
            return new List<TileGrouping>
            {
                new TileGrouping
                {
                    TileInstance.TwoOfDots,
                    TileInstance.TwoOfDots,
                    TileInstance.TwoOfDots
                },
                new TileGrouping
                {
                    TileInstance.TwoOfDots,
                    TileInstance.ThreeOfDots,
                    TileInstance.FourOfDots
                },
                new TileGrouping
                {
                    TileInstance.ThreeOfDots,
                    TileInstance.FourOfDots,
                    TileInstance.FiveOfDots
                },
                new TileGrouping
                {
                    TileInstance.FourOfDots,
                    TileInstance.FiveOfDots,
                    TileInstance.SixOfDots
                },
                new TileGrouping
                {
                    TileInstance.FiveOfDots,
                    TileInstance.SixOfDots,
                    TileInstance.SevenOfDots
                },
                new TileGrouping
                {
                    TileInstance.SixOfDots,
                    TileInstance.SevenOfDots,
                    TileInstance.EightOfDots
                },
                new TileGrouping
                {
                    TileInstance.SevenOfDots,
                    TileInstance.EightOfDots,
                    TileInstance.NineOfDots
                }
            };
        }

        private IList<IList<TileGrouping>> GetAllWaysToGroupTilesFromSimpleHandOfSuitedTilesGroups()
        {
            // 11234 D, 111 B
            return new List<IList<TileGrouping>>
            {
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.OneOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.OneOfBamboo,
                        TileInstance.OneOfBamboo,
                        TileInstance.OneOfBamboo
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.OneOfBamboo,
                        TileInstance.OneOfBamboo,
                        TileInstance.OneOfBamboo
                    }
                }
            };
        }

        private IList<IList<TileGrouping>> GetAllWaysToGroupTilesFromSimpleHandOfMixedTilesGroups()
        {
            // 11234 D, EEE
            return new List<IList<TileGrouping>>
            {
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.OneOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.EastWind,
                        TileInstance.EastWind,
                        TileInstance.EastWind
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.EastWind,
                        TileInstance.EastWind,
                        TileInstance.EastWind
                    }
                }
            };
        }

        private IList<IList<TileGrouping>> GetAllWaysToGroupTilesFromComplexHandOfSuitedTiles()
        {
            // 22234566789 D
            return new List<IList<TileGrouping>>
            {
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.FiveOfDots,
                        TileInstance.SixOfDots,
                        TileInstance.SevenOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.SixOfDots,
                        TileInstance.SevenOfDots,
                        TileInstance.EightOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.SevenOfDots,
                        TileInstance.EightOfDots,
                        TileInstance.NineOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots,
                        TileInstance.FiveOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.SixOfDots,
                        TileInstance.SevenOfDots,
                        TileInstance.EightOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots,
                        TileInstance.FiveOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.SevenOfDots,
                        TileInstance.EightOfDots,
                        TileInstance.NineOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.FourOfDots,
                        TileInstance.FiveOfDots,
                        TileInstance.SixOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.SixOfDots,
                        TileInstance.SevenOfDots,
                        TileInstance.EightOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.FourOfDots,
                        TileInstance.FiveOfDots,
                        TileInstance.SixOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.SevenOfDots,
                        TileInstance.EightOfDots,
                        TileInstance.NineOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.FiveOfDots,
                        TileInstance.SixOfDots,
                        TileInstance.SevenOfDots
                    }
                }
            };
        }

        private IList<IList<TileGrouping>> GetAllWaysToGroupTilesFromSimpleHandOfSuitedTilesSequencesOrTriplets()
        {
            // 222 333 444 D
            return new List<IList<TileGrouping>>
            {
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.FourOfDots
                    }
                },
                new List<TileGrouping>
                {
                    new TileGrouping
                    {
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots,
                        TileInstance.TwoOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.ThreeOfDots,
                        TileInstance.ThreeOfDots,
                        TileInstance.ThreeOfDots
                    },
                    new TileGrouping
                    {
                        TileInstance.FourOfDots,
                        TileInstance.FourOfDots,
                        TileInstance.FourOfDots
                    }
                }
            };
        }
    }
}
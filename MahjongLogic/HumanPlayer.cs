using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fraser.Mahjong
{
    class HumanPlayer : Player
    {
        public HumanPlayer(Game game) : base(game, "Player")
        {

        }

        public override bool IsClaimingDiscardedTileToCompleteGroup(Tile discardedTile, bool canBeSequence)
        {
            if (!CanClaimDiscardedTileToCompleteGroup(discardedTile, canBeSequence))
            {
                return false;
            }
            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };
            var groupsInvolvingDiscardedTile = TileGrouper.FindAllGroupsInTiles(potentialHandTiles)
                .Where(group => group.Contains(discardedTile)).ToList();
            if (!canBeSequence)
            {
                groupsInvolvingDiscardedTile = groupsInvolvingDiscardedTile.Where(group => !group.IsSequence()).ToList();
            }

            while (true)
            {
                Console.WriteLine($"Choose one of the following options:");
                Console.WriteLine($"\t1. Do not claim the discarded {discardedTile}.");
                for (int i = 0; i < groupsInvolvingDiscardedTile.Count; i++)
                {
                    Console.WriteLine($"\t{i + 2}. Claim the discarded {discardedTile} to make: " +
                        $"[{groupsInvolvingDiscardedTile[i]}].");
                }
                var input = Console.ReadLine();
                if (int.TryParse(input, out int numericChoice) && numericChoice > 0 &&
                    numericChoice <= groupsInvolvingDiscardedTile.Count + 1)
                {
                    return numericChoice != 1;
                }
                Console.WriteLine();
            }
        }

        public override bool IsClaimingDiscardedTileToCompleteWinningHand(Tile discardedTile)
        {
            var uncalledTilesWithClaimedDiscard = new List<Tile>(Hand.UncalledTiles)
            {
                discardedTile
            };
            if (!TileGrouper.CanGroupTilesIntoLegalHand(uncalledTilesWithClaimedDiscard))
            {
                return false;
            }

            Console.WriteLine($"Type 'yes' to claim the discarded {discardedTile} for the win.");
            var input = Console.ReadLine().ToLower();
            return input.Equals("yes");
        }

        public override Tile ChooseTileToDiscard()
        {
            while (true)
            {
                Console.WriteLine($"Choose a tile to discard by typing a number from 1 to {Hand.UncalledTiles.Count}.");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int numericChoice) && numericChoice > 0 &&
                    numericChoice <= Hand.UncalledTiles.Count)
                {
                    var tileToDiscard = Hand.UncalledTiles[numericChoice - 1];
                    Hand.UncalledTiles.Remove(tileToDiscard);
                    return tileToDiscard;
                }
                Console.WriteLine();
            }
        }

        public override bool IsDeclaringWin()
        {
            Console.WriteLine($"Type 'yes' to declare a win.");
            var input = Console.ReadLine().ToLower();
            return input.Equals("yes");
        }

        public override TileGrouping GetOpenOrPromotedQuadMade()
        {
            var closedQuads = TileGrouper.FindAllGroupsInTiles(Hand.UncalledTiles)
                .Where(group => group.IsQuad()).ToList();
            var promotedQuads = new List<TileGrouping>(Hand.CalledSets.Where(
                group => group.IsTriplet() && Hand.UncalledTiles.Contains(group.First())));
            while (true)
            {
                Console.WriteLine($"Choose one of the following options:");
                Console.WriteLine($"\t1. Do not expose a quad.");
                for (int i = 0; i < closedQuads.Count; i++)
                {
                    Console.WriteLine($"\t{i + 2}. Create a closed quad with the four copies of {closedQuads[i].First()} " +
                        $"in your hand.");
                }
                for (int i = 0; i < promotedQuads.Count; i++)
                {
                    Console.WriteLine($"\t{i + closedQuads.Count() + 2}. Create a promoted quad with the " +
                        $"{promotedQuads[i].First()} in your hand.");
                }
                var input = Console.ReadLine();
                var inputIsInt = int.TryParse(input, out int numericChoice);
                if (!inputIsInt)
                {
                    Console.WriteLine();
                    continue;
                }

                if (numericChoice == 1)
                {
                    return null;
                }
                if (numericChoice > 0 && numericChoice < closedQuads.Count + 2)
                {
                    return closedQuads[numericChoice - 2];
                }
                if (numericChoice > 0 && numericChoice < closedQuads.Count + promotedQuads.Count + 2)
                {
                    return promotedQuads[numericChoice - closedQuads.Count - 2];
                }
            }
        }

        //public string ParseInput()
        //{
        //    var input = Console.ReadLine();
        //    if (input.Equals(""))
        //    {

        //    }
        //    return input;
        //}

        public override TileGrouping GetGroupMadeWithDiscardedTileOrEmptyGroupForWin(Tile discardedTile,
            Player discardingPlayer)
        {
            var indexInPlayerList = Array.IndexOf(Game.Players, this);
            var arrayLength = Game.Players.Count();
            var moduloPreviousPlayerIndex = ((indexInPlayerList - 1) % arrayLength + arrayLength) % arrayLength;
            var isNextInTurnOrder = Game.Players[moduloPreviousPlayerIndex] == discardingPlayer;

            var canMakeWinningHand = CanClaimDiscardedTileToCompleteWinningHand(discardedTile);
            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };
            var groupsInvolvingDiscardedTile = TileGrouper.FindAllGroupsInTiles(potentialHandTiles)
                .Where(group => group.Contains(discardedTile)).ToList();
            if (!isNextInTurnOrder)
            {
                groupsInvolvingDiscardedTile = groupsInvolvingDiscardedTile.Where(group => !group.IsSequence()).ToList();
            }

            while (true)
            {
                Console.WriteLine($"Choose one of the following options:");
                Console.WriteLine($"\t1. Do not claim the discarded {discardedTile}.");
                for (int i = 0; i < groupsInvolvingDiscardedTile.Count; i++)
                {
                    Console.WriteLine($"\t{i + 2}. Claim the discarded {discardedTile} to make: " +
                        $"[{groupsInvolvingDiscardedTile[i]}].");
                }
                if (canMakeWinningHand)
                {
                    Console.WriteLine($"\t{groupsInvolvingDiscardedTile.Count + 2}. Claim the discarded {discardedTile} " +
                        $"to make a winning hand.");
                }
                var input = Console.ReadLine();
                var inputIsInt = int.TryParse(input, out int numericChoice);
                if (!inputIsInt)
                {
                    Console.WriteLine();
                    continue;
                }

                if (numericChoice == 1)
                {
                    return null;
                }
                if (numericChoice == groupsInvolvingDiscardedTile.Count + 2 && canMakeWinningHand)
                {
                    return new TileGrouping();
                }
                if (numericChoice > 0 && numericChoice < groupsInvolvingDiscardedTile.Count + 2)
                {
                    return groupsInvolvingDiscardedTile[numericChoice - 2];
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    class HumanPlayer : Player
    {
        public HumanPlayer(Game game, string name, HonorType seatWind) : base(game, name, seatWind)
        {
            IndexOfRecentlySelectedTile = -1;
        }

        public HumanPlayer(Game game) : this(game, "Player", HonorType.East)
        {
        }

        private int IndexOfRecentlySelectedTile { get; set; }

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
                WriteOptionChoiceIntroductoryText();
                for (int i = 0; i < groupsInvolvingDiscardedTile.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. Claim the discarded {discardedTile} to make: " +
                        $"[{groupsInvolvingDiscardedTile[i]}].");
                }
                Console.WriteLine("\tP: [P]ass.");
                var choice = TakeInputAsLowercase();
                if (choice == "p")
                {
                    return false;
                }
                int.TryParse(choice, out int numericChoice);
                if (numericChoice > 0 && numericChoice <= groupsInvolvingDiscardedTile.Count)
                {
                    return true;
                }
                WriteUnrecognizedInputText();
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

            while (true)
            {
                WriteOptionChoiceIntroductoryText();
                Console.WriteLine($"\tW: Claim the discarded {discardedTile} to form a [w]inning hand.");
                Console.WriteLine("\tP: [P]ass.");
                var choice = TakeInputAsLowercase();
                if (choice == "w")
                {
                    return true;
                }
                if (choice == "p")
                {
                    return false;
                }
                WriteUnrecognizedInputText();
            }
        }

        public override TurnAction GetTurnAction(bool canDeclareWinAndFormQuads)
        {
            var choiceDict = BuildTurnActionDict(canDeclareWinAndFormQuads);

            while (true)
            {
                WriteOptionChoiceIntroductoryText();
                WritePossibleTurnActions(canDeclareWinAndFormQuads);

                var choice = TakeInputAsLowercase();
                if (choiceDict.ContainsKey(choice))
                {
                    return choiceDict[choice];
                }

                int.TryParse(choice, out int numericChoice);
                if (numericChoice > 0 && numericChoice <= Hand.UncalledTiles.Count)
                {
                    IndexOfRecentlySelectedTile = numericChoice - 1;
                    return TurnAction.Discard;
                }
                WriteUnrecognizedInputText();
            }
        }

        public override int ChooseIndexOfTileToDiscard()
        {
            if (IndexOfRecentlySelectedTile != -1)
            {
                var temp = IndexOfRecentlySelectedTile;
                IndexOfRecentlySelectedTile = -1;
                return temp;
            }
            while (true)
            {
                Console.WriteLine($"Choose a tile to discard by typing a number from 1 to {Hand.UncalledTiles.Count}, " +
                    $"or else by typing the two-character code for that tile.");
                Console.WriteLine("You may also type 'B' to go [b]ack and choose another option.");

                var choice = TakeInputAsLowercase().ToUpper();
                if (choice == "B")
                {
                    return -1;
                }

                if (TileInstance.tileInstanceShorthandDict.ContainsKey(choice) &&
                    Hand.UncalledTiles.Contains(TileInstance.tileInstanceShorthandDict[choice]))
                {
                    return Hand.UncalledTiles.IndexOf(TileInstance.tileInstanceShorthandDict[choice]);
                }

                int.TryParse(choice, out int numericChoice);
                if (numericChoice > 0 && numericChoice <= Hand.UncalledTiles.Count)
                {
                    return numericChoice - 1;
                }
                WriteUnrecognizedInputText();
            }
        }

        public override bool IsDeclaringWin()
        {
            while (true)
            {
                WriteOptionChoiceIntroductoryText();
                Console.WriteLine("\tW: Declare a [w]in.");
                Console.WriteLine("\tP: [P]ass.");
                var choice = TakeInputAsLowercase();
                if (choice == "w")
                {
                    return true;
                }
                if (choice == "p")
                {
                    return false;
                }
                WriteUnrecognizedInputText();
            }
        }

        public override TileGrouping GetClosedOrPromotedQuadMade()
        {
            var closedQuads = TileGrouper.FindAllGroupsInTiles(Hand.UncalledTiles)
                .Where(group => group.IsQuad()).ToList();
            var promotedQuads = new List<TileGrouping>(Hand.CalledSets.Where(
                group => group.IsTriplet() && Hand.UncalledTiles.Contains(group.First())));
            var allQuads = closedQuads.Concat(promotedQuads).ToList();

            while (true)
            {
                WriteOptionChoiceIntroductoryText();
                for (int i = 0; i < closedQuads.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. Create a closed quad with the four " +
                        $"{closedQuads[i].First()} tiles in your hand.");
                }
                for (int i = 0; i < promotedQuads.Count; i++)
                {
                    Console.WriteLine($"\t{i + closedQuads.Count() + 1}. Create a promoted quad with the " +
                        $"{promotedQuads[i].First()} tile in your hand.");
                }
                Console.WriteLine($"\tP: [P]ass.");

                var choice = TakeInputAsLowercase();
                if (choice == "p")
                {
                    return null;
                }

                int.TryParse(choice, out int numericChoice);
                if (numericChoice > 0 && numericChoice < allQuads.Count + 1)
                {
                    return allQuads[numericChoice - 1];
                }
                WriteUnrecognizedInputText();
            }
        }

        public override TileGrouping GetGroupMadeWithDiscardedTile(Tile discardedTile, TurnAction typeOfGroup)
        {
            return RecentlySelectedTileGrouping;
        }

        public override TurnAction GetTurnActionAgainstDiscardedTile(Tile discardedTile, Player discardingPlayer)
        {
            RecentlySelectedTileGrouping = null;
            var isNextInTurnOrder = IsPlayerNextInTurnOrder(discardingPlayer);
            var canMakeWinningHand = CanClaimDiscardedTileToCompleteWinningHand(discardedTile);

            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };
            var groupsInvolvingDiscardedTile = TileGrouper.FindAllGroupsInTiles(potentialHandTiles)
                .Where(group => group.Contains(discardedTile)).ToList();
            var allowedGroups = FilterUnallowedGroups(groupsInvolvingDiscardedTile,
                x => x.IsSequence() && !isNextInTurnOrder);

            while (true)
            {
                WriteOptionChoiceIntroductoryText();

                for (int i = 0; i < allowedGroups.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. Claim the discarded {discardedTile} to make: " +
                        $"[{allowedGroups[i]}].");
                }
                if (canMakeWinningHand)
                {
                    Console.WriteLine($"\tW: Claim the discarded {discardedTile} to form a [w]inning hand.");
                }
                Console.WriteLine($"\tP: [P]ass.");
                var choice = TakeInputAsLowercase();

                if (choice == "w" && canMakeWinningHand)
                {
                    return TurnAction.DeclareWin;
                }
                if (choice == "p")
                {
                    return TurnAction.Pass;
                }

                int.TryParse(choice, out int numericChoice);
                if (numericChoice > 0 && numericChoice < allowedGroups.Count + 1)
                {
                    RecentlySelectedTileGrouping = allowedGroups[numericChoice - 1];
                    var action = RecentlySelectedTileGrouping.IsTriplet() ? TurnAction.FormTriplet :
                        (RecentlySelectedTileGrouping.IsQuad() ? TurnAction.FormQuad :
                        (RecentlySelectedTileGrouping.IsSequence() ? TurnAction.FormSequence : TurnAction.Pass));
                    return action;
                }
                WriteUnrecognizedInputText();
            }
        }

        private IDictionary<string, TurnAction> BuildTurnActionDict(bool canDeclareWinAndFormQuads)
        {
            var choiceDict = new Dictionary<string, TurnAction>
            {
                ["s"] = TurnAction.CheckScores,
                ["p"] = TurnAction.CheckPatterns,
                ["d"] = TurnAction.Discard
            };
            if (canDeclareWinAndFormQuads)
            {
                if (Hand.IsWinningHand())
                {
                    choiceDict["w"] = TurnAction.DeclareWin;
                }
                if (Hand.ContainsQuad())
                {
                    choiceDict["q"] = TurnAction.FormQuad;
                }
            }
            return choiceDict;
        }

        private string TakeInputAsLowercase()
        {
            Console.Write(">>>");
            return Console.ReadLine().ToLower();
        }

        private void WriteOptionChoiceIntroductoryText()
        {
            Console.WriteLine($"{Name}, choose one of the following options:");
        }

        private void WritePossibleTurnActions(bool canDeclareWinAndFormQuads)
        {
            if (canDeclareWinAndFormQuads)
            {
                if (Hand.IsWinningHand())
                {
                    Console.WriteLine($"\tW: Declare a [w]in.");
                }
                if (Hand.ContainsQuad())
                {
                    Console.WriteLine($"\tQ: Create a [q]uad.");
                }
            }
            Console.WriteLine($"\tS: Check the current [s]cores.");
            Console.WriteLine($"\tP: Check all legal scoring [p]atterns.");
            Console.WriteLine($"\tD: Choose a tile to [d]iscard.");
            Console.WriteLine($"You may also choose a tile to discard just by typing its index, from 1 to " +
                $"{Hand.UncalledTiles.Count}.");
        }

        private void WriteUnrecognizedInputText()
        {
            Console.WriteLine("That input was not recognized.\n");
            Console.ReadKey();
            Game.CurrentRound.CurrentDeal.WriteGameState();
        }

        private bool IsPlayerNextInTurnOrder(Player otherPlayer)
        {
            var indexInPlayerList = Array.IndexOf(Game.Players, this);
            var arrayLength = Game.Players.Count();
            var moduloPreviousPlayerIndex = (indexInPlayerList + arrayLength - 1) % arrayLength;
            return Game.Players[moduloPreviousPlayerIndex] == otherPlayer;
        }

        private IList<TileGrouping> FilterUnallowedGroups(IList<TileGrouping> groups,
            Func<TileGrouping, bool> restriction)
        {
            return groups.Where(group => !restriction(group)).ToList();
        }
    }
}

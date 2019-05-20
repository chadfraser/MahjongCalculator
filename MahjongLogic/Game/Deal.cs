using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class Deal
    {
        public static readonly int maximumNameLength = 8;

        // The short colored string of a tile is 4 characters long, but we pad the line we write to with enough spaces
        // to match the maximumNameLength
        private static readonly int maximumTilesWrittenPerLine = (Console.WindowWidth - (maximumNameLength + 6)) / 4;
        private string mostRecentActionText = "";

        public Deal(Round round, IList<Tile> tiles)
        {
            Round = round;
            RemainingTiles = new List<Tile>(tiles);
            DiscardedTiles = new List<Tile>();
            IndexOfCurrentPlayer = Round.Game.DealerIndex;
            DealIsOver = false;
            DiscardedTilesPerPlayer = new Dictionary<Player, IList<Tile>>();
            InitializeDiscardedTilesPerPlayerDict();
        }

        public Round Round { get; set; }

        private IList<Tile> RemainingTiles { get; set; }

        public IList<Tile> DiscardedTiles { get; private set; }

        public IDictionary<Player, IList<Tile>> DiscardedTilesPerPlayer { get; private set; }

        private int IndexOfCurrentPlayer { get; set; }

        private bool DealIsOver { get; set; }

        private void InitializeDiscardedTilesPerPlayerDict()
        {
            foreach (var player in Round.GetPlayers())
            {
                DiscardedTilesPerPlayer[player] = new List<Tile>();
            }
        }

        public void PlayDeal()
        {
            Round.Game.HandScorer.ClearCircumstantialValues();
            DealInitialHands();
            while (!DealIsOver)
            {
                PlayTurn();
            }
        }

        public int GetRemainingTilesCount()
        {
            return RemainingTiles.Count();
        }

        private void DealInitialHands()
        {
            ShuffleTiles();
            //RemainingTiles[RemainingTiles.Count - 1] = TileInstance.PlumBlossom;
            //RemainingTiles[RemainingTiles.Count - 2] = TileInstance.Chrysanthemum;
            //RemainingTiles[RemainingTiles.Count - 3] = TileInstance.BambooPlant;
            //RemainingTiles[RemainingTiles.Count - 4] = TileInstance.Orchid;
            //RemainingTiles[RemainingTiles.Count - 19] = TileInstance.Winter;
            //RemainingTiles[RemainingTiles.Count - 18] = TileInstance.Summer;
            //RemainingTiles[0] = TileInstance.Autumn;
            for (int timesToDrawInitialSetOfTiles = 0; timesToDrawInitialSetOfTiles < 3; timesToDrawInitialSetOfTiles++)
            {
                foreach (var player in Round.GetPlayers())
                {
                    for (int amountOfInitialTilesToDraw = 0; amountOfInitialTilesToDraw < 4; amountOfInitialTilesToDraw++)
                    {
                        GiveNextTileToPlayer(player);
                    }
                }
            }
            foreach (var player in Round.GetPlayers())
            {
                GiveNextTileToPlayer(player);
            }
            //Round.GetPlayers()[0].Hand.UncalledTiles = new List<Tile>{
            //    TileInstance.EastWind,
            //    TileInstance.EastWind,
            //    TileInstance.EastWind,
            //    TileInstance.SouthWind,
            //    TileInstance.SouthWind,
            //    TileInstance.SouthWind,
            //    TileInstance.WestWind,
            //    TileInstance.WestWind,
            //    TileInstance.WestWind,
            //    TileInstance.NorthWind,
            //    TileInstance.NorthWind,
            //    TileInstance.NorthWind,
            //    TileInstance.WhiteDragon
            //};
            //Game.Players[0].Hand.UncalledTiles = new List<Tile>{
            //    TileInstance.OneOfBamboo,
            //    TileInstance.NineOfBamboo,
            //    TileInstance.OneOfDots,
            //    TileInstance.NineOfDots,
            //    TileInstance.OneOfCharacters,
            //    TileInstance.NineOfCharacters,
            //    TileInstance.EastWind,
            //    TileInstance.SouthWind,
            //    TileInstance.WestWind,
            //    TileInstance.NorthWind,
            //    TileInstance.WhiteDragon,
            //    TileInstance.GreenDragon,
            //    TileInstance.RedDragon
            //};
            foreach (var player in Round.GetPlayers())
            {
                CheckForAndReplaceBonusTiles(player);
                player.Hand.SortHand();
            }
        }

        private void ShuffleTiles()
        {
            var tiles = new List<Tile>(RemainingTiles);
            var rnd = new Random();
            RemainingTiles = tiles.OrderBy(item => rnd.Next()).ToList();
        }

        private Tile PopTileAtParticularIndexFromRemainingTiles(int indexToPop)
        {
            if (RemainingTiles.Count == 0)
            {
                Round.Stalemate();
                DealIsOver = true;
                return null;
            }
            var poppedTile = RemainingTiles[indexToPop];
            RemainingTiles.RemoveAt(indexToPop);

            return poppedTile;
        }

        private void GiveTileAtParticularIndexToPlayer(Player player, int indexToPop)
        {
            var tileAtIndex = PopTileAtParticularIndexFromRemainingTiles(indexToPop);
            if (tileAtIndex is null)
            {
                return;
            }
            player.Hand.UncalledTiles.Add(tileAtIndex);
        }

        private void GiveNextTileToPlayer(Player player)
        {
            GiveTileAtParticularIndexToPlayer(player, RemainingTiles.Count - 1);
        }

        private void PlayTurn()
        {
            var turnPlayer = Round.GetPlayers()[IndexOfCurrentPlayer];
            GiveNextTileToPlayer(turnPlayer);
            WriteGameStateWithoutPlayerData();
            WriteLastDrawnTileData(turnPlayer);

            var hadBonusTiles = CheckForAndReplaceBonusTiles(turnPlayer);
            if (DealIsOver)
            {
                return;
            }
            if (hadBonusTiles)
            {
                WriteLastDrawnTileData(turnPlayer);
            }
            Console.WriteLine();
            WriteAllPlayerData();

            bool finishedTurn;
            var quadsMadeThisTurn = 0;
            do
            {
                finishedTurn = HandleTurnAction(turnPlayer, true, ref quadsMadeThisTurn);
            }
            while (!finishedTurn);
        }

        private bool DiscardTile(Player turnPlayer)
        {
            var tileIndex = turnPlayer.ChooseIndexOfTileToDiscard();
            if (tileIndex == -1)
            {
                return false;
            }

            var discardedTile = turnPlayer.Hand.UncalledTiles[tileIndex];
            turnPlayer.Hand.UncalledTiles.RemoveAt(tileIndex);
            turnPlayer.TilesSeenSinceLastTurn.Clear();
            turnPlayer.Hand.SortHand();
            Console.WriteLine($"\n{turnPlayer.Name} discards {discardedTile}.\n");
            DiscardedTilesPerPlayer[turnPlayer].Add(discardedTile);

            var playerCount = Round.GetPlayers().Length;
            var actionsToClaimDiscardedTile = PollPlayersForClaimingDiscardedTile(discardedTile, turnPlayer);
            foreach (var player in Round.GetPlayers())
            {
                player.TilesSeenSinceLastTurn.Add(discardedTile);
            }

            var indexOfPlayerWithPriorityToClaimDiscard = FindIndexOfPlayerWithPriorityToClaimDiscardedTile(
                actionsToClaimDiscardedTile);

            if (indexOfPlayerWithPriorityToClaimDiscard == -1)
            {
                if (RemainingTiles.Count == 0)
                {
                    Round.Stalemate();
                    DealIsOver = true;
                    return true;
                }
                DiscardedTiles.Add(discardedTile);
                IndexOfCurrentPlayer = (IndexOfCurrentPlayer + 1) % playerCount;
                return true;
            }
            var currentAction = actionsToClaimDiscardedTile[indexOfPlayerWithPriorityToClaimDiscard];
            var otherPlayer = Round.GetPlayers()[indexOfPlayerWithPriorityToClaimDiscard];

            if (currentAction == TurnAction.DeclareWin)
            {
                // score hand, adjust points, end game, open set that scores
                otherPlayer.Hand.UncalledTiles.Add(discardedTile);
                HandleWinningHand(otherPlayer, turnPlayer, 0);
                return true;
            }
            else
            {
                var groupMadeWithDiscardedTile = otherPlayer.GetGroupMadeWithDiscardedTile(discardedTile, currentAction);
                HandleClaimedDiscard(indexOfPlayerWithPriorityToClaimDiscard, discardedTile, groupMadeWithDiscardedTile);
                return true;
            }
            // Console.ReadLine();
        }

        private TurnAction[] PollPlayersForClaimingDiscardedTile(Tile discardedTile, Player turnPlayer)
        {
            var playerCount = Round.GetPlayers().Length;
            var actionsToClaimDiscardedTile = new TurnAction[playerCount];

            for (int i = 1; i < playerCount; i++)
            {
                var otherPlayerIndex = (i + IndexOfCurrentPlayer) % playerCount;
                var otherPlayer = Round.GetPlayers()[otherPlayerIndex];
                if (!otherPlayer.CanClaimDiscardedTileToCompleteWinningHand(discardedTile) &&
                    !otherPlayer.CanClaimDiscardedTileToCompleteGroup(discardedTile, i == 1))
                {
                    actionsToClaimDiscardedTile[otherPlayerIndex] = TurnAction.Pass;
                    continue;
                }

                var action = otherPlayer.GetTurnActionAgainstDiscardedTile(discardedTile, turnPlayer);
                actionsToClaimDiscardedTile[otherPlayerIndex] = action;
            }

            return actionsToClaimDiscardedTile;
        }

        private void HandleClaimedDiscard(int indexOfStealingPlayer, Tile discardedTile, TileGrouping group)
        {
            var stealingPlayer = Round.GetPlayers()[indexOfStealingPlayer];

            WriteOpenGroupMessage(stealingPlayer, group);
            stealingPlayer.MakeGroupWithDiscardedTile(discardedTile, group);
            stealingPlayer.Hand.SortHand();
            if (group.IsQuad())
            {
                GiveTileAtParticularIndexToPlayer(stealingPlayer, 0);
                CheckForAndReplaceBonusTiles(stealingPlayer);
                if (stealingPlayer.Hand.IsWinningHand() && stealingPlayer.IsDeclaringWin())
                {
                    HandleWinningHand(stealingPlayer, stealingPlayer, 1);
                }
                int foo = 1;
                HandleQuads(stealingPlayer, ref foo);
                if (DealIsOver)
                {
                    return;
                }
            }
            IndexOfCurrentPlayer = indexOfStealingPlayer;
            // Console.ReadLine();
            WriteGameState();
            DiscardTile(stealingPlayer);
        }

        private bool CheckForAndReplaceBonusTiles(Player player)
        {
            var hadBonusTiles = false;
            while (player.Hand.UncalledTiles.Where(x => x is BonusTile).Any())
            {
                hadBonusTiles = true;
                var bonusTiles = new List<Tile>(player.Hand.UncalledTiles.OfType<BonusTile>());
                foreach (var tile in bonusTiles)
                {
                    player.Hand.UncalledTiles.Remove(tile);
                    player.Hand.BonusSets.Add(new TileGrouping { tile });
                    Console.WriteLine($"{player.Name} melds {tile}.");
                    if (player.Hand.BonusSets.Count == 7 && !player.Hand.UncalledTiles.Any(t => t is BonusTile))
                    {
                        WriteGameState($"{player.Name} has seven bonus tiles.");
                        if (player.IsDeclaringWin())
                        {
                            HKOSHandScorer.isBonusWin = true;
                            HandleWinningHand(player, player, 0);
                            return true;
                        }
                    }
                    else if (player.Hand.BonusSets.Count == 8)
                    {
                        WriteGameState($"{player.Name} has all eight bonus tiles.");
                        Console.ReadKey();
                        Console.WriteLine();
                        HKOSHandScorer.isBonusWin = true;
                        HandleWinningHand(player, player, 0);
                        return true;
                    }
                    GiveTileAtParticularIndexToPlayer(player, 0);
                }
                if (player.Hand.IsWinningHand() && player.IsDeclaringWin())
                {
                    HandleWinningHand(player, player, 1);
                    return true;
                }
            }
            return hadBonusTiles;
        }

        private void HandleQuads(Player player, ref int quadsMadeThisTurn)
        {
            while (player.Hand.ContainsQuad() && RemainingTiles.Count > 0)
            {
                var selectedQuad = player.GetOpenOrPromotedQuadMade();
                if (selectedQuad == null)
                {
                    return;
                }

                player.Hand.SortHand();
                var tileInQuad = selectedQuad.First();
                var quadIsMade = false;
                foreach (var group in player.Hand.CalledSets)
                {
                    if (group.IsTriplet() && group.First().Equals(tileInQuad))
                    {
                        var playerCount = Round.GetPlayers().Length;
                        var groupsToBeMadeWithDiscardedTile = PollPlayersForClaimingDiscardedTile(tileInQuad, player);

                        for (int i = 1; i < playerCount; i++)
                        {
                            var otherPlayerIndex = (i + IndexOfCurrentPlayer) % playerCount;
                            if (Round.GetPlayers()[otherPlayerIndex].
                                IsClaimingDiscardedTileToCompleteWinningHand(tileInQuad))
                            {
                                Round.GetPlayers()[otherPlayerIndex].Hand.UncalledTiles.Add(tileInQuad);
                                HandleWinningHand(Round.GetPlayers()[otherPlayerIndex], player, 1);
                                return;
                            }
                            Round.GetPlayers()[otherPlayerIndex].TilesSeenSinceLastTurn.Add(tileInQuad);
                        }
                        // player.TilesSeenSinceLastTurn.Add(tileInQuad);

                        group.Add(tileInQuad);
                        player.Hand.UncalledTiles.Remove(tileInQuad);
                        quadIsMade = true;
                        break;
                    }
                }
                if (!quadIsMade)
                {
                    player.Hand.CalledSets.Add(selectedQuad);
                    for (int i = 0; i < 4; i++)
                    {
                        player.Hand.UncalledTiles.Remove(tileInQuad);
                    }
                }
                WriteOpenGroupMessage(player, selectedQuad);
                quadsMadeThisTurn++;
                GiveTileAtParticularIndexToPlayer(player, 0);
                CheckForAndReplaceBonusTiles(player);
            }
        }

        private bool HandleTurnAction(Player player, bool canDeclareWinOrFormQuads, ref int quadsMadeThisTurn)
        {
            var turnAction = player.GetTurnAction(canDeclareWinOrFormQuads);

            switch (turnAction)
            {
                case TurnAction.FormQuad:
                    HandleQuads(player, ref quadsMadeThisTurn);
                    break;
                case TurnAction.DeclareWin:
                    HandleWinningHand(player, player, quadsMadeThisTurn);
                    return true;
                case TurnAction.CheckScores:
                    Console.WriteLine();
                    Round.Game.WriteScores();
                    break;
                case TurnAction.CheckPatterns:
                    Console.WriteLine();
                    HKOSScoringPatternConstants.WriteAllScoringData();
                    break;
                case TurnAction.Discard:
                    return DiscardTile(player);
                default:
                    break;
            }

            WriteGameStateWithoutPlayerData();
            // WriteLastDrawnTileData(player);
            Console.WriteLine();
            WriteAllPlayerData();
                    
            if (DealIsOver)
            {
                return true;
            }
            return false;
        }

        private void HandleWinningHand(Player winningPlayer, Player sourceOfWinningTile,
            int replacementTilesThisTurn)
        {
            Console.WriteLine($"{winningPlayer.Name} says \"MAHJONG.\"");
            winningPlayer.Hand.SortHand();
            Round.GetHandScorer().Hand = winningPlayer.Hand;
            Round.GetHandScorer().SetCircumstantialValues(winningPlayer, sourceOfWinningTile, this,
                replacementTilesThisTurn);
            Console.WriteLine($"{winningPlayer.Name} scores {Round.GetHandScorer().ScoreHand()} doubles.");
            var handPoints = Round.GetHandScorer().GetPointsFromHand();
            Round.GetHandScorer().WriteHandScoringPatterns();
            WriteRevealedWinningHand(winningPlayer);

            if (sourceOfWinningTile.Equals(winningPlayer))
            {
                winningPlayer.Points += 3 * handPoints;
                foreach (var otherPlayer in Round.GetPlayers())
                {
                    if (otherPlayer.Equals(winningPlayer))
                    {
                        continue;
                    }
                    otherPlayer.Points -= handPoints;
                }
            }
            else
            {
                winningPlayer.Points += 2 * handPoints;
                sourceOfWinningTile.Points -= 2 * handPoints;
            }
            if (winningPlayer.SeatWind.Equals(HonorType.East))
            {
                Round.DealerKeepCount++;
            }
            else
            {
                Round.DealerKeepCount = 0;
                Round.DealCount++;
                Round.Game.IncreaseDealerIndexAndCycleWinds();
            }
            Console.ReadKey();
            DealIsOver = true;
        }

        private int FindIndexOfPlayerWithPriorityToClaimDiscardedTile(TurnAction[] actionsToClaimDiscardedTile)
        {
            var currentPrioritizedClaim = TurnAction.Pass;
            int currentPrioritizedClaimingPlayerIndex = -1;
            var playerCount = Round.GetPlayers().Length;

            for (int i = 1; i < playerCount; i++)
            {
                var otherPlayerIndex = (IndexOfCurrentPlayer - i + playerCount) % playerCount;
                var currentAction = actionsToClaimDiscardedTile[otherPlayerIndex];

                switch (currentAction)
                {
                    case TurnAction.DeclareWin:
                        currentPrioritizedClaim = currentAction;
                        currentPrioritizedClaimingPlayerIndex = otherPlayerIndex;
                        break;
                    case TurnAction.FormQuad:
                    case TurnAction.FormTriplet:
                        if (currentPrioritizedClaim != TurnAction.DeclareWin)
                        {
                            currentPrioritizedClaim = currentAction;
                            currentPrioritizedClaimingPlayerIndex = otherPlayerIndex;
                        }
                        break;
                    case TurnAction.FormSequence:
                        if (currentPrioritizedClaim == TurnAction.Pass ||
                            currentPrioritizedClaim == TurnAction.FormSequence)
                        {
                            currentPrioritizedClaim = currentAction;
                            currentPrioritizedClaimingPlayerIndex = otherPlayerIndex;
                        }
                        break;
                    default:
                        break;
                }
            }

            return currentPrioritizedClaimingPlayerIndex;
        }

        public void WriteGameState()
        {
            // Console.Clear();
            WriteGameStateWithoutPlayerData();
            WriteAllPlayerData();
        }

        public void WriteGameState(string currentActionData)
        {
            // Console.Clear();
            Console.WriteLine("=====================================");
            WriteGameStateWithoutPlayerData();
            Console.WriteLine(currentActionData);
            Console.WriteLine();
            WriteAllPlayerData();
        }

        private void WriteGameStateWithoutPlayerData()
        {
            Console.WriteLine();
            Round.WriteRoundData();
            WriteDiscardedTiles();
            WriteCurrentTurnData();
        }

        private void WriteRevealedWinningHand(Player winningPlayer)
        {
            Console.WriteLine($"{winningPlayer.Name} wins the deal.\n");
            foreach (var player in Round.GetPlayers())
            {
                WritePlayerData(player, player is HumanPlayer || player.Equals(winningPlayer));
            }
        }

        private void WriteCurrentTurnData()
        {
            Console.WriteLine($"{RemainingTiles.Count} tiles remaining.");
            Console.WriteLine($"Player {IndexOfCurrentPlayer + 1}'s turn.\n");
        }

        private void WriteDiscardIndices(Player player)
        {
            PadLineWithWhiteSpacesToAlignWithMaximumNameLength();
            for (int i = 0; i < player.Hand.UncalledTiles.Count; i++)
            {
                Console.Write($" {i + 1} ");
                if (i < 9)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        private void WriteDiscardedTiles()
        {
            var discardedTilesText = "Discarded tiles: ";
            var maximumTilesPerDiscardedLine = (Console.WindowWidth - discardedTilesText.Length) / 4;
            var tilesWritten = 0;

            // DiscardedTiles = new SuitedHonorTileSorter().SortTiles(DiscardedTiles);
            Console.Write(discardedTilesText);
            foreach (var tile in DiscardedTiles)
            {
                if (tilesWritten == maximumTilesPerDiscardedLine)
                {
                    Console.WriteLine();
                    PadLineWithWhiteSpaces(discardedTilesText.Length);
                    tilesWritten = 0;
                }
                tile.WriteShortColoredString();
                tilesWritten++;
            }
            Console.WriteLine();
        }

        private void WriteAllPlayerData()
        {
            foreach (var player in Round.GetPlayers())
            {
                if (player.Equals(Round.GetPlayers()[IndexOfCurrentPlayer]))
                {
                    WriteDiscardIndices(player);
                }
                WritePlayerData(player, player is HumanPlayer);
            }
        }

        private void WritePlayerData(Player player, bool revealTiles)
        {
            Console.Write($"({player.SeatWind.ToString().Substring(0, 1)}) ");
            Console.Write($"{player.Name.PadRight(maximumNameLength).Substring(0, maximumNameLength)}: ");
            WriteTilesInPlayersHand(player, revealTiles);
            WriteTilesInPlayersCalledSets(player, revealTiles);
            WriteTilesInPlayersBonusSets(player);
        }

        private void WriteTilesInPlayersHand(Player player, bool revealTiles)
        {
            foreach (var tile in player.Hand.UncalledTiles)
            {
                if (revealTiles)
                {
                    tile.WriteShortColoredString();
                }
                else
                {
                    Console.Write("[??]");
                }
            }
            Console.WriteLine();
        }

        private void WriteTilesInPlayersCalledSets(Player player, bool revealTiles)
        {
            var tilesWritten = 0;

            PadLineWithWhiteSpacesToAlignWithMaximumNameLength();
            for (int i = 0; i < player.Hand.CalledSets.Count; i++)
            {
                if (tilesWritten + player.Hand.CalledSets[i].Count > maximumTilesWrittenPerLine)
                {
                    Console.WriteLine();
                    PadLineWithWhiteSpacesToAlignWithMaximumNameLength();
                    tilesWritten = 0;
                }
                if (player.Hand.CalledSets[i].IsQuad() && !player.Hand.CalledSets[i].IsOpenGroup)
                {
                    if (revealTiles)
                    {
                        Console.Write("[??]");
                        player.Hand.CalledSets[i].First().WriteShortColoredString();
                        player.Hand.CalledSets[i].First().WriteShortColoredString();
                        Console.Write("[??]");
                    }
                    else
                    {
                        Console.Write("[??]");
                        Console.Write("[??]");
                        Console.Write("[??]");
                        Console.Write("[??]");
                    }
                }
                else
                {
                    foreach (var tile in player.Hand.CalledSets[i])
                    {
                        tile.WriteShortColoredString();
                    }
                }
                if (i < player.Hand.CalledSets.Count - 1)
                {
                    WriteSpaceBetweenTiles();
                }
                tilesWritten += player.Hand.CalledSets[i].Count + 1;
            }
            Console.WriteLine();
        }

        private void WriteTilesInPlayersBonusSets(Player player)
        {
            PadLineWithWhiteSpacesToAlignWithMaximumNameLength();
            for (int i = 0; i < player.Hand.BonusSets.Count; i++)
            {
                foreach (var tile in player.Hand.BonusSets[i])
                {
                    tile.WriteShortColoredString();
                }
                if (i < player.Hand.BonusSets.Count - 1)
                {
                    WriteSpaceBetweenTiles();
                }
            }
            Console.WriteLine();
        }

        private void WriteOpenGroupMessage(Player player, TileGrouping groupFormed)
        {
            if (groupFormed.IsQuad())
            {
                Console.WriteLine($"{player.Name} says \"GONG.\"");
            }
            else if (groupFormed.IsTriplet())
            {
                Console.WriteLine($"{player.Name} says \"PUNG.\"");
            }
            else if (groupFormed.IsSequence())
            {
                Console.WriteLine($"{player.Name} says \"CHOW.\"");
            }
        }

        private void WriteLastDrawnTileData(Player player)
        {
            var drawnTileData = player is HumanPlayer ?
                    player.Hand.UncalledTiles[player.Hand.UncalledTiles.Count - 1].ToString() : "a tile";
            Console.WriteLine($"{player.Name} draws {drawnTileData}.");
        }

        private void PadLineWithWhiteSpacesToAlignWithMaximumNameLength()
        {
            PadLineWithWhiteSpaces(maximumNameLength + 6);
        }

        private void WriteSpaceBetweenTiles()
        {
            PadLineWithWhiteSpaces(4);
        }

        private void PadLineWithWhiteSpaces(int whiteSpaceCount)
        {
            for (int i = 0; i < whiteSpaceCount; i++)
            {
                Console.Write(" ");
            }
        }
    }
}

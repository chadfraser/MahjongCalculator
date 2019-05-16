using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class Deal
    {
        public Deal(Game game, IList<Tile> tiles)
        {
            Game = game;
            RemainingTiles = new List<Tile>(tiles);
            DiscardedTiles = new List<Tile>();
            IndexOfCurrentPlayer = 0;
            DealIsOver = false;
        }

        public Game Game { get; set; }

        private IList<Tile> RemainingTiles { get; set; }

        public IList<Tile> DiscardedTiles { get; private set; }

        private int IndexOfCurrentPlayer { get; set; }

        private bool DealIsOver { get; set; }

        public void PlayDeal()
        {
            DealInitialHands();
            while (!DealIsOver)
            {
                PlayTurn();
            }
        }

        private void DealInitialHands()
        {
            ShuffleTiles();
            for (int timesToDrawInitialSetOfTiles = 0; timesToDrawInitialSetOfTiles < 1; timesToDrawInitialSetOfTiles++)
            {
                foreach (var player in Game.Players)
                {
                    for (int amountOfInitialTilesToDraw = 0; amountOfInitialTilesToDraw < 12; amountOfInitialTilesToDraw++)
                    {
                        GiveNextTileToPlayer(player);
                    }
                }
            }
            foreach (var player in Game.Players)
            {
                GiveNextTileToPlayer(player);
            }
            //Game.Players[0].Hand.UncalledTiles = new List<Tile>{
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
            //    TileInstance.RedDragon
            //};
            foreach (var player in Game.Players)
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
                //Game.Stalemate()
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
            WriteGameState();
        }

        private void GiveNextTileToPlayer(Player player)
        {
            GiveTileAtParticularIndexToPlayer(player, RemainingTiles.Count - 1);
        }

        private void PlayTurn()
        {
            var turnPlayer = Game.Players.ElementAt(IndexOfCurrentPlayer);
            GiveNextTileToPlayer(turnPlayer);
            CheckForAndReplaceBonusTiles(turnPlayer);
            if (DealIsOver)
            {
                return;
            }

            while (turnPlayer.Hand.ContainsQuad() && RemainingTiles.Count > 0)
            {
                var selectedQuad = turnPlayer.GetOpenOrPromotedQuadMade();
                if (selectedQuad == null)
                {
                    break;
                }
                var tileInQuad = selectedQuad.First();
                var quadIsMade = false;
                foreach (var group in turnPlayer.Hand.CalledSets)
                {
                    if (group.IsTriplet() && group.First().Equals(tileInQuad))
                    {
                        // check for discard
                        group.Add(tileInQuad);
                        turnPlayer.Hand.UncalledTiles.Remove(tileInQuad);
                        quadIsMade = true;
                        GiveNextTileToPlayer(turnPlayer);
                    }
                }
                if (!quadIsMade)
                {
                    turnPlayer.Hand.CalledSets.Add(selectedQuad);
                    for (int i = 0; i < 4; i++)
                    {
                        turnPlayer.Hand.UncalledTiles.Remove(tileInQuad);
                    }
                    GiveNextTileToPlayer(turnPlayer);
                }
            }

            // while can Kan and tiles in deck (chankan)
            // if is Kan, else break
            if (turnPlayer.Hand.IsWinningHand() && turnPlayer.IsDeclaringWin())
            {
                // score hand
                DealIsOver = true;
            }
            else
            {
                DiscardTile(turnPlayer);
            }
        }

        private void DiscardTile(Player turnPlayer)
        {
            var discardedTile = turnPlayer.ChooseTileToDiscard();
            turnPlayer.Hand.SortHand();
            Console.WriteLine($"{turnPlayer.Name} discards {discardedTile}.");

            var playerCount = Game.Players.Length;
            var groupsToBeMadeWithDiscardedTile = PollPlayersForClaimingDiscardedTile(discardedTile, turnPlayer);
            //, out int indexOfFirstPlayerToClaimForWin, out int indexOfFirstPlayerToClaimForTripletOrQuad);

            for (int i = 1; i < playerCount; i++)
            {
                var otherPlayerIndex = (i + IndexOfCurrentPlayer) % playerCount;
                var currentGroup = groupsToBeMadeWithDiscardedTile[otherPlayerIndex];

                if (currentGroup != null && currentGroup.Equals(new TileGrouping()))
                {
                    // Game.Players[indexOfFirstPlayerToClaimForWin].Win(discardedTile)
                    // score hand, adjust points, end game
                    Console.WriteLine("\"MAHJONG.\"");
                    DealIsOver = true;
                    return;
                }
            }
            if (RemainingTiles.Count == 0)
            {
                //Game.Stalemate()
                DealIsOver = true;
                return;
            }
            for (int i = 1; i < playerCount ; i++)
            {
                var otherPlayerIndex = (i + IndexOfCurrentPlayer) % playerCount;
                var currentGroup = groupsToBeMadeWithDiscardedTile[otherPlayerIndex];

                if (currentGroup != null && (currentGroup.IsTriplet() || currentGroup.IsQuad()))
                {
                    Game.Players[otherPlayerIndex].MakeGroupWithDiscardedTile(discardedTile, currentGroup);
                    if (currentGroup.IsQuad())
                    {
                        GiveTileAtParticularIndexToPlayer(Game.Players[otherPlayerIndex], 0);
                    }
                    IndexOfCurrentPlayer = otherPlayerIndex;
                    // Console.ReadLine();
                    WriteGameState();
                    DiscardTile(Game.Players[IndexOfCurrentPlayer]);
                    return;
                }
            }
            for (int i = 1; i < playerCount; i++)
            {
                var otherPlayerIndex = (i + IndexOfCurrentPlayer) % playerCount;
                var currentGroup = groupsToBeMadeWithDiscardedTile[otherPlayerIndex];

                if (currentGroup != null)
                {
                    Game.Players[otherPlayerIndex].MakeGroupWithDiscardedTile(discardedTile, currentGroup);
                    IndexOfCurrentPlayer = otherPlayerIndex;
                    // Console.ReadLine();
                    WriteGameState();
                    DiscardTile(Game.Players[IndexOfCurrentPlayer]);
                    return;
                }
            }

            DiscardedTiles.Add(discardedTile);
            IndexOfCurrentPlayer = (IndexOfCurrentPlayer + 1) % playerCount;
            // Console.ReadLine();
        }

        private TileGrouping[] PollPlayersForClaimingDiscardedTile(Tile discardedTile, Player turnPlayer)
            //, out int indexOfFirstPlayerToClaimForWin, out int indexOfFirstPlayerToClaimForTripletOrQuad)
        {
            var playerCount = Game.Players.Length;
            var groupsToBeMadeWithDiscardedTile = new TileGrouping[playerCount];
            //indexOfFirstPlayerToClaimForWin = -1;
            //indexOfFirstPlayerToClaimForTripletOrQuad = -1;

            for (int i = 1; i < playerCount; i++)
            {
                var otherPlayerIndex = (i + IndexOfCurrentPlayer) % playerCount;
                if (Game.Players[otherPlayerIndex] == turnPlayer)
                {
                    continue;
                }

                if (!Game.Players[otherPlayerIndex].CanClaimDiscardedTileToCompleteWinningHand(discardedTile) &&
                    !Game.Players[otherPlayerIndex].CanClaimDiscardedTileToCompleteGroup(discardedTile, i == 1))
                {
                    groupsToBeMadeWithDiscardedTile[otherPlayerIndex] = null;
                    continue;
                }

                var groupMade = Game.Players[otherPlayerIndex].
                    GetGroupMadeWithDiscardedTileOrEmptyGroupForWin(discardedTile, turnPlayer);
                groupsToBeMadeWithDiscardedTile[otherPlayerIndex] = groupMade;

                //if (groupMade == new TileGrouping() && indexOfFirstPlayerToClaimForWin == -1)
                //{
                //    indexOfFirstPlayerToClaimForWin = otherPlayerIndex;
                //}
                //else if ((groupMade.IsTriplet() || groupMade.IsQuad()) && indexOfFirstPlayerToClaimForTripletOrQuad == -1)
                //{
                //    indexOfFirstPlayerToClaimForTripletOrQuad = otherPlayerIndex;
                //}
            }

            return groupsToBeMadeWithDiscardedTile;
        }

        private void CheckForAndReplaceBonusTiles(Player player)
        {
            while (player.Hand.UncalledTiles.Where(x => x is BonusTile).Any())
            {
                var bonusTiles = new List<Tile>(player.Hand.UncalledTiles.OfType<BonusTile>());
                foreach (var tile in bonusTiles)
                {
                    player.Hand.UncalledTiles.Remove(tile);
                    player.Hand.BonusSets.Add(new TileGrouping { tile });
                    Console.WriteLine($"{player.Name} melds {tile}.");
                    if (player.Hand.BonusSets.Count >= 7)
                    {
                        //Console.WriteLine($"{player.Name} has seven bonus tiles.");
                        if (player.IsDeclaringWin())
                        {
                            // score win
                            DealIsOver = true;
                            return;
                        }
                    }
                    GiveTileAtParticularIndexToPlayer(player, 0);
                }
            }
        }

        public void WriteGameState()
        {
            Console.Clear();
            Console.WriteLine();
            Game.WriteGameState();
            Console.Write($"Discarded tiles:");
            DiscardedTiles = new SuitedHonorTileSorter().SortTiles(DiscardedTiles);
            foreach (var tile in DiscardedTiles)
            {
                tile.WriteShortColoredString();
            }
            Console.WriteLine();
            Console.WriteLine($"{RemainingTiles.Count} tiles remaining.");
            Console.WriteLine($"Player {IndexOfCurrentPlayer + 1}'s turn.\n");
            foreach (var player in Game.Players)
            {
                Console.Write($"{player.Name.PadRight(6).Substring(0, 6)}: ");
                foreach (var tile in player.Hand.UncalledTiles)
                {
                    if (player is Player)
                    //if (player is HumanPlayer)
                        {
                        tile.WriteShortColoredString();
                    }
                    else
                    {
                        Console.Write("[??]");
                    }
                }
                Console.WriteLine();
                if (player.Hand.CalledSets.Sum(group => group.Count()) < 16)
                {
                    Console.Write("    ");

                }
                Console.Write("    ");

                for (int i = 0; i < player.Hand.CalledSets.Count; i++)
                {
                    foreach (var tile in player.Hand.CalledSets[i])
                    {
                        tile.WriteShortColoredString();
                    }
                    if (i < player.Hand.CalledSets.Count - 1)
                    {
                        Console.Write("    ");
                    }
                }
                Console.WriteLine();
                Console.Write("        ");

                for (int i = 0; i < player.Hand.BonusSets.Count; i++)
                {
                    foreach (var tile in player.Hand.BonusSets[i])
                    {
                        tile.WriteShortColoredString();
                    }
                    if (i < player.Hand.BonusSets.Count - 1)
                    {
                        Console.Write("    ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

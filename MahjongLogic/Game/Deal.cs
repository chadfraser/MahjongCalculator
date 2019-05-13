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
            RemainingTiles = tiles;
            DiscardedTiles = new List<Tile>();
            IndexOfCurrentPlayer = 0;
        }

        public Game Game { get; set; }

        public IList<Tile> RemainingTiles { get; set; }

        public IList<Tile> DiscardedTiles { get; set; }

        private int IndexOfCurrentPlayer { get; set; }

        public void DealInitialHands()
        {
            ShuffleTiles();
            for (int timesToDrawInitialSetOfTiles = 0; timesToDrawInitialSetOfTiles < 3; timesToDrawInitialSetOfTiles++)
            {
                foreach (var player in Game.Players)
                {
                    for (int amountOfInitialTilesToDraw = 0; amountOfInitialTilesToDraw < 4; amountOfInitialTilesToDraw++)
                    {
                        player.DrawTile(RemainingTiles);
                    }
                }
            }
            foreach (var player in Game.Players)
            {
                CheckForAndReplaceBonusTiles(player);
            }
        }

        private void ShuffleTiles()
        {
            var tiles = new List<Tile>(RemainingTiles);
            var rnd = new Random();
            RemainingTiles = tiles.OrderBy(item => rnd.Next()).ToList();
        }

        public void TakeTurn()
        {
            var turnPlayer = Game.Players.ElementAt(IndexOfCurrentPlayer);
            turnPlayer.Hand.SortHand();
            turnPlayer.DrawTile(RemainingTiles);
            WriteGameState();

            CheckForAndReplaceBonusTiles(turnPlayer);

            // turnPlayer.ReadInput();
            if (turnPlayer.Hand.IsWinningHand())
            {
                // 
            }
            else
            {
                // choose to discard
            }
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
                    player.DrawTile(RemainingTiles);
                }
            }
        }

        public void WriteGameState()
        {
            Console.WriteLine();
            Game.WriteGameState();
            Console.WriteLine($"{RemainingTiles.Count} tiles remaining.");
            Console.WriteLine($"Player {IndexOfCurrentPlayer + 1}'s turn.\n");
            foreach (var player in Game.Players)
            {
                var name = player is HumanPlayer ? "Player" : "COM";
                Console.Write($"{name.PadRight(6).Substring(0, 6)}: ");
                foreach (var tile in player.Hand.UncalledTiles)
                {
                    tile.WriteShortColoredString();
                }
                Console.WriteLine();
                Console.Write("        ");

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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class HKOSDeal
    {
        // public HKOSGame Game { get; set; }
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
                        //player.DrawTile(RemainingTiles);
                    }
                }
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
            //turnPlayer.DrawTile(RemainingTiles);

            CheckForAndReplaceBonusTiles(turnPlayer);

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
                var bonusTiles = player.Hand.UncalledTiles.OfType<BonusTile>();
                foreach (var tile in bonusTiles)
                {
                    player.Hand.UncalledTiles.Remove(tile);
                    // player.Hand.BonusTiles.Add(tile);
                    //player.DrawTile(RemainingTiles);
                }
            }

        }
    }
}

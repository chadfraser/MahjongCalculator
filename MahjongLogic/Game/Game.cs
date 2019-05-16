using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong   
{
    public class Game
    {
        public Game()
        {
            RoundWind = HonorType.East;
            DealCount = 1;
            DealerKeepCount = 0;
            AllTiles = new List<Tile>(TileInstance.AllMainTileInstancesFourOfEachTilePlusBonusTiles);
            CurrentDeal = new Deal(this, AllTiles);
            Players = new Player[] { new HumanPlayer(this), new HumanPlayer(this), new HumanPlayer(this), new HumanPlayer(this) };
        }

        public Player[] Players { get; set; }
        // Round CurrentRound { get; set; }

        public Deal CurrentDeal { get; set; }

        public HonorType RoundWind { get; set; }

        public int DealCount { get; set; }

        public int DealerKeepCount { get; set; }

        private IList<Tile> AllTiles { get; set; }

        public void WriteGameState()
        {
            Console.WriteLine($"{RoundWind} {DealCount}, {DealerKeepCount} dealer keeps.");
        }

        public void Stalemate()
        {
            DealerKeepCount++;
            CurrentDeal = new Deal(this, AllTiles);
            CurrentDeal.PlayDeal();
        }
    }
}

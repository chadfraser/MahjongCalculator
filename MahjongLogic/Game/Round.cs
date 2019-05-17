using System;
using System.Collections.Generic;

namespace Fraser.Mahjong
{
    public class Round
    {
        public Round(Game game, IList<Tile> allTiles, HonorType roundWind)
        {
            Game = game;
            RoundWind = roundWind;
            AllTiles = allTiles;
            DealCount = 1;
            DealerKeepCount = 0;
        }

        public Round(Game game, IList<Tile> allTiles) : this(game, allTiles, HonorType.East)
        { }

        public Game Game{ get; set; }

        public HonorType RoundWind { get; set; }

        public int DealCount { get; set; }

        public int DealerKeepCount { get; set; }

        public Deal CurrentDeal { get; set; }

        public IList<Tile> AllTiles { get; set; }

        public void PlayRound()
        {
            foreach (var player in Game.Players)
            {
                player.Hand.UncalledTiles.Clear();
                player.Hand.CalledSets.Clear();
                player.Hand.BonusSets.Clear();
            }
            CurrentDeal = new Deal(this, AllTiles);
            CurrentDeal.PlayDeal();
        }

        public void Stalemate()
        {
            DealerKeepCount++;
            CurrentDeal = new Deal(this, AllTiles);
            CurrentDeal.PlayDeal();
        }

        public void WriteRoundData()
        {
            Console.WriteLine($"{RoundWind} {DealCount}, {DealerKeepCount} dealer keeps.");
        }

        public Player[] GetPlayers()
        {
            return Game.Players;
        }

        public HKOSHandScorer GetHandScorer()
        {
            return Game.HandScorer;
        }
    }
}

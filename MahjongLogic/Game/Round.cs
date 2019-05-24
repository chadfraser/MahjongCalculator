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
            while (DealCount < 5)
            {
                foreach (var player in Game.Players)
                {
                    player.Hand.UncalledTiles.Clear();
                    player.Hand.CalledSets.Clear();
                    player.Hand.BonusSets.Clear();
                    player.Hand.IsOpen = false;
                    player.Hand.BestWayToParseHand = null;
                    player.TilesSeenSinceLastTurn.Clear();
                }
                CurrentDeal = new Deal(this, AllTiles);
                CurrentDeal.PlayDeal();
                Console.WriteLine();
                Game.WriteScores();
                Console.WriteLine("\n\n================================================\n");
            }
        }

        public void HandleWin(Player winningPlayer)
        {
            if (winningPlayer.SeatWind.Equals(HonorType.East))
            {
                DealerKeepCount++;
            }
            else
            {
                DealerKeepCount = 0;
                DealCount++;
                Game.IncreaseDealerIndexAndCycleWinds();
            }
        }

        public void Stalemate()
        {
            DealerKeepCount++;
            CurrentDeal = new Deal(this, AllTiles);
            CurrentDeal.PlayDeal();
        }

        public void WriteRoundData()
        {
            var suffix = DealerKeepCount == 1 ? "." : "s.";
            Console.WriteLine($"{RoundWind} {DealCount}, {DealerKeepCount} dealer keep{suffix}");
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

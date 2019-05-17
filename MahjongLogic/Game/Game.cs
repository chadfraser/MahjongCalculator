using System;
using System.Collections.Generic;

namespace Fraser.Mahjong   
{
    public class Game
    {
        private readonly int numberOfRounds = 4;
        private readonly HonorType[] orderedWinds = new HonorType[]
        {
            HonorType.East,
            HonorType.South,
            HonorType.West,
            HonorType.North
        };

        public Game()
        {
            Players = new Player[]
            {
                new HumanPlayer(this, "Player", HonorType.East),
                new EfficientAI(this, "COM 1", HonorType.South),
                new EfficientAI(this, "COM 2", HonorType.West),
                new EfficientAI(this, "COM 3", HonorType.North)
            };

            AllTiles = new List<Tile>(TileInstance.AllMainTileInstancesFourOfEachTilePlusBonusTiles);
            HandScorer = new HKOSHandScorer();
            DealerIndex = 0;
        }

        public Player[] Players { get; private set; }

        public Round CurrentRound { get; set; }

        public int DealerIndex { get; set; }

        public HKOSHandScorer HandScorer { get; set; }

        private IList<Tile> AllTiles { get; set; }

        public void PlayGame()
        {
            for (int i = 0; i < numberOfRounds; i++)
            {
                CurrentRound = new Round(this, AllTiles, orderedWinds[i]);
                CurrentRound.PlayRound();
            }
        }

        public void WriteScores()
        {
            foreach (var player in Players)
            {
                Console.Write($"{player.Name.PadRight(Deal.maximumNameLength).Substring(0, Deal.maximumNameLength)} ");
                for (int i = 0; i < Deal.maximumNameLength + 2 - player.Name.Length; i++)
                {
                    Console.Write($"-");
                }
                Console.Write($" {player.Points}");
            }
            Console.ReadKey();
        }

        public void IncreaseDealerIndexAndCycleWinds()
        {
            DealerIndex = (DealerIndex + 1) % Players.Length;
            for (int i = 0; i < Players.Length; i++)
            {
                Players[(DealerIndex + i) % Players.Length].SeatWind = orderedWinds[i];
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong
{
    public abstract class Player
    {
        public Player(Game game)
        {
            Game = Game;
            Hand = new HKOSHand();
            SeatWind = HonorType.East;
        }

        public Game Game { get; set; }

        public HKOSHand Hand { get; set; }

        public HonorType SeatWind { get; set; }

        public abstract void ClaimDiscardedTile(Tile discardedTile);

        public abstract void ChooseTileToDiscard();

        public abstract void DeclareWin(Tile winningTile);

        public void DrawTile(IList<Tile> deck)
        {
            if (deck.Count == 0)
            {
                //Game.Stalemate()
            }
            Hand.UncalledTiles.Add(deck[deck.Count - 1]);
            deck.RemoveAt(deck.Count - 1);
        }

        public Tile DiscardTile(int discardIndex)
        {
            var discardedTile = Hand.UncalledTiles[discardIndex];
            Hand.UncalledTiles.RemoveAt(discardIndex);
            return discardedTile;
        }
    }
}

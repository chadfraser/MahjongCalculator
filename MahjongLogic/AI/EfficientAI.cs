using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong
{
    public class EfficientAI : Player
    {
        public EfficientAI(Game game) : base(game)
        {
            EfficientDrawsFinder = new EfficientDrawsFinder();
            WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
        }

        public IEfficientDrawsFinder EfficientDrawsFinder { get; set; }

        public IWaitingDistanceFinder WaitingDistanceFinder{ get; set; }

        public override void ChooseTileToDiscard()
        {
            throw new NotImplementedException();
        }

        public override void ClaimDiscardedTile(Tile discardedTile)
        {
            throw new NotImplementedException();
        }

        public override void DeclareWin(Tile winningTile)
        {
            throw new NotImplementedException();
        }

        public void OnOpponentDiscard(Tile discardedTile)
        {
            var tilesInHandIfCallsTile = new List<Tile>(Hand.UncalledTiles)
            {
                discardedTile
            };
            if (WaitingDistanceFinder.GetWaitingDistance(tilesInHandIfCallsTile) == -1)
            {
                Console.WriteLine("\"Mahjong.\"");
            }
        }

        public void OnTurn()
        {
            if (WaitingDistanceFinder.GetWaitingDistance(Hand.UncalledTiles) == -1)
            {
                Console.WriteLine("\"Mahjong.\"");
            }
            else
            {
                // discard worst tile
            }
        }
    }
}

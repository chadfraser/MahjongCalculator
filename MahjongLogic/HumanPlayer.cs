using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong
{
    class HumanPlayer : Player
    {
        public HumanPlayer(Game game) : base(game)
        {

        }

        public override void ChooseTileToDiscard()
        {
            throw new NotImplementedException();
        }

        public override void ClaimDiscardedTile(Tile discardedTile)
        {
            throw new NotImplementedException();
        }

        public override void DeclareWin(Tile discardedTile)
        {
            throw new NotImplementedException();
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}

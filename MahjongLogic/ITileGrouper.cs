using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong
{
    interface ITileGrouper
    {
        bool CanGroupTilesIntoLegalHand(IList<Tile> tiles);

        IList<IList<TileGrouping>> FindAllWaysToGroupTiles(IList<Tile> tiles);

        IList<IList<TileGrouping>> FindAllWaysToGroupTilesAfterRemovingAPair(IList<Tile> tiles);

        IList<TileGrouping> FindAllGroupsInTiles(IList<Tile> tiles);
    }
}

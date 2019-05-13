using System.Collections.Generic;

namespace Fraser.Mahjong
{
    public interface ITileGrouper
    {
        bool CanGroupTilesIntoLegalHand(IList<Tile> tiles);

        IList<IList<TileGrouping>> FindAllWaysToGroupTiles(IList<Tile> tiles);

        IList<IList<TileGrouping>> FindAllWaysToGroupTilesAfterRemovingAPair(IList<Tile> tiles);

        IList<IList<TileGrouping>> FindAllWaysToFullyGroupTilesAfterRemovingAPair(IList<Tile> tiles);

        IList<TileGrouping> FindAllGroupsInTiles(IList<Tile> tiles);
    }
}

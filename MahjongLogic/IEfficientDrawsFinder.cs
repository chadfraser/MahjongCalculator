using System.Collections.Generic;

namespace Fraser.Mahjong
{
    public interface IEfficientDrawsFinder
    {
        int GetEfficientDrawCount(IList<Tile> tiles);

        int GetEfficientDrawCountWithSeenTiles(IList<Tile> tilesInHand, IList<Tile> seenTiles);

        //IList<Tile> GetListOfEfficientDraws(IList<Tile> tiles);

        //Dictionary<IList<Tile>, int> GetDictOfEfficientDrawsAndCount
    }
}

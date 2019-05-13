using System.Collections.Generic;

namespace Fraser.Mahjong
{
    public interface IEfficientDrawsFinder
    {
        int GetEfficientDrawCount(IList<Tile> tiles);

        //IList<Tile> GetListOfEfficientDraws(IList<Tile> tiles);

        //Dictionary<IList<Tile>, int> GetDictOfEfficientDrawsAndCount
    }
}

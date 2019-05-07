﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong
{
    public interface ITileSorter
    {
        IEnumerable<Tile> SortTiles(List<Tile> tiles);
    }
}
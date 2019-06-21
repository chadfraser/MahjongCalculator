using System;

namespace Fraser.Mahjong
{
    class Program
    {
        static void Main(string[] args)
        {
            TileInstance.InitializeTileShorthandDict();
            var a = new HandParser();

            a.GetHandString();
        }
    }
}

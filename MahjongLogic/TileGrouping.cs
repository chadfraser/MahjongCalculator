using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class TileGrouping : ICollection<Tile>
    {
        private readonly List<Tile> tiles;

        public TileGrouping(params Tile[] tileParams)
        {
            tiles = tileParams.ToList();
            tiles.Sort();
            IsOpenGroup = false;
        }

        public TileGrouping()
        {
            tiles = new List<Tile>();
            IsOpenGroup = false;
        }

        public bool IsOpenGroup { get; set; }

        public bool IsGroup()
        {
            var firstTile = tiles.FirstOrDefault();
            return !(firstTile is null) && firstTile.IsGroup(tiles.ToArray());
        }

        public bool IsPair()
        {
            var firstTile = tiles.FirstOrDefault();
            return !(firstTile is null) && firstTile.IsPair(tiles.ToArray());
        }

        public bool IsSequence()
        {
            var firstTile = tiles.FirstOrDefault();
            return !(firstTile is null) && firstTile.IsSequence(tiles.ToArray());
        }

        public bool IsTriplet()
        {
            var firstTile = tiles.FirstOrDefault();
            return !(firstTile is null) && firstTile.IsTriplet(tiles.ToArray());
        }

        public bool IsQuad()
        {
            var firstTile = tiles.FirstOrDefault();
            return !(firstTile is null) && firstTile.IsQuad(tiles.ToArray());
        }

        public bool IsBonus()
        {
            var firstTile = tiles.FirstOrDefault();
            return !(firstTile is null) && tiles.First().GetType() == typeof(BonusTile) && tiles.Count == 1;
        }

        public int Count { get => tiles.Count; }

        public bool IsReadOnly => false;

        public void Add(Tile item)
        {
            tiles.Add(item);
        }

        public void Clear()
        {
            tiles.Clear();
        }

        public bool Contains(Tile item)
        {
            return tiles.Contains(item);
        }

        public void CopyTo(Tile[] array, int arrayIndex)
        {
            tiles.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            return tiles.GetEnumerator();
        }

        public bool Remove(Tile item)
        {
            return tiles.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tiles.GetEnumerator();
        }

        public override string ToString()
        {
            var text = "";
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                text = $"{text} {tiles[i].GetShortName()},";
            }
            text = $"{text} {tiles[tiles.Count - 1].GetShortName()}";
            return text;
        }

        public override bool Equals(Object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TileGrouping t = (TileGrouping)obj;
                return tiles.OrderBy(tile => tile).SequenceEqual(t.tiles.OrderBy(tile => tile));
            }
        }

        public override int GetHashCode()
        {
            const int baseHash = 19073;
            const int hashFactor = 91423;

            int hash = baseHash;
            foreach (var tile in tiles)
            {
                hash = (hash * hashFactor) ^ tile.GetHashCode();
            }
            return hash;
        }
    }
}
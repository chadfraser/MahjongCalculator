using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mahjong
{
    public class TileGrouping : ICollection<Tile>
    {
        private List<Tile> tiles;

        public TileGrouping(params Tile[] tileParams)
        {
            tiles = tileParams.ToList();
        }

        public TileGrouping()
        {
            tiles = new List<Tile>();
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

        public override bool Equals(Object obj)
        {
            if ((obj is null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TileGrouping t = (TileGrouping)obj;
                return (tiles.OrderBy(tile => tile.Suit)).SequenceEqual(t.tiles.OrderBy(tile => tile.Suit));
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

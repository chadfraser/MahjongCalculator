using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fraser.Mahjong
{
    public abstract class Player
    {
        public Player(Game game, string name)
        {
            Name = name;
            Game = game;
            Hand = new HKOSHand();
            SeatWind = HonorType.East;
            TileGrouper = new SequenceTripletQuadTileGrouper(new SuitedHonorBonusTileSorter());
            WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
        }

        public IWaitingDistanceFinder WaitingDistanceFinder { get; set; }

        public string Name { get; set; }

        public Game Game { get; set; }

        public HKOSHand Hand { get; set; }

        public HonorType SeatWind { get; set; }

        public ITileGrouper TileGrouper { get; set; }

        public abstract Tile ChooseTileToDiscard();

        public abstract bool IsClaimingDiscardedTileToCompleteGroup(Tile discardedTile, bool canBeSequence);

        public abstract bool IsClaimingDiscardedTileToCompleteWinningHand(Tile discardedTile);

        public abstract TileGrouping GetGroupMadeWithDiscardedTileOrEmptyGroupForWin(Tile discardedTile,
            Player discardingPlayer);

        public abstract bool IsDeclaringWin();

        public abstract TileGrouping GetOpenOrPromotedQuadMade();

        public Tile DiscardTile(int discardIndex)
        {
            var discardedTile = Hand.UncalledTiles[discardIndex];
            Hand.UncalledTiles.RemoveAt(discardIndex);
            return discardedTile;
        }

        public bool CanClaimDiscardedTileToCompleteGroup(Tile discardedTile, bool canBeSequence)
        {
            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };
            var groupsInvolvingDiscardedTile = TileGrouper.FindAllGroupsInTiles(potentialHandTiles)
                .Where(group => group.Contains(discardedTile)).ToList();
            if (!canBeSequence)
            {
                groupsInvolvingDiscardedTile = groupsInvolvingDiscardedTile.Where(group => !group.IsSequence()).ToList();
            }

            return groupsInvolvingDiscardedTile.Count > 0;
        }

        public bool CanClaimDiscardedTileToCompleteWinningHand(Tile discardedTile)
        {
            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };

            return WaitingDistanceFinder.GetWaitingDistance(potentialHandTiles) == -1;
        }

        public void MakeGroupWithDiscardedTile(Tile discardedTile, TileGrouping group)
        {
            group.IsOpenGroup = true;
            Hand.CalledSets.Add(group);
            Hand.UncalledTiles.Add(discardedTile);
            Hand.IsOpen = true;
            foreach (var tile in group)
            {
                Hand.UncalledTiles.Remove(tile);
            }
        }

        public IList<TileGrouping> GetAllGroupsThatCanBeMadeWithDiscardedTile(Tile discardedTile, bool canBeSequence)
        {
            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };
            var groupsInvolvingDiscardedTile = TileGrouper.FindAllGroupsInTiles(potentialHandTiles)
                .Where(group => group.Contains(discardedTile)).ToList();
            if (!canBeSequence)
            {
                groupsInvolvingDiscardedTile = groupsInvolvingDiscardedTile.Where(group => !group.IsSequence()).ToList();
            }

            return groupsInvolvingDiscardedTile;
        }
    }
}

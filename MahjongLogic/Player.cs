using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fraser.Mahjong
{
    public abstract class Player
    {
        public Player(Game game, string name) : this(game, name, HonorType.East)
        {
        }

        public Player(Game game, string name, HonorType seatWind)
        {
            Name = name;
            Game = game;
            Hand = new HKOSHand();
            SeatWind = seatWind;
            Points = 0;
            TilesSeenSinceLastTurn = new HashSet<Tile>();
            TileGrouper = new SequenceTripletQuadTileGrouper(new SuitedHonorBonusTileSorter());
            WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
        }

        public string Name { get; set; }

        public Game Game { get; set; }

        public HKOSHand Hand { get; set; }

        public HonorType SeatWind { get; set; }

        public ISet<Tile> TilesSeenSinceLastTurn { get; set; }

        public int Points { get; set; }

        protected TileGrouping RecentlySelectedTileGrouping { get; set; }

        public ITileGrouper TileGrouper { get; set; }

        public IWaitingDistanceFinder WaitingDistanceFinder { get; set; }

        public abstract TurnAction GetTurnAction(bool canDeclareWin);

        public abstract int ChooseIndexOfTileToDiscard();

        public abstract bool IsClaimingDiscardedTileToCompleteGroup(Tile discardedTile, bool canBeSequence);

        public abstract bool IsClaimingDiscardedTileToCompleteWinningHand(Tile discardedTile);

        public abstract TileGrouping GetGroupMadeWithDiscardedTile(Tile discardedTile, TurnAction typeOfGroup);

        public abstract TurnAction GetTurnActionAgainstDiscardedTile(Tile discardedTile, Player discardingPlayer);

        public abstract bool IsDeclaringWin();

        public abstract TileGrouping GetClosedOrPromotedQuadMade();

        public Tile DiscardTile(int discardIndex)
        {
            var discardedTile = Hand.UncalledTiles[discardIndex];
            Hand.UncalledTiles.RemoveAt(discardIndex);
            TilesSeenSinceLastTurn.Clear();
            Hand.SortHand();
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
            if (TilesSeenSinceLastTurn.Contains(discardedTile))
            {
                return false;
            }
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

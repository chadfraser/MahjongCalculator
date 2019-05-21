using System;
using System.Collections.Generic;
using System.Linq;

namespace Fraser.Mahjong
{
    public class EfficientAI : Player
    {
        public EfficientAI(Game game, string name, HonorType seatWind) : base(game, name, seatWind)
        {
            EfficientDrawsFinder = new AppliedHKOSEfficientDrawsFinder();
            WaitingDistanceFinder = new RegularHandSevenPairsThirteenOrphansWaitingDistanceFinder();
            SeenTiles = new List<Tile>();
        }

        public EfficientAI(Game game, HonorType seatWind) : this(game, "COM", seatWind)
        {
        }

        public EfficientAI(Game game) : this(game, "COM", HonorType.East)
        {
        }

        public IEfficientDrawsFinder EfficientDrawsFinder { get; set; }

        public List<Tile> SeenTiles { get; set; }

        private void UpdateSeenTiles()
        {
            SeenTiles.Clear();
            SeenTiles.AddRange(Hand.UncalledTiles);
            SeenTiles.AddRange(Game.CurrentRound.CurrentDeal.DiscardedTiles);
            foreach (var player in Game.Players)
            {
                foreach (var group in player.Hand.CalledSets)
                {
                    if (!group.IsOpenGroup && !player.Equals(this))
                    {
                        continue;
                    }
                    SeenTiles.AddRange(group);
                }
                foreach (var group in player.Hand.BonusSets)
                {
                    SeenTiles.AddRange(group);
                }
            }
        }

        public override TurnAction GetTurnAction(bool canDeclareWin)
        {
            if (canDeclareWin && Hand.IsWinningHand())
            {
                return TurnAction.DeclareWin;
            }
            else
            {
                return TurnAction.Discard;
            }
        }

        public override int ChooseIndexOfTileToDiscard()
        {
            UpdateSeenTiles();
            var indexOfTileToDiscard = Hand.UncalledTiles.Count - 1;
            var currentWaitingDistance = WaitingDistanceFinder.GetWaitingDistance(Hand.UncalledTiles);
            var bestEfficientDrawCount = 0;
            for (int i = 0; i < Hand.UncalledTiles.Count; i++)
            {
                var remainingTiles = new List<Tile>(Hand.UncalledTiles);
                remainingTiles.RemoveAt(i);
                if (WaitingDistanceFinder.GetWaitingDistance(remainingTiles) > currentWaitingDistance)
                {
                    continue;
                }

                var newEfficientDrawCount = EfficientDrawsFinder.GetEfficientDrawCountWithSeenTiles(remainingTiles,
                    SeenTiles);

                if (newEfficientDrawCount > bestEfficientDrawCount) {
                    bestEfficientDrawCount = newEfficientDrawCount;
                    indexOfTileToDiscard = i;
                }
            }
            return indexOfTileToDiscard;
        }

        public TileGrouping ChooseGroupToMakeWithDiscardedTile(Tile discardedTile, bool canBeSequence)
        {
            UpdateSeenTiles();
            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };
            var groupsInvolvingDiscardedTile = GetAllGroupsThatCanBeMadeWithDiscardedTile(discardedTile, canBeSequence);

            var minimumWaitingDistance = WaitingDistanceFinder.GetWaitingDistance(Hand.UncalledTiles);
            var maximumTileEfficiency = EfficientDrawsFinder.GetEfficientDrawCountWithSeenTiles(Hand.UncalledTiles,
                SeenTiles);
            TileGrouping idealGroup = null;

            foreach (var group in groupsInvolvingDiscardedTile)
            {
                var remainingTiles = new List<Tile>(potentialHandTiles);
                foreach (var tile in group)
                {
                    remainingTiles.Remove(tile);
                }

                var newWaitingDistance = WaitingDistanceFinder.GetWaitingDistance(remainingTiles);
                var newTileEfficiency = EfficientDrawsFinder.GetEfficientDrawCountWithSeenTiles(remainingTiles,
                    SeenTiles);
                if (newWaitingDistance < minimumWaitingDistance || 
                    (newWaitingDistance == minimumWaitingDistance && newTileEfficiency > maximumTileEfficiency))
                {
                    minimumWaitingDistance = newWaitingDistance;
                    maximumTileEfficiency = newTileEfficiency;
                    idealGroup = group;
                }
            }
            return idealGroup;
        }

        public override bool IsClaimingDiscardedTileToCompleteGroup(Tile discardedTile, bool canBeSequence)
        {
            UpdateSeenTiles();
            var potentialHandTiles = new List<Tile>(Hand.UncalledTiles) { discardedTile };
            var groupsInvolvingDiscardedTile = GetAllGroupsThatCanBeMadeWithDiscardedTile(discardedTile, canBeSequence);

            var currentWaitingDistance = WaitingDistanceFinder.GetWaitingDistance(Hand.UncalledTiles);
            var currentTileEfficiency = EfficientDrawsFinder.GetEfficientDrawCountWithSeenTiles(Hand.UncalledTiles,
                SeenTiles);
            foreach (var group in groupsInvolvingDiscardedTile)
            {
                var remainingTiles = new List<Tile>(potentialHandTiles);
                foreach (var tile in group)
                {
                    remainingTiles.Remove(tile);
                }
                var newWaitingDistance = WaitingDistanceFinder.GetWaitingDistance(remainingTiles);
                if (newWaitingDistance < currentWaitingDistance ||
                    (newWaitingDistance == currentWaitingDistance && 
                    EfficientDrawsFinder.GetEfficientDrawCountWithSeenTiles(remainingTiles, SeenTiles)
                        > currentTileEfficiency))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool IsClaimingDiscardedTileToCompleteWinningHand(Tile discardedTile)
        {
            //return false;
            var uncalledTilesWithClaimedDiscard = new List<Tile>(Hand.UncalledTiles)
            {
                discardedTile
            };
            return WaitingDistanceFinder.GetWaitingDistance(uncalledTilesWithClaimedDiscard) == -1;
        }

        public override TileGrouping GetGroupMadeWithDiscardedTile(Tile discardedTile, TurnAction typeOfGroup)
        {
            return RecentlySelectedTileGrouping;
        }

        public override TurnAction GetTurnActionAgainstDiscardedTile(Tile discardedTile, Player discardingPlayer)
        {
            RecentlySelectedTileGrouping = null;
            var indexInPlayerList = Array.IndexOf(Game.Players, this);
            var arrayLength = Game.Players.Count();
            var moduloPreviousPlayerIndex = (indexInPlayerList - 1 + arrayLength) % arrayLength;
            var isNextInTurnOrder = Game.Players[moduloPreviousPlayerIndex] == discardingPlayer;
            if (IsClaimingDiscardedTileToCompleteWinningHand(discardedTile))
            {
                return TurnAction.DeclareWin;
            }
            if (IsClaimingDiscardedTileToCompleteGroup(discardedTile, isNextInTurnOrder))
            {
                RecentlySelectedTileGrouping = ChooseGroupToMakeWithDiscardedTile(discardedTile, isNextInTurnOrder);
                var action = RecentlySelectedTileGrouping.IsTriplet() ? TurnAction.FormTriplet :
                    (RecentlySelectedTileGrouping.IsQuad() ? TurnAction.FormQuad :
                    (RecentlySelectedTileGrouping.IsSequence() ? TurnAction.FormSequence : TurnAction.Pass));
                return action;
            }
            return TurnAction.Pass;
        }

        public override bool IsDeclaringWin()
        {
            //return false;
            return Hand.IsWinningHand();
        }

        public override TileGrouping GetOpenOrPromotedQuadMade()
        {
            return null;
        }
    }
}

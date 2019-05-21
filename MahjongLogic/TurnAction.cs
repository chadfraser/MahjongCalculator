using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong
{
    public enum TurnAction
    {
        DeclareWin,
        Discard,
        FormSequence,
        FormTriplet,
        FormQuad,
        CheckScores,
        CheckPatterns,
        Pass
    }
}

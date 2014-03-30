using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCVDemo1
{
    class Constants
    {
        internal const string PositionFolderName = @"\Positions\";
        internal const int GRID_SIZE = 8;

        internal const string WhiteRook = "WR";
        internal const string WhiteBishop = "WB";
        internal const string WhiteKnight = "WN";
        internal const string WhiteKing = "WK";
        internal const string WhiteQueen = "WQ";
        internal const string WhitePawn1 = "WP";
        internal const string WhitePawn2 = "WP";
        internal const string EmptyGridZone1 = "WE1";
        internal const string EmptyGridZone2 = "WE2";

        internal const string BlackRook = "BR";
        internal const string BlackBishop = "BB";
        internal const string BlackKnight = "BN";
        internal const string BlackKing = "BK";
        internal const string BlackQueen = "BQ";
        internal const string BlackPawn1 = "BP";
        internal const string BlackPawn2 = "BP";

        internal static string ActiveMove = string.Empty;
        internal const string WhiteMove = "w";
        internal const string BlackMove = "b";
    }
}

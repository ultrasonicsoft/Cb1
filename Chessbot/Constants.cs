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

        internal const string WhiteRook_Code = "R";
        internal const string WhiteBishop_Code = "B";
        internal const string WhiteKnight_Code = "N";
        internal const string WhiteKing_Code = "K";
        internal const string WhiteQueen_Code = "Q";
        internal const string WhitePawn_Code = "P";

        internal const string BlackRook = "BR";
        internal const string BlackBishop = "BB";
        internal const string BlackKnight = "BN";
        internal const string BlackKing = "BK";
        internal const string BlackQueen = "BQ";
        internal const string BlackPawn1 = "BP";
        internal const string BlackPawn2 = "BP";

        internal const string BlackRook_Code = "r";
        internal const string BlackBishop_Code = "b";
        internal const string BlackKnight_Code = "n";
        internal const string BlackKing_Code = "k";
        internal const string BlackQueen_Code = "q";
        internal const string BlackPawn_Code = "p";

        internal const string WhiteMove = "w";
        internal const string BlackMove = "b";
        internal static string ActiveMove = WhiteMove;

        internal static int FullmoveNumber = 0;
        internal static int HalfmoveClock = 0;

        internal const string TEMPLATE_CATELOG_FILE = @"TemplateCatalog.txt";
        internal const string TEMPLATE_PATH = @"Template";
        internal const string TEMPLATE_EXTENSION = @".bin";
        internal const string TEMPLATE_EXTENSION_SEARCH = @"*.bin";

        //internal const double STANDARD_IMAGE_COMPARISON_FACTOR = 0.75;
        internal const double MARKER_IMAGE_COMPARISON_FACTOR = 0.50;
        internal const double IMAGE_RESIZE_FACTOR = 0.50;

        internal const string STOCKFISHENGINE = "stockfishengine.exe";

        internal const string INVALID_FEN_STRING = "8/8/8/8/8/8/8/8";

        //modifiers
        public const int NOMOD = 0x0000;
        public const int ALT = 0x0001;
        public const int CTRL = 0x0002;
        public const int SHIFT = 0x0004;
        public const int WIN = 0x0008;

        //windows message id for hotkey
        public const int WM_HOTKEY_MSG_ID = 0x0312;

        internal const string TEMPLATE_SETTING_FILE_NAME = "{0}_Settings.bin";
    }

}

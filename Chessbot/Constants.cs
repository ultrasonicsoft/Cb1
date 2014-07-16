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

        internal const string ENGINE_SETTING_FILE_NAME = "Engine_Settings.bin";

        //internal const string UPDATE_CONTEMP_FACTOR_TYPE = "setoption name Contempt Factor value {0}";
        //internal const string UPDATE_MIN_SPLIT_DEPTH = "setoption name Min Split Depth value {0}";
        //internal const string UPDATE_THREADS = "setoption name Threads value {0}";
        //internal const string UPDATE_HASH = "setoption name Hash value {0}";
        //internal const string CLEAR_HASH = "setoption name Clear Hash";
        //internal const string UPDATE_MULTI_PV = "setoption name MultiPV value {0}";
        //internal const string UPDATE_PONDER = "setoption name Ponder value {0}";
        //internal const string UPDATE_SKILL_LEVEL = "setoption name Skill Level value {0}";
        //internal const string UPDATE_EMG_MOVE_HORIZON = "setoption name Emergency Move Horizon value {0}";
        //internal const string UPDATE_EMG_BASE_TIME = "setoption name Emergency Base Time value {0}";
        //internal const string UPDATE_EMG_MOVE_TIME = "setoption name Emergency Move Time value {0}";
        //internal const string UPDATE_EMG_THINKING_TIME = "setoption name Emergency Thinking Time value {0}";
        //internal const string UPDATE_SLOW_MOVER = "setoption name Emergency Thinking Time value {0}";
        //internal const string UPDATE_UC_CHESS960 = "setoption name UCI_Chess960 value {0}";

        internal const string UPDATE_COMMAND = "setoption name {0} value {1}";
        internal const string CLEAR_HASH = "setoption name Clear Hash";

        internal const string UPDATE_CONTEMP_FACTOR_TYPE = "Contempt Factor";
        internal const string UPDATE_MIN_SPLIT_DEPTH = "Min Split Depth";
        internal const string UPDATE_THREADS = "Threads";
        internal const string UPDATE_HASH = "Hash";
        internal const string UPDATE_MULTI_PV = "MultiPV";
        internal const string UPDATE_PONDER = "Ponder";
        internal const string UPDATE_SKILL_LEVEL = "Skill Level";
        internal const string UPDATE_EMG_MOVE_HORIZON = "Emergency Move Horizon";
        internal const string UPDATE_EMG_BASE_TIME = "Emergency Base Time";
        internal const string UPDATE_EMG_MOVE_TIME = "Emergency Move Time";
        internal const string UPDATE_EMG_THINKING_TIME = "Minimum Thinking Time";
        internal const string UPDATE_SLOW_MOVER = "Slow Mover";
        internal const string UPDATE_UC_CHESS960 = "UCI_Chess960";
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace OpenCVDemo1
{
    internal class ChessPiece
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Image<Gray, Byte> Piece { get; set; }
    }

    internal class ChessEntity
    {
        public ChessPiece PieceInfo { get; set; }
        public int RowPosition { get; set; }
        public int ColumnPosition { get; set; }
        public bool IsAlive { get; set; }
    }

}

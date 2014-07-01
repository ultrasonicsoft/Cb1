using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCVDemo1
{
    public partial class PreviewTemplate : Form
    {
        public Image ChessBoardImage { get; set; }
        public int Padding { get; set; }
        public PreviewTemplate()
        {
            InitializeComponent();
        }

        private void PreviewTemplate_Load(object sender, EventArgs e)
        {
            try
            {
                int width = ChessBoardImage.Width;
                int height = ChessBoardImage.Height;

                Bitmap bmpChessboard = new Bitmap(ChessBoardImage);

                int blockWidth = (width / Constants.GRID_SIZE);
                int blockHeight = (height / Constants.GRID_SIZE);
                int blockLeft = 0;
                int blockTop = 0;
                Rectangle r = new Rectangle();

                if (blockHeight < blockWidth)
                    blockHeight = blockWidth;
                else
                    blockWidth = blockHeight;

                lblChessbotWidth.Text = ChessBoardImage.Width.ToString();
                lblChessbotHeight.Text = ChessBoardImage.Height.ToString();

                lblPadding.Text = Padding.ToString();

                bool pieceSizeDisplayed = false;
                for (int rowIndex = 1; rowIndex <= Constants.GRID_SIZE; rowIndex++)
                {
                    blockLeft = 0;
                    for (int colIndex = 1; colIndex <= Constants.GRID_SIZE; colIndex++)
                    {
                        r = new Rectangle(blockLeft + Padding, blockTop + Padding, blockWidth - Padding, blockHeight - Padding);
                        //r = new Rectangle(blockLeft, blockTop, blockWidth, blockHeight);
                        
                        using (Bitmap currentPiece = bmpChessboard.Clone(r, System.Drawing.Imaging.PixelFormat.DontCare))
                        {
                            if (pieceSizeDisplayed == false)
                            {
                                lblPieceWidth.Text = currentPiece.Width.ToString();
                                lblPieceHeight.Text = currentPiece.Height.ToString();
                            }
                            SetPictureBoxImage(currentPiece, rowIndex, colIndex);
                            blockLeft += blockWidth;
                        }
                    }
                    blockTop += blockHeight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetPictureBoxImage(Bitmap currentPiece, int rowIndex, int colIndex)
        {
            System.IO.Stream imageStream = new System.IO.MemoryStream();
            currentPiece.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //bitmap.Save("screen.jpg", ImageFormat.Jpeg);
            Image imgPiece = Image.FromStream(imageStream); ;

            switch (rowIndex)
            {
                case 1:
                    SetFirstRow(imgPiece, colIndex);
                    break;
                case 2:
                    SetSecondRow(imgPiece, colIndex);
                    break;
                case 3:
                    SetThirdRow(imgPiece, colIndex);
                    break;
                case 4:
                    SetFourthRow(imgPiece, colIndex);
                    break;
                case 5:
                    SetFifthRow(imgPiece, colIndex);
                    break;
                case 6:
                    SetSixthRow(imgPiece, colIndex);
                    break;
                case 7:
                    SetSeventhRow(imgPiece, colIndex);
                    break;
                case 8:
                    SetEighthRow(imgPiece, colIndex);
                    break;
                default:
                    break;
            }
        }

        private void SetFirstRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a1.Image = currentPiece;
                    break;
                case 2:
                    b1.Image = currentPiece;
                    break;
                case 3:
                    c1.Image = currentPiece;
                    break;
                case 4:
                    d1.Image = currentPiece;
                    break;
                case 5:
                    e1.Image = currentPiece;
                    break;
                case 6:
                    f1.Image = currentPiece;
                    break;
                case 7:
                    g1.Image = currentPiece;
                    break;
                case 8:
                    h1.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }
        private void SetSecondRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a2.Image = currentPiece;
                    break;
                case 2:
                    b2.Image = currentPiece;
                    break;
                case 3:
                    c2.Image = currentPiece;
                    break;
                case 4:
                    d2.Image = currentPiece;
                    break;
                case 5:
                    e2.Image = currentPiece;
                    break;
                case 6:
                    f2.Image = currentPiece;
                    break;
                case 7:
                    g2.Image = currentPiece;
                    break;
                case 8:
                    h2.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }
        private void SetThirdRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a3.Image = currentPiece;
                    break;
                case 2:
                    b3.Image = currentPiece;
                    break;
                case 3:
                    c3.Image = currentPiece;
                    break;
                case 4:
                    d3.Image = currentPiece;
                    break;
                case 5:
                    e3.Image = currentPiece;
                    break;
                case 6:
                    f3.Image = currentPiece;
                    break;
                case 7:
                    g3.Image = currentPiece;
                    break;
                case 8:
                    h3.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }
        private void SetFourthRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a4.Image = currentPiece;
                    break;
                case 2:
                    b4.Image = currentPiece;
                    break;
                case 3:
                    c4.Image = currentPiece;
                    break;
                case 4:
                    d4.Image = currentPiece;
                    break;
                case 5:
                    e4.Image = currentPiece;
                    break;
                case 6:
                    f4.Image = currentPiece;
                    break;
                case 7:
                    g4.Image = currentPiece;
                    break;
                case 8:
                    h4.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }
        private void SetFifthRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a5.Image = currentPiece;
                    break;
                case 2:
                    b5.Image = currentPiece;
                    break;
                case 3:
                    c5.Image = currentPiece;
                    break;
                case 4:
                    d5.Image = currentPiece;
                    break;
                case 5:
                    e5.Image = currentPiece;
                    break;
                case 6:
                    f5.Image = currentPiece;
                    break;
                case 7:
                    g5.Image = currentPiece;
                    break;
                case 8:
                    h5.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }
        private void SetSixthRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a6.Image = currentPiece;
                    break;
                case 2:
                    b6.Image = currentPiece;
                    break;
                case 3:
                    c6.Image = currentPiece;
                    break;
                case 4:
                    d6.Image = currentPiece;
                    break;
                case 5:
                    e6.Image = currentPiece;
                    break;
                case 6:
                    f6.Image = currentPiece;
                    break;
                case 7:
                    g6.Image = currentPiece;
                    break;
                case 8:
                    h6.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }
        private void SetSeventhRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a7.Image = currentPiece;
                    break;
                case 2:
                    b7.Image = currentPiece;
                    break;
                case 3:
                    c7.Image = currentPiece;
                    break;
                case 4:
                    d7.Image = currentPiece;
                    break;
                case 5:
                    e7.Image = currentPiece;
                    break;
                case 6:
                    f7.Image = currentPiece;
                    break;
                case 7:
                    g7.Image = currentPiece;
                    break;
                case 8:
                    h7.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }
        private void SetEighthRow(Image currentPiece, int colIndex)
        {
            switch (colIndex)
            {
                case 1:
                    a8.Image = currentPiece;
                    break;
                case 2:
                    b8.Image = currentPiece;
                    break;
                case 3:
                    c8.Image = currentPiece;
                    break;
                case 4:
                    d8.Image = currentPiece;
                    break;
                case 5:
                    e8.Image = currentPiece;
                    break;
                case 6:
                    f8.Image = currentPiece;
                    break;
                case 7:
                    g8.Image = currentPiece;
                    break;
                case 8:
                    h8.Image = currentPiece;
                    break;
                default:
                    break;
            }
        }

       

       

     

    

      

      

       
    }
}

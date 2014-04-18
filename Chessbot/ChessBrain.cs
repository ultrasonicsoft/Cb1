using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace OpenCVDemo1
{
    public partial class ChessBrain : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public ChessBrain()
        {
            InitializeComponent();
        }

        private void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            ImageProcessingManager.TakeScreenShot();
            CaptureChessBoard chessBoard = new CaptureChessBoard();
            chessBoard.CapturedScreen = Image.FromFile("screen.jpg");
            chessBoard.Show();
            //pictureBox1.Image = img;
        }

        private void btnSplitImae_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Console.WriteLine("Reading current Chess position...");
            int paddingPixel = int.Parse(txtPadding.Text);
            if (rbtnWhite.Checked)
            {
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("white.png"), 5, rbtnWhite.Checked);
                ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("test.png"), paddingPixel, rbtnWhite.Checked);
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("inprogress.PNG"), paddingPixel, rbtnWhite.Checked);
                ImageProcessingManager.PrintChessBoard(rbtnWhite.Checked);
                ImageProcessingManager.PrepareFenString();
            }
            else
            {
                ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("black.png"), 5, rbtnWhite.Checked);
                ImageProcessingManager.PrintChessBoard(rbtnWhite.Checked);
                ImageProcessingManager.PrepareFenString();
            }
            Cursor = Cursors.Default;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> template = new Image<Gray, byte>("A-1.jpg");
            //Image<Gray, Byte> template = new Image<Gray, byte>("1.jpg");
            Image<Gray, Byte> input_Image = new Image<Gray, byte>("after.jpg");
            bool result = ImageProcessingManager.AreImagesSame(template, input_Image);

            //long matchTime = 0;
            //Image<Bgr, Byte> resultImage = ImageProcessingManager.Draw(template, input_Image, out matchTime);
            //int i = 0;
            if (result)
            {
                MessageBox.Show("matched");
            }
            else
            {
                MessageBox.Show("not matched");
            }
        }

        private void btnReadMasterTemplate_Click(object sender, EventArgs e)
        {
            //this.SetDesktopLocation(Left+20, this.Top);

            Cursor = Cursors.WaitCursor;
            Console.WriteLine("Reading master template...");
            int paddingPixel = int.Parse(txtPadding.Text);
            if (rbtnWhite.Checked)
            {
                //ImageProcessingManager.FillMasterTemplate(Image.FromFile("white.png"), paddingPixel, rbtnWhite.Checked);
                ImageProcessingManager.FillMasterTemplate(Image.FromFile("croppedTemplate.png"), paddingPixel, rbtnWhite.Checked);
                //ImageProcessingManager.FillMasterTemplate(Image.FromFile("test.jpg"), paddingPixel, rbtnWhite.Checked);
                //ImageProcessingManager.PrintChessBoard();
            }
            else
                ImageProcessingManager.FillMasterTemplate(Image.FromFile("black.png"), 5, rbtnWhite.Checked);
            Cursor = Cursors.Default;
            Console.WriteLine("Done!");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
            int frm_width = this.Width;
            int frm_height = this.Height;
            System.Windows.Forms.Screen src = System.Windows.Forms.Screen.PrimaryScreen;
            int src_height = src.Bounds.Height;
            int src_width = src.Bounds.Width;
            this.Left = ((src_width - frm_width) / 2) + 100;
            //this.Top = (src_height - frm_height) / 2;
            //Console.WriteLine("Jai Ganesh");
        }


        private void rbtnWhite_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnWhite.Checked)
                Constants.ActiveMove = Constants.WhiteMove;
            else
                Constants.ActiveMove = Constants.BlackMove;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (int.TryParse(textBox1.Text, out result))
                Constants.HalfmoveClock = result;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (int.TryParse(textBox2.Text, out result))
                Constants.FullmoveNumber = result;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char delete = (char)8;
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != delete;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char delete = (char)8;
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != delete;
        }

        private void btnShowImage_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> normalizedMasterImage = new Image<Gray, Byte>("inprogress.PNG");
            CvInvoke.cvShowImage("Current Image under use...", normalizedMasterImage);
        }
    }
}

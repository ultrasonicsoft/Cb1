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
        public ChessBrain()
        {
            InitializeComponent();
        }

        private void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            ImageProcessingManager.TakeScreenShot();
            Image img = Image.FromFile("screen.jpg");
            //pictureBox1.Image = img;
        }

        private void btnSplitImae_Click(object sender, EventArgs e)
        {
            int paddingPixel = int.Parse(txtPadding.Text);
            
            if (rbtnWhite.Checked)
            {
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("white.png"), 5, rbtnWhite.Checked);
                ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("inprogress.PNG"), paddingPixel, rbtnWhite.Checked);
                ImageProcessingManager.PrintChessBoard();
`                
                ImageProcessingManager.PrepareFenString();
            }
            else
                ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("black.png"), 5, rbtnWhite.Checked);
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
            int paddingPixel = int.Parse(txtPadding.Text);
            if (rbtnWhite.Checked)
            {
                ImageProcessingManager.FillMasterTemplate(Image.FromFile("white.png"), paddingPixel, rbtnWhite.Checked);
                //ImageProcessingManager.PrintChessBoard();
            }
            else
                ImageProcessingManager.FillMasterTemplate(Image.FromFile("black.png"), 5, rbtnWhite.Checked);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
            //Console.WriteLine("Jai Ganesh");
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void rbtnWhite_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnWhite.Checked)
                Constants.ActiveMove = Constants.WhiteMove;
            else
                Constants.ActiveMove = Constants.BlackMove;

        }        
    }
}

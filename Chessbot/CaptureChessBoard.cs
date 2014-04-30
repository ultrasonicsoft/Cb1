﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace OpenCVDemo1
{
    public partial class CaptureChessBoard : Form
    {
        private Rectangle croprect = Rectangle.Empty;
        private bool mouseDown = false;
        private Point sp, ep;
        private bool isGetXEnabled = false;
        private bool isGetYEnabled = false;
        private List<TemplateEntity> allLoadedTemplates = new List<TemplateEntity>();
        private TemplateEntity selectedChessEntity = null;

        private ChessTemplate masterTemplate = null;
        public Rectangle ScreenBoardCoordinates { get; set; }
        public Rectangle TriggerCoordinates { get; set; }

        public Image CapturedScreen { get; set; }
        public static Image CapturedBoard { get; set; }
        public CaptureChessBoard()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void CaptureChessBoard_Load(object sender, EventArgs e)
        {
            AllocConsole();

            CapturedScreen = ImageProcessingManager.TakeScreenShot();
            pbScreen.Image = CapturedScreen;

            pbScreen.MouseDown += new MouseEventHandler(Crop_MouseDown);
            pbScreen.MouseUp += new MouseEventHandler(Crop_MouseUp);
            pbScreen.MouseMove += new MouseEventHandler(Crop_MouseMove);
            pbScreen.Paint += new PaintEventHandler(Crop_Paint);

            // reduce flickering
            this.DoubleBuffered = true;

            LoadTemplates();
        }

        private void LoadTemplates()
        {
            //var allTemplate = Directory.EnumerateFiles(ImageProcessingManager.TemplatePath, Constants.TEMPLATE_EXTENSION_SEARCH);
            //cmbTemplates.Items.Clear();
            //foreach (string template in allTemplate)
            //{
            //    cmbTemplates.Items.Add( Path.GetFileNameWithoutExtension(template));
            //}

            string tempalteCatalogFileName = ImageProcessingManager.TemplatePath + Constants.TEMPLATE_CATELOG_FILE;
            if (File.Exists(tempalteCatalogFileName) == false)
            {
                MessageBox.Show("There are no template present. Please create and save them.", "Load Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            var allTemplate = File.ReadAllLines(tempalteCatalogFileName);
            allLoadedTemplates = new List<TemplateEntity>();
            cmbTemplates.Items.Clear();

            foreach (string templateEntry in allTemplate)
            {
                TemplateEntity newTemplate = new TemplateEntity();
                var templateParts = templateEntry.Split(',');
                newTemplate.TemplateName = templateParts[0];
                newTemplate.WebsiteURL = templateParts[1];
                newTemplate.TemplateFileName = templateParts[2];
                allLoadedTemplates.Add(newTemplate);

                cmbTemplates.Items.Add(newTemplate.TemplateName + " <-> " + newTemplate.WebsiteURL);
            }

        }
        private void Crop_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Red, GetRectangle(sp, ep));
            e.Graphics.Save();
        }

        private void Crop_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                ep = e.Location;
                pbScreen.Invalidate();
            }
            lblCurrentMouseX.Text = e.X.ToString();
            lblCurrentMouseY.Text = e.Y.ToString();

            if (isGetXEnabled)
            {
                txtLeft.Text = e.X.ToString();
                txtTop.Text = e.Y.ToString();
            }
            if (isGetYEnabled)
            {
                txtRight.Text = e.X.ToString();
                txtBottom.Text = e.Y.ToString();
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            ep = e.Location;
            mouseDown = false;
            croprect = GetRectangle(sp, ep);

            if (croprect.Width > 10 && croprect.Height > 10)
            {
                //selectedArea = true;
            }
            else
            {
                croprect = Rectangle.Empty;
            }
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                this.Close();
            else
            {
                mouseDown = true;
                sp = ep = e.Location;
            }

            if (isGetXEnabled)
            {
                isGetXEnabled = false;
            }
            if (isGetYEnabled)
            {
                isGetYEnabled = false;
            }
        }

        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y)
                );
        }

        private void btnCropBoard_Click(object sender, EventArgs e)
        {
            try
            {
                CropChessBoard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CropChessBoard()
        {
            if (croprect.IsEmpty)
                return;
            ScreenBoardCoordinates = new Rectangle();
            ScreenBoardCoordinates = croprect;

            pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);

            CapturedScreen = pbScreen.Image;
            ClearSelection();

            //txtResizeLeft.Text = "0";
            //txtResizeTop.Text = "0";
            txtWidth.Text = CapturedScreen.Width.ToString();
            txtHeight.Text = CapturedScreen.Height.ToString();

            //cbAutoRefresh.Checked = false;
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        private void ClearSelection()
        {
            sp = new Point(0, 0);
            ep = new Point(0, 0);
            croprect = Rectangle.Empty;
            pbScreen.Invalidate();
        }
        private void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            Image img = (Image)ImageProcessingManager.TakeScreenShot();
            CapturedScreen = img;
            pbScreen.Image = img;
            //CaptureChessBoard chessBoard = new CaptureChessBoard();
            //chessBoard.CapturedScreen = Image.FromFile("screen.jpg");
            //chessBoard.ShowDialog();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lblCurrentMouseX.Text = string.Empty;
            lblCurrentMouseY.Text = string.Empty;
            txtLeft.Text = string.Empty;
            txtRight.Text = string.Empty;
            txtTop.Text = string.Empty;
            txtBottom.Text = string.Empty;
            isGetXEnabled = false;
            isGetYEnabled = false;

            sp = new Point(0, 0);
            ep = new Point(0, 0);
            croprect = Rectangle.Empty;
            pbScreen.Image = null;
            ScreenBoardCoordinates = Rectangle.Empty;
        }

        private void btnCropUsingCoordinates_Click(object sender, EventArgs e)
        {
            int left = 0;
            int top = 0;
            int right = 0;
            int bottom = 0;
            int.TryParse(txtLeft.Text, out left);
            int.TryParse(txtTop.Text, out top);
            int.TryParse(txtRight.Text, out right);
            int.TryParse(txtBottom.Text, out bottom);
            int width = Math.Abs(right - left);
            int height = Math.Abs(bottom - top);

            sp = new Point(left, top);
            ep = new Point(right, bottom);
            croprect = new Rectangle(left, top, width, height);
            //pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);

            pbScreen.Invalidate();
        }
        private void btnGetX_Click(object sender, EventArgs e)
        {
            isGetXEnabled = true;
        }

        private void btnGetY_Click(object sender, EventArgs e)
        {
            isGetYEnabled = true;
        }

        private void ValidateBoard()
        {
            string message = string.Empty;
            if (pbScreen.Image.Width != pbScreen.Image.Height && pbScreen.Image.Width % 8 != 0 && pbScreen.Image.Height % 8 != 0)
            {
                int requiredSize = pbScreen.Image.Width / 8;
                 message = string.Format("The cropped chess board is not even. Make sure it is perfect square. Try resizing to Width x Height :{0} x {1}", requiredSize.ToString());
            }
            else
            {
                message = "Great!!! Let's play...";
            }
            MessageBox.Show(message);
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            int left = 0;
            int top = 0;
            int width = 0;
            int height = 0;

            try
            {

                left = int.Parse(txtResizeLeft.Text);
                top = int.Parse(txtResizeTop.Text);
                width = int.Parse(txtWidth.Text);
                height = int.Parse(txtHeight.Text);

                croprect = new Rectangle(left, top, width, height);
                pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);
                CapturedScreen = pbScreen.Image;

                ClearSelection();

                ValidateBoard();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            PreviewTemplate preview = new PreviewTemplate();
            var binaryImage = (ImageProcessingManager.GetBinaryImage(CapturedScreen,trackBar1.Value).Bitmap).Clone(new Rectangle(0,0,CapturedScreen.Width,CapturedScreen.Height), CapturedScreen.PixelFormat);

            //preview.ChessBoardImage = CapturedScreen;
            preview.ChessBoardImage = binaryImage;
            preview.Show();
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            FrmSaveTemplate saveTemplate = new FrmSaveTemplate();
            saveTemplate.ChessBoard = pbScreen.Image;
            saveTemplate.Padding = int.Parse(txtPadding.Text);
            saveTemplate.IsWhiteFirst = rbtnWhite.Checked;
            saveTemplate.ShowDialog();
        }

        private void btnScanAgain_Click(object sender, EventArgs e)
        {
            croprect = ScreenBoardCoordinates;
            Image img = (Image)ImageProcessingManager.TakeScreenShot();
            CapturedScreen = img;
            pbScreen.Image = img;

            try
            {
                ScreenBoardCoordinates = new Rectangle();
                ScreenBoardCoordinates = croprect;

                pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);

                CapturedScreen = pbScreen.Image;
                ClearSelection();

                //txtResizeLeft.Text = "0";
                //txtResizeTop.Text = "0";
                txtWidth.Text = CapturedScreen.Width.ToString();
                txtHeight.Text = CapturedScreen.Height.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            TemplateEntity selectedEntity = allLoadedTemplates[cmbTemplates.SelectedIndex];
            if (selectedEntity != null)
            {
                Console.WriteLine("Reading template " + selectedEntity.TemplateName);
               masterTemplate = ImageProcessingManager.ReadTemplate(selectedEntity.TemplateFileName);

                MessageBox.Show("Template successfully loaded..", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Reading template DONE!");
            }
        }

        private void btnShowBoardConfiguration_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Console.WriteLine("Reading current Chess position...");
            int paddingPixel = int.Parse(txtPadding.Text);
            if (rbtnWhite.Checked)
            {
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("white.png"), 5, rbtnWhite.Checked);
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("test.png"), paddingPixel, rbtnWhite.Checked);
                ImageProcessingManager.ReadChessBoardCurrentPosition(pbScreen.Image, paddingPixel, rbtnWhite.Checked);
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

        private void btnShowTemplate_Click(object sender, EventArgs e)
        {
            Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> normalizedMasterImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(masterTemplate.CurrentTemplateImage as Bitmap);
            Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", normalizedMasterImage);
        }

        private void timerAutoRefresh_Tick(object sender, EventArgs e)
        {
            if (cbAutoRefresh.Checked)
            {
                CapturedScreen = ImageProcessingManager.TakeScreenShot();
                pbScreen.Image = CapturedScreen;
                CropChessBoard();
                croprect = ScreenBoardCoordinates;
            }
            pbPreview.Image = ImageProcessingManager.TakeScreenShot();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnMarkTrigger_Click(object sender, EventArgs e)
        {
            if (croprect.IsEmpty)
                return;
            ScreenBoardCoordinates = new Rectangle();
            ScreenBoardCoordinates = croprect;
            TriggerCoordinates = croprect;
            pbTriggerImage.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);

        }

        private void CheckWhosTurnToPlay()
        {
            if( cbTriggerMarker.Checked && pbTriggerImage.Image != null )
            {
                Image currentScreen = ImageProcessingManager.TakeScreenShot();

                if (TriggerCoordinates.IsEmpty)
                    return;

                pbCurrentMarker.Image = ((Bitmap)currentScreen).Clone(TriggerCoordinates, currentScreen.PixelFormat);

                Image<Gray, Byte> currentMarker = new Image<Gray, byte>(pbCurrentMarker.Image as Bitmap);
                Image<Gray, Byte> triggerMarker = new Image<Gray, byte>(pbTriggerImage.Image as Bitmap);
                if(ImageProcessingManager.AreImagesSame(triggerMarker,currentMarker,Constants.STANDARD_IMAGE_COMPARISON_FACTOR))
                {
                    lblWhosMove.Text = "User Move";
                }
                else
                {
                    lblWhosMove.Text = "Computer Move";
                }
            }
        }

        private void timerTriggerChecker_Tick(object sender, EventArgs e)
        {
            CheckWhosTurnToPlay();
        }

        private void txtRefreshInterval_Leave(object sender, EventArgs e)
        {
            int interval = 5;
            int.TryParse(txtRefreshInterval.Text, out interval);

            if (interval < 50)
            {
                interval = 50;
                txtRefreshInterval.Text = "50";
            }
            else if (interval > 2000)
            {
                interval = 2000;
                txtRefreshInterval.Text = "2000";
            }
            timerAutoRefresh.Interval = interval;
        }

        private void txtRefreshMarkerInterval_Leave(object sender, EventArgs e)
        {
            int interval = 5;
            int.TryParse(txtRefreshMarkerInterval.Text, out interval);

            if (interval < 30)
            {
                interval = 30;
                txtRefreshMarkerInterval.Text = "30";
            }

            timerTriggerChecker.Interval = interval;
        }

        private void btnStartNewGame_Click(object sender, EventArgs e)
        {
            int padding = int.Parse(txtPadding.Text);
            rbtnWhite.Checked = ImageProcessingManager.CheckFirstWhosFirstMove(pbScreen.Image, padding);
            rbtnBlack.Checked = !rbtnWhite.Checked;
        }

        private void btnRefreshTemplate_Click(object sender, EventArgs e)
        {
            LoadTemplates();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            RefreshGrayImage();
        }

        private void RefreshGrayImage()
        {
            Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> cvImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(test as Bitmap);
            //Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", cvImage);

            double intensity = trackBar1.Value;
            var binaryImage = cvImage.Convert<Gray, byte>().ThresholdBinary(new Gray(intensity), new Gray(255));
            Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", binaryImage);
            txtIntensity.Text = trackBar1.Value.ToString();

           ImageProcessingManager.IntensityValue = intensity;
        }

        private void btnUseIntensity_Click(object sender, EventArgs e)
        {
            trackBar1.Value = int.Parse(txtIntensity.Text);
            RefreshGrayImage();

        }
        Image test = Image.FromFile("in progress black.jpg");

       

    }
}

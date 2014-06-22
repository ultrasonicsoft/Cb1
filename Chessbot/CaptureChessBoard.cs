using System;
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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

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
        private IntPtr desktop = IntPtr.Zero;
        int chessDrawingRowIndex = -1;
        int chessDrawingColumnIndex = -1;
        int chessDrawingNextRowIndex = -1;
        int chessDrawingNextColumnIndex = -1;
        private readonly FrmShowNextMove showNextMove;
        private bool IsComputingNextMove = false;
        private string nextMove = "-";
        private bool hasComputerPlayed = true;
        private GlobalHotkey ghk;
        private string _engineDepth = "16";
        public string EngineDepth { get { return _engineDepth; } set { _engineDepth = value; } }

        private ChessTemplate masterTemplate = null;
        public Rectangle ScreenBoardCoordinates { get; set; }
        public Rectangle TriggerCoordinates { get; set; }

        public Image CapturedScreen { get; set; }
        public static Image CapturedBoard { get; set; }
        public CaptureChessBoard()
        {
            InitializeComponent();

            showNextMove = new FrmShowNextMove();
            showNextMove.DrawNextMoveOnScreen += DrawOnDesktopNextMove;

            ghk = new GlobalHotkey(Keys.NumPad0, this);

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                GetBestMove();
            base.WndProc(ref m);
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

            // Show Next Move dialog box
            showNextMove.Show();

            // Register for Keypad event
            ghk.Register();
            //WriteLine("Trying to register SHIFT+ALT+O");
            //if (ghk.Register())
                //WriteLine("Hotkey registered.");
            //else
                //WriteLine("Hotkey failed to register");
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
            var rect = GetRectangle(sp, ep);
            e.Graphics.DrawRectangle(Pens.Red, rect);

            int left = sp.X;
            int top = sp.Y;
            int right = rect.Width / 8;
            int bottom = rect.Height / 8;


            //var pieceRect = new Rectangle(left, top, right, bottom);
            //e.Graphics.DrawRectangle(Pens.Red, pieceRect);

            int blockLeft = sp.X;
            int blockTop = sp.Y;
            int blockWidth = (rect.Width / Constants.GRID_SIZE);
            int blockHeight = (rect.Height / Constants.GRID_SIZE);

            Rectangle pieceRect = Rectangle.Empty;

            for (int rowIndex = 1; rowIndex <= Constants.GRID_SIZE; rowIndex++)
            {
                blockLeft = sp.X;
                for (int colIndex = 1; colIndex <= Constants.GRID_SIZE; colIndex++)
                {
                    pieceRect = new Rectangle(blockLeft, blockTop, blockWidth, blockHeight);
                    //r = new Rectangle(blockLeft + blockPaddingAmount, blockTop + blockPaddingAmount, blockWidth - blockPaddingAmount, blockHeight - blockPaddingAmount);
                    e.Graphics.DrawRectangle(Pens.Red, pieceRect);
                    blockLeft += blockWidth;
                }
                blockTop += blockHeight;
            }
            e.Graphics.Save();
        }

        private void Crop_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                ep = e.Location;
                pbScreen.Invalidate();
            }

            var rect = GetRectangle(sp, ep);


            lblCurrentMouseX.Text = e.X.ToString();
            lblCurrentMouseY.Text = e.Y.ToString();


            if (isGetXEnabled)
            {
                txtLeft.Text = e.X.ToString();
                txtTop.Text = e.Y.ToString();
            }
            else
            {
                txtLeft.Text = rect.X.ToString();
                txtTop.Text = rect.Y.ToString();
            }
            if (isGetYEnabled)
            {
                txtRight.Text = e.X.ToString();
                txtBottom.Text = e.Y.ToString();
            }
            else
            {
                txtRight.Text = rect.Width.ToString();
                txtBottom.Text = rect.Height.ToString();
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            ep = e.Location;
            mouseDown = false;
            croprect = GetRectangle(sp, ep);

            txtWidth.Text = ep.X.ToString();
            txtHeight.Text = ep.Y.ToString();

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

                CurrentCapturedScreen = pbScreen.Image;
                RefreshGrayImage();

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

            txtWidth.Text = croprect.Width.ToString();
            txtHeight.Text = croprect.Height.ToString();

            if (croprect.Width % 2 != 0 || croprect.Height % 2 != 0)
            {
                MessageBox.Show("Please select region with heigh and width divisible by 2 for accuracy.");
                return;
            }

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
            RefreshGrayImage();
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
            if (pbScreen.Image.Width % 8 != 0 && pbScreen.Image.Height % 8 != 0)
            {
                int requiredSize = pbScreen.Image.Width / 8;
                message = string.Format("The cropped chess board is not even. Make sure it is perfect square. Try resizing to Width x Height :{0} x {0}", (requiredSize * 8).ToString());
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

                int oldWidth = CapturedScreen.Width;
                int oldHeight = CapturedScreen.Height;

                left = Math.Abs(oldWidth - width) / 2;
                top = Math.Abs(oldHeight - height) / 2;

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
            preview.ChessBoardImage = (ImageProcessingManager.GetBinaryImage(CapturedScreen, tbIntensity.Value).Bitmap).Clone(new Rectangle(0, 0, CapturedScreen.Width, CapturedScreen.Height), CapturedScreen.PixelFormat);
            preview.Show();
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            FrmSaveTemplate saveTemplate = new FrmSaveTemplate();

            var binaryImage = (ImageProcessingManager.GetBinaryImage(pbScreen.Image, tbIntensity.Value).Bitmap).Clone(new Rectangle(0, 0, pbScreen.Image.Width, pbScreen.Image.Height), pbScreen.Image.PixelFormat);

            saveTemplate.ChessBoard = binaryImage;
            saveTemplate.Padding = int.Parse(txtPadding.Text);
            saveTemplate.IsWhiteFirst = rbtnWhite.Checked;
            saveTemplate.Intensity = tbIntensity.Value;
            saveTemplate.ShowDialog();

            LoadTemplates();
        }

        private void btnScanAgain_Click(object sender, EventArgs e)
        {
            ScanBoardAgain();
        }

        private bool ScanBoardAgain()
        {
            croprect = ScreenBoardCoordinates;
            if (croprect == Rectangle.Empty)
            {
                MessageBox.Show("Board selection is invalid. Please crop board properly.");
                return false;
            }
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
            return true;
        }

        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            TemplateEntity selectedEntity = allLoadedTemplates[cmbTemplates.SelectedIndex];
            if (selectedEntity != null)
            {
                Console.WriteLine("Reading template " + selectedEntity.TemplateName);
                masterTemplate = ImageProcessingManager.ReadTemplate(selectedEntity.TemplateFileName);
                tbIntensity.Value = masterTemplate.Intensity;
                txtIntensity.Text = tbIntensity.Value.ToString();
                pbScreen.Image = masterTemplate.CurrentTemplateImage;
                MessageBox.Show("Template successfully loaded..", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Reading template DONE!");
            }
        }

        private void btnShowBoardConfiguration_Click(object sender, EventArgs e)
        {
            if (ImageProcessingManager.allChessBoardTemplate == null )
            {
                MessageBox.Show("Template not loaded.");
                return;
            }
            GetBestMove();
        }

        private void GetBestMove()
        {
            IsComputingNextMove = true;

            if(ScanBoardAgain() == false)
            { return; }

            showNextMove.IsThinkingNextMove = true;
            //timerAutoRefresh.Enabled = false;
            Application.DoEvents();
            ProcessAndPrintBoard();
            IsComputingNextMove = false;
            //timerAutoRefresh.Enabled = true;


            //DrawOnDesktop();
           
        }

        private void ProcessAndPrintBoard()
        {
            Cursor = Cursors.WaitCursor;
            Console.WriteLine("Reading current Chess position...");
            int paddingPixel = int.Parse(txtPadding.Text);
            string fenString = string.Empty;
            if (rbtnWhite.Checked)
            {
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("white.png"), 5, rbtnWhite.Checked);
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("test.png"), paddingPixel, rbtnWhite.Checked);
                ImageProcessingManager.ReadChessBoardCurrentPosition(pbScreen.Image, paddingPixel, rbtnWhite.Checked, tbIntensity.Value);
                //ImageProcessingManager.ReadChessBoardCurrentPosition(Image.FromFile("inprogress.PNG"), paddingPixel, rbtnWhite.Checked);
                ImageProcessingManager.PrintChessBoard(rbtnWhite.Checked);
                fenString = ImageProcessingManager.PrepareFenString(rbtnWhite.Checked);
            }
            else
            {
                ImageProcessingManager.ReadChessBoardCurrentPosition(pbScreen.Image, paddingPixel, rbtnWhite.Checked, tbIntensity.Value);
                ImageProcessingManager.PrintChessBoard(rbtnWhite.Checked);
                fenString = ImageProcessingManager.PrepareFenString(rbtnWhite.Checked);
            }
            lblExecutionTime.Text = ImageProcessingManager.TotalProcessingTime ;

            if (fenString.Contains(Constants.INVALID_FEN_STRING) == false)
            {
                GetNextBestMove(fenString);
            }
            else
            {
                MessageBox.Show("Invalid chess board screen fed. Please check proper chess board is configured.");
            }
            Cursor = Cursors.Default;
        }

        private string GetNextBestMove(string fenString)
        {
            string bestMove = string.Empty;
            try
            {
                UCI engine = new UCI();
                engine.BestMovFound += engine_BestMovFound;
                engine.InitEngine("stockfishengine.exe", string.Empty);
                var parts = fenString.Split(' ');
                fenString = parts[0] + " " + parts[1];
                engine.CalculateBestMove(fenString, EngineDepth);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bestMove;
        }

        private void btnShowTemplate_Click(object sender, EventArgs e)
        {
            Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> normalizedMasterImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(masterTemplate.CurrentTemplateImage as Bitmap);
            Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", normalizedMasterImage);
        }

        private void timerAutoRefresh_Tick(object sender, EventArgs e)
        {
            if (cbAutoRefresh.Checked && IsComputingNextMove == false)
            {
                CapturedScreen = ImageProcessingManager.TakeScreenShot();
                pbScreen.Image = CapturedScreen;
                CropChessBoard();
                croprect = ScreenBoardCoordinates;
                //ProcessBoardAndPrint();
            }
            //pbPreview.Image = ImageProcessingManager.TakeScreenShot();

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
            if (IsComputingNextMove == false)
            {
                if (cbTriggerMarker.Checked && pbTriggerImage.Image != null)
                {
                    Image currentScreen = ImageProcessingManager.TakeScreenShot();

                    if (TriggerCoordinates.IsEmpty)
                        return;

                    pbCurrentMarker.Image = ((Bitmap)currentScreen).Clone(TriggerCoordinates, currentScreen.PixelFormat);

                    Image<Gray, Byte> currentMarker = new Image<Gray, byte>(pbCurrentMarker.Image as Bitmap);
                    Image<Gray, Byte> triggerMarker = new Image<Gray, byte>(pbTriggerImage.Image as Bitmap);
                    if (ImageProcessingManager.AreImagesSame(triggerMarker, currentMarker, ImageProcessingManager.StandardMatchingFactor))
                    {

                        lblWhosMove.Text = "User Move";
                        if (hasComputerPlayed )
                            //if (hasComputerPlayed && showNextMove.BestMove != null && string.Equals(showNextMove.BestMove, nextMove) == false)
                        {
                            IsComputingNextMove = true;
                            //TODO: Check what can be done here for next move
                            //GetBestMove();
                        }
                        hasComputerPlayed = false;
                    }
                    else
                    {
                        lblWhosMove.Text = "Computer Move";
                        hasComputerPlayed = true;
                    }
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
            btnGetBestMove.Enabled = true;
            
        }

        private void btnRefreshTemplate_Click(object sender, EventArgs e)
        {
            LoadTemplates();
        }

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void ReleaseDC(IntPtr dc);
        private void DrawOnDesktop()
        {
            try
            {
                if (desktop == IntPtr.Zero)
                {
                    desktop = GetDC(IntPtr.Zero);
                }
                using (Graphics g = Graphics.FromHdc(desktop))
                {
                    g.DrawRectangle(Pens.Red, ScreenBoardCoordinates);
                }
                //  ReleaseDC(desktop);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void engine_BestMovFound(object sender, EventArgs e)
        {
            var args = (BestMoveFoundArgs)e;
            if (false == string.IsNullOrEmpty(args.BestMove))
            {
                //MessageBox.Show(args.BestMove);

                showNextMove.IsThinkingNextMove = false;
                Application.DoEvents();

                showNextMove.BestMove = args.BestMove;
                if (txtBestMove.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtBestMove.Text = args.BestMove; // runs on UI thread
                    });

                }
                char[] info = args.BestMove.ToUpper().ToCharArray();

                GetChessDrawingIndex(info[0], info[1]);
                GetChessDrawingNextMoveIndex(info[2], info[3]);
                DrawOnDesktopNextMove();

                nextMove = args.BestMove;
                IsComputingNextMove = false;
            }
        }

        private void GetChessDrawingIndex(char column, char row)
        {
            int rowIndex = int.Parse(row.ToString());


            if (rbtnWhite.Checked == false)
            {
                chessDrawingRowIndex = 8 - rowIndex;
                switch (column)
                {
                    case 'A':
                        chessDrawingColumnIndex = 0;
                        break;
                    case 'B':
                        chessDrawingColumnIndex = 1;
                        break;
                    case 'C':
                        chessDrawingColumnIndex = 2;
                        break;
                    case 'D':
                        chessDrawingColumnIndex = 3;
                        break;
                    case 'E':
                        chessDrawingColumnIndex = 4;
                        break;
                    case 'F':
                        chessDrawingColumnIndex = 5;
                        break;
                    case 'G':
                        chessDrawingColumnIndex = 6;
                        break;
                    case 'H':
                        chessDrawingColumnIndex = 7;
                        break;
                }
            }
            else
            {
                chessDrawingRowIndex = rowIndex -1;
                switch (column)
                {
                    case 'A':
                        chessDrawingColumnIndex = 7;
                        break;
                    case 'B':
                        chessDrawingColumnIndex = 6;
                        break;
                    case 'C':
                        chessDrawingColumnIndex = 5;
                        break;
                    case 'D':
                        chessDrawingColumnIndex = 4;
                        break;
                    case 'E':
                        chessDrawingColumnIndex = 3;
                        break;
                    case 'F':
                        chessDrawingColumnIndex = 2;
                        break;
                    case 'G':
                        chessDrawingColumnIndex = 1;
                        break;
                    case 'H':
                        chessDrawingColumnIndex = 0;
                        break;
                }
            }
        }

        private void GetChessDrawingNextMoveIndex(char column, char row)
        {
            int rowIndex = int.Parse(row.ToString());

            if (rbtnWhite.Checked == false)
            {
                chessDrawingNextRowIndex = 8 - rowIndex;
                switch (column)
                {
                    case 'A':
                        chessDrawingNextColumnIndex = 0;
                        break;
                    case 'B':
                        chessDrawingNextColumnIndex = 1;
                        break;
                    case 'C':
                        chessDrawingNextColumnIndex = 2;
                        break;
                    case 'D':
                        chessDrawingNextColumnIndex = 3;
                        break;
                    case 'E':
                        chessDrawingNextColumnIndex = 4;
                        break;
                    case 'F':
                        chessDrawingNextColumnIndex = 5;
                        break;
                    case 'G':
                        chessDrawingNextColumnIndex = 6;
                        break;
                    case 'H':
                        chessDrawingNextColumnIndex = 7;
                        break;
                }
            }
            else
            {
                chessDrawingNextRowIndex = rowIndex -1;
                switch (column)
                {
                    case 'A':
                        chessDrawingNextColumnIndex = 7;
                        break;
                    case 'B':
                        chessDrawingNextColumnIndex = 6;
                        break;
                    case 'C':
                        chessDrawingNextColumnIndex = 5;
                        break;
                    case 'D':
                        chessDrawingNextColumnIndex = 4;
                        break;
                    case 'E':
                        chessDrawingNextColumnIndex = 3;
                        break;
                    case 'F':
                        chessDrawingNextColumnIndex = 2;
                        break;
                    case 'G':
                        chessDrawingNextColumnIndex = 1;
                        break;
                    case 'H':
                        chessDrawingNextColumnIndex = 0;
                        break;
                }
            }
        }
        public void DrawOnDesktopNextMove()
        {
            try
            {
                if (desktop == IntPtr.Zero)
                {
                    desktop = GetDC(IntPtr.Zero);
                }
                using (Graphics g = Graphics.FromHdc(desktop))
                {
                    //g.DrawRectangle(Pens.Red, ScreenBoardCoordinates);

                    // Draw current position
                    int startX = ScreenBoardCoordinates.X + chessDrawingColumnIndex * 64; // TODO: calculate each block height and width instead of 64
                    int startY = ScreenBoardCoordinates.Y + chessDrawingRowIndex * 64;

                    // Draw next move position
                    int startNewX = ScreenBoardCoordinates.X + chessDrawingNextColumnIndex * 64; // TODO: calculate each block height and width instead of 64
                    int startNewY = ScreenBoardCoordinates.Y + chessDrawingNextRowIndex * 64;

                    Pen oldPen = new Pen(Color.Red, 5);
                    Pen newPen = new Pen(Color.Blue, 5);

                    for (int i = 0; i < 10; i++)
                    {
                        //g.DrawRectangle(Pens.Red, new Rectangle(startX + 5, startY + 5, 50, 50));
                        //g.DrawRectangle(Pens.Blue, new Rectangle(startNewX + 5, startNewY + 5, 50, 50));

                        g.DrawRectangle(oldPen, new Rectangle(startX + 5, startY + 5, 50, 50));
                        g.DrawRectangle(newPen, new Rectangle(startNewX + 5, startNewY + 5, 50, 50));
                    }

                }
                //  ReleaseDC(desktop);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            RefreshGrayImage();
        }

        private void RefreshGrayImage()
        {
            if (pbScreen.Image == null || CurrentCapturedScreen == null)
                return;

            //Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> cvImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(test as Bitmap);
            Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> cvImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(CurrentCapturedScreen as Bitmap);
            //Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", cvImage);

            double intensity = tbIntensity.Value;
            var binaryImage = cvImage.Convert<Gray, byte>().ThresholdBinary(new Gray(intensity), new Gray(255));
            //Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", binaryImage);
            pbIntensityTest.Image = (binaryImage.Bitmap).Clone(new Rectangle(0, 0, binaryImage.Width, binaryImage.Height), (binaryImage.Bitmap).PixelFormat);

            txtIntensity.Text = tbIntensity.Value.ToString();
            ImageProcessingManager.IntensityValue = intensity;

            if (cbShowIntensityOnTop.Checked)
            {
                pbScreen.Image = pbIntensityTest.Image;
            }
        }

        private void btnUseIntensity_Click(object sender, EventArgs e)
        {
            tbIntensity.Value = int.Parse(txtIntensity.Text);
            RefreshGrayImage();

        }

        private void cbShowIntensityOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowIntensityOnTop.Checked == false)
            {
                pbScreen.Image = CurrentCapturedScreen;
            }
            else
            {
                pbScreen.Image = pbIntensityTest.Image;
                ProcessAndPrintBoard();
            }
        }

        Image CurrentCapturedScreen = null;

        private void btnUpdateStandardMatchingFactor_Click(object sender, EventArgs e)
        {
            ImageProcessingManager.StandardMatchingFactor = (double)(int.Parse(txtStandardMatchingFactor.Text) / 100.0);
        }
        private void cbTriggerMarker_CheckedChanged(object sender, EventArgs e)
        {
            timerTriggerChecker.Enabled = cbTriggerMarker.Checked;
        }

        private void CaptureChessBoard_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.NumPad0)
            //{
            //    GetBestMove();
            //}
        }

        private void CaptureChessBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            ghk.Unregiser();
            //if (!ghk.Unregiser())
            //    MessageBox.Show("Hotkey failed to unregister!");
        }

        private void cbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            timerAutoRefresh.Enabled = cbAutoRefresh.Checked;
        }

        private void txtEngineDepth_TextChanged(object sender, EventArgs e)
        {
            EngineDepth = txtEngineDepth.Text;
        }

       

    }
}

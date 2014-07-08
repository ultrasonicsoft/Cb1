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
        #region Members

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
        private ChessTemplate masterTemplate = null;
        private GlobalHotkey ghk;
        private string _engineDepth = "16";
        Image CurrentCapturedScreen = null;
        private bool enableHotKeyForGetNextMove = false;
        #endregion

        #region Properties
        public string EngineDepth { get { return _engineDepth; } set { _engineDepth = value; } }

        public int NextMoveHighlightDuration { get; set; }

        public Rectangle ScreenBoardCoordinates { get; set; }
        public Rectangle PreviousScreenBoardCoordinates { get; set; }
        public Rectangle TriggerCoordinates { get; set; }
        public Image CapturedScreen { get; set; }
        public static Image CapturedBoard { get; set; }
        public CompactView SmallView { get; set; }

        #endregion

        #region Constructor

        public CaptureChessBoard()
        {
            InitializeComponent();

            //showNextMove = new FrmShowNextMove();
            //showNextMove.DrawNextMoveOnScreen += DrawOnDesktopNextMove;

            LogHelper.logger.Info("Constructor called.");
            ghk = new GlobalHotkey(Keys.NumPad0, this);

            if (SmallView == null)
            {
                LogHelper.logger.Info("Registering small view.");

                SmallView = new CompactView();
                SmallView.MainView = this;
                SmallView.DrawNextMoveOnScreen += DrawOnDesktopNextMove;
            }
            LogHelper.logger.Info("Constructor finished.");
        }
        #endregion

        #region Global function definition

        protected override void WndProc(ref Message m)
        {
            if (enableHotKeyForGetNextMove && IsComputingNextMove == false && m.Msg == Constants.WM_HOTKEY_MSG_ID)
                GetBestMove();
            base.WndProc(ref m);
        }

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void ReleaseDC(IntPtr dc);

        #endregion

        #region Form Load
        private void CaptureChessBoard_Load(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("CaptureChessBoard_Load started.");
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
                //showNextMove.Show();

                // Register for Keypad event
                ghk.Register();
                //WriteLine("Trying to register SHIFT+ALT+O");
                //if (ghk.Register())
                //WriteLine("Hotkey registered.");
                //else
                //WriteLine("Hotkey failed to register");
                LogHelper.logger.Info("CaptureChessBoard_Load finished.");
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("CaptureChessBoard_Load: " + exception.Message);
                LogHelper.logger.Error("CaptureChessBoard_Load: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Event Handlers
        private void btnCropBoard_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnCropBoard_Click called...");
                CropChessBoard();

                CurrentCapturedScreen = pbScreen.Image;
                RefreshGrayImage();
                LogHelper.logger.Info("btnCropBoard_Click finished...");
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnCropBoard_Click: " + exception.Message);
                LogHelper.logger.Error("btnCropBoard_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }
        private void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnCaptureScreen_Click called...");
                Image img = (Image)ImageProcessingManager.TakeScreenShot();
                CapturedScreen = img;
                pbScreen.Image = img;
                //CaptureChessBoard chessBoard = new CaptureChessBoard();
                //chessBoard.CapturedScreen = Image.FromFile("screen.jpg");
                //chessBoard.ShowDialog();
                RefreshGrayImage();
                LogHelper.logger.Info("btnCaptureScreen_Click finished...");
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnCaptureScreen_Click: " + exception.Message);
                LogHelper.logger.Error("btnCaptureScreen_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lblCurrentMouseX.Text = string.Empty;
            lblCurrentMouseY.Text = string.Empty;
            //txtLeft.Text = string.Empty;
            //txtRight.Text = string.Empty;
            //txtTop.Text = string.Empty;
            //txtBottom.Text = string.Empty;
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
            //int left = 0;
            //int top = 0;
            //int right = 0;
            //int bottom = 0;
            //int.TryParse(txtLeft.Text, out left);
            //int.TryParse(txtTop.Text, out top);
            //int.TryParse(txtRight.Text, out right);
            //int.TryParse(txtBottom.Text, out bottom);
            //int width = Math.Abs(right - left);
            //int height = Math.Abs(bottom - top);

            //try
            //{
            //    LogHelper.logger.Info("btnCropUsingCoordinates_Click called...");
            //    sp = new Point(left, top);
            //    ep = new Point(right, bottom);
            //    croprect = new Rectangle(left, top, width, height);
            //    //pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);

            //    pbScreen.Invalidate();
            //    LogHelper.logger.Info("btnCropUsingCoordinates_Click finished...");
            //}
            //catch (Exception exception)
            //{
            //    LogHelper.logger.Error("btnCropUsingCoordinates_Click: " + exception.Message);
            //    LogHelper.logger.Error("btnCropUsingCoordinates_Click: " + exception.StackTrace);
            //    MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void btnGetX_Click(object sender, EventArgs e)
        {
            isGetXEnabled = true;
            isGetYEnabled = false;
        }

        private void btnGetY_Click(object sender, EventArgs e)
        {
            isGetYEnabled = true;
            isGetXEnabled = false;
        }
        private void btnResize_Click(object sender, EventArgs e)
        {
            //int left = 0;
            //int top = 0;
            //int width = 0;
            //int height = 0;

            //try
            //{

            //    LogHelper.logger.Info("btnResize_Click called...");
            //    left = int.Parse(txtResizeLeft.Text);
            //    top = int.Parse(txtResizeTop.Text);
            //    width = int.Parse(txtWidth.Text);
            //    height = int.Parse(txtHeight.Text);

            //    int oldWidth = CapturedScreen.Width;
            //    int oldHeight = CapturedScreen.Height;

            //    left = Math.Abs(oldWidth - width) / 2;
            //    top = Math.Abs(oldHeight - height) / 2;

            //    croprect = new Rectangle(left, top, width, height);
            //    pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);
            //    CapturedScreen = pbScreen.Image;

            //    ClearSelection();

            //    ValidateBoard();

            //}
            //catch (Exception exception)
            //{
            //    LogHelper.logger.Error("btnResize_Click: " + exception.Message);
            //    LogHelper.logger.Error("btnResize_Click: " + exception.StackTrace);
            //    MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //LogHelper.logger.Info("btnResize_Click finished...");
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnTemplate_Click called...");
                PreviewTemplate preview = new PreviewTemplate();
                preview.ChessBoardImage = (ImageProcessingManager.GetBinaryImage(pbScreen.Image, tbIntensity.Value).Bitmap).Clone(new Rectangle(0, 0, pbScreen.Image.Width, pbScreen.Image.Height), pbScreen.Image.PixelFormat);
                preview.Padding = int.Parse(txtPadding.Text);
                preview.Show();
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnTemplate_Click: " + exception.Message);
                LogHelper.logger.Error("btnTemplate_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnTemplate_Click finished...");
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnSaveTemplate_Click called...");
                FrmSaveTemplate saveTemplate = new FrmSaveTemplate();
                var binaryImage = (ImageProcessingManager.GetBinaryImage(pbScreen.Image, tbIntensity.Value).Bitmap).Clone(new Rectangle(0, 0, pbScreen.Image.Width, pbScreen.Image.Height), pbScreen.Image.PixelFormat);
                saveTemplate.ChessBoard = binaryImage;
                saveTemplate.Padding = int.Parse(txtPadding.Text);
                saveTemplate.IsWhiteFirst = rbtnWhite.Checked;
                saveTemplate.Intensity = tbIntensity.Value;
                saveTemplate.ShowDialog();

                LoadTemplates();
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnSaveTemplate_Click: " + exception.Message);
                LogHelper.logger.Error("btnSaveTemplate_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnSaveTemplate_Click finished...");
        }

        private void btnScanAgain_Click(object sender, EventArgs e)
        {
            ScanBoardAgain();
        }
        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Error("Testing error log");

                LogHelper.logger.Info("btnLoadTemplate_Click called...");
                TemplateEntity selectedEntity = allLoadedTemplates[cmbTemplates.SelectedIndex];
                if (selectedEntity != null)
                {
                    //Console.WriteLine("Reading template " + selectedEntity.TemplateName);
                    masterTemplate = ImageProcessingManager.ReadTemplate(selectedEntity.TemplateFileName);
                    tbIntensity.Value = masterTemplate.Intensity;
                    txtIntensity.Text = tbIntensity.Value.ToString();
                    pbScreen.Image = masterTemplate.CurrentTemplateImage;
                    //MessageBox.Show("Template successfully loaded..", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMessage.Text = "Template loaded successfully";
                    //Console.WriteLine("Reading template DONE!");
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnLoadTemplate_Click: " + exception.Message);
                LogHelper.logger.Error("btnLoadTemplate_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnLoadTemplate_Click finished...");
        }

        private void btnShowBoardConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnShowBoardConfiguration_Click called...");
                if (ImageProcessingManager.allChessBoardTemplate == null)
                {
                    MessageBox.Show("Template not loaded.");
                    return;
                }
                GetBestMove();
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnShowBoardConfiguration_Click: " + exception.Message);
                LogHelper.logger.Error("btnShowBoardConfiguration_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnShowBoardConfiguration_Click finished...");
        }
        private void btnShowTemplate_Click(object sender, EventArgs e)
        {
            LogHelper.logger.Info("btnShowTemplate_Click called...");
            try
            {
                Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> normalizedMasterImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(masterTemplate.CurrentTemplateImage as Bitmap);
                Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", normalizedMasterImage);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnShowTemplate_Click: " + exception.Message);
                LogHelper.logger.Error("btnShowTemplate_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnShowTemplate_Click finished...");
        }

        private void timerAutoRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("timerAutoRefresh_Tick called...");
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("timerAutoRefresh_Tick: " + exception.Message);
                LogHelper.logger.Error("timerAutoRefresh_Tick: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("timerAutoRefresh_Tick finished...");
        }

        private void btnMarkTrigger_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnMarkTrigger_Click called...");
                if (croprect.IsEmpty)
                    return;
                ScreenBoardCoordinates = new Rectangle();
                ScreenBoardCoordinates = croprect;
                TriggerCoordinates = croprect;
                pbTriggerImage.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnMarkTrigger_Click: " + exception.Message);
                LogHelper.logger.Error("btnMarkTrigger_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnMarkTrigger_Click finished...");
        }
        private void timerTriggerChecker_Tick(object sender, EventArgs e)
        {
            CheckWhosTurnToPlay();
        }

        private void txtRefreshInterval_Leave(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("txtRefreshInterval_Leave called...");
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("txtRefreshInterval_Leave: " + exception.Message);
                LogHelper.logger.Error("txtRefreshInterval_Leave: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("txtRefreshInterval_Leave finished...");
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
            try
            {
                LogHelper.logger.Info("btnStartNewGame_Click called...");
                int padding = int.Parse(txtPadding.Text);
                rbtnWhite.Checked = ImageProcessingManager.CheckFirstWhosFirstMove(pbScreen.Image, padding);
                rbtnBlack.Checked = !rbtnWhite.Checked;
                btnGetBestMove.Enabled = true;
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnStartNewGame_Click: " + exception.Message);
                LogHelper.logger.Error("btnStartNewGame_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnStartNewGame_Click finished...");
        }

        private void btnRefreshTemplate_Click(object sender, EventArgs e)
        {
            LoadTemplates();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            RefreshGrayImage();
        }

        private void btnUseIntensity_Click(object sender, EventArgs e)
        {
            tbIntensity.Value = int.Parse(txtIntensity.Text);
            RefreshGrayImage();
        }

        private void cbShowIntensityOnTop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("cbShowIntensityOnTop_CheckedChanged called...");
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("cbShowIntensityOnTop_CheckedChanged: " + exception.Message);
                LogHelper.logger.Error("cbShowIntensityOnTop_CheckedChanged: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("cbShowIntensityOnTop_CheckedChanged finished...");
        }

        private void btnUpdateStandardMatchingFactor_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnUpdateStandardMatchingFactor_Click called...");
                ImageProcessingManager.StandardMatchingFactor = (double)(int.Parse(txtStandardMatchingFactor.Text) / 100.0);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnUpdateStandardMatchingFactor_Click: " + exception.Message);
                LogHelper.logger.Error("btnUpdateStandardMatchingFactor_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnUpdateStandardMatchingFactor_Click finished...");
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
            try
            {
                LogHelper.logger.Info("CaptureChessBoard_FormClosing called...");
                ghk.Unregiser();
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("CaptureChessBoard_FormClosing: " + exception.Message);
                LogHelper.logger.Error("CaptureChessBoard_FormClosing: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if (!ghk.Unregiser())
            //    MessageBox.Show("Hotkey failed to unregister!");
            LogHelper.logger.Info("CaptureChessBoard_FormClosing finished...");
        }

        private void cbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            timerAutoRefresh.Enabled = cbAutoRefresh.Checked;
        }

        private void txtEngineDepth_TextChanged(object sender, EventArgs e)
        {
            EngineDepth = txtEngineDepth.Text;
        }

        private void btnCompactView_Click(object sender, EventArgs e)
        {
            this.Hide();
            SmallView.Show();
        }

        private void cbEnableHotKey_CheckedChanged(object sender, EventArgs e)
        {
            enableHotKeyForGetNextMove = cbEnableHotKey.Checked;
        }

        private void btnDeleteTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("btnDeleteTemplate_Click called...");
                var result = MessageBox.Show("Are you sure to delete this template?", "Chessbot",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                TemplateEntity selectedEntity = allLoadedTemplates[cmbTemplates.SelectedIndex];
                if (selectedEntity != null && result == DialogResult.Yes)
                {
                    string tempalteCatalogFileName = ImageProcessingManager.TemplatePath + Constants.TEMPLATE_CATELOG_FILE;
                    if (File.Exists(tempalteCatalogFileName) == false)
                    {
                        MessageBox.Show("There are no template present. Please create and save them.", "Load Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var allTemplate = File.ReadAllLines(tempalteCatalogFileName);

                    List<string> newTemplateList = new List<string>();
                    foreach (string templateEntry in allTemplate)
                    {
                        TemplateEntity newTemplate = new TemplateEntity();
                        var templateParts = templateEntry.Split(',');
                        newTemplate.TemplateName = templateParts[0];
                        newTemplate.WebsiteURL = templateParts[1];
                        newTemplate.TemplateFileName = templateParts[2];
                        if (templateParts[0] != selectedEntity.TemplateName)
                        {
                            newTemplateList.Add(templateEntry);
                        }
                    }
                    File.WriteAllLines(tempalteCatalogFileName, newTemplateList.ToArray());

                    File.Delete(selectedEntity.TemplateFileName);

                    MessageBox.Show("Template Deleted!", "Chessbot",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTemplates();
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("btnDeleteTemplate_Click: " + exception.Message);
                LogHelper.logger.Error("btnDeleteTemplate_Click: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("btnDeleteTemplate_Click finished...");
        }
        #endregion

        #region Drawing Methods

        private void Crop_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //LogHelper.logger.Info("Crop_Paint called...");
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("Crop_Paint: " + exception.Message);
                LogHelper.logger.Error("Crop_Paint: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("Crop_Paint finished...");
        }

        private void Crop_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //LogHelper.logger.Info("Crop_MouseMove callled...");
                if (mouseDown)
                {
                    ep = e.Location;
                    pbScreen.Invalidate();
                }

                var rect = GetRectangle(sp, ep);


                lblCurrentMouseX.Text = e.X.ToString();
                lblCurrentMouseY.Text = e.Y.ToString();


                //if (isGetXEnabled)
                //{
                //    txtLeft.Text = e.X.ToString();
                //    txtTop.Text = e.Y.ToString();
                //}
                //else
                //{
                //    txtLeft.Text = rect.X.ToString();
                //    txtTop.Text = rect.Y.ToString();
                //}
                //if (isGetYEnabled)
                //{
                //    txtRight.Text = e.X.ToString();
                //    txtBottom.Text = e.Y.ToString();
                //}
                //else
                //{
                //    txtRight.Text = rect.Width.ToString();
                //    txtBottom.Text = rect.Height.ToString();
                //}
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("Crop_MouseMove: " + exception.Message);
                LogHelper.logger.Error("Crop_MouseMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("Crop_MouseMove finished...");
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                LogHelper.logger.Info("Crop_MouseUp called...");
                ep = e.Location;
                mouseDown = false;
                croprect = GetRectangle(sp, ep);

                //txtWidth.Text = ep.X.ToString();
                //txtHeight.Text = ep.Y.ToString();

                txtSelectedLeft.Text = croprect.Left.ToString();
                txtSelectedTop.Text = croprect.Top.ToString();
                txtSelectedWidth.Text = croprect.Width.ToString();
                txtSelectedHeight.Text = croprect.Height.ToString();

                if (croprect.Width > 10 && croprect.Height > 10)
                {
                    //selectedArea = true;
                }
                else
                {
                    croprect = Rectangle.Empty;
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("Crop_MouseUp: " + exception.Message);
                LogHelper.logger.Error("Crop_MouseUp: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("Crop_MouseUp finished...");
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                LogHelper.logger.Info("Crop_MouseDown called...");
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("Crop_MouseDown: " + exception.Message);
                LogHelper.logger.Error("Crop_MouseDown: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("Crop_MouseDown finished...");
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
        public void DrawOnDesktopNextMove()
        {
            try
            {
                LogHelper.logger.Info("DrawOnDesktopNextMove called...");
                if (desktop == IntPtr.Zero)
                {
                    desktop = GetDC(IntPtr.Zero);
                }
                else
                {
                    desktop = IntPtr.Zero;
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

                    for (int i = 0; i < NextMoveHighlightDuration; i++)
                    {
                        //g.DrawRectangle(Pens.Red, new Rectangle(startX + 5, startY + 5, 50, 50));
                        //g.DrawRectangle(Pens.Blue, new Rectangle(startNewX + 5, startNewY + 5, 50, 50));

                        g.DrawRectangle(oldPen, new Rectangle(startX + 5, startY + 5, 50, 50));
                        g.DrawRectangle(newPen, new Rectangle(startNewX + 5, startNewY + 5, 50, 50));
                    }

                }
                //  ReleaseDC(desktop);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("DrawOnDesktopNextMove: " + exception.Message);
                LogHelper.logger.Error("DrawOnDesktopNextMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("DrawOnDesktopNextMove finished...");
        }
        private void GetChessDrawingIndex(char column, char row)
        {
            try
            {
                LogHelper.logger.Info("GetChessDrawingIndex called...");
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
                    chessDrawingRowIndex = rowIndex - 1;
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetChessDrawingIndex: " + exception.Message);
                LogHelper.logger.Error("GetChessDrawingIndex: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("GetChessDrawingIndex finished...");
        }

        private void GetChessDrawingNextMoveIndex(char column, char row)
        {
            try
            {
                LogHelper.logger.Info("GetChessDrawingNextMoveIndex called...");
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
                    chessDrawingNextRowIndex = rowIndex - 1;
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetChessDrawingIndex: " + exception.Message);
                LogHelper.logger.Error("GetChessDrawingIndex: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("GetChessDrawingNextMoveIndex finished...");
        }
        #endregion

        #region Crop board and Validate
        private void CropChessBoard()
        {
            try
            {
                LogHelper.logger.Info("CropChessBoard called...");
                if (croprect.IsEmpty)
                    return;

                //txtWidth.Text = croprect.Width.ToString();
                //txtHeight.Text = croprect.Height.ToString();

                if (croprect.Width % 2 != 0 || croprect.Height % 2 != 0)
                {
                    MessageBox.Show("Please select region with heigh and width divisible by 2 for accuracy.");
                    return;
                }

                ScreenBoardCoordinates = new Rectangle();
                ScreenBoardCoordinates = croprect;
                PreviousScreenBoardCoordinates = croprect;

                pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);

                CapturedScreen = pbScreen.Image;
                ClearSelection();

                //txtResizeLeft.Text = "0";
                //txtResizeTop.Text = "0";
                //txtWidth.Text = CapturedScreen.Width.ToString();
                //txtHeight.Text = CapturedScreen.Height.ToString();

                //cbAutoRefresh.Checked = false;
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("CropChessBoard: " + exception.Message);
                LogHelper.logger.Error("CropChessBoard: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("CropChessBoard finished...");
        }
        private bool ScanBoardAgain()
        {
            try
            {
                LogHelper.logger.Info("ScanBoardAgain called...");
                croprect = PreviousScreenBoardCoordinates;
                //croprect = ScreenBoardCoordinates;
                if (croprect == Rectangle.Empty)
                {
                    MessageBox.Show("Board selection is invalid. Please crop board properly.");
                    return false;
                }
                Image img = (Image)ImageProcessingManager.TakeScreenShot();
                CapturedScreen = img;
                pbScreen.Image = img;

                ScreenBoardCoordinates = new Rectangle();
                ScreenBoardCoordinates = croprect;

                pbScreen.Image = ((Bitmap)CapturedScreen).Clone(croprect, CapturedScreen.PixelFormat);

                CapturedScreen = pbScreen.Image;
                ClearSelection();

                //txtResizeLeft.Text = "0";
                //txtResizeTop.Text = "0";
                //txtWidth.Text = CapturedScreen.Width.ToString();
                //txtHeight.Text = CapturedScreen.Height.ToString();

            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("ScanBoardAgain: " + exception.Message);
                LogHelper.logger.Error("ScanBoardAgain: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("ScanBoardAgain finished...");
            return true;
        }
        private void ValidateBoard()
        {
            LogHelper.logger.Info("ValidateBoard called...");
            string message = string.Empty;
            try
            {
                if (pbScreen.Image.Width % 8 != 0 && pbScreen.Image.Height % 8 != 0)
                {
                    int requiredSize = pbScreen.Image.Width / 8;
                    message = string.Format("The cropped chess board is not even. Make sure it is perfect square. Try resizing to Width x Height :{0} x {0}", (requiredSize * 8).ToString());
                }
                else
                {
                    message = "Great!!! Let's play...";
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("ValidateBoard: " + exception.Message);
                LogHelper.logger.Error("ValidateBoard: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show(message);
            LogHelper.logger.Info("ValidateBoard finished...");
        }
        private void ClearSelection()
        {
            sp = new Point(0, 0);
            ep = new Point(0, 0);
            croprect = Rectangle.Empty;
            pbScreen.Invalidate();
        }
        #endregion

        #region Process Board and Print Methods

        private void LoadTemplates()
        {
            try
            {
                LogHelper.logger.Info("LoadTemplates called...");
                string tempalteCatalogFileName = ImageProcessingManager.TemplatePath + Constants.TEMPLATE_CATELOG_FILE;
                if (File.Exists(tempalteCatalogFileName) == false)
                {
                    MessageBox.Show("There are no template present. Please create and save them.", "Load Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("LoadTemplate: " + exception.Message);
                LogHelper.logger.Error("LoadTemplate: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("LoadTemplates finished...");
        }

        private void ProcessAndPrintBoard()
        {
            try
            {
                LogHelper.logger.Info("ProcessAndPrintBoard called...");
                Cursor = Cursors.WaitCursor;
                //Console.WriteLine("Reading current Chess position...");
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
                lblExecutionTime.Text = ImageProcessingManager.TotalProcessingTime;

                if (string.IsNullOrEmpty(fenString) == false && fenString.Contains(Constants.INVALID_FEN_STRING) == false)
                {
                    GetNextBestMove(fenString);
                }
                else
                {
                    MessageBox.Show("Invalid chess board screen fed. Please check proper chess board is configured.");
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("ProcessAndPrintBoard: " + exception.Message);
                LogHelper.logger.Error("ProcessAndPrintBoard: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("ProcessAndPrintBoard finished...");
        }

        private void CheckWhosTurnToPlay()
        {
            try
            {
                LogHelper.logger.Info("CheckWhosTurnToPlay called...");
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
                            if (hasComputerPlayed)
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("CheckWhosTurnToPlay: " + exception.Message);
                LogHelper.logger.Error("CheckWhosTurnToPlay: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("CheckWhosTurnToPlay finished...");
        }

        private void RefreshGrayImage()
        {
            try
            {
                LogHelper.logger.Info("RefreshGrayImage called...");
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("RefreshGrayImage: " + exception.Message);
                LogHelper.logger.Error("RefreshGrayImage: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("RefreshGrayImage finished...");
        }

        #endregion

        #region Get Next Best Move
        private void GetBestMove()
        {
            try
            {
                LogHelper.logger.Info("GetBestMove called...");
                IsComputingNextMove = true;
                if (ScanBoardAgain() == false)
                { return; }

                txtStatus.Text = "Computing next best move...";
                //showNextMove.IsThinkingNextMove = true;
                SmallView.IsThinkingNextMove = true;
                //timerAutoRefresh.Enabled = false;
                Application.DoEvents();
                ProcessAndPrintBoard();
                //timerAutoRefresh.Enabled = true;
                //DrawOnDesktop();
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetBestMove: " + exception.Message);
                LogHelper.logger.Error("GetBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("GetBestMove finished...");
        }

        private string GetNextBestMove(string fenString)
        {
            LogHelper.logger.Info("Engine: GetNextBestMove called for fenstring: " + fenString);
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
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("GetNextBestMove finished...");
            return bestMove;
        }
        private void engine_BestMovFound(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("engine_BestMovFound called...");
                var args = (BestMoveFoundArgs)e;
                if (false == string.IsNullOrEmpty(args.BestMove))
                {
                    //MessageBox.Show(args.BestMove);

                    //showNextMove.IsThinkingNextMove = false;

                    SmallView.IsThinkingNextMove = false;
                    SmallView.BestMove = args.BestMove;
                    SmallView.MoveScore = args.CurrentMoveScore;
                    SmallView.TimeTaken = ImageProcessingManager.TotalProcessingTime;
                    Application.DoEvents();
                    //showNextMove.BestMove = args.BestMove;
                    if (statusStrip1.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            txtStatus.Text = "Best move is: " + args.BestMove;
                            txtScore.Text = "Score of move is: " + args.CurrentMoveScore;
                        });
                    }

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
            catch (Exception exception)
            {
                LogHelper.logger.Error("engine_BestMovFound: " + exception.Message);
                LogHelper.logger.Error("engine_BestMovFound: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("engine_BestMovFound finished...");
        }
        #endregion

        private void cbEnableLogging_CheckedChanged(object sender, EventArgs e)
        {
            LogHelper.SetLoggingState(cbEnableLogging.Checked);
        }

        private void txtStandardMatchingFactor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogHelper.logger.Info("txtStandardMatchingFactor_TextChanged called...");
                ImageProcessingManager.StandardMatchingFactor = (double)(int.Parse(txtStandardMatchingFactor.Text) / 100.0);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("txtStandardMatchingFactor_TextChanged: " + exception.Message);
                LogHelper.logger.Error("txtStandardMatchingFactor_TextChanged: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LogHelper.logger.Info("txtStandardMatchingFactor_TextChanged finished...");
        }

        private void txtHighlightDuration_TextChanged(object sender, EventArgs e)
        {
            int duration = NextMoveHighlightDuration;
            if (int.TryParse(txtHighlightDuration.Text, out duration))
            {
                NextMoveHighlightDuration = duration;
            }
        }

        private void btnUpdatedSelection_Click(object sender, EventArgs e)
        {
            int left = 0;
            int top= 0;
            int width= 0;
            int height = 0;

            
            int.TryParse(txtSelectedLeft.Text, out left);
            int.TryParse(txtSelectedTop.Text, out top);
            int.TryParse(txtSelectedWidth.Text, out width);
            int.TryParse(txtSelectedHeight.Text, out height);

            int diffWidth = width - croprect.Width;
            int diffHeigght = height- croprect.Height;

            ep = new Point(ep.X + diffWidth, ep.Y + diffHeigght);
            croprect = new Rectangle(left,top,width,height);
            pbScreen.Invalidate();
        }

        private void tbIntensity_Scroll(object sender, EventArgs e)
        {
            txtIntensity.Text = tbIntensity.Value.ToString();
        }

       
    }
}

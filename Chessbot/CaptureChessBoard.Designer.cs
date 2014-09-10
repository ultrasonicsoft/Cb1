namespace OpenCVDemo1
{
    partial class CaptureChessBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.timerTriggerChecker = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlImageHolder = new System.Windows.Forms.Panel();
            this.pbScreen = new System.Windows.Forms.PictureBox();
            this.tbctrController = new System.Windows.Forms.TabControl();
            this.tbHome = new System.Windows.Forms.TabPage();
            this.txtFENString = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbtnNoBlackCastling = new System.Windows.Forms.RadioButton();
            this.rbtnBlackQueenCastling = new System.Windows.Forms.RadioButton();
            this.rbtnBlackKingCastling = new System.Windows.Forms.RadioButton();
            this.rbtnBothBlackCastling = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtnNoWhiteCastling = new System.Windows.Forms.RadioButton();
            this.rbtnWhiteQueenCastling = new System.Windows.Forms.RadioButton();
            this.rbtnWhiteKingCastling = new System.Windows.Forms.RadioButton();
            this.rbtnBothWhiteCastling = new System.Windows.Forms.RadioButton();
            this.txtBoardConfiguration = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBestMove = new System.Windows.Forms.TextBox();
            this.btnGetBestMove = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.btnCompactView = new System.Windows.Forms.Button();
            this.lblExecutionTime = new System.Windows.Forms.Label();
            this.btnStartNewGame = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.pbIntensityTest = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEngineConfiguration = new System.Windows.Forms.Button();
            this.lblWhosMove = new System.Windows.Forms.Label();
            this.btnMarkTrigger = new System.Windows.Forms.Button();
            this.cbTriggerMarker = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.pbTriggerImage = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.pbCurrentMarker = new System.Windows.Forms.PictureBox();
            this.txtRefreshMarkerInterval = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.txtPadding = new System.Windows.Forms.TextBox();
            this.rbtnBlack = new System.Windows.Forms.RadioButton();
            this.btnRefreshTemplate = new System.Windows.Forms.Button();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnDeleteTemplate = new System.Windows.Forms.Button();
            this.rbtnWhite = new System.Windows.Forms.RadioButton();
            this.cmbTemplates = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnLoadTemplate = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbAutoRefresh = new System.Windows.Forms.CheckBox();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.btnScanAgain = new System.Windows.Forms.Button();
            this.btnCaptureScreen = new System.Windows.Forms.Button();
            this.btnCropBoard = new System.Windows.Forms.Button();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblCurrentMouseY = new System.Windows.Forms.Label();
            this.txtSelectedTop = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSelectedWidth = new System.Windows.Forms.TextBox();
            this.lblCurrentMouseX = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtSelectedHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUpdatedSelection = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.txtSelectedLeft = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHighlightBoxSize = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtHighlightDuration = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtEngineDepth = new System.Windows.Forms.TextBox();
            this.txtStandardMatchingFactor = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbShowIntensityOnTop = new System.Windows.Forms.CheckBox();
            this.txtIntensity = new System.Windows.Forms.TextBox();
            this.btnUseIntensity = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.tbIntensity = new System.Windows.Forms.TrackBar();
            this.label22 = new System.Windows.Forms.Label();
            this.cbEnableLogging = new System.Windows.Forms.CheckBox();
            this.cbEnableHotKey = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbDrawNextMoveOnScreen = new System.Windows.Forms.CheckBox();
            this.cbAutoMove = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlImageHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreen)).BeginInit();
            this.tbctrController.SuspendLayout();
            this.tbHome.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIntensityTest)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTriggerImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentMarker)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensity)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerAutoRefresh
            // 
            this.timerAutoRefresh.Interval = 50;
            this.timerAutoRefresh.Tick += new System.EventHandler(this.timerAutoRefresh_Tick);
            // 
            // timerTriggerChecker
            // 
            this.timerTriggerChecker.Interval = 30;
            this.timerTriggerChecker.Tick += new System.EventHandler(this.timerTriggerChecker_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.pnlImageHolder);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbctrController);
            this.splitContainer1.Size = new System.Drawing.Size(1124, 664);
            this.splitContainer1.SplitterDistance = 368;
            this.splitContainer1.TabIndex = 2;
            // 
            // pnlImageHolder
            // 
            this.pnlImageHolder.AutoScroll = true;
            this.pnlImageHolder.Controls.Add(this.pbScreen);
            this.pnlImageHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImageHolder.Location = new System.Drawing.Point(0, 0);
            this.pnlImageHolder.Name = "pnlImageHolder";
            this.pnlImageHolder.Size = new System.Drawing.Size(1122, 366);
            this.pnlImageHolder.TabIndex = 2;
            // 
            // pbScreen
            // 
            this.pbScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbScreen.Location = new System.Drawing.Point(0, 0);
            this.pbScreen.Name = "pbScreen";
            this.pbScreen.Size = new System.Drawing.Size(1122, 270);
            this.pbScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbScreen.TabIndex = 3;
            this.pbScreen.TabStop = false;
            // 
            // tbctrController
            // 
            this.tbctrController.Controls.Add(this.tbHome);
            this.tbctrController.Controls.Add(this.tabPage1);
            this.tbctrController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbctrController.Location = new System.Drawing.Point(0, 0);
            this.tbctrController.Name = "tbctrController";
            this.tbctrController.SelectedIndex = 0;
            this.tbctrController.Size = new System.Drawing.Size(1122, 290);
            this.tbctrController.TabIndex = 1;
            // 
            // tbHome
            // 
            this.tbHome.Controls.Add(this.cbAutoMove);
            this.tbHome.Controls.Add(this.cbDrawNextMoveOnScreen);
            this.tbHome.Controls.Add(this.txtFENString);
            this.tbHome.Controls.Add(this.groupBox7);
            this.tbHome.Controls.Add(this.groupBox4);
            this.tbHome.Controls.Add(this.txtBoardConfiguration);
            this.tbHome.Controls.Add(this.groupBox2);
            this.tbHome.Controls.Add(this.pbIntensityTest);
            this.tbHome.Controls.Add(this.groupBox1);
            this.tbHome.Controls.Add(this.label2);
            this.tbHome.Controls.Add(this.groupBox5);
            this.tbHome.Location = new System.Drawing.Point(4, 22);
            this.tbHome.Name = "tbHome";
            this.tbHome.Padding = new System.Windows.Forms.Padding(3);
            this.tbHome.Size = new System.Drawing.Size(1114, 264);
            this.tbHome.TabIndex = 0;
            this.tbHome.Text = "Control Panel";
            this.tbHome.UseVisualStyleBackColor = true;
            // 
            // txtFENString
            // 
            this.txtFENString.Location = new System.Drawing.Point(582, 143);
            this.txtFENString.Multiline = true;
            this.txtFENString.Name = "txtFENString";
            this.txtFENString.Size = new System.Drawing.Size(174, 36);
            this.txtFENString.TabIndex = 67;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbtnNoBlackCastling);
            this.groupBox7.Controls.Add(this.rbtnBlackQueenCastling);
            this.groupBox7.Controls.Add(this.rbtnBlackKingCastling);
            this.groupBox7.Controls.Add(this.rbtnBothBlackCastling);
            this.groupBox7.Location = new System.Drawing.Point(582, 67);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(174, 48);
            this.groupBox7.TabIndex = 66;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Black Castling";
            // 
            // rbtnNoBlackCastling
            // 
            this.rbtnNoBlackCastling.AutoSize = true;
            this.rbtnNoBlackCastling.Location = new System.Drawing.Point(135, 20);
            this.rbtnNoBlackCastling.Name = "rbtnNoBlackCastling";
            this.rbtnNoBlackCastling.Size = new System.Drawing.Size(31, 17);
            this.rbtnNoBlackCastling.TabIndex = 72;
            this.rbtnNoBlackCastling.Text = "n";
            this.rbtnNoBlackCastling.UseVisualStyleBackColor = true;
            // 
            // rbtnBlackQueenCastling
            // 
            this.rbtnBlackQueenCastling.AutoSize = true;
            this.rbtnBlackQueenCastling.Location = new System.Drawing.Point(101, 21);
            this.rbtnBlackQueenCastling.Name = "rbtnBlackQueenCastling";
            this.rbtnBlackQueenCastling.Size = new System.Drawing.Size(31, 17);
            this.rbtnBlackQueenCastling.TabIndex = 72;
            this.rbtnBlackQueenCastling.Text = "q";
            this.rbtnBlackQueenCastling.UseVisualStyleBackColor = true;
            // 
            // rbtnBlackKingCastling
            // 
            this.rbtnBlackKingCastling.AutoSize = true;
            this.rbtnBlackKingCastling.Location = new System.Drawing.Point(55, 22);
            this.rbtnBlackKingCastling.Name = "rbtnBlackKingCastling";
            this.rbtnBlackKingCastling.Size = new System.Drawing.Size(31, 17);
            this.rbtnBlackKingCastling.TabIndex = 71;
            this.rbtnBlackKingCastling.Text = "k";
            this.rbtnBlackKingCastling.UseVisualStyleBackColor = true;
            // 
            // rbtnBothBlackCastling
            // 
            this.rbtnBothBlackCastling.AutoSize = true;
            this.rbtnBothBlackCastling.Checked = true;
            this.rbtnBothBlackCastling.Location = new System.Drawing.Point(9, 22);
            this.rbtnBothBlackCastling.Name = "rbtnBothBlackCastling";
            this.rbtnBothBlackCastling.Size = new System.Drawing.Size(37, 17);
            this.rbtnBothBlackCastling.TabIndex = 69;
            this.rbtnBothBlackCastling.TabStop = true;
            this.rbtnBothBlackCastling.Text = "kq";
            this.rbtnBothBlackCastling.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnNoWhiteCastling);
            this.groupBox4.Controls.Add(this.rbtnWhiteQueenCastling);
            this.groupBox4.Controls.Add(this.rbtnWhiteKingCastling);
            this.groupBox4.Controls.Add(this.rbtnBothWhiteCastling);
            this.groupBox4.Location = new System.Drawing.Point(582, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(174, 51);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "White Castling";
            // 
            // rbtnNoWhiteCastling
            // 
            this.rbtnNoWhiteCastling.AutoSize = true;
            this.rbtnNoWhiteCastling.Location = new System.Drawing.Point(135, 22);
            this.rbtnNoWhiteCastling.Name = "rbtnNoWhiteCastling";
            this.rbtnNoWhiteCastling.Size = new System.Drawing.Size(33, 17);
            this.rbtnNoWhiteCastling.TabIndex = 68;
            this.rbtnNoWhiteCastling.Text = "N";
            this.rbtnNoWhiteCastling.UseVisualStyleBackColor = true;
            // 
            // rbtnWhiteQueenCastling
            // 
            this.rbtnWhiteQueenCastling.AutoSize = true;
            this.rbtnWhiteQueenCastling.Location = new System.Drawing.Point(101, 21);
            this.rbtnWhiteQueenCastling.Name = "rbtnWhiteQueenCastling";
            this.rbtnWhiteQueenCastling.Size = new System.Drawing.Size(33, 17);
            this.rbtnWhiteQueenCastling.TabIndex = 68;
            this.rbtnWhiteQueenCastling.Text = "Q";
            this.rbtnWhiteQueenCastling.UseVisualStyleBackColor = true;
            // 
            // rbtnWhiteKingCastling
            // 
            this.rbtnWhiteKingCastling.AutoSize = true;
            this.rbtnWhiteKingCastling.Location = new System.Drawing.Point(55, 22);
            this.rbtnWhiteKingCastling.Name = "rbtnWhiteKingCastling";
            this.rbtnWhiteKingCastling.Size = new System.Drawing.Size(32, 17);
            this.rbtnWhiteKingCastling.TabIndex = 67;
            this.rbtnWhiteKingCastling.Text = "K";
            this.rbtnWhiteKingCastling.UseVisualStyleBackColor = true;
            // 
            // rbtnBothWhiteCastling
            // 
            this.rbtnBothWhiteCastling.AutoSize = true;
            this.rbtnBothWhiteCastling.Checked = true;
            this.rbtnBothWhiteCastling.Location = new System.Drawing.Point(9, 22);
            this.rbtnBothWhiteCastling.Name = "rbtnBothWhiteCastling";
            this.rbtnBothWhiteCastling.Size = new System.Drawing.Size(40, 17);
            this.rbtnBothWhiteCastling.TabIndex = 38;
            this.rbtnBothWhiteCastling.TabStop = true;
            this.rbtnBothWhiteCastling.Text = "KQ";
            this.rbtnBothWhiteCastling.UseVisualStyleBackColor = true;
            // 
            // txtBoardConfiguration
            // 
            this.txtBoardConfiguration.Location = new System.Drawing.Point(778, 6);
            this.txtBoardConfiguration.Name = "txtBoardConfiguration";
            this.txtBoardConfiguration.Size = new System.Drawing.Size(290, 238);
            this.txtBoardConfiguration.TabIndex = 42;
            this.txtBoardConfiguration.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txtBestMove);
            this.groupBox2.Controls.Add(this.btnGetBestMove);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.btnCompactView);
            this.groupBox2.Controls.Add(this.lblExecutionTime);
            this.groupBox2.Controls.Add(this.btnStartNewGame);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Location = new System.Drawing.Point(275, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 215);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Game";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(6, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "Stop Finding Move";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtBestMove
            // 
            this.txtBestMove.Location = new System.Drawing.Point(74, 187);
            this.txtBestMove.Name = "txtBestMove";
            this.txtBestMove.Size = new System.Drawing.Size(47, 20);
            this.txtBestMove.TabIndex = 19;
            // 
            // btnGetBestMove
            // 
            this.btnGetBestMove.Enabled = false;
            this.btnGetBestMove.Location = new System.Drawing.Point(6, 97);
            this.btnGetBestMove.Name = "btnGetBestMove";
            this.btnGetBestMove.Size = new System.Drawing.Size(101, 23);
            this.btnGetBestMove.TabIndex = 23;
            this.btnGetBestMove.Text = "Get Best Move";
            this.btnGetBestMove.UseVisualStyleBackColor = true;
            this.btnGetBestMove.Click += new System.EventHandler(this.btnShowBoardConfiguration_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 190);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 13);
            this.label25.TabIndex = 2;
            this.label25.Text = "Next  Move:";
            // 
            // btnCompactView
            // 
            this.btnCompactView.Location = new System.Drawing.Point(6, 61);
            this.btnCompactView.Name = "btnCompactView";
            this.btnCompactView.Size = new System.Drawing.Size(101, 23);
            this.btnCompactView.TabIndex = 35;
            this.btnCompactView.Text = "Compact View";
            this.btnCompactView.UseVisualStyleBackColor = true;
            this.btnCompactView.Click += new System.EventHandler(this.btnCompactView_Click);
            // 
            // lblExecutionTime
            // 
            this.lblExecutionTime.AutoSize = true;
            this.lblExecutionTime.Location = new System.Drawing.Point(47, 171);
            this.lblExecutionTime.Name = "lblExecutionTime";
            this.lblExecutionTime.Size = new System.Drawing.Size(36, 13);
            this.lblExecutionTime.TabIndex = 1;
            this.lblExecutionTime.Text = "[Time]";
            // 
            // btnStartNewGame
            // 
            this.btnStartNewGame.Location = new System.Drawing.Point(6, 28);
            this.btnStartNewGame.Name = "btnStartNewGame";
            this.btnStartNewGame.Size = new System.Drawing.Size(101, 23);
            this.btnStartNewGame.TabIndex = 26;
            this.btnStartNewGame.Text = "Start New Game";
            this.btnStartNewGame.UseVisualStyleBackColor = true;
            this.btnStartNewGame.Click += new System.EventHandler(this.btnStartNewGame_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 171);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(33, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "Time:";
            // 
            // pbIntensityTest
            // 
            this.pbIntensityTest.Location = new System.Drawing.Point(1217, 24);
            this.pbIntensityTest.Name = "pbIntensityTest";
            this.pbIntensityTest.Size = new System.Drawing.Size(77, 69);
            this.pbIntensityTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIntensityTest.TabIndex = 39;
            this.pbIntensityTest.TabStop = false;
            this.pbIntensityTest.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEngineConfiguration);
            this.groupBox1.Controls.Add(this.lblWhosMove);
            this.groupBox1.Controls.Add(this.btnMarkTrigger);
            this.groupBox1.Controls.Add(this.cbTriggerMarker);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.pbTriggerImage);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.pbCurrentMarker);
            this.groupBox1.Controls.Add(this.txtRefreshMarkerInterval);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Location = new System.Drawing.Point(417, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 238);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Turn Marker";
            // 
            // btnEngineConfiguration
            // 
            this.btnEngineConfiguration.Location = new System.Drawing.Point(9, 190);
            this.btnEngineConfiguration.Name = "btnEngineConfiguration";
            this.btnEngineConfiguration.Size = new System.Drawing.Size(128, 25);
            this.btnEngineConfiguration.TabIndex = 65;
            this.btnEngineConfiguration.Text = "Engine Configuration";
            this.btnEngineConfiguration.UseVisualStyleBackColor = true;
            this.btnEngineConfiguration.Click += new System.EventHandler(this.btnEngineConfiguration_Click);
            // 
            // lblWhosMove
            // 
            this.lblWhosMove.AutoSize = true;
            this.lblWhosMove.Location = new System.Drawing.Point(71, 134);
            this.lblWhosMove.Name = "lblWhosMove";
            this.lblWhosMove.Size = new System.Drawing.Size(40, 13);
            this.lblWhosMove.TabIndex = 39;
            this.lblWhosMove.Text = "[Move]";
            // 
            // btnMarkTrigger
            // 
            this.btnMarkTrigger.Location = new System.Drawing.Point(9, 160);
            this.btnMarkTrigger.Name = "btnMarkTrigger";
            this.btnMarkTrigger.Size = new System.Drawing.Size(128, 24);
            this.btnMarkTrigger.TabIndex = 28;
            this.btnMarkTrigger.Text = "Crop User Turn Marker";
            this.btnMarkTrigger.UseVisualStyleBackColor = true;
            this.btnMarkTrigger.Click += new System.EventHandler(this.btnMarkTrigger_Click);
            // 
            // cbTriggerMarker
            // 
            this.cbTriggerMarker.AutoSize = true;
            this.cbTriggerMarker.Location = new System.Drawing.Point(22, 21);
            this.cbTriggerMarker.Name = "cbTriggerMarker";
            this.cbTriggerMarker.Size = new System.Drawing.Size(76, 17);
            this.cbTriggerMarker.TabIndex = 33;
            this.cbTriggerMarker.Text = "Auto Scan";
            this.cbTriggerMarker.UseVisualStyleBackColor = true;
            this.cbTriggerMarker.CheckedChanged += new System.EventHandler(this.cbTriggerMarker_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 106);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(105, 13);
            this.label20.TabIndex = 35;
            this.label20.Text = "Current Turn Marker:";
            // 
            // pbTriggerImage
            // 
            this.pbTriggerImage.Location = new System.Drawing.Point(117, 77);
            this.pbTriggerImage.Name = "pbTriggerImage";
            this.pbTriggerImage.Size = new System.Drawing.Size(20, 20);
            this.pbTriggerImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTriggerImage.TabIndex = 27;
            this.pbTriggerImage.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 81);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(93, 13);
            this.label19.TabIndex = 34;
            this.label19.Text = "User Turn Marker:";
            // 
            // pbCurrentMarker
            // 
            this.pbCurrentMarker.Location = new System.Drawing.Point(117, 103);
            this.pbCurrentMarker.Name = "pbCurrentMarker";
            this.pbCurrentMarker.Size = new System.Drawing.Size(20, 20);
            this.pbCurrentMarker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCurrentMarker.TabIndex = 29;
            this.pbCurrentMarker.TabStop = false;
            // 
            // txtRefreshMarkerInterval
            // 
            this.txtRefreshMarkerInterval.Location = new System.Drawing.Point(117, 45);
            this.txtRefreshMarkerInterval.Name = "txtRefreshMarkerInterval";
            this.txtRefreshMarkerInterval.Size = new System.Drawing.Size(20, 20);
            this.txtRefreshMarkerInterval.TabIndex = 21;
            this.txtRefreshMarkerInterval.Text = "30";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "Scan Interval (ms)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(579, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "FEN String:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSaveTemplate);
            this.groupBox5.Controls.Add(this.txtPadding);
            this.groupBox5.Controls.Add(this.rbtnBlack);
            this.groupBox5.Controls.Add(this.btnRefreshTemplate);
            this.groupBox5.Controls.Add(this.btnTemplate);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.btnDeleteTemplate);
            this.groupBox5.Controls.Add(this.rbtnWhite);
            this.groupBox5.Controls.Add(this.cmbTemplates);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.btnLoadTemplate);
            this.groupBox5.Location = new System.Drawing.Point(7, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(262, 215);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Template";
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Location = new System.Drawing.Point(6, 111);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(101, 24);
            this.btnSaveTemplate.TabIndex = 23;
            this.btnSaveTemplate.Text = "Save Template";
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // txtPadding
            // 
            this.txtPadding.Location = new System.Drawing.Point(220, 153);
            this.txtPadding.Name = "txtPadding";
            this.txtPadding.Size = new System.Drawing.Size(35, 20);
            this.txtPadding.TabIndex = 25;
            this.txtPadding.Text = "0";
            // 
            // rbtnBlack
            // 
            this.rbtnBlack.AutoSize = true;
            this.rbtnBlack.Location = new System.Drawing.Point(176, 183);
            this.rbtnBlack.Name = "rbtnBlack";
            this.rbtnBlack.Size = new System.Drawing.Size(53, 17);
            this.rbtnBlack.TabIndex = 28;
            this.rbtnBlack.Text = "White";
            this.rbtnBlack.UseVisualStyleBackColor = true;
            // 
            // btnRefreshTemplate
            // 
            this.btnRefreshTemplate.Location = new System.Drawing.Point(113, 71);
            this.btnRefreshTemplate.Name = "btnRefreshTemplate";
            this.btnRefreshTemplate.Size = new System.Drawing.Size(101, 23);
            this.btnRefreshTemplate.TabIndex = 37;
            this.btnRefreshTemplate.Text = "Refresh";
            this.btnRefreshTemplate.UseVisualStyleBackColor = true;
            this.btnRefreshTemplate.Click += new System.EventHandler(this.btnRefreshTemplate_Click);
            // 
            // btnTemplate
            // 
            this.btnTemplate.Location = new System.Drawing.Point(6, 150);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(101, 23);
            this.btnTemplate.TabIndex = 21;
            this.btnTemplate.Text = "Preview Template";
            this.btnTemplate.UseVisualStyleBackColor = true;
            this.btnTemplate.Click += new System.EventHandler(this.btnTemplate_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Block Padding (px):";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "What color is player?";
            // 
            // btnDeleteTemplate
            // 
            this.btnDeleteTemplate.Location = new System.Drawing.Point(113, 112);
            this.btnDeleteTemplate.Name = "btnDeleteTemplate";
            this.btnDeleteTemplate.Size = new System.Drawing.Size(101, 23);
            this.btnDeleteTemplate.TabIndex = 24;
            this.btnDeleteTemplate.Text = "Delete Template";
            this.btnDeleteTemplate.UseVisualStyleBackColor = true;
            this.btnDeleteTemplate.Click += new System.EventHandler(this.btnDeleteTemplate_Click);
            // 
            // rbtnWhite
            // 
            this.rbtnWhite.AutoSize = true;
            this.rbtnWhite.Checked = true;
            this.rbtnWhite.Location = new System.Drawing.Point(118, 183);
            this.rbtnWhite.Name = "rbtnWhite";
            this.rbtnWhite.Size = new System.Drawing.Size(52, 17);
            this.rbtnWhite.TabIndex = 27;
            this.rbtnWhite.TabStop = true;
            this.rbtnWhite.Text = "Black";
            this.rbtnWhite.UseVisualStyleBackColor = true;
            // 
            // cmbTemplates
            // 
            this.cmbTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplates.FormattingEnabled = true;
            this.cmbTemplates.Location = new System.Drawing.Point(6, 38);
            this.cmbTemplates.Name = "cmbTemplates";
            this.cmbTemplates.Size = new System.Drawing.Size(208, 21);
            this.cmbTemplates.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(122, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Select Master Template:";
            // 
            // btnLoadTemplate
            // 
            this.btnLoadTemplate.Location = new System.Drawing.Point(6, 71);
            this.btnLoadTemplate.Name = "btnLoadTemplate";
            this.btnLoadTemplate.Size = new System.Drawing.Size(101, 23);
            this.btnLoadTemplate.TabIndex = 19;
            this.btnLoadTemplate.Text = "Load Template";
            this.btnLoadTemplate.UseVisualStyleBackColor = true;
            this.btnLoadTemplate.Click += new System.EventHandler(this.btnLoadTemplate_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtHighlightBoxSize);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtHighlightDuration);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.txtEngineDepth);
            this.tabPage1.Controls.Add(this.txtStandardMatchingFactor);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.cbEnableLogging);
            this.tabPage1.Controls.Add(this.cbEnableHotKey);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1114, 264);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Advance Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbAutoRefresh);
            this.groupBox3.Controls.Add(this.txtRefreshInterval);
            this.groupBox3.Controls.Add(this.btnScanAgain);
            this.groupBox3.Controls.Add(this.btnCaptureScreen);
            this.groupBox3.Controls.Add(this.btnCropBoard);
            this.groupBox3.Controls.Add(this.btnClearSelection);
            this.groupBox3.Controls.Add(this.btnClearAll);
            this.groupBox3.Location = new System.Drawing.Point(215, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 141);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Capture Chess Board";
            // 
            // cbAutoRefresh
            // 
            this.cbAutoRefresh.AutoSize = true;
            this.cbAutoRefresh.Location = new System.Drawing.Point(16, 117);
            this.cbAutoRefresh.Name = "cbAutoRefresh";
            this.cbAutoRefresh.Size = new System.Drawing.Size(110, 17);
            this.cbAutoRefresh.TabIndex = 32;
            this.cbAutoRefresh.Text = "Auto Refresh (ms)";
            this.cbAutoRefresh.UseVisualStyleBackColor = true;
            this.cbAutoRefresh.CheckedChanged += new System.EventHandler(this.cbAutoRefresh_CheckedChanged);
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Location = new System.Drawing.Point(132, 115);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(26, 20);
            this.txtRefreshInterval.TabIndex = 30;
            this.txtRefreshInterval.Text = "50";
            this.txtRefreshInterval.Leave += new System.EventHandler(this.txtRefreshInterval_Leave);
            // 
            // btnScanAgain
            // 
            this.btnScanAgain.Location = new System.Drawing.Point(16, 86);
            this.btnScanAgain.Name = "btnScanAgain";
            this.btnScanAgain.Size = new System.Drawing.Size(95, 23);
            this.btnScanAgain.TabIndex = 29;
            this.btnScanAgain.Text = "Scan Again";
            this.btnScanAgain.UseVisualStyleBackColor = true;
            this.btnScanAgain.Click += new System.EventHandler(this.btnScanAgain_Click);
            // 
            // btnCaptureScreen
            // 
            this.btnCaptureScreen.Location = new System.Drawing.Point(16, 19);
            this.btnCaptureScreen.Name = "btnCaptureScreen";
            this.btnCaptureScreen.Size = new System.Drawing.Size(95, 23);
            this.btnCaptureScreen.TabIndex = 1;
            this.btnCaptureScreen.Text = "Capture Screen";
            this.btnCaptureScreen.UseVisualStyleBackColor = true;
            this.btnCaptureScreen.Click += new System.EventHandler(this.btnCaptureScreen_Click);
            // 
            // btnCropBoard
            // 
            this.btnCropBoard.Location = new System.Drawing.Point(16, 54);
            this.btnCropBoard.Name = "btnCropBoard";
            this.btnCropBoard.Size = new System.Drawing.Size(95, 23);
            this.btnCropBoard.TabIndex = 0;
            this.btnCropBoard.Text = "Crop Board";
            this.btnCropBoard.UseVisualStyleBackColor = true;
            this.btnCropBoard.Click += new System.EventHandler(this.btnCropBoard_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(138, 19);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(95, 23);
            this.btnClearSelection.TabIndex = 2;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(138, 54);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(95, 23);
            this.btnClearAll.TabIndex = 3;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblCurrentMouseY);
            this.groupBox8.Controls.Add(this.txtSelectedTop);
            this.groupBox8.Controls.Add(this.label15);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.txtSelectedWidth);
            this.groupBox8.Controls.Add(this.lblCurrentMouseX);
            this.groupBox8.Controls.Add(this.label26);
            this.groupBox8.Controls.Add(this.txtSelectedHeight);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.btnUpdatedSelection);
            this.groupBox8.Controls.Add(this.label27);
            this.groupBox8.Controls.Add(this.txtSelectedLeft);
            this.groupBox8.Controls.Add(this.label28);
            this.groupBox8.Location = new System.Drawing.Point(460, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(251, 140);
            this.groupBox8.TabIndex = 60;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Selection Size";
            // 
            // lblCurrentMouseY
            // 
            this.lblCurrentMouseY.AutoSize = true;
            this.lblCurrentMouseY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMouseY.Location = new System.Drawing.Point(151, 93);
            this.lblCurrentMouseY.Name = "lblCurrentMouseY";
            this.lblCurrentMouseY.Size = new System.Drawing.Size(23, 13);
            this.lblCurrentMouseY.TabIndex = 11;
            this.lblCurrentMouseY.Text = "[Y]";
            // 
            // txtSelectedTop
            // 
            this.txtSelectedTop.Location = new System.Drawing.Point(131, 25);
            this.txtSelectedTop.Name = "txtSelectedTop";
            this.txtSelectedTop.Size = new System.Drawing.Size(35, 20);
            this.txtSelectedTop.TabIndex = 20;
            this.txtSelectedTop.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Width";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(128, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Y:";
            // 
            // txtSelectedWidth
            // 
            this.txtSelectedWidth.Location = new System.Drawing.Point(44, 59);
            this.txtSelectedWidth.Name = "txtSelectedWidth";
            this.txtSelectedWidth.Size = new System.Drawing.Size(35, 20);
            this.txtSelectedWidth.TabIndex = 13;
            this.txtSelectedWidth.Text = "0";
            // 
            // lblCurrentMouseX
            // 
            this.lblCurrentMouseX.AutoSize = true;
            this.lblCurrentMouseX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMouseX.Location = new System.Drawing.Point(54, 93);
            this.lblCurrentMouseX.Name = "lblCurrentMouseX";
            this.lblCurrentMouseX.Size = new System.Drawing.Size(23, 13);
            this.lblCurrentMouseX.TabIndex = 9;
            this.lblCurrentMouseX.Text = "[X]";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(87, 62);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(38, 13);
            this.label26.TabIndex = 14;
            this.label26.Text = "Height";
            // 
            // txtSelectedHeight
            // 
            this.txtSelectedHeight.Location = new System.Drawing.Point(131, 59);
            this.txtSelectedHeight.Name = "txtSelectedHeight";
            this.txtSelectedHeight.Size = new System.Drawing.Size(35, 20);
            this.txtSelectedHeight.TabIndex = 15;
            this.txtSelectedHeight.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "X:";
            // 
            // btnUpdatedSelection
            // 
            this.btnUpdatedSelection.Location = new System.Drawing.Point(181, 23);
            this.btnUpdatedSelection.Name = "btnUpdatedSelection";
            this.btnUpdatedSelection.Size = new System.Drawing.Size(61, 57);
            this.btnUpdatedSelection.TabIndex = 16;
            this.btnUpdatedSelection.Text = "Update Selection";
            this.btnUpdatedSelection.UseVisualStyleBackColor = true;
            this.btnUpdatedSelection.Click += new System.EventHandler(this.btnUpdatedSelection_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(13, 28);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(25, 13);
            this.label27.TabIndex = 17;
            this.label27.Text = "Left";
            // 
            // txtSelectedLeft
            // 
            this.txtSelectedLeft.Location = new System.Drawing.Point(44, 25);
            this.txtSelectedLeft.Name = "txtSelectedLeft";
            this.txtSelectedLeft.Size = new System.Drawing.Size(35, 20);
            this.txtSelectedLeft.TabIndex = 18;
            this.txtSelectedLeft.Text = "0";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(99, 29);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(26, 13);
            this.label28.TabIndex = 19;
            this.label28.Text = "Top";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Highlight Box Size";
            // 
            // txtHighlightBoxSize
            // 
            this.txtHighlightBoxSize.Location = new System.Drawing.Point(7, 99);
            this.txtHighlightBoxSize.Name = "txtHighlightBoxSize";
            this.txtHighlightBoxSize.Size = new System.Drawing.Size(33, 20);
            this.txtHighlightBoxSize.TabIndex = 61;
            this.txtHighlightBoxSize.Text = "20";
            this.txtHighlightBoxSize.TextChanged += new System.EventHandler(this.txtHighlightDuration_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(46, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(160, 13);
            this.label14.TabIndex = 62;
            this.label14.Text = "Next Move Highlighting Duration";
            // 
            // txtHighlightDuration
            // 
            this.txtHighlightDuration.Location = new System.Drawing.Point(7, 69);
            this.txtHighlightDuration.Name = "txtHighlightDuration";
            this.txtHighlightDuration.Size = new System.Drawing.Size(33, 20);
            this.txtHighlightDuration.TabIndex = 61;
            this.txtHighlightDuration.Text = "100";
            this.txtHighlightDuration.TextChanged += new System.EventHandler(this.txtHighlightDuration_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(46, 41);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(72, 13);
            this.label24.TabIndex = 58;
            this.label24.Text = "Engine Depth";
            // 
            // txtEngineDepth
            // 
            this.txtEngineDepth.Location = new System.Drawing.Point(7, 38);
            this.txtEngineDepth.Name = "txtEngineDepth";
            this.txtEngineDepth.Size = new System.Drawing.Size(33, 20);
            this.txtEngineDepth.TabIndex = 57;
            this.txtEngineDepth.Text = "16";
            this.txtEngineDepth.TextChanged += new System.EventHandler(this.txtEngineDepth_TextChanged);
            // 
            // txtStandardMatchingFactor
            // 
            this.txtStandardMatchingFactor.Location = new System.Drawing.Point(7, 8);
            this.txtStandardMatchingFactor.Name = "txtStandardMatchingFactor";
            this.txtStandardMatchingFactor.Size = new System.Drawing.Size(33, 20);
            this.txtStandardMatchingFactor.TabIndex = 52;
            this.txtStandardMatchingFactor.Text = "75";
            this.txtStandardMatchingFactor.TextChanged += new System.EventHandler(this.txtStandardMatchingFactor_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbShowIntensityOnTop);
            this.groupBox6.Controls.Add(this.txtIntensity);
            this.groupBox6.Controls.Add(this.btnUseIntensity);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.tbIntensity);
            this.groupBox6.Location = new System.Drawing.Point(734, 11);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(219, 118);
            this.groupBox6.TabIndex = 56;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Intensity Settings";
            // 
            // cbShowIntensityOnTop
            // 
            this.cbShowIntensityOnTop.AutoSize = true;
            this.cbShowIntensityOnTop.Location = new System.Drawing.Point(6, 99);
            this.cbShowIntensityOnTop.Name = "cbShowIntensityOnTop";
            this.cbShowIntensityOnTop.Size = new System.Drawing.Size(173, 17);
            this.cbShowIntensityOnTop.TabIndex = 33;
            this.cbShowIntensityOnTop.Text = "Show Intensity Preview on Top";
            this.cbShowIntensityOnTop.UseVisualStyleBackColor = true;
            this.cbShowIntensityOnTop.CheckedChanged += new System.EventHandler(this.cbShowIntensityOnTop_CheckedChanged);
            // 
            // txtIntensity
            // 
            this.txtIntensity.Location = new System.Drawing.Point(62, 24);
            this.txtIntensity.Name = "txtIntensity";
            this.txtIntensity.Size = new System.Drawing.Size(33, 20);
            this.txtIntensity.TabIndex = 49;
            this.txtIntensity.Text = "100";
            // 
            // btnUseIntensity
            // 
            this.btnUseIntensity.Location = new System.Drawing.Point(101, 24);
            this.btnUseIntensity.Name = "btnUseIntensity";
            this.btnUseIntensity.Size = new System.Drawing.Size(94, 23);
            this.btnUseIntensity.TabIndex = 48;
            this.btnUseIntensity.Text = "Use Intensity";
            this.btnUseIntensity.UseVisualStyleBackColor = true;
            this.btnUseIntensity.Click += new System.EventHandler(this.btnUseIntensity_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 13);
            this.label21.TabIndex = 47;
            this.label21.Text = "Intensity:";
            // 
            // tbIntensity
            // 
            this.tbIntensity.Location = new System.Drawing.Point(6, 50);
            this.tbIntensity.Maximum = 255;
            this.tbIntensity.Name = "tbIntensity";
            this.tbIntensity.Size = new System.Drawing.Size(207, 45);
            this.tbIntensity.TabIndex = 46;
            this.tbIntensity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbIntensity.Value = 100;
            this.tbIntensity.Scroll += new System.EventHandler(this.tbIntensity_Scroll);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(46, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 13);
            this.label22.TabIndex = 50;
            this.label22.Text = "Matching Factor (%)";
            // 
            // cbEnableLogging
            // 
            this.cbEnableLogging.AutoSize = true;
            this.cbEnableLogging.Location = new System.Drawing.Point(7, 161);
            this.cbEnableLogging.Name = "cbEnableLogging";
            this.cbEnableLogging.Size = new System.Drawing.Size(100, 17);
            this.cbEnableLogging.TabIndex = 55;
            this.cbEnableLogging.Text = "Enable Logging";
            this.cbEnableLogging.UseVisualStyleBackColor = true;
            this.cbEnableLogging.CheckedChanged += new System.EventHandler(this.cbEnableLogging_CheckedChanged);
            // 
            // cbEnableHotKey
            // 
            this.cbEnableHotKey.AutoSize = true;
            this.cbEnableHotKey.Location = new System.Drawing.Point(7, 134);
            this.cbEnableHotKey.Name = "cbEnableHotKey";
            this.cbEnableHotKey.Size = new System.Drawing.Size(100, 17);
            this.cbEnableHotKey.TabIndex = 54;
            this.cbEnableHotKey.Text = "Enable Hot Key";
            this.cbEnableHotKey.UseVisualStyleBackColor = true;
            this.cbEnableHotKey.CheckedChanged += new System.EventHandler(this.cbEnableHotKey_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus,
            this.txtScore,
            this.txtMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 642);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1124, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(47, 17);
            this.txtStatus.Text = "[Status]";
            // 
            // txtScore
            // 
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(44, 17);
            this.txtScore.Text = "[Score]";
            // 
            // txtMessage
            // 
            this.txtMessage.Margin = new System.Windows.Forms.Padding(100, 3, 0, 2);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(61, 17);
            this.txtMessage.Text = "[Message]";
            // 
            // cbDrawNextMoveOnScreen
            // 
            this.cbDrawNextMoveOnScreen.AutoSize = true;
            this.cbDrawNextMoveOnScreen.Checked = true;
            this.cbDrawNextMoveOnScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDrawNextMoveOnScreen.Location = new System.Drawing.Point(582, 185);
            this.cbDrawNextMoveOnScreen.Name = "cbDrawNextMoveOnScreen";
            this.cbDrawNextMoveOnScreen.Size = new System.Drawing.Size(160, 17);
            this.cbDrawNextMoveOnScreen.TabIndex = 38;
            this.cbDrawNextMoveOnScreen.Text = "Draw Next Move On Screen";
            this.cbDrawNextMoveOnScreen.UseVisualStyleBackColor = true;
            // 
            // cbAutoMove
            // 
            this.cbAutoMove.AutoSize = true;
            this.cbAutoMove.Checked = true;
            this.cbAutoMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoMove.Location = new System.Drawing.Point(582, 208);
            this.cbAutoMove.Name = "cbAutoMove";
            this.cbAutoMove.Size = new System.Drawing.Size(149, 17);
            this.cbAutoMove.TabIndex = 68;
            this.cbAutoMove.Text = "Do Auto Move On Screen";
            this.cbAutoMove.UseVisualStyleBackColor = true;
            // 
            // CaptureChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1124, 664);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "CaptureChessBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Capture Chess Board";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaptureChessBoard_FormClosing);
            this.Load += new System.EventHandler(this.CaptureChessBoard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CaptureChessBoard_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlImageHolder.ResumeLayout(false);
            this.pnlImageHolder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreen)).EndInit();
            this.tbctrController.ResumeLayout(false);
            this.tbHome.ResumeLayout(false);
            this.tbHome.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIntensityTest)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTriggerImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentMarker)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensity)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerAutoRefresh;
        private System.Windows.Forms.Timer timerTriggerChecker;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tbctrController;
        private System.Windows.Forms.TabPage tbHome;
        private System.Windows.Forms.PictureBox pbIntensityTest;
        private System.Windows.Forms.Button btnRefreshTemplate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblWhosMove;
        private System.Windows.Forms.Button btnMarkTrigger;
        private System.Windows.Forms.CheckBox cbTriggerMarker;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.PictureBox pbTriggerImage;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox pbCurrentMarker;
        private System.Windows.Forms.TextBox txtRefreshMarkerInterval;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnStartNewGame;
        private System.Windows.Forms.Button btnDeleteTemplate;
        private System.Windows.Forms.Button btnGetBestMove;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.TextBox txtPadding;
        private System.Windows.Forms.RadioButton rbtnBlack;
        private System.Windows.Forms.Button btnTemplate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbtnWhite;
        private System.Windows.Forms.Button btnLoadTemplate;
        private System.Windows.Forms.ComboBox cmbTemplates;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblExecutionTime;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.Panel pnlImageHolder;
        private System.Windows.Forms.PictureBox pbScreen;
        private System.Windows.Forms.Button btnCompactView;
        private System.Windows.Forms.ToolStripStatusLabel txtScore;
        private System.Windows.Forms.ToolStripStatusLabel txtMessage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox cbEnableHotKey;
        private System.Windows.Forms.CheckBox cbEnableLogging;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtStandardMatchingFactor;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox cbShowIntensityOnTop;
        private System.Windows.Forms.TextBox txtIntensity;
        private System.Windows.Forms.Button btnUseIntensity;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TrackBar tbIntensity;
        private System.Windows.Forms.Label lblCurrentMouseY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCurrentMouseX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtEngineDepth;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtHighlightDuration;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtSelectedTop;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSelectedWidth;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtSelectedHeight;
        private System.Windows.Forms.Button btnUpdatedSelection;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtSelectedLeft;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbAutoRefresh;
        private System.Windows.Forms.TextBox txtRefreshInterval;
        private System.Windows.Forms.Button btnScanAgain;
        private System.Windows.Forms.Button btnCaptureScreen;
        private System.Windows.Forms.Button btnCropBoard;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHighlightBoxSize;
        private System.Windows.Forms.RichTextBox txtBoardConfiguration;
        private System.Windows.Forms.Button btnEngineConfiguration;
        private System.Windows.Forms.TextBox txtBestMove;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rbtnBlackQueenCastling;
        private System.Windows.Forms.RadioButton rbtnBlackKingCastling;
        private System.Windows.Forms.RadioButton rbtnBothBlackCastling;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtnWhiteQueenCastling;
        private System.Windows.Forms.RadioButton rbtnWhiteKingCastling;
        private System.Windows.Forms.RadioButton rbtnBothWhiteCastling;
        private System.Windows.Forms.RadioButton rbtnNoBlackCastling;
        private System.Windows.Forms.RadioButton rbtnNoWhiteCastling;
        private System.Windows.Forms.TextBox txtFENString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbDrawNextMoveOnScreen;
        private System.Windows.Forms.CheckBox cbAutoMove;
    }
}
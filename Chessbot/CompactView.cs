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
    public partial class CompactView : Form
    {
        public CaptureChessBoard MainView { get; set; }

        public delegate void HighlightNextMoveOnScreen();

        public HighlightNextMoveOnScreen DrawNextMoveOnScreen { get; set; }

        private bool _isThinkingNextMove = false;

        public bool IsThinkingNextMove
        {
            get { return _isThinkingNextMove; }
            set
            {
                _isThinkingNextMove = value;
                string status = string.Empty;
                if (_isThinkingNextMove == true)
                    status = "Computing next move...";
                else
                    status = "Found best move!";
                if (statusStrip1.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtStatus.Text = status;
                    });
                }
                else
                {
                    txtStatus.Text = status;
                }
            }
        }

        private string _bestMove;
        public string BestMove
        {
            get { return _bestMove; }
            set
            {
                _bestMove = value;
                if (txtBestMove.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtBestMove.Text = _bestMove; ; // runs on UI thread
                    });
                }
                else
                {
                    txtBestMove.Text = _bestMove; ;                
                }
            }
        }

        private string _moveScore;
        public string MoveScore
        {
            get { return _bestMove; }
            set
            {
                _moveScore = value;
                if (statusStrip1.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtScore.Text = _moveScore; ; // runs on UI thread
                    });
                }
                else
                {
                    txtScore.Text = _moveScore; ;
                }
            }
        }

        private string _timeTaken;
        public string TimeTaken
        {
            get { return _timeTaken; }
            set
            {
                _timeTaken = value;
                if (lblTimeTaken.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblTimeTaken.Text = _timeTaken; ; // runs on UI thread
                    });
                }
            }
        }

        public CompactView()
        {
            InitializeComponent();
        }

        private void btnMainView_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (MainView != null)
                MainView.Show();
        }

        private void CompactView_Load(object sender, EventArgs e)
        {
            PlaceLowerRight();
        }
        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtBestMove.Text);
        }

        private void btnHighlightOnScreen_Click(object sender, EventArgs e)
        {
            if (DrawNextMoveOnScreen != null)
                DrawNextMoveOnScreen();
        }
    }
}

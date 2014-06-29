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
    public partial class FrmShowNextMove : Form
    {
        private string _bestMove;
        private bool _isThinkingNextMove = false;

        public delegate void HighlightNextMoveOnScreen();

        public HighlightNextMoveOnScreen DrawNextMoveOnScreen { get; set; }

        public bool IsThinkingNextMove 
        {
            get { return _isThinkingNextMove; }
            set 
            { 
                _isThinkingNextMove = value;
                if (pbThinkingBrain.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        pbThinkingBrain.Visible = _isThinkingNextMove;
                    });
                }
                else
                {
                    pbThinkingBrain.Visible = _isThinkingNextMove;
                }
            }
        }

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
            }
        }
        public FrmShowNextMove()
        {
            InitializeComponent();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtBestMove.Text);
        }

        private void FrmShowNextMove_Load(object sender, EventArgs e)
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

        private void btnHighlightOnScreen_Click(object sender, EventArgs e)
        {
            if (DrawNextMoveOnScreen != null)
                DrawNextMoveOnScreen();
        }
    }
}

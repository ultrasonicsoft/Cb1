namespace OpenCVDemo1
{
    partial class CompactView
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
            this.btnMainView = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBestMove = new System.Windows.Forms.TextBox();
            this.btnHighlightOnScreen = new System.Windows.Forms.Button();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTimeTaken = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMainView
            // 
            this.btnMainView.Location = new System.Drawing.Point(336, 5);
            this.btnMainView.Name = "btnMainView";
            this.btnMainView.Size = new System.Drawing.Size(75, 24);
            this.btnMainView.TabIndex = 0;
            this.btnMainView.Text = "Main View";
            this.btnMainView.UseVisualStyleBackColor = true;
            this.btnMainView.Click += new System.EventHandler(this.btnMainView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Next Best Move: ";
            // 
            // txtBestMove
            // 
            this.txtBestMove.Location = new System.Drawing.Point(109, 7);
            this.txtBestMove.Name = "txtBestMove";
            this.txtBestMove.ReadOnly = true;
            this.txtBestMove.Size = new System.Drawing.Size(100, 20);
            this.txtBestMove.TabIndex = 2;
            // 
            // btnHighlightOnScreen
            // 
            this.btnHighlightOnScreen.Location = new System.Drawing.Point(212, 31);
            this.btnHighlightOnScreen.Name = "btnHighlightOnScreen";
            this.btnHighlightOnScreen.Size = new System.Drawing.Size(118, 22);
            this.btnHighlightOnScreen.TabIndex = 6;
            this.btnHighlightOnScreen.Text = "Highlight on Screen";
            this.btnHighlightOnScreen.UseVisualStyleBackColor = true;
            this.btnHighlightOnScreen.Click += new System.EventHandler(this.btnHighlightOnScreen_Click);
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Location = new System.Drawing.Point(215, 5);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(115, 22);
            this.btnCopyToClipboard.TabIndex = 5;
            this.btnCopyToClipboard.Text = "Copy to Clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Time taken (ms):";
            // 
            // lblTimeTaken
            // 
            this.lblTimeTaken.AutoSize = true;
            this.lblTimeTaken.Location = new System.Drawing.Point(109, 36);
            this.lblTimeTaken.Name = "lblTimeTaken";
            this.lblTimeTaken.Size = new System.Drawing.Size(36, 13);
            this.lblTimeTaken.TabIndex = 8;
            this.lblTimeTaken.Text = "[Time]";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus,
            this.txtScore});
            this.statusStrip1.Location = new System.Drawing.Point(0, 57);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(416, 22);
            this.statusStrip1.TabIndex = 9;
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
            // CompactView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 79);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblTimeTaken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHighlightOnScreen);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.txtBestMove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMainView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompactView";
            this.Text = "Chess Bot CompactView";
            this.Load += new System.EventHandler(this.CompactView_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMainView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBestMove;
        private System.Windows.Forms.Button btnHighlightOnScreen;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTimeTaken;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.ToolStripStatusLabel txtScore;
    }
}
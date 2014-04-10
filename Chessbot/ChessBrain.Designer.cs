namespace OpenCVDemo1
{
    partial class ChessBrain
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
            this.btnCaptureScreen = new System.Windows.Forms.Button();
            this.btnSplitImae = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rbtnWhite = new System.Windows.Forms.RadioButton();
            this.rbtnBlack = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReadMasterTemplate = new System.Windows.Forms.Button();
            this.txtPadding = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnShowImage = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCaptureScreen
            // 
            this.btnCaptureScreen.Location = new System.Drawing.Point(568, 52);
            this.btnCaptureScreen.Name = "btnCaptureScreen";
            this.btnCaptureScreen.Size = new System.Drawing.Size(97, 23);
            this.btnCaptureScreen.TabIndex = 0;
            this.btnCaptureScreen.Text = "Capture Screen";
            this.btnCaptureScreen.UseVisualStyleBackColor = true;
            this.btnCaptureScreen.Click += new System.EventHandler(this.btnCaptureScreen_Click);
            // 
            // btnSplitImae
            // 
            this.btnSplitImae.Location = new System.Drawing.Point(160, 57);
            this.btnSplitImae.Name = "btnSplitImae";
            this.btnSplitImae.Size = new System.Drawing.Size(129, 23);
            this.btnSplitImae.TabIndex = 2;
            this.btnSplitImae.Text = "Read Chessboard";
            this.btnSplitImae.UseVisualStyleBackColor = true;
            this.btnSplitImae.Click += new System.EventHandler(this.btnSplitImae_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(568, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Compare";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbtnWhite
            // 
            this.rbtnWhite.AutoSize = true;
            this.rbtnWhite.Checked = true;
            this.rbtnWhite.Location = new System.Drawing.Point(6, 22);
            this.rbtnWhite.Name = "rbtnWhite";
            this.rbtnWhite.Size = new System.Drawing.Size(53, 17);
            this.rbtnWhite.TabIndex = 4;
            this.rbtnWhite.TabStop = true;
            this.rbtnWhite.Text = "White";
            this.rbtnWhite.UseVisualStyleBackColor = true;
            this.rbtnWhite.CheckedChanged += new System.EventHandler(this.rbtnWhite_CheckedChanged);
            // 
            // rbtnBlack
            // 
            this.rbtnBlack.AutoSize = true;
            this.rbtnBlack.Location = new System.Drawing.Point(74, 22);
            this.rbtnBlack.Name = "rbtnBlack";
            this.rbtnBlack.Size = new System.Drawing.Size(52, 17);
            this.rbtnBlack.TabIndex = 5;
            this.rbtnBlack.Text = "Black";
            this.rbtnBlack.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnBlack);
            this.groupBox1.Controls.Add(this.rbtnWhite);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 53);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "What\'s your side?";
            // 
            // btnReadMasterTemplate
            // 
            this.btnReadMasterTemplate.Location = new System.Drawing.Point(160, 12);
            this.btnReadMasterTemplate.Name = "btnReadMasterTemplate";
            this.btnReadMasterTemplate.Size = new System.Drawing.Size(129, 23);
            this.btnReadMasterTemplate.TabIndex = 7;
            this.btnReadMasterTemplate.Text = "Read Master Template";
            this.btnReadMasterTemplate.UseVisualStyleBackColor = true;
            this.btnReadMasterTemplate.Click += new System.EventHandler(this.btnReadMasterTemplate_Click);
            // 
            // txtPadding
            // 
            this.txtPadding.Location = new System.Drawing.Point(86, 81);
            this.txtPadding.Name = "txtPadding";
            this.txtPadding.Size = new System.Drawing.Size(48, 20);
            this.txtPadding.TabIndex = 8;
            this.txtPadding.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Padding";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(439, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Halfmove Clock";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fullmove number";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(439, 59);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(48, 20);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "0";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // btnShowImage
            // 
            this.btnShowImage.Location = new System.Drawing.Point(568, 86);
            this.btnShowImage.Name = "btnShowImage";
            this.btnShowImage.Size = new System.Drawing.Size(97, 23);
            this.btnShowImage.TabIndex = 14;
            this.btnShowImage.Text = "Show Image";
            this.btnShowImage.UseVisualStyleBackColor = true;
            this.btnShowImage.Click += new System.EventHandler(this.btnShowImage_Click);
            // 
            // ChessBrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 116);
            this.Controls.Add(this.btnShowImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPadding);
            this.Controls.Add(this.btnReadMasterTemplate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSplitImae);
            this.Controls.Add(this.btnCaptureScreen);
            this.MaximizeBox = false;
            this.Name = "ChessBrain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chessbot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCaptureScreen;
        private System.Windows.Forms.Button btnSplitImae;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbtnWhite;
        private System.Windows.Forms.RadioButton rbtnBlack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReadMasterTemplate;
        private System.Windows.Forms.TextBox txtPadding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnShowImage;
    }
}


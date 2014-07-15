namespace OpenCVDemo1
{
    partial class EngineConfiguration
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtEnginePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContempFactor = new System.Windows.Forms.NumericUpDown();
            this.txtMinSplitDepth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtThreads = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHash = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMultiPV = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSkillLevel = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmergencyMoveHorizon = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmergencyBaseTime = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEmergencyMoveTime = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMinimumThinkingTime = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSlowMover = new System.Windows.Forms.NumericUpDown();
            this.btnClearHash = new System.Windows.Forms.Button();
            this.cbPonder = new System.Windows.Forms.CheckBox();
            this.cbUCChess960 = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTestCommand = new System.Windows.Forms.TextBox();
            this.btnTestCommand = new System.Windows.Forms.Button();
            this.txtEngineOutput = new System.Windows.Forms.RichTextBox();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtContempFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinSplitDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMultiPV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSkillLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmergencyMoveHorizon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmergencyBaseTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmergencyMoveTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimumThinkingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSlowMover)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Engine Path:";
            // 
            // txtEnginePath
            // 
            this.txtEnginePath.Location = new System.Drawing.Point(111, 27);
            this.txtEnginePath.Name = "txtEnginePath";
            this.txtEnginePath.Size = new System.Drawing.Size(195, 20);
            this.txtEnginePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(312, 26);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contemp Factor:";
            // 
            // txtContempFactor
            // 
            this.txtContempFactor.Location = new System.Drawing.Point(111, 111);
            this.txtContempFactor.Name = "txtContempFactor";
            this.txtContempFactor.Size = new System.Drawing.Size(56, 20);
            this.txtContempFactor.TabIndex = 5;
            // 
            // txtMinSplitDepth
            // 
            this.txtMinSplitDepth.Location = new System.Drawing.Point(111, 148);
            this.txtMinSplitDepth.Name = "txtMinSplitDepth";
            this.txtMinSplitDepth.Size = new System.Drawing.Size(56, 20);
            this.txtMinSplitDepth.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Min Split Depth";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Threads";
            // 
            // txtThreads
            // 
            this.txtThreads.Location = new System.Drawing.Point(111, 185);
            this.txtThreads.Name = "txtThreads";
            this.txtThreads.Size = new System.Drawing.Size(56, 20);
            this.txtThreads.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Hash";
            // 
            // txtHash
            // 
            this.txtHash.Location = new System.Drawing.Point(111, 222);
            this.txtHash.Name = "txtHash";
            this.txtHash.Size = new System.Drawing.Size(56, 20);
            this.txtHash.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "MultiPV";
            // 
            // txtMultiPV
            // 
            this.txtMultiPV.Location = new System.Drawing.Point(109, 291);
            this.txtMultiPV.Name = "txtMultiPV";
            this.txtMultiPV.Size = new System.Drawing.Size(56, 20);
            this.txtMultiPV.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(270, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Skill Level";
            // 
            // txtSkillLevel
            // 
            this.txtSkillLevel.Location = new System.Drawing.Point(331, 106);
            this.txtSkillLevel.Name = "txtSkillLevel";
            this.txtSkillLevel.Size = new System.Drawing.Size(56, 20);
            this.txtSkillLevel.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(196, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Emergency Move Horizon";
            // 
            // txtEmergencyMoveHorizon
            // 
            this.txtEmergencyMoveHorizon.Location = new System.Drawing.Point(331, 143);
            this.txtEmergencyMoveHorizon.Name = "txtEmergencyMoveHorizon";
            this.txtEmergencyMoveHorizon.Size = new System.Drawing.Size(56, 20);
            this.txtEmergencyMoveHorizon.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Emergency Base Time";
            // 
            // txtEmergencyBaseTime
            // 
            this.txtEmergencyBaseTime.Location = new System.Drawing.Point(331, 180);
            this.txtEmergencyBaseTime.Name = "txtEmergencyBaseTime";
            this.txtEmergencyBaseTime.Size = new System.Drawing.Size(56, 20);
            this.txtEmergencyBaseTime.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(209, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Emergency Move Time";
            // 
            // txtEmergencyMoveTime
            // 
            this.txtEmergencyMoveTime.Location = new System.Drawing.Point(331, 217);
            this.txtEmergencyMoveTime.Name = "txtEmergencyMoveTime";
            this.txtEmergencyMoveTime.Size = new System.Drawing.Size(56, 20);
            this.txtEmergencyMoveTime.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(207, 256);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Minimum Thinking Time";
            // 
            // txtMinimumThinkingTime
            // 
            this.txtMinimumThinkingTime.Location = new System.Drawing.Point(331, 254);
            this.txtMinimumThinkingTime.Name = "txtMinimumThinkingTime";
            this.txtMinimumThinkingTime.Size = new System.Drawing.Size(56, 20);
            this.txtMinimumThinkingTime.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(262, 293);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Slow Mover";
            // 
            // txtSlowMover
            // 
            this.txtSlowMover.Location = new System.Drawing.Point(331, 291);
            this.txtSlowMover.Name = "txtSlowMover";
            this.txtSlowMover.Size = new System.Drawing.Size(56, 20);
            this.txtSlowMover.TabIndex = 7;
            // 
            // btnClearHash
            // 
            this.btnClearHash.Location = new System.Drawing.Point(92, 251);
            this.btnClearHash.Name = "btnClearHash";
            this.btnClearHash.Size = new System.Drawing.Size(75, 23);
            this.btnClearHash.TabIndex = 8;
            this.btnClearHash.Text = "Clear Hash";
            this.btnClearHash.UseVisualStyleBackColor = true;
            // 
            // cbPonder
            // 
            this.cbPonder.AutoSize = true;
            this.cbPonder.Location = new System.Drawing.Point(87, 327);
            this.cbPonder.Name = "cbPonder";
            this.cbPonder.Size = new System.Drawing.Size(60, 17);
            this.cbPonder.TabIndex = 9;
            this.cbPonder.Text = "Ponder";
            this.cbPonder.UseVisualStyleBackColor = true;
            // 
            // cbUCChess960
            // 
            this.cbUCChess960.AutoSize = true;
            this.cbUCChess960.Location = new System.Drawing.Point(293, 327);
            this.cbUCChess960.Name = "cbUCChess960";
            this.cbUCChess960.Size = new System.Drawing.Size(94, 17);
            this.cbUCChess960.TabIndex = 9;
            this.cbUCChess960.Text = "UC_Chess960";
            this.cbUCChess960.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(199, 373);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 373);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(37, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Test Commnd";
            // 
            // txtTestCommand
            // 
            this.txtTestCommand.Location = new System.Drawing.Point(111, 63);
            this.txtTestCommand.Name = "txtTestCommand";
            this.txtTestCommand.Size = new System.Drawing.Size(195, 20);
            this.txtTestCommand.TabIndex = 1;
            // 
            // btnTestCommand
            // 
            this.btnTestCommand.Location = new System.Drawing.Point(312, 62);
            this.btnTestCommand.Name = "btnTestCommand";
            this.btnTestCommand.Size = new System.Drawing.Size(75, 23);
            this.btnTestCommand.TabIndex = 2;
            this.btnTestCommand.Text = "Execute";
            this.btnTestCommand.UseVisualStyleBackColor = true;
            this.btnTestCommand.Click += new System.EventHandler(this.btnTestCommand_Click);
            // 
            // txtEngineOutput
            // 
            this.txtEngineOutput.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtEngineOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEngineOutput.ForeColor = System.Drawing.Color.White;
            this.txtEngineOutput.Location = new System.Drawing.Point(407, 12);
            this.txtEngineOutput.Name = "txtEngineOutput";
            this.txtEngineOutput.ReadOnly = true;
            this.txtEngineOutput.Size = new System.Drawing.Size(490, 396);
            this.txtEngineOutput.TabIndex = 10;
            this.txtEngineOutput.Text = "";
            this.txtEngineOutput.TextChanged += new System.EventHandler(this.txtEngineOutput_TextChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(92, 373);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // EngineConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 420);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtEngineOutput);
            this.Controls.Add(this.cbUCChess960);
            this.Controls.Add(this.cbPonder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClearHash);
            this.Controls.Add(this.txtSlowMover);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtMinimumThinkingTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtEmergencyBaseTime);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSkillLevel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEmergencyMoveTime);
            this.Controls.Add(this.txtHash);
            this.Controls.Add(this.txtEmergencyMoveHorizon);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMultiPV);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMinSplitDepth);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtThreads);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContempFactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTestCommand);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtTestCommand);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtEnginePath);
            this.Controls.Add(this.label1);
            this.Name = "EngineConfiguration";
            this.Text = "Engine Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.txtContempFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinSplitDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMultiPV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSkillLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmergencyMoveHorizon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmergencyBaseTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmergencyMoveTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinimumThinkingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSlowMover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEnginePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtContempFactor;
        private System.Windows.Forms.NumericUpDown txtMinSplitDepth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtThreads;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtHash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtMultiPV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtSkillLevel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtEmergencyMoveHorizon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown txtEmergencyBaseTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown txtEmergencyMoveTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown txtMinimumThinkingTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown txtSlowMover;
        private System.Windows.Forms.Button btnClearHash;
        private System.Windows.Forms.CheckBox cbPonder;
        private System.Windows.Forms.CheckBox cbUCChess960;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTestCommand;
        private System.Windows.Forms.Button btnTestCommand;
        private System.Windows.Forms.RichTextBox txtEngineOutput;
        private System.Windows.Forms.Button btnApply;
    }
}
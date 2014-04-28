namespace OpenCVDemo1
{
    partial class FrmSaveTemplate
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.txtWebsiteURL = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(73, 99);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save && Close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Template Name:";
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Location = new System.Drawing.Point(103, 16);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(222, 20);
            this.txtTemplateName.TabIndex = 2;
            // 
            // txtWebsiteURL
            // 
            this.txtWebsiteURL.Location = new System.Drawing.Point(103, 52);
            this.txtWebsiteURL.Name = "txtWebsiteURL";
            this.txtWebsiteURL.Size = new System.Drawing.Size(222, 20);
            this.txtWebsiteURL.TabIndex = 4;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(12, 55);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(74, 13);
            this.lbl.TabIndex = 3;
            this.lbl.Text = "Website URL:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(202, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmSaveTemplate
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(337, 147);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtWebsiteURL);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.txtTemplateName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.MaximizeBox = false;
            this.Name = "FrmSaveTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.TextBox txtWebsiteURL;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button btnCancel;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCVDemo1
{
    public partial class FrmSaveTemplate : Form
    {
        public Image ChessBoard { get; set; }
        public int Padding { get; set; }

        public int Intensity { get; set; }
        public bool IsWhiteFirst { get; set; }
        public FrmSaveTemplate()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string templateFileName = string.Concat(ImageProcessingManager.TemplatePath, txtTemplateName.Text, ".bin");
            if (Directory.Exists(ImageProcessingManager.TemplatePath) == false)
            {
                Directory.CreateDirectory(ImageProcessingManager.TemplatePath);
            }

            try
            {
                if (File.Exists(templateFileName))
                {
                    var result = MessageBox.Show("Template Name already present. Do you want to overwrite it?", "Save Template", MessageBoxButtons.YesNoCancel);
                    if (result != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                }
                var masterTemplate = ImageProcessingManager.FillMasterTemplate(ChessBoard, Padding, IsWhiteFirst);
                ImageProcessingManager.SaveTemplate(ChessBoard, templateFileName, masterTemplate, Intensity);
                AddEntryToTemplateCatalog(txtTemplateName.Text, lbl.Text, templateFileName);

                MessageBox.Show("Template saved successfully!", "Save Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.Close();
        }
        private void AddEntryToTemplateCatalog(string templateName, string websiteURL, string templateFileName)
        {
            string catelogFileName = ImageProcessingManager.TemplatePath + Constants.TEMPLATE_CATELOG_FILE;
            string newTemplateEntry = string.Concat(templateName, ",", websiteURL, ",", templateFileName, Environment.NewLine);
            File.AppendAllText(catelogFileName, newTemplateEntry);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

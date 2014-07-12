using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCVDemo1
{
    public partial class EngineConfiguration : Form
    {
        private UCI Engine = new UCI();

        public EngineConfiguration()
        {
            InitializeComponent();

            Engine = new UCI();
            Engine.InitEngine("stockfishengine.exe", string.Empty, EngineDataReceived);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtEnginePath.Text = openFile.FileName;
            }
        }

        public void EngineDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (outLine.Data == null)
                return;
            if (txtEngineOutput.InvokeRequired)
            {
                this.Invoke((MethodInvoker) delegate
                {
                    txtEngineOutput.Text += outLine.Data + Environment.NewLine;
                });
            }
            else
            {
                txtEngineOutput.Text += outLine.Data + Environment.NewLine;
            }
        }

        private void btnTestCommand_Click(object sender, EventArgs e)
        {
            Engine.EngineCommand(txtTestCommand.Text);
        }
    }
}

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
    public partial class BoardConfigurationTextVersion : Form
    {
        public string TextBoardConfiguration { get; set; }

        public BoardConfigurationTextVersion()
        {
            InitializeComponent();
        }

        private void BoardConfigurationTextVersion_Load(object sender, EventArgs e)
        {
            txtBoardConfiguration.Text = TextBoardConfiguration;
        }
    }
}

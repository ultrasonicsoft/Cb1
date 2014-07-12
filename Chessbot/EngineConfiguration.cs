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
        private void btnTestCommand_Click(object sender, EventArgs e)
        {
            Engine.EngineCommand(txtTestCommand.Text);
        }
        private void txtEngineOutput_TextChanged(object sender, EventArgs e)
        {
            txtEngineOutput.SelectionStart = txtEngineOutput.Text.Length; //Set the current caret position at the end
            txtEngineOutput.ScrollToCaret(); //Now scroll it automatically
        }
        public void EngineDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (outLine.Data == null)
                return;
           
            if (outLine.Data.Contains("option name Contempt Factor type"))
            {
                SetContempFactorType(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Min Split Depth type"))
            {
                SetMinSplitDepth(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Threads type"))
            {
                SetThreadType(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Hash type"))
            {
                SetHash(outLine.Data);
            }
            else if (outLine.Data.Contains("option name MultiPV type"))
            {
                SetMultiPV(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Skill Level type"))
            {
                SetSkillLevel(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Emergency Move Horizon type"))
            {
                SetEmergencyMoveHorizon(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Emergency Base Time type"))
            {
                SetEmergencyBaseTime(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Emergency Move Time type"))
            {
                SetEmergencyMoveTime(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Minimum Thinking Time type"))
            {
                SetMinimumThinkingTime(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Slow Mover type"))
            {
                SetSlowMover(outLine.Data);
            }
            else if (outLine.Data.Contains("option name UCI_Chess960 type"))
            {
                SetUCI_960(outLine.Data);
            }
            else if (outLine.Data.Contains("option name Ponder type"))
            {
                SetPonder(outLine.Data);
            }
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
        private void SetContempFactorType(string data)
        {
            LogHelper.logger.Info("SetContempFactorType called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtContempFactor.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtContempFactor.Text = allParts[7];
                        txtContempFactor.Minimum = int.Parse(allParts[9]);
                        txtContempFactor.Maximum = int.Parse(allParts[11]);
                    });
                }
                else
                {
                    txtContempFactor.Text = allParts[7];
                    txtContempFactor.Minimum = int.Parse(allParts[9]);
                    txtContempFactor.Maximum = int.Parse(allParts[11]);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetContempFactorType: " + exception.Message);
                LogHelper.logger.Error("SetContempFactorType: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetContempFactorType finished...");
        }
        private void SetMinSplitDepth(string data)
        {
            LogHelper.logger.Info("SetMinSplitDepth called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtMinSplitDepth.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtMinSplitDepth.Text = allParts[8];
                        txtMinSplitDepth.Minimum = int.Parse(allParts[10]);
                        txtMinSplitDepth.Maximum = int.Parse(allParts[12]);
                    });
                }
                else
                {
                    txtMinSplitDepth.Text = allParts[8];
                    txtMinSplitDepth.Minimum = int.Parse(allParts[10]);
                    txtMinSplitDepth.Maximum = int.Parse(allParts[12]);
                }
                
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetMinSplitDepth: " + exception.Message);
                LogHelper.logger.Error("SetMinSplitDepth: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetMinSplitDepth finished...");
        }

        private void SetThreadType(string data)
        {
            LogHelper.logger.Info("SetThreadType called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtThreads.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtThreads.Text = allParts[6];
                        txtThreads.Minimum = int.Parse(allParts[8]);
                        txtThreads.Maximum = int.Parse(allParts[10]);
                    });
                }
                else
                {
                    txtThreads.Text = allParts[6];
                    txtThreads.Minimum = int.Parse(allParts[8]);
                    txtThreads.Maximum = int.Parse(allParts[10]);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetThreadType: " + exception.Message);
                LogHelper.logger.Error("SetThreadType: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetThreadType finished...");
        }

        private void SetHash(string data)
        {
            LogHelper.logger.Info("SetHash called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtHash.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtHash.Text = allParts[6];
                        txtHash.Minimum = int.Parse(allParts[8]);
                        txtHash.Maximum = int.Parse(allParts[10]);
                    });
                }
                else
                {
                    txtHash.Text = allParts[6];
                    txtHash.Minimum = int.Parse(allParts[8]);
                    txtHash.Maximum = int.Parse(allParts[10]);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetHash: " + exception.Message);
                LogHelper.logger.Error("SetHash: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetHash finished...");
        }

        private void SetMultiPV(string data)
        {
            LogHelper.logger.Info("SetMultiPV called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtMultiPV.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtMultiPV.Text = allParts[6];
                        txtMultiPV.Minimum = int.Parse(allParts[8]);
                        txtMultiPV.Maximum = int.Parse(allParts[10]);
                    });
                }
                else
                {
                    txtMultiPV.Text = allParts[6];
                    txtMultiPV.Minimum = int.Parse(allParts[8]);
                    txtMultiPV.Maximum = int.Parse(allParts[11]);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetMultiPV: " + exception.Message);
                LogHelper.logger.Error("SetMultiPV: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetMultiPV finished...");
        }

        private void SetSkillLevel(string data)
        {
            LogHelper.logger.Info("SetSkillLevel called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtSkillLevel.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtSkillLevel.Text = allParts[7];
                        txtSkillLevel.Minimum = int.Parse(allParts[9]);
                        txtSkillLevel.Maximum = int.Parse(allParts[11]);
                    });
                }
                else
                {
                    txtSkillLevel.Text = allParts[7];
                    txtSkillLevel.Minimum = int.Parse(allParts[9]);
                    txtSkillLevel.Maximum = int.Parse(allParts[10]);
                }
                
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetSkillLevel: " + exception.Message);
                LogHelper.logger.Error("SetSkillLevel: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetSkillLevel finished...");
        }

        private void SetEmergencyMoveHorizon(string data)
        {
            LogHelper.logger.Info("SetEmergencyMoveHorizon called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtEmergencyMoveHorizon.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtEmergencyMoveHorizon.Text = allParts[8];
                        txtEmergencyMoveHorizon.Minimum = int.Parse(allParts[10]);
                        txtEmergencyMoveHorizon.Maximum = int.Parse(allParts[12]);
                    });
                }
                else
                {
                    txtEmergencyMoveHorizon.Text = allParts[8];
                    txtEmergencyMoveHorizon.Minimum = int.Parse(allParts[10]);
                    txtEmergencyMoveHorizon.Maximum = int.Parse(allParts[12]);
                }
                
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetEmergencyMoveHorizon: " + exception.Message);
                LogHelper.logger.Error("SetEmergencyMoveHorizon: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetEmergencyMoveHorizon finished...");
        }

        private void SetEmergencyBaseTime(string data)
        {
            LogHelper.logger.Info("SetEmergencyBaseTime called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtEmergencyBaseTime.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtEmergencyBaseTime.Text = allParts[8];
                        txtEmergencyBaseTime.Minimum = int.Parse(allParts[10]);
                        txtEmergencyBaseTime.Maximum = int.Parse(allParts[12]);
                    });
                }
                else
                {
                    txtEmergencyBaseTime.Text = allParts[8];
                    txtEmergencyBaseTime.Minimum = int.Parse(allParts[10]);
                    txtEmergencyBaseTime.Maximum = int.Parse(allParts[12]);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetEmergencyBaseTime: " + exception.Message);
                LogHelper.logger.Error("SetEmergencyBaseTime: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetEmergencyBaseTime finished...");
        }
        private void SetEmergencyMoveTime(string data)
        {
            LogHelper.logger.Info("SetEmergencyMoveTime called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtEmergencyMoveTime.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtEmergencyMoveTime.Text = allParts[8];
                        txtEmergencyMoveTime.Minimum = int.Parse(allParts[10]);
                        txtEmergencyMoveTime.Maximum = int.Parse(allParts[12]);
                    });
                }
                else
                {
                    txtEmergencyMoveTime.Text = allParts[8];
                    txtEmergencyMoveTime.Minimum = int.Parse(allParts[10]);
                    txtEmergencyMoveTime.Maximum = int.Parse(allParts[12]);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetEmergencyMoveTime: " + exception.Message);
                LogHelper.logger.Error("SetEmergencyMoveTime: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetEmergencyMoveTime finished...");
        }
        private void SetMinimumThinkingTime(string data)
        {
            LogHelper.logger.Info("SetMinimumThinkingTime called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtMinimumThinkingTime.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtMinimumThinkingTime.Text = allParts[8];
                        txtMinimumThinkingTime.Minimum = int.Parse(allParts[10]);
                        txtMinimumThinkingTime.Maximum = int.Parse(allParts[12]);
                    });
                }
                else
                {
                    txtMinimumThinkingTime.Text = allParts[8];
                    txtMinimumThinkingTime.Minimum = int.Parse(allParts[10]);
                    txtMinimumThinkingTime.Maximum = int.Parse(allParts[12]);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetMinimumThinkingTime: " + exception.Message);
                LogHelper.logger.Error("SetMinimumThinkingTime: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetMinimumThinkingTime finished...");
        }
        private void SetSlowMover(string data)
        {
            LogHelper.logger.Info("SetSlowMover called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtSlowMover.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtSlowMover.Text = allParts[7];
                        txtSlowMover.Minimum = int.Parse(allParts[9]);
                        txtSlowMover.Maximum = int.Parse(allParts[11]);
                    });
                }
                else
                {
                    txtSlowMover.Text = allParts[7];
                    txtSlowMover.Minimum = int.Parse(allParts[9]);
                    txtSlowMover.Maximum = int.Parse(allParts[11]);
                }
            
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetSlowMover: " + exception.Message);
                LogHelper.logger.Error("SetSlowMover: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetSlowMover finished...");
        }
        private void SetUCI_960(string data)
        {
            LogHelper.logger.Info("SetUCI_960 called...");
            try
            {
                var allParts = data.Split(' ');
                if (cbUCChess960.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        bool state = false;
                        bool.TryParse(allParts[6], out state);
                        cbUCChess960.Checked = state;
                    });
                }
                else
                {
                    bool state = false;
                    bool.TryParse(allParts[7], out state);
                    cbUCChess960.Checked = state;
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetUCI_960: " + exception.Message);
                LogHelper.logger.Error("SetUCI_960: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetUCI_960 finished...");
        }
        private void SetPonder(string data)
        {
            LogHelper.logger.Info("SetPonder called...");
            try
            {
                var allParts = data.Split(' ');
                if (cbPonder.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        bool state = false;
                        bool.TryParse(allParts[6], out state);
                        cbPonder.Checked = state;
                    });
                }
                else
                {
                    bool state = false;
                    bool.TryParse(allParts[6], out state);
                    cbPonder.Checked = state;
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetPonder: " + exception.Message);
                LogHelper.logger.Error("SetPonder: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            LogHelper.logger.Info("SetPonder finished...");
        }
    }
}

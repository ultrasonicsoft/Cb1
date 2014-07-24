using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

namespace OpenCVDemo1
{
    public partial class EngineConfiguration : Form
    {
        private UCI Engine = null;

        private int contemptFactorValue = 0;
        private int contemptFactorMin = 0;
        private int contemptFactorMax = 0;

        private int MinSplitDepthValue = 0;
        private int MinSplitDepthMin = 0;
        private int MinSplitDepthMax = 0;

        private int ThreadValue = 0;
        private int ThreadMin = 0;
        private int ThreadMax = 0;

        private int HashValue = 0;
        private int HashMin = 0;
        private int HashMax = 0;

        private int MultiPVValue = 0;
        private int MultiPVMin = 0;
        private int MultiPVMax = 0;

        private bool Ponder = true;

        private int SkillLevelValue = 0;
        private int SkillLevelMin = 0;
        private int SkillLevelMax = 0;

        private int MoveHorizonValue = 0;
        private int MoveHorizonMin = 0;
        private int MoveHorizonMax = 0;

        private int BaseTimeValue = 0;
        private int BaseTimeMin = 0;
        private int BaseTimeMax = 0;

        private int MoveTimeValue = 0;
        private int MoveTimeMin = 0;
        private int MoveTimeMax = 0;

        private int ThinkingTimeValue = 0;
        private int ThinkingTimeMin = 0;
        private int ThinkingTimeMax = 0;

        private int SlowMoverValue = 0;
        private int SlowMoverMin = 0;
        private int SlowMoverMax = 0;
       
        private bool UC_960 = false;


        public EngineConfiguration()
        {
            InitializeComponent();

            Engine = UCI.GetEngine();
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
            else if (outLine.Data.Contains("uciok"))
            {
                UpdateEngineSettings();
                LoadSavedEngineSettings();

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

        private void LoadSavedEngineSettings(bool loadFromFile = true)
        {
            //LogHelper.logger.Info("LoadSavedEngineSettings called...");
            try
            {
                
                SetValue(txtContempFactor, CaptureChessBoard.CurrentEngineSettings.ContemptFactorValue);
                SetValue(txtMinSplitDepth, CaptureChessBoard.CurrentEngineSettings.MinSplitDepthValue);
                SetValue(txtThreads, CaptureChessBoard.CurrentEngineSettings.ThreadValue);
                SetValue(txtHash, CaptureChessBoard.CurrentEngineSettings.HashValue);
                SetValue(txtMultiPV, CaptureChessBoard.CurrentEngineSettings.MultiPVValue);
                SetValue(txtSkillLevel, CaptureChessBoard.CurrentEngineSettings.SkillLevelValue);
                SetValue(txtEmergencyMoveHorizon, CaptureChessBoard.CurrentEngineSettings.MoveHorizonValue);
                SetValue(txtEmergencyBaseTime, CaptureChessBoard.CurrentEngineSettings.BaseTimeValue);
                SetValue(txtEmergencyMoveTime, CaptureChessBoard.CurrentEngineSettings.MoveTimeValue);
                SetValue(txtMinimumThinkingTime, CaptureChessBoard.CurrentEngineSettings.ThinkingTimeValue);
                SetValue(txtSlowMover, CaptureChessBoard.CurrentEngineSettings.SlowMoverValue);

                SetValue(cbPonder, CaptureChessBoard.CurrentEngineSettings.Ponder);
                SetValue(cbUCChess960, CaptureChessBoard.CurrentEngineSettings.UC_960);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("LoadSavedEngineSettings: " + exception.Message);
                LogHelper.logger.Error("LoadSavedEngineSettings: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("LoadSavedEngineSettings finished...");
        }
        private void SetContempFactorType(string data)
        {
            //LogHelper.logger.Info("SetContempFactorType called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtContempFactor.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtContempFactor, int.Parse(allParts[7]), int.Parse(allParts[9]), int.Parse(allParts[11]),
                            ref contemptFactorValue, ref contemptFactorMin, ref contemptFactorMax);
                    });
                }
                else
                {
                    SetValue(txtContempFactor, int.Parse(allParts[7]), int.Parse(allParts[9]), int.Parse(allParts[11]),
                           ref contemptFactorValue, ref contemptFactorMin, ref contemptFactorMax);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetContempFactorType: " + exception.Message);
                LogHelper.logger.Error("SetContempFactorType: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetContempFactorType finished...");
        }

        private void SetValue(NumericUpDown target, int value)
        {
            if (target.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    target.Text = value.ToString();
                });
            }
            else
            {
                target.Text = value.ToString();

            }
        }

        private void SetValue(CheckBox target, bool value)
        {
            if (target.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    target.Checked = value;
                });
            }
            else
            {
                target.Checked = value;
            }
        }

        private void SetValue(NumericUpDown target, int value, int min, int max, 
            ref int localValue, ref int localMin, ref int localMax)
        {
            target.Text = value.ToString();
            target.Minimum = min;
            target.Maximum = max;

            localValue = value;
            localMin = min;
            localMax = max;    
        }
        private void SetMinSplitDepth(string data)
        {
            //LogHelper.logger.Info("SetMinSplitDepth called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtMinSplitDepth.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtMinSplitDepth, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                           ref MinSplitDepthValue, ref MinSplitDepthMin, ref MinSplitDepthMax);
                    });
                }
                else
                {
                    SetValue(txtMinSplitDepth, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                          ref MinSplitDepthValue, ref MinSplitDepthMin, ref MinSplitDepthMax);
                }
                
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetMinSplitDepth: " + exception.Message);
                LogHelper.logger.Error("SetMinSplitDepth: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetMinSplitDepth finished...");
        }

        private void SetThreadType(string data)
        {
            //LogHelper.logger.Info("SetThreadType called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtThreads.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtThreads, int.Parse(allParts[6]), int.Parse(allParts[8]), int.Parse(allParts[10]),
                          ref ThreadValue, ref ThreadMin, ref ThreadMax);
                    });
                }
                else
                {
                    SetValue(txtThreads, int.Parse(allParts[6]), int.Parse(allParts[8]), int.Parse(allParts[10]),
                          ref ThreadValue, ref ThreadMin, ref ThreadMax);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetThreadType: " + exception.Message);
                LogHelper.logger.Error("SetThreadType: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetThreadType finished...");
        }

        private void SetHash(string data)
        {
            //LogHelper.logger.Info("SetHash called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtHash.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtHash, int.Parse(allParts[6]), int.Parse(allParts[8]), int.Parse(allParts[10]),
                         ref HashValue, ref HashMin, ref HashMax);
                    });
                }
                else
                {
                    SetValue(txtHash, int.Parse(allParts[6]), int.Parse(allParts[8]), int.Parse(allParts[10]),
                          ref HashValue, ref HashMin, ref HashMax);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetHash: " + exception.Message);
                LogHelper.logger.Error("SetHash: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetHash finished...");
        }

        private void SetMultiPV(string data)
        {
            //LogHelper.logger.Info("SetMultiPV called...");
            try
            {
                var allParts = data.Split(' ');

                if (txtMultiPV.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtMultiPV, int.Parse(allParts[6]), int.Parse(allParts[8]), int.Parse(allParts[10]),
                          ref MultiPVValue, ref MultiPVMin, ref MultiPVMax);
                    });
                }
                else
                {
                    SetValue(txtMultiPV, int.Parse(allParts[6]), int.Parse(allParts[8]), int.Parse(allParts[11]),
                         ref MultiPVValue, ref MultiPVMin, ref MultiPVMax);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetMultiPV: " + exception.Message);
                LogHelper.logger.Error("SetMultiPV: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetMultiPV finished...");
        }

        private void SetSkillLevel(string data)
        {
            //LogHelper.logger.Info("SetSkillLevel called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtSkillLevel.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtSkillLevel, int.Parse(allParts[7]), int.Parse(allParts[9]), int.Parse(allParts[11]),
                         ref SkillLevelValue, ref SkillLevelMin, ref SkillLevelMax);
                    });
                }
                else
                {
                    SetValue(txtSkillLevel, int.Parse(allParts[7]), int.Parse(allParts[9]), int.Parse(allParts[11]),
                        ref SkillLevelValue, ref SkillLevelMin, ref SkillLevelMax);
                }
                
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetSkillLevel: " + exception.Message);
                LogHelper.logger.Error("SetSkillLevel: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetSkillLevel finished...");
        }

        private void SetEmergencyMoveHorizon(string data)
        {
            //LogHelper.logger.Info("SetEmergencyMoveHorizon called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtEmergencyMoveHorizon.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtEmergencyMoveHorizon, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                        ref MoveHorizonValue, ref MoveHorizonMin, ref MoveHorizonMax);
                    });
                }
                else
                {
                    SetValue(txtEmergencyMoveHorizon, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                        ref MoveHorizonValue, ref MoveHorizonMin, ref MoveHorizonMax);
                }
                
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetEmergencyMoveHorizon: " + exception.Message);
                LogHelper.logger.Error("SetEmergencyMoveHorizon: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetEmergencyMoveHorizon finished...");
        }

        private void SetEmergencyBaseTime(string data)
        {
            //LogHelper.logger.Info("SetEmergencyBaseTime called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtEmergencyBaseTime.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtEmergencyBaseTime, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                       ref BaseTimeValue, ref BaseTimeMin, ref BaseTimeMax);
                    });
                }
                else
                {
                    SetValue(txtEmergencyBaseTime, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                      ref BaseTimeValue, ref BaseTimeMin, ref BaseTimeMax);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetEmergencyBaseTime: " + exception.Message);
                LogHelper.logger.Error("SetEmergencyBaseTime: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetEmergencyBaseTime finished...");
        }
        private void SetEmergencyMoveTime(string data)
        {
            //LogHelper.logger.Info("SetEmergencyMoveTime called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtEmergencyMoveTime.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtEmergencyMoveTime, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                      ref MoveTimeValue, ref MoveTimeMin, ref MoveTimeMax);
                    });
                }
                else
                {
                    SetValue(txtEmergencyMoveTime, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                     ref MoveTimeValue, ref MoveTimeMin, ref MoveTimeMax);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetEmergencyMoveTime: " + exception.Message);
                LogHelper.logger.Error("SetEmergencyMoveTime: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetEmergencyMoveTime finished...");
        }
        private void SetMinimumThinkingTime(string data)
        {
            //LogHelper.logger.Info("SetMinimumThinkingTime called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtMinimumThinkingTime.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtMinimumThinkingTime, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                     ref ThinkingTimeValue, ref ThinkingTimeMin, ref ThinkingTimeMax);
                    });
                }
                else
                {
                    SetValue(txtMinimumThinkingTime, int.Parse(allParts[8]), int.Parse(allParts[10]), int.Parse(allParts[12]),
                     ref ThinkingTimeValue, ref ThinkingTimeMin, ref ThinkingTimeMax);
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetMinimumThinkingTime: " + exception.Message);
                LogHelper.logger.Error("SetMinimumThinkingTime: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetMinimumThinkingTime finished...");
        }
        private void SetSlowMover(string data)
        {
            //LogHelper.logger.Info("SetSlowMover called...");
            try
            {
                var allParts = data.Split(' ');
                if (txtSlowMover.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetValue(txtSlowMover, int.Parse(allParts[7]), int.Parse(allParts[9]), int.Parse(allParts[11]),
                    ref SlowMoverValue, ref SlowMoverMin, ref SlowMoverMax);
                    });
                }
                else
                {
                    SetValue(txtSlowMover, int.Parse(allParts[7]), int.Parse(allParts[9]), int.Parse(allParts[11]),
                    ref SlowMoverValue, ref SlowMoverMin, ref SlowMoverMax);
                }
            
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetSlowMover: " + exception.Message);
                LogHelper.logger.Error("SetSlowMover: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetSlowMover finished...");
        }
        private void SetUCI_960(string data)
        {
            //LogHelper.logger.Info("SetUCI_960 called...");
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
                        UC_960 = cbUCChess960.Checked;
                    });
                }
                else
                {
                    bool state = false;
                    bool.TryParse(allParts[7], out state);
                    cbUCChess960.Checked = state;
                    UC_960 = cbUCChess960.Checked;
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetUCI_960: " + exception.Message);
                LogHelper.logger.Error("SetUCI_960: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetUCI_960 finished...");
        }
        private void SetPonder(string data)
        {
            //LogHelper.logger.Info("SetPonder called...");
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
                        Ponder = cbPonder.Checked;
                    });
                }
                else
                {
                    bool state = false;
                    bool.TryParse(allParts[6], out state);
                    cbPonder.Checked = state;
                    Ponder = cbPonder.Checked;
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("SetPonder: " + exception.Message);
                LogHelper.logger.Error("SetPonder: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("SetPonder finished...");
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateEngineSettings()
        {
            UpdateEngineParameter(Constants.UPDATE_CONTEMP_FACTOR_TYPE, txtContempFactor.Text);
            UpdateEngineParameter(Constants.UPDATE_MIN_SPLIT_DEPTH, txtMinSplitDepth.Text);
            UpdateEngineParameter(Constants.UPDATE_THREADS, txtThreads.Text);
            UpdateEngineParameter(Constants.UPDATE_HASH, txtHash.Text);
            UpdateEngineParameter(Constants.UPDATE_MULTI_PV, txtMultiPV.Text);
            UpdateEngineParameter(Constants.UPDATE_PONDER, cbPonder.Checked?"true":"false");
            UpdateEngineParameter(Constants.UPDATE_SKILL_LEVEL, txtSkillLevel.Text);
            UpdateEngineParameter(Constants.UPDATE_EMG_MOVE_HORIZON, txtEmergencyMoveHorizon.Text);
            UpdateEngineParameter(Constants.UPDATE_EMG_BASE_TIME, txtEmergencyBaseTime.Text);
            UpdateEngineParameter(Constants.UPDATE_EMG_MOVE_TIME, txtEmergencyMoveTime.Text);
            UpdateEngineParameter(Constants.UPDATE_EMG_THINKING_TIME, txtMinimumThinkingTime.Text);
            UpdateEngineParameter(Constants.UPDATE_SLOW_MOVER, txtSlowMover.Text);
            UpdateEngineParameter(Constants.UPDATE_UC_CHESS960, cbUCChess960.Checked ? "true" : "false");

           
        }

        private void UpdateEngineParameter(string parameterName, string parameterValue)
        {
            //LogHelper.logger.Info("UpdateEngineParameter called...");
            //LogHelper.logger.Info("Setting engine parameter: " + parameterName + " with value: " + parameterValue);
            try
            {

                string command = string.Format(Constants.UPDATE_COMMAND, parameterName,parameterValue);
                //LogHelper.logger.Info("Update Parameter Command: "+ command);
                Engine.EngineCommand(command);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("UpdateEngineParameter: " + exception.Message);
                LogHelper.logger.Error("UpdateEngineParameter: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            //LogHelper.logger.Info("UpdateEngineParameter finished...");
        }

        private void btnResetEngine_Click(object sender, EventArgs e)
        {
            txtContempFactor.Text = contemptFactorValue.ToString();
            txtMinSplitDepth.Text = MinSplitDepthValue.ToString();
            txtThreads.Text = ThreadValue.ToString();
            txtHash.Text = HashValue.ToString();
            txtMultiPV.Text = MultiPVValue.ToString();
            txtSkillLevel.Text = SkillLevelValue.ToString();
            txtEmergencyMoveHorizon.Text = MoveHorizonValue.ToString();
            txtEmergencyBaseTime.Text = BaseTimeValue.ToString();
            txtEmergencyMoveTime.Text = MoveTimeValue.ToString();
            txtMinimumThinkingTime.Text = ThinkingTimeValue.ToString();
            txtSlowMover.Text = SlowMoverValue.ToString();
            cbPonder.Checked = Ponder;
            cbUCChess960.Checked = UC_960;

            UpdateEngineSettings();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CaptureChessBoard.CurrentEngineSettings.ContemptFactorValue = int.Parse(txtContempFactor.Text);
            CaptureChessBoard.CurrentEngineSettings.MinSplitDepthValue = int.Parse(txtMinSplitDepth.Text);
            CaptureChessBoard.CurrentEngineSettings.ThreadValue = int.Parse(txtThreads.Text);
            CaptureChessBoard.CurrentEngineSettings.HashValue = int.Parse(txtHash.Text);
            CaptureChessBoard.CurrentEngineSettings.MultiPVValue = int.Parse(txtMultiPV.Text);
            CaptureChessBoard.CurrentEngineSettings.SkillLevelValue = int.Parse(txtSkillLevel.Text);
            CaptureChessBoard.CurrentEngineSettings.MoveHorizonValue = int.Parse(txtEmergencyMoveHorizon.Text);
            CaptureChessBoard.CurrentEngineSettings.BaseTimeValue = int.Parse(txtEmergencyBaseTime.Text);
            CaptureChessBoard.CurrentEngineSettings.MoveTimeValue = int.Parse(txtEmergencyMoveTime.Text);
            CaptureChessBoard.CurrentEngineSettings.ThinkingTimeValue = int.Parse(txtMinimumThinkingTime.Text);
            CaptureChessBoard.CurrentEngineSettings.SlowMoverValue = int.Parse(txtSlowMover.Text);
            CaptureChessBoard.CurrentEngineSettings.Ponder = cbPonder.Checked;
            CaptureChessBoard.CurrentEngineSettings.UC_960 = cbUCChess960.Checked;

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateEngineSettings();
            MessageBox.Show("Engine settings saved!");
        }

    }
}

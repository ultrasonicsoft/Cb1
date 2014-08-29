using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
//using Globals;
using log4net.Repository.Hierarchy;
using System.Windows.Forms;

namespace OpenCVDemo1
{

    // http://wbec-ridderkerk.nl/html/UCIProtocol.html
    public class UCI
    {
        //////////////////////////////////////////////////////////////////////////
        // CONSTANTS
        //////////////////////////////////////////////////////////////////////////
        public static String kSetUCIMode = "uci";
       public static String kResetEngine = "ucinewgame";
       public static String kStopEngine = "stop";
       public static String kQuitEngine = "quit";
       public static String kSetPosition = "position ";
       public static String kStartPosition = "startpos ";
       public static String kStartMoves = "moves ";
       public static String kStartMovesFromStartPos = kSetPosition + kStartPosition + kStartMoves;

        public event EventHandler BestMovFound;
        private string currentMoveScore = string.Empty;

        private static UCI SingletonObject = null;

        private Process UCI_Engine;

        #region Constructor

        private UCI()
        {
            
        }

        public static UCI GetEngine()
        {
            if(SingletonObject == null)
                SingletonObject = new UCI();
                return SingletonObject;

        }
        #endregion

        #region Public methods

        public void AddDataReceivedEventHandler(System.Diagnostics.DataReceivedEventHandler outputDataReceivedProc )
        {
            UCI_Engine.OutputDataReceived += outputDataReceivedProc;
        }
        public bool InitEngine(String enginePath, String engineIniPath, System.Diagnostics.DataReceivedEventHandler outputDataReceivedProc)
        {
            LogHelper.logger.Info("InitEngine called with enginePath: " + enginePath + " and engineIniPath: " + engineIniPath);
            try
            {
                // create process
                //if (UCI_Engine == null)
                //{
                    UCI_Engine = new Process();
                    UCI_Engine.StartInfo.FileName = enginePath;
                    //UCI_Engine.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(enginePath);
                    UCI_Engine.StartInfo.UseShellExecute = false;
                    UCI_Engine.StartInfo.CreateNoWindow = true;
                    UCI_Engine.StartInfo.RedirectStandardInput = true;
                    UCI_Engine.StartInfo.RedirectStandardOutput = true;
                    UCI_Engine.Start();
                    UCI_Engine.OutputDataReceived += outputDataReceivedProc;
                    UCI_Engine.BeginOutputReadLine();
                //}
                // start new game
                EngineCommand(kSetUCIMode);
                ResetEngine();
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error(exception.Message);
                MessageBox.Show(
                    "Engine initialization failed. Please restart. If problem persists, contact your vendor.",
                    "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            LogHelper.logger.Info("InitEngine finished.");

            return true;
        }

        public bool ResetEngine()
        {
            LogHelper.logger.Info("ResetEngine called");
            try
            {
                // stop engine 
                EngineCommand(kStopEngine);

                // reset engine
                EngineCommand(kResetEngine);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error(exception.Message);
                throw;
            }
            LogHelper.logger.Info("ResetEngine finished");
            
            return true;
        }

        public bool ShutdownEngine()
        {
            try
            {
                if (UCI_Engine != null)
                {
                    // stop kill STOP!
                    EngineCommand(kStopEngine);
                    EngineCommand(kQuitEngine);
                    UCI_Engine.Kill();
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error(exception.Message);
                throw;
            }
            return true;
        }

        public bool CalculateBestMove(string fenString, string engineDepth)
        {
            LogHelper.logger.Info("CalculateBestMove called. fenstring: " + fenString + "  and engine Depth: " + engineDepth);
            if (UCI_Engine != null)
            {
                // setup engine board string
                string searchString = "position fen " + fenString;
                //string searchString = "position fen rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w";
                //String searchString = kStartMovesFromStartPos + ConstructMoveString();

                // stop thinking
                LogHelper.logger.Info("Stopping engine with stop command: " + kStopEngine);
                EngineCommand(kStopEngine);

                // setup engine board
                LogHelper.logger.Info("Searching next best move with searh query: " + searchString);
                EngineCommand(searchString);

                // think!
                string depthQuery = "go depth " + engineDepth;
                LogHelper.logger.Info("Searching next best move with depth query: " + depthQuery);
                EngineCommand(depthQuery);
            }

            return true;
        }

        #endregion


        #region Private methods

        public void OutputDataReceivedProc(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (outLine.Data == null)
                return;

            String t = outLine.Data;
            if (t.Contains("score"))
            {
                //LogHelper.logger.Info("Trying to parse engine output for searching score. Current line: " + t);
                var allParts = t.Split(' ');
                int score = 0;
                if (int.TryParse(allParts[7], out score))
                {
                    //LogHelper.logger.Info("score parsed as: " + score);
                    currentMoveScore = (score / 100.0).ToString();
                    //LogHelper.logger.Info("score parsed as in percentage : " + currentMoveScore);
                }
            }
            if (t.Contains("bestmove"))
            {
                LogHelper.logger.Info("Trying to parse engine output for best move score. Current line: " + t);
                String bestmove = t.Substring(9, 4);
                //Console.WriteLine("Best move: " + bestmove);

                LogHelper.logger.Info("Best move parsed as: " + bestmove);
                BestMovFound(null, new BestMoveFoundArgs(bestmove, currentMoveScore));
            }
            //else if (t.Contains(" pv "))
            //{
            //    String considerering = t;
            //    Int32 length = considerering.Length;
            //    Int32 idxofline = considerering.IndexOf(" pv ") + 4;
            //    considerering = considerering.Substring(idxofline, length - idxofline);
            //    //Console.WriteLine(" Considering line: " + considerering);
            //}
        }

        //private String ConstructMoveString()
        //{
        //    String result = "";

        //    for (Int32 idx = 0; idx < Globals.GameData.g_MoveHistory.Count; ++idx)
        //    {
        //        Int32 From = Globals.GameData.g_MoveHistory[idx].FromSquare;
        //        Int32 To = Globals.GameData.g_MoveHistory[idx].ToSquare;

        //        Int32 RowFrom = 0;
        //        Int32 ColFrom = 0;
        //        Globals.Etc.GetRowColFromSquare(From, out RowFrom, out ColFrom);

        //        Int32 RowTo = 0;
        //        Int32 ColTo = 0;
        //        Globals.Etc.GetRowColFromSquare(To, out RowTo, out ColTo);

        //        String FromString = "";
        //        switch (ColFrom)
        //        {
        //            case 0:
        //                FromString += "a";
        //                break;
        //            case 1:
        //                FromString += "b";
        //                break;
        //            case 2:
        //                FromString += "c";
        //                break;
        //            case 3:
        //                FromString += "d";
        //                break;
        //            case 4:
        //                FromString += "e";
        //                break;
        //            case 5:
        //                FromString += "f";
        //                break;
        //            case 6:
        //                FromString += "g";
        //                break;
        //            case 7:
        //                FromString += "h";
        //                break;
        //        }

        //        FromString += (RowFrom + 1).ToString();

        //        String ToString = "";
        //        switch (ColTo)
        //        {
        //            case 0:
        //                FromString += "a";
        //                break;
        //            case 1:
        //                FromString += "b";
        //                break;
        //            case 2:
        //                FromString += "c";
        //                break;
        //            case 3:
        //                FromString += "d";
        //                break;
        //            case 4:
        //                FromString += "e";
        //                break;
        //            case 5:
        //                FromString += "f";
        //                break;
        //            case 6:
        //                FromString += "g";
        //                break;
        //            case 7:
        //                FromString += "h";
        //                break;
        //        }

        //        FromString += (RowTo + 1).ToString();

        //        result += FromString + ToString + " ";
        //    }

        //    return result;
        //}

        public void EngineCommand(String cmd)
        {
            LogHelper.logger.Info("Executing command on engine. Current Command is: " + cmd);
            try
            {
                if (UCI_Engine != null)
                    UCI_Engine.StandardInput.WriteLine(cmd);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error(exception.Message);
                throw;
            }
          LogHelper.logger.Info("EngineCommand finished");
            //string value = UCI_Engine.StandardOutput.ReadToEnd();
        }

        #endregion
    }

    public class BestMoveFoundArgs : EventArgs
    {
        public string BestMove { get; set; }
        public string CurrentMoveScore { get; set; }

        public BestMoveFoundArgs(string bestMove, string currentMoveScore)
        {
            BestMove = bestMove;
            CurrentMoveScore = currentMoveScore;
        }
    }
}
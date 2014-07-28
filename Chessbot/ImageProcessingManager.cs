using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using Emgu.CV.GPU;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace OpenCVDemo1
{
    static class ImageProcessingManager
    {
        #region Members
        private static string templatePath = null;
        public static readonly string CurrentExecutationPath = AssemblyDirectory;
        private static double _standardMatchingFactor = 0.75;
        public static List<ChessPiece> allChessBoardTemplate = null;
        static List<ChessEntity> currentChessBoardPosition = null;
        public static StringBuilder TextChessboardConfiguration = new StringBuilder();
        #endregion

        #region Properties
        public static double IntensityValue { get; set; }
        public static Image CurrentTemplate { get; set; }
        public static string TemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(templatePath))
                {
                    templatePath = string.Concat(CurrentExecutationPath, "\\", Constants.TEMPLATE_PATH, "\\");
                }
                return templatePath;
            }
        }

        public static double StandardMatchingFactor { get { return _standardMatchingFactor; } set { _standardMatchingFactor = value; } }
        public static string TotalProcessingTime { get; set; }
        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        #endregion

        #region Image Processing Methods
        public static Image TakeScreenShot()
        {
            Image capturedScreen = null;

            //LogHelper.logger.Info("TakeScreenShot called...");
            try
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }
                    System.IO.Stream imageStream = new MemoryStream();

                    //EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,100L);
                    //myEncoderParameters.Param[0] = myEncoderParameter;
                    //ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                    //bitmap.Save(imageStream, jgpEncoder, myEncoderParameters);

                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    ImageCodecInfo jgpEncoder = Helper.GetEncoder(ImageFormat.Png);
                    bitmap.Save(imageStream, jgpEncoder, myEncoderParameters);

                    //bitmap.Save(imageStream, ImageFormat.Jpeg);
                    //bitmap.Save("screen.jpg", ImageFormat.Jpeg);
                    capturedScreen = Image.FromStream(imageStream); ;
                    //capturedScreen = Image.FromFile("screen.jpg");
                }

                // Capture only Active Window ************

                //Rectangle bounds = this.Bounds;
                //using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                //{
                //    using (Graphics g = Graphics.FromImage(bitmap))
                //    {
                //        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                //    }
                //    bitmap.Save("C://test.jpg", ImageFormat.Jpeg);
                //}
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot","Chessbot",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("TakeScreenShot finished...");

            return capturedScreen;
        }

       
        public static void ConvertImageToGrayScale(Bitmap image)
        {
            try
            {
                //LogHelper.logger.Info("ConvertImageToGrayScale called...");
                // Normalizing it to grayscale
                Image<Gray, Byte> normalizedMasterImage = new Image<Gray, Byte>(image);
                normalizedMasterImage.Save("before.jpg");
                normalizedMasterImage = normalizedMasterImage.ThresholdBinary(new Gray(100), new Gray(255));
                normalizedMasterImage.Save("after.jpg");
                //CvInvoke.cvShowImage("gray scale input image", normalizedMasterImage.Ptr);
                //image = normalizedMasterImage.ToBitmap();
                //image.Save("gray.jpg");
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("ConvertImageToGrayScale finished...");
        }
        public static bool AreImagesSame(Image<Gray, Byte> inputImage, Image<Gray, Byte> templateImage, double comparisonFactor)
        {
            bool Success = false;
            try
            {
                //LogHelper.logger.Info("AreImagesSame called...");
                //Point Object_Location = new Point();
                Point dftSize = new Point(inputImage.Width + (templateImage.Width * 2), inputImage.Height + (templateImage.Height * 2));
                using (Image<Gray, Byte> pad_array = new Image<Gray, Byte>(dftSize.X, dftSize.Y))
                {
                    //copy centre
                    pad_array.ROI = new Rectangle(templateImage.Width, templateImage.Height, inputImage.Width, inputImage.Height);
                    CvInvoke.cvCopy(inputImage.Convert<Gray, Byte>(), pad_array, IntPtr.Zero);

                    //CvInvoke.cvShowImage("pad_array", pad_array);
                    pad_array.ROI = (new Rectangle(0, 0, dftSize.X, dftSize.Y));
                    using (Image<Gray, float> result_Matrix = pad_array.MatchTemplate(templateImage, TM_TYPE.CV_TM_CCOEFF_NORMED))
                    {
                        result_Matrix.ROI = new Rectangle(templateImage.Width, templateImage.Height, inputImage.Width, inputImage.Height);

                        Point[] MAX_Loc, Min_Loc;
                        double[] min, max;
                        result_Matrix.MinMax(out min, out max, out Min_Loc, out MAX_Loc);

                        using (Image<Gray, double> RG_Image = result_Matrix.Convert<Gray, double>().Copy())
                        {
                            //#TAG WILL NEED TO INCREASE SO THRESHOLD AT LEAST 0.8

                            if (max[0] > comparisonFactor)
                            //if (max[0] > 0.75)
                            {
                                //Object_Location = MAX_Loc[0];
                                Success = true;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("AreImagesSame finished...");
            return Success;
        }
        public static Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> GetBinaryImage(Image inputImage, double intensity)
        {
            //LogHelper.logger.Info("GetBinaryImage called...");
            Image<Gray, byte> binaryImage = null;
            try
            {
                Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> cvImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(inputImage as Bitmap);
                binaryImage = cvImage.Convert<Gray, byte>().ThresholdBinary(new Gray(intensity), new Gray(255));
                //Emgu.CV.CvInvoke.cvShowImage("Current Image under use...", binaryImage);
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("GetBinaryImage finished...");
            return binaryImage;
        }
        #endregion

        #region Template Methods
        public static ChessTemplate ReadTemplate(string templateFileName)
        {
            //LogHelper.logger.Info("ReadTemplate called...");
            ChessTemplate chessTemplate = null;
            try
            {
                if (File.Exists(templateFileName) == false)
                {
                    MessageBox.Show("Template not found. Please create and save template.", "Read Tempalte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                using (System.IO.Stream stream = File.Open(templateFileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    //masterTemplate = (List<ChessPiece>)bin.Deserialize(stream);
                    chessTemplate = (ChessTemplate)bin.Deserialize(stream);

                    allChessBoardTemplate = chessTemplate.ChessConfiguration;
                }
            }
            catch (Exception exception)
            {
                allChessBoardTemplate = null;
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("ReadTemplate finished...");
            return chessTemplate;
        }
        public static bool SaveTemplate(Image masterTemplateImage, string templateFileName, List<ChessPiece> masterTemplate, int intensity)
        {
            //LogHelper.logger.Info("SaveTemplate called...");
            bool result = true;
            try
            {
                using (System.IO.Stream stream = File.Open(templateFileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    //bin.Serialize(stream, masterTemplate);
                    ChessTemplate template = new ChessTemplate();
                    template.ChessConfiguration = masterTemplate;
                    template.CurrentTemplateImage = masterTemplateImage;
                    template.Intensity = intensity;
                    bin.Serialize(stream, template);
                }
            }
            catch (Exception exception)
            {
                result = false;
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("SaveTemplate finished...");
            return result;
        }
        public static List<ChessPiece> FillMasterTemplate(Image chessboardImage, int blockPaddingAmount, bool isWhiteFirst, double intensity = 100)
        {
            try
            {
                //LogHelper.logger.Info("FillMasterTemplate called...");
                //ChessPieceTemplate = new System.Collections.Generic.Dictionary<string, Image<Gray, byte>>();
                allChessBoardTemplate = new List<ChessPiece>();

                int width = chessboardImage.Width;
                int height = chessboardImage.Height;

                Bitmap bmpChessboard = new Bitmap(chessboardImage);
                //Image<Gray, Byte> grayScaledChessboard = new Image<Gray, Byte>(bmpChessboard);
                Image<Gray, Byte> grayScaledChessboard = ImageProcessingManager.GetBinaryImage(chessboardImage, ImageProcessingManager.IntensityValue);
                //grayScaledChessboard = grayScaledChessboard.ThresholdBinary(new Gray(100), new Gray(255));
                bmpChessboard = grayScaledChessboard.ToBitmap();
                bmpChessboard.Save("gray board.jpg");

                int blockWidth = (width / Constants.GRID_SIZE);
                int blockHeight = (height / Constants.GRID_SIZE);
                int blockLeft = 0;
                int blockTop = 0;

                int blockPaddingAmountDouble = blockPaddingAmount * 2;

                string position = string.Empty;
                string fileName = CurrentExecutationPath + Constants.PositionFolderName + "{0}.jpg";
                string[] columnNames = { "A", "B", "C", "D", "E", "F", "G", "H" };

                string rowName = string.Empty;
                Rectangle r = new Rectangle();
                for (int rowIndex = 1; rowIndex <= Constants.GRID_SIZE; rowIndex++)
                {
                    blockLeft = 0;
                    for (int colIndex = 1; colIndex <= Constants.GRID_SIZE; colIndex++)
                    {
                        if (rowIndex == 1  // scan all pieace from first row
                            || (rowIndex == 2 && colIndex == 1) // Scan first pawn from first side
                            || (rowIndex == 2 && colIndex == 2) // Scan second pawn from first side
                            || (rowIndex == 3 && colIndex == 1) // Scan first empty position
                            || (rowIndex == 3 && colIndex == 2) // Scan second alternative empty position
                            // Second part of chess board
                            || (rowIndex == 7 && colIndex == 1) // Scan first pawn from second side
                            || (rowIndex == 7 && colIndex == 2) // Scan second pawn from second side
                            || rowIndex == 8) // scan all main pieces from last row
                        {
                            r = new Rectangle(blockLeft + blockPaddingAmount, blockTop + blockPaddingAmount, blockWidth - blockPaddingAmountDouble, blockHeight - blockPaddingAmountDouble);
                            using (Bitmap currentPiece = bmpChessboard.Clone(r, PixelFormat.DontCare))
                            {
                                string pieceName = string.Empty;
                                string pieceCode = string.Empty;
                                if (isWhiteFirst)
                                {
                                    //position = String.Format("{0}{1}", columnNames[whiteRowCounter], rowIndex);
                                    //whiteRowCounter--;

                                    // White side pieces
                                    if ((rowIndex == 1 && colIndex == 1) || (rowIndex == 1 && colIndex == 8))
                                    { pieceName = Constants.WhiteRook; pieceCode = Constants.WhiteRook_Code; }
                                    else if ((rowIndex == 1 && colIndex == 2) || (rowIndex == 1 && colIndex == 7))
                                    { pieceName = Constants.WhiteKnight; pieceCode = Constants.WhiteKnight_Code; }
                                    else if ((rowIndex == 1 && colIndex == 3) || (rowIndex == 1 && colIndex == 6))
                                    { pieceName = Constants.WhiteBishop; pieceCode = Constants.WhiteBishop_Code; }
                                    else if (rowIndex == 1 && colIndex == 4)
                                    { pieceName = Constants.WhiteKing; pieceCode = Constants.WhiteKing_Code; }
                                    else if (rowIndex == 1 && colIndex == 5)
                                    { pieceName = Constants.WhiteQueen; pieceCode = Constants.WhiteQueen_Code; }
                                    // Save 2 pieaces of white pawn of different background color to match in future
                                    else if (rowIndex == 2 && colIndex == 1)
                                    { pieceName = Constants.WhitePawn1; pieceCode = Constants.WhitePawn_Code; }
                                    else if (rowIndex == 2 && colIndex == 2)
                                    { pieceName = Constants.WhitePawn2; pieceCode = Constants.WhitePawn_Code; }
                                    else if (rowIndex == 3 && colIndex == 1)
                                        pieceName = Constants.EmptyGridZone1;
                                    else if (rowIndex == 3 && colIndex == 2)
                                        pieceName = Constants.EmptyGridZone2;

                                    // black side pieces
                                    else if (rowIndex == 7 && colIndex == 1)
                                    { pieceName = Constants.BlackPawn1; pieceCode = Constants.BlackPawn_Code; }
                                    else if (rowIndex == 7 && colIndex == 2)
                                    { pieceName = Constants.BlackPawn2; ; pieceCode = Constants.BlackPawn_Code; }
                                    else if ((rowIndex == 8 && colIndex == 1) || (rowIndex == 8 && colIndex == 8))
                                    { pieceName = Constants.BlackRook; ; pieceCode = Constants.BlackRook_Code; }
                                    else if ((rowIndex == 8 && colIndex == 2) || (rowIndex == 8 && colIndex == 7))
                                    { pieceName = Constants.BlackKnight; ; pieceCode = Constants.BlackKnight_Code; }
                                    else if ((rowIndex == 8 && colIndex == 3) || (rowIndex == 8 && colIndex == 6))
                                    { pieceName = Constants.BlackBishop; ; pieceCode = Constants.BlackBishop_Code; }
                                    else if (rowIndex == 8 && colIndex == 4)
                                    { pieceName = Constants.BlackKing; ; pieceCode = Constants.BlackKing_Code; }
                                    else if (rowIndex == 8 && colIndex == 5)
                                    { pieceName = Constants.BlackQueen; ; pieceCode = Constants.BlackQueen_Code; }
                                }
                                else
                                {
                                    //position = String.Format("{0}{1}", columnNames[blackRowCounter], Constants.GRID_SIZE - rowIndex + 1);
                                    //blackRowCounter++;

                                    // black side pieces
                                    if ((rowIndex == 1 && colIndex == 1) || (rowIndex == 1 && colIndex == 8))
                                    { pieceName = Constants.BlackRook; ; pieceCode = Constants.BlackRook; }
                                    else if ((rowIndex == 1 && colIndex == 2) || (rowIndex == 1 && colIndex == 7))
                                    { pieceName = Constants.BlackKnight; ; pieceCode = Constants.BlackKnight_Code; }
                                    else if ((rowIndex == 1 && colIndex == 3) || (rowIndex == 1 && colIndex == 6))
                                    { pieceName = Constants.BlackBishop; ; pieceCode = Constants.BlackBishop_Code; }
                                    else if (rowIndex == 1 && colIndex == 4)
                                    { pieceName = Constants.BlackQueen; ; pieceCode = Constants.BlackQueen_Code; }
                                    else if (rowIndex == 1 && colIndex == 5)
                                    { pieceName = Constants.BlackKing; ; pieceCode = Constants.BlackKing_Code; }
                                    else if (rowIndex == 2)
                                    { pieceName = Constants.BlackPawn1; ; pieceCode = Constants.BlackPawn_Code; }
                                    else if (rowIndex == 3 && colIndex == 1)
                                        pieceName = Constants.EmptyGridZone1;
                                    else if (rowIndex == 3 && colIndex == 2)
                                        pieceName = Constants.EmptyGridZone2;

                                     // White side pieces
                                    else if ((rowIndex == 8 && colIndex == 1) || (rowIndex == 8 && colIndex == 8))
                                    { pieceName = Constants.WhiteRook; pieceCode = Constants.WhiteRook_Code; }
                                    else if ((rowIndex == 8 && colIndex == 2) || (rowIndex == 8 && colIndex == 7))
                                    { pieceName = Constants.WhiteKnight; pieceCode = Constants.WhiteKnight_Code; }
                                    else if ((rowIndex == 8 && colIndex == 3) || (rowIndex == 8 && colIndex == 6))
                                    { pieceName = Constants.WhiteBishop; pieceCode = Constants.WhiteBishop_Code; }
                                    else if (rowIndex == 8 && colIndex == 4)
                                    { pieceName = Constants.WhiteQueen; pieceCode = Constants.WhiteQueen_Code; }
                                    else if (rowIndex == 8 && colIndex == 5)
                                    { pieceName = Constants.WhiteKing; pieceCode = Constants.WhiteKing_Code; }
                                    else if (rowIndex == 7 && colIndex == 1)
                                    { pieceName = Constants.WhitePawn1; pieceCode = Constants.WhitePawn_Code; }
                                    else if (rowIndex == 7 && colIndex == 2)
                                    { pieceName = Constants.WhitePawn2; pieceCode = Constants.WhitePawn_Code; }
                                }

                                // Add current piece to list
                                //ChessBoardTemplate.Add(new ChessPiece { Name = pieceName, Piece = new Image<Gray, Byte>(currentPiece), ColumnPosition = colIndex, RowPosition = rowIndex, IsAlive = true });
                                allChessBoardTemplate.Add(new ChessPiece { Name = pieceName, Code = pieceCode, Piece = new Image<Gray, Byte>(currentPiece) });
                                //temp.Save(String.Format(fileName, columnNames[rowIndex - 1], colIndex), ImageFormat.Jpeg);
                                //ChessPieceTemplate[position].Save(String.Format(fileName, position));
                                blockLeft += blockWidth;
                            }
                        }
                    }
                    blockTop += blockHeight;
                }
                //MessageBox.Show("done");
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("FillMasterTemplate finished...");
            return allChessBoardTemplate;
        }
        #endregion
        public static void ReadChessBoardCurrentPosition(Image chessboardImage, int blockPaddingAmount, bool isWhiteFirst, int intensityValue)
        {
            #region Old code
            //try
            //{
            //    //dicChessPosition = new System.Collections.Generic.Dictionary<string, Image<Gray, byte>>();
            //    //CreateMasterTemplate(chessboardImage);

            //    int width = chessboardImage.Width;
            //    int height = chessboardImage.Height;
            //    int startTop = 0;
            //    int startLeft = 0;

            //    Bitmap bmpChessboard = new Bitmap(chessboardImage);
            //    Image<Gray, Byte> grayScaledChessboard = new Image<Gray, Byte>(bmpChessboard);
            //    //grayScaledChessboard = grayScaledChessboard.ThresholdBinary(new Gray(255), new Gray(255));
            //    bmpChessboard = grayScaledChessboard.ToBitmap();
            //    bmpChessboard.Save("gray board.jpg");

            //    int blockWidth = (width / Constants.GRID_SIZE);
            //    int blockHeight = (height / Constants.GRID_SIZE);
            //    int blockLeft = 0;
            //    int blockTop = 0;

            //    string piecePosition = string.Empty;
            //    string fileName = CurrentExecutationPath + Constants.PositionFolderName + "{0}.jpg";
            //    string[] columnNames = { "A", "B", "C", "D", "E", "F", "G", "H" };

            //    string rowName = string.Empty;
            //    Rectangle r = new Rectangle();
            //    for (int rowIndex = 1; rowIndex <= Constants.GRID_SIZE; rowIndex++)
            //    {
            //        blockLeft = 0;
            //        int whiteRowCounter = 7;
            //        int blackRowCounter = 0;
            //        for (int colIndex = 1; colIndex <= Constants.GRID_SIZE; colIndex++)
            //        {
            //            r = new Rectangle(blockLeft + blockPaddingAmount, blockTop + blockPaddingAmount, blockWidth - blockPaddingAmount, blockHeight - blockPaddingAmount);
            //            using (Bitmap currentPiece = bmpChessboard.Clone(r, PixelFormat.DontCare))
            //            {
            //                if (isWhiteFirst)
            //                {
            //                    piecePosition = String.Format("{0}{1}", columnNames[whiteRowCounter], rowIndex);
            //                    whiteRowCounter--;
            //                }
            //                else
            //                {
            //                    piecePosition = String.Format("{0}{1}", columnNames[blackRowCounter], Constants.GRID_SIZE - rowIndex + 1);
            //                    blackRowCounter++;
            //                }
            //                //dicChessPosition[piecePosition] = new Image<Gray, Byte>(currentPiece);
            //                //temp.Save(String.Format(fileName, columnNames[rowIndex - 1], colIndex), ImageFormat.Jpeg);
            //                //dicChessPosition[piecePosition].Save(String.Format(fileName, piecePosition));
            //                blockLeft += blockWidth;
            //            }
            //        }
            //        blockTop += blockHeight;
            //    }
            //    //MessageBox.Show("done");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            #endregion
            try
            {
                //LogHelper.logger.Info("ReadChessBoardCurrentPosition called...");
                var totalExecutionTime = System.Diagnostics.Stopwatch.StartNew();
                //Bitmap bmpChessboard = new Bitmap(chessboardImage);
                ////Bitmap bmpChessboard = new Bitmap(CaptureChessBoard.CapturedBoard);
                //Image<Gray, Byte> grayScaledChessboard = new Image<Gray, Byte>(bmpChessboard);
                //bmpChessboard = grayScaledChessboard.ToBitmap();

                //Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> cvImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>(chessboardImage as Bitmap);

                //var binaryImage = cvImage.Convert<Gray, byte>().ThresholdBinary(new Gray(intensityValue), new Gray(255));

                Bitmap bmpChessboard = (ImageProcessingManager.GetBinaryImage(chessboardImage, intensityValue).Bitmap).Clone(new Rectangle(0, 0, chessboardImage.Width, chessboardImage.Height), chessboardImage.PixelFormat);

                //bmpChessboard.Save("gray board.jpg");

                int blockWidth = (chessboardImage.Width / Constants.GRID_SIZE);
                int blockHeight = (chessboardImage.Height / Constants.GRID_SIZE);

                int blockLeft = 0;
                int blockTop = 0;
                Rectangle r = new Rectangle();

                if (blockHeight < blockWidth)
                    blockHeight = blockWidth;
                else
                    blockWidth = blockHeight;

                currentChessBoardPosition = new List<ChessEntity>();
                ChessEntity currentEntity = null;


                Image<Gray, Byte> emptyGridZone1 = allChessBoardTemplate.FirstOrDefault(x => x.Name == Constants.EmptyGridZone1).Piece;
                Image<Gray, Byte> emptyGridZone2 = allChessBoardTemplate.FirstOrDefault(x => x.Name == Constants.EmptyGridZone2).Piece;

                int blockPaddingAmountDouble = blockPaddingAmount * 2;
                //emptyGridZone1 = emptyGridZone1.Resize(0.5, INTER.CV_INTER_AREA);
                //emptyGridZone2 = emptyGridZone2.Resize(0.5, INTER.CV_INTER_AREA);
                for (int rowIndex = 1; rowIndex <= Constants.GRID_SIZE; rowIndex++)
                {
                    blockLeft = 0;
                    for (int colIndex = 1; colIndex <= Constants.GRID_SIZE; colIndex++)
                    {
                        r = new Rectangle(blockLeft + blockPaddingAmount, blockTop + blockPaddingAmount, blockWidth - blockPaddingAmountDouble, blockHeight - blockPaddingAmountDouble);
                        using (Bitmap currentPiece = bmpChessboard.Clone(r, PixelFormat.DontCare))
                        {
                            // Mark each grid zone entity
                            currentEntity = new ChessEntity();

                            // Check is empty grid zone?
                            bool isEmptyGridZone = AreImagesSame(emptyGridZone1, new Image<Gray, Byte>(currentPiece), StandardMatchingFactor) || AreImagesSame(emptyGridZone2, new Image<Gray, Byte>(currentPiece), StandardMatchingFactor);
                            if (isEmptyGridZone)
                            {
                                currentEntity.IsAlive = false;
                            }
                            // If not empty grid zone, find exact piece from template
                            else
                            {
                                foreach (ChessPiece item in allChessBoardTemplate)
                                {
                                    bool result = AreImagesSame(item.Piece, new Image<Gray, Byte>(currentPiece), StandardMatchingFactor);
                                    if (result == true)
                                    {
                                        // Piece matched. Extract its name and save its position
                                        currentEntity.PieceInfo = new ChessPiece();
                                        currentEntity.PieceInfo.Name = item.Name;
                                        currentEntity.PieceInfo.Code = item.Code;
                                        currentEntity.PieceInfo.Piece = item.Piece;
                                        currentEntity.IsAlive = true;

                                        //currentEntity.PieceInfo.Piece.Save(currentEntity.PieceInfo.Name + ".jpg");
                                        break;
                                    }
                                }
                            }

                            //Add current entity to chessboard position list
                            currentEntity.RowPosition = rowIndex;
                            currentEntity.ColumnPosition = colIndex;
                            currentChessBoardPosition.Add(currentEntity);
                            blockLeft += blockWidth;
                        }
                    }
                    blockTop += blockHeight;
                }
                totalExecutionTime.Stop();
                //MessageBox.Show("Total time to read: " + totalExecutionTime.ElapsedMilliseconds.ToString());
                TotalProcessingTime = totalExecutionTime.ElapsedMilliseconds.ToString();

                //MessageBox.Show("done");
                //var allPieces = currentChessBoardPosition.Where(x => x.IsAlive == true);
                //foreach (var item in allPieces)
                //{
                //    //item.PieceInfo.Piece.Save(item.PieceInfo.Name + ".jpg");
                //}
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //LogHelper.logger.Info("ReadChessBoardCurrentPosition finished...");
        }

        public static bool CheckFirstWhosFirstMove(Image chessboardImage, int blockPaddingAmount)
        {
            bool isWhitePlaying = false;
            try
            {
                //LogHelper.logger.Info("CheckFirstWhosFirstMove called...");
                if (allChessBoardTemplate == null || allChessBoardTemplate.Count == 0)
                {
                    MessageBox.Show("Please Load Template.", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                int width = chessboardImage.Width;
                int height = chessboardImage.Height;

                Bitmap bmpChessboard = new Bitmap(chessboardImage);
                //Bitmap bmpChessboard = new Bitmap(CaptureChessBoard.CapturedBoard);
                Image<Gray, Byte> grayScaledChessboard = new Image<Gray, Byte>(bmpChessboard);
                bmpChessboard = grayScaledChessboard.ToBitmap();
                //bmpChessboard.Save("gray board.jpg");

                int blockWidth = (width / Constants.GRID_SIZE);
                int blockHeight = (height / Constants.GRID_SIZE);

                int blockLeft = 0;
                int blockTop = 0;
                Rectangle r = new Rectangle();

                if (blockHeight < blockWidth)
                    blockHeight = blockWidth;
                else
                    blockWidth = blockHeight;

                currentChessBoardPosition = new List<ChessEntity>();

                Image<Gray, Byte> emptyGridZone1 = allChessBoardTemplate.FirstOrDefault(x => x.Name == Constants.EmptyGridZone1).Piece;
                Image<Gray, Byte> emptyGridZone2 = allChessBoardTemplate.FirstOrDefault(x => x.Name == Constants.EmptyGridZone2).Piece;


                int blockPaddingAmountDouble = blockPaddingAmount * 2;

                r = new Rectangle(blockLeft + blockPaddingAmount, blockTop + blockPaddingAmount, blockWidth - blockPaddingAmountDouble, blockHeight - blockPaddingAmountDouble);

                using (Bitmap currentPiece = bmpChessboard.Clone(r, PixelFormat.DontCare))
                {
                    //currentPiece.Save("NewGameTopLeftRook.png");
                    //allChessBoardTemplate[0].Piece.Save("TemplateTopLeftRook.png");
                    if (r.Width > allChessBoardTemplate[0].Piece.Bitmap.Width ||
                        r.Height > allChessBoardTemplate[0].Piece.Bitmap.Height)
                    {
                        LogHelper.logger.Error("Chessboard template piece size is lesser than current board chessboard size. Please use padding for correct result!");
                        MessageBox.Show("Chessboard template piece size is lesser than current board chessboard size. Please use padding for correct result!", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    var templateCroppedPiece = allChessBoardTemplate[0].Piece.Bitmap.Clone(r, PixelFormat.DontCare);
                    isWhitePlaying = AreImagesSame(new Image<Gray, byte>(currentPiece as Bitmap), new Image<Gray, byte>(templateCroppedPiece), ImageProcessingManager.StandardMatchingFactor);
                    if (isWhitePlaying)
                    {
                        //MessageBox.Show("User is playing with White side.");
                        //LogHelper.logger.Info("CheckFirstWhosFirstMove finished...");
                        return false;
                    }
                    else
                    {
                        //MessageBox.Show("User is playing with Black side.");
                        //LogHelper.logger.Info("CheckFirstWhosFirstMove finished...");
                        return true;
                    }
                }

            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isWhitePlaying;
        }

        internal static void PrintChessBoard(bool isUserPlayingWhite)
        {
            //try
            //{

            //    //LogHelper.logger.Info("PrintChessBoard called...");

            //    //var totalExecutionTime = System.Diagnostics.Stopwatch.StartNew();

            //    if (currentChessBoardPosition == null || currentChessBoardPosition.Count == 0)
            //    {
            //        MessageBox.Show("Template is not loaded.");
            //        return;
            //    }
            //    Console.Clear();

            //    //Console.WriteLine("**************************** Chess board ****************************");
            //    //Console.WriteLine();

            //    string rowHeader = "     A    B    C    D    E    F    G    H";
            //    //string blackRowHeader = "     A    B    C    D    E    F    G    H";
            //    //string whiteRowHeader = "     H    G    F    E    D    C    B    A";

            //    if (!isUserPlayingWhite)
            //    {
            //        rowHeader = "     H    G    F    E    D    C    B    A";
            //    }
            //    else
            //    {
            //        rowHeader = "     A    B    C    D    E    F    G    H";
            //    }

            //    //Console.WriteLine(rowHeader);
            //    string rowSeparator = "  +----+----+----+----+----+----+----+----+";
            //    //Console.WriteLine(rowSeparator);
            //    string chessRowTemplate = "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} |";
            //    string emptyPiece = "  ";
            //    string currentRow = string.Empty;
            //    for (int rowIndex = 1; rowIndex <= Constants.GRID_SIZE; rowIndex++)
            //    {
            //        currentRow = string.Empty;
            //        string one = string.Empty;
            //        string two = string.Empty;
            //        string three = string.Empty;
            //        string four = string.Empty;
            //        string five = string.Empty;
            //        string six = string.Empty;
            //        string seven = string.Empty;
            //        string eight = string.Empty;
            //        for (int columnIndex = 1; columnIndex <= Constants.GRID_SIZE; columnIndex++)
            //        {
            //            var currentPiece = currentChessBoardPosition.FirstOrDefault(x => x.RowPosition == rowIndex && x.ColumnPosition == columnIndex);
            //            switch (columnIndex)
            //            {
            //                case 1:
            //                    one = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (one.Equals("WE1") || one.Equals("WE2") || one.Equals("BE1") || one.Equals("BE2"))
            //                        one = emptyPiece;
            //                    break;
            //                case 2:
            //                    two = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (two.Equals("WE1") || two.Equals("WE2") || two.Equals("BE1") || two.Equals("BE2"))
            //                        two = emptyPiece;
            //                    break;
            //                case 3:
            //                    three = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (three.Equals("WE1") || three.Equals("WE2") || three.Equals("BE1") || three.Equals("BE2"))
            //                        three = emptyPiece;
            //                    break;
            //                case 4:
            //                    four = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (four.Equals("WE1") || four.Equals("WE2") || four.Equals("BE1") || four.Equals("BE2"))
            //                        four = emptyPiece;
            //                    break;
            //                case 5:
            //                    five = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (five.Equals("WE1") || five.Equals("WE2") || five.Equals("BE1") || five.Equals("BE2"))
            //                        five = emptyPiece;
            //                    break;
            //                case 6:
            //                    six = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (six.Equals("WE1") || six.Equals("WE2") || six.Equals("BE1") || six.Equals("BE2"))
            //                        six = emptyPiece;
            //                    break;
            //                case 7:
            //                    seven = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (seven.Equals("WE1") || seven.Equals("WE2") || seven.Equals("BE1") || seven.Equals("BE2"))
            //                        seven = emptyPiece;
            //                    break;
            //                case 8:
            //                    eight = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
            //                    if (eight.Equals("WE1") || eight.Equals("WE2") || eight.Equals("BE1") || eight.Equals("BE2"))
            //                        eight = emptyPiece;
            //                    break;
            //                default:
            //                    break;
            //            }

            //        }
            //        if (isUserPlayingWhite)
            //        {
            //            Console.Write(string.Format(chessRowTemplate, 9 - rowIndex, one, two, three, four, five, six, seven, eight));
            //        }
            //        else
            //        {
            //            Console.Write(string.Format(chessRowTemplate, rowIndex, one, two, three, four, five, six, seven, eight));
            //        }
            //        //Console.WriteLine();
            //        //Console.WriteLine(rowSeparator);

            //    }
            //    //totalExecutionTime.Stop();
            //    //MessageBox.Show("Total time to read: " + totalExecutionTime.ElapsedMilliseconds.ToString());
            //}
            //catch (Exception exception)
            //{
            //    LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
            //    LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
            //    MessageBox.Show("An error occurred. Please restart bot","Chessbot",MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //LogHelper.logger.Info("PrintChessBoard finished...");


            try
            {
                if (currentChessBoardPosition == null || currentChessBoardPosition.Count == 0)
                {
                    MessageBox.Show("Template is not loaded.");
                    return;
                }
                //Console.Clear();
                TextChessboardConfiguration = new StringBuilder();

                //Console.WriteLine("**************************** Chess board ****************************");
                //Console.WriteLine();

                string rowHeader = "     A    B    C    D    E    F    G    H";
                //string blackRowHeader = "     A    B    C    D    E    F    G    H";
                //string whiteRowHeader = "     H    G    F    E    D    C    B    A";

                if (!isUserPlayingWhite)
                {
                    rowHeader = "     H    G    F    E    D    C    B    A";
                }
                else
                {
                    rowHeader = "     A    B    C    D    E    F    G    H";
                }

                TextChessboardConfiguration.Append(rowHeader);
                TextChessboardConfiguration.Append(Environment.NewLine);
                TextChessboardConfiguration.Append("=================================");
                TextChessboardConfiguration.Append(Environment.NewLine);
                //Console.WriteLine(rowHeader);
                string rowSeparator = "  ==================================";
                //Console.WriteLine(rowSeparator);
                string chessRowTemplate = "{0} || {1} || {2} || {3} || {4} || {5} || {6} || {7} || {8} ||";
                string emptyPiece = "  ";
                string currentRow = string.Empty;
                for (int rowIndex = 1; rowIndex <= Constants.GRID_SIZE; rowIndex++)
                {
                    currentRow = string.Empty;
                    string one = string.Empty;
                    string two = string.Empty;
                    string three = string.Empty;
                    string four = string.Empty;
                    string five = string.Empty;
                    string six = string.Empty;
                    string seven = string.Empty;
                    string eight = string.Empty;
                    for (int columnIndex = 1; columnIndex <= Constants.GRID_SIZE; columnIndex++)
                    {
                        var currentPiece = currentChessBoardPosition.FirstOrDefault(x => x.RowPosition == rowIndex && x.ColumnPosition == columnIndex);
                        switch (columnIndex)
                        {
                            case 1:
                                one = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (one.Equals("WE1") || one.Equals("WE2") || one.Equals("BE1") || one.Equals("BE2"))
                                    one = emptyPiece;
                                break;
                            case 2:
                                two = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (two.Equals("WE1") || two.Equals("WE2") || two.Equals("BE1") || two.Equals("BE2"))
                                    two = emptyPiece;
                                break;
                            case 3:
                                three = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (three.Equals("WE1") || three.Equals("WE2") || three.Equals("BE1") || three.Equals("BE2"))
                                    three = emptyPiece;
                                break;
                            case 4:
                                four = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (four.Equals("WE1") || four.Equals("WE2") || four.Equals("BE1") || four.Equals("BE2"))
                                    four = emptyPiece;
                                break;
                            case 5:
                                five = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (five.Equals("WE1") || five.Equals("WE2") || five.Equals("BE1") || five.Equals("BE2"))
                                    five = emptyPiece;
                                break;
                            case 6:
                                six = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (six.Equals("WE1") || six.Equals("WE2") || six.Equals("BE1") || six.Equals("BE2"))
                                    six = emptyPiece;
                                break;
                            case 7:
                                seven = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (seven.Equals("WE1") || seven.Equals("WE2") || seven.Equals("BE1") || seven.Equals("BE2"))
                                    seven = emptyPiece;
                                break;
                            case 8:
                                eight = currentPiece.PieceInfo == null ? emptyPiece : currentPiece.PieceInfo.Name;
                                if (eight.Equals("WE1") || eight.Equals("WE2") || eight.Equals("BE1") || eight.Equals("BE2"))
                                    eight = emptyPiece;
                                break;
                            default:
                                break;
                        }

                    }
                    if (isUserPlayingWhite)
                    {
                        TextChessboardConfiguration.Append(string.Format(chessRowTemplate, 9 - rowIndex, one, two, three, four, five, six, seven, eight));
                        TextChessboardConfiguration.Append(Environment.NewLine);
                        //Console.Write(string.Format(chessRowTemplate, 9 - rowIndex, one, two, three, four, five, six, seven, eight));
                    }
                    else
                    {
                        TextChessboardConfiguration.Append(string.Format(chessRowTemplate, rowIndex, one, two, three, four, five, six, seven, eight));
                        TextChessboardConfiguration.Append(Environment.NewLine);
                        //Console.Write(string.Format(chessRowTemplate, rowIndex, one, two, three, four, five, six, seven, eight));
                    }
                    //Console.WriteLine();
                    //Console.WriteLine(rowSeparator);
                    TextChessboardConfiguration.Append(rowSeparator);
                    TextChessboardConfiguration.Append(Environment.NewLine);

                }
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("BR", "r");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("BN", "n");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("BB", "b");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("BQ", "q");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("BK", "k");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("BP", "p");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("WP", "P");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("WR", "R");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("WN", "N");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("WB", "B");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("WQ", "Q");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("WK", "K");
                TextChessboardConfiguration = TextChessboardConfiguration.Replace("WK", "K");
                //totalExecutionTime.Stop();
                //MessageBox.Show("Total time to read: " + totalExecutionTime.ElapsedMilliseconds.ToString());
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //LogHelper.logger.Info("PrintChessBoard finished...");
        }

        public static string PrepareFenString(bool isBlackMoveNext)
        {
            try
            {

                //LogHelper.logger.Info("PrepareFenString called...");

                //var totalExecutionTime = System.Diagnostics.Stopwatch.StartNew();

                StringBuilder fenString = new StringBuilder(string.Empty);
                //Prepare string part for "Piece Placement"
                for (int rowCounter = 1; rowCounter <= Constants.GRID_SIZE; rowCounter++)
                {
                    StringBuilder rowFenString = new StringBuilder(string.Empty);
                    var rowTemplate = currentChessBoardPosition.Where(c => c.RowPosition == rowCounter && c.PieceInfo != null && c.IsAlive);
                    if (rowTemplate == null || rowTemplate.Count() == 0)
                        rowFenString.Append("8");
                    else
                    {
                        int previousPieceColumnPosition = 0;
                        foreach (var piece in rowTemplate)
                        {
                            if (piece.IsAlive)
                            {
                                if (piece.ColumnPosition > 1 && piece.ColumnPosition - previousPieceColumnPosition > 1)
                                    rowFenString.Append((piece.ColumnPosition - previousPieceColumnPosition - 1).ToString()).Append(piece.PieceInfo.Code);
                                else
                                    rowFenString.Append(piece.PieceInfo.Code);

                                previousPieceColumnPosition = piece.ColumnPosition;
                            }
                        }
                        if (previousPieceColumnPosition < Constants.GRID_SIZE)
                            rowFenString.Append(Constants.GRID_SIZE - previousPieceColumnPosition);
                    }
                    fenString.Append(rowFenString);
                    if (rowCounter < Constants.GRID_SIZE)
                        fenString.Append("/");
                }
                fenString.Append(" ");

                //Append string part for "Active Color"

                if (isBlackMoveNext)
                {
                    fenString.Append(Constants.BlackMove);
                }
                else
                    fenString.Append(Constants.WhiteMove);

                fenString.Append(" ");

                //Append string part for "Castling"
                //TO DO: Need to implement exact Castling rule
                bool castlingPossible = false;
                int blackPrimeRow = 0;
                int whitePrimeRow = 0;
                if (Constants.ActiveMove == "w")
                {
                    whitePrimeRow = 1;
                    blackPrimeRow = Constants.GRID_SIZE;
                }
                else if (Constants.ActiveMove == "b")
                {
                    whitePrimeRow = Constants.GRID_SIZE;
                    blackPrimeRow = 1;
                }

                //Check castling for white pieces
                var whitePieces = currentChessBoardPosition.Where(c => c.RowPosition == whitePrimeRow);

                if (whitePieces.Where(c => (c.PieceInfo != null && c.PieceInfo.Name == Constants.WhiteKing) && c.ColumnPosition == 5).FirstOrDefault() != null)
                {
                    if (whitePieces.Where(c => (c.PieceInfo != null && c.PieceInfo.Name == Constants.WhiteRook) && c.ColumnPosition == 8).FirstOrDefault() != null &&
                        whitePieces.Where(c => (6 == c.ColumnPosition || c.ColumnPosition == 7) && c.IsAlive && c.PieceInfo != null) == null)
                    {
                        fenString.Append("K");
                        castlingPossible = true;
                    }

                    if (whitePieces.Where(c => (c.PieceInfo != null && c.PieceInfo.Name == Constants.WhiteRook) && c.ColumnPosition == 1).FirstOrDefault() != null &&
                       whitePieces.Where(c => 1 < c.ColumnPosition && c.ColumnPosition < 5 && c.IsAlive && c.PieceInfo != null) == null)
                    {
                        fenString.Append("Q");
                        castlingPossible = true;
                    }
                }

                var blackPieces = currentChessBoardPosition.Where(c => c.RowPosition == blackPrimeRow);

                if (blackPieces.Where(c => (c.PieceInfo != null && c.PieceInfo.Name == Constants.BlackKing) && c.ColumnPosition == 5).FirstOrDefault() != null)
                {
                    if (blackPieces.Where(c => (c.PieceInfo != null && c.PieceInfo.Name == Constants.BlackRook) && c.ColumnPosition == 8).FirstOrDefault() != null &&
                        blackPieces.Where(c => (6 == c.ColumnPosition || c.ColumnPosition == 7) && c.IsAlive && c.PieceInfo != null).FirstOrDefault() == null)
                    {
                        fenString.Append("k");
                        castlingPossible = true;
                    }

                    if (blackPieces.Where(c => (c.PieceInfo != null && c.PieceInfo.Name == Constants.WhiteRook) && c.ColumnPosition == 1).FirstOrDefault() != null &&
                       blackPieces.Where(c => 1 < c.ColumnPosition && c.ColumnPosition < 5 && c.IsAlive && c.PieceInfo != null) == null)
                    {
                        fenString.Append("q");
                        castlingPossible = true;
                    }
                }
                if (castlingPossible == false)
                    fenString.Append("-");

                fenString.Append(" ");

                //En Passant rule is not applied yet.
                //TO DO: Need to implement En Passant rule
                fenString.Append("-");
                fenString.Append(" ");

                //Append Halfmove clock count
                fenString.Append(Constants.HalfmoveClock.ToString());
                fenString.Append(" ");

                //Append Fullmove number
                fenString.Append(Constants.FullmoveNumber.ToString());
                fenString.Append(" ");

                fenString = fenString.Replace("BR", "r");

                if (isBlackMoveNext)
                {
                    var firstPart = fenString.ToString().Split(' ')[0];
                    var tempPart = firstPart.Split('/');
                    StringBuilder blackFenStringBuilder = new StringBuilder();
                    for (int index = tempPart.Length - 1; index >= 0; index--)
                    {
                        blackFenStringBuilder.Append(ReverseString(tempPart[index]));
                        if (index != 0)
                            blackFenStringBuilder.Append("/");
                    }
                    fenString = fenString.Replace(firstPart, blackFenStringBuilder.ToString());
                }

                //Console.WriteLine();
                //Console.WriteLine("FEN String is:");
                //Console.WriteLine(fenString.ToString());

                //LogHelper.logger.Info("PrepareFenString finished...");
                return fenString.ToString();
                //totalExecutionTime.Stop();
                //MessageBox.Show("Total time to read: " + totalExecutionTime.ElapsedMilliseconds.ToString());
            }
            catch (Exception exception)
            {
                LogHelper.logger.Error("GetNextBestMove: " + exception.Message);
                LogHelper.logger.Error("GetNextBestMove: " + exception.StackTrace);
                MessageBox.Show("An error occurred. Please restart bot", "Chessbot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        public static string ReverseString(string s)
        {
            //LogHelper.logger.Info("ReverseString called...");
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            //LogHelper.logger.Info("ReverseString finished...");

            return new string(arr);
        }



    }
}

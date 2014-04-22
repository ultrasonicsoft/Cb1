using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenCVDemo1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CaptureChessBoard());
        }
    }
}


//http://www.prodigyproductionsllc.com/articles/programming/template-matching-with-opencv-and-c/
//http://www.emgu.com/wiki/index.php/SURF_feature_detector_in_CSharp
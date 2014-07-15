using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenCVDemo1
{
      [Serializable()]
    public class TemplateConfiguration
    {
          public int Intensity { get; set; }
          public string TemplateName { get; set; }
          public bool UserPlayingWhite { get; set; }
          public Rectangle PreviousScreenCordinates { get; set; }
          public int Paddding { get; set; }
          public int MatchingFactor { get; set; }
          public int EngineDepth { get; set; }
          public int NextMoveHighlightDuration { get; set; }
          public bool EnableHotKey { get; set; }
          public bool EnableLogging { get; set; }
    }
}

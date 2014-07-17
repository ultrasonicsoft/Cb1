using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenCVDemo1
{
      [Serializable()]
    public class EngineConfigurationSettings
    {
        public int ContemptFactorValue { get; set; }

        public int MinSplitDepthValue { get; set; }

        public int ThreadValue { get; set; }

        public int HashValue { get; set; }

        public int MultiPVValue { get; set; }

        public bool Ponder { get; set; }

        public int SkillLevelValue { get; set; }

        public int MoveHorizonValue { get; set; }

        public int BaseTimeValue { get; set; }

        public int MoveTimeValue { get; set; }

        public int ThinkingTimeValue { get; set; }

        public int SlowMoverValue { get; set; }

        public bool UC_960 { get; set; }
        
    }
}

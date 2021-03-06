﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Config;

namespace OpenCVDemo1
{
    public static class LogHelper
    {
        public static readonly ILog logger =
          LogManager.GetLogger(typeof(LogHelper));

        static LogHelper()
        {
            DOMConfigurator.Configure();
            SetLoggingState(false);
        }

        public static void SetLoggingState(bool state)
        {
            if (state == false)
            {
                LogManager.GetLoggerRepository().Threshold = LogManager.GetLoggerRepository().LevelMap["OFF"];
            }
            else
            {
                LogManager.GetLoggerRepository().Threshold = LogManager.GetLoggerRepository().LevelMap["ALL"];
            }
        }

    }
}

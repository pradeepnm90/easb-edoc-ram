using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
        /// <summary>Define level of logger</summary>
        [Browsable(false)]
        public enum LoggerLevel_Delete
        {
            /// <summary>For Debug.</summary>
            Debug = 0,
            /// <summary>For Activity.</summary>
            Activity = 1,
            /// <summary>For Exception.</summary>
            Exception = 2,
            /// <summary>For Warning.</summary>
            Warning = 3
        }
}

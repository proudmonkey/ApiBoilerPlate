using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Helpers
{
    public class HttpClientPolicyConfigs
    {
        public int RetryCount { get; set; }
        public int RetryDelayInMs { get; set; }
        public int RetryTimeoutInSeconds { get; set; }
        public int BreakDurationInSeconds { get; set; }
        public int MaxAttemptBeforeBreak { get; set; }
        public int HandlerTimeoutInMinutes { get; set; }
    }
}

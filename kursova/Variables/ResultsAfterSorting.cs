using System;
using System.Collections.Generic;
using System.Text;

namespace Variables
{
    public class ResultsAfterSorting
    {
        public ulong CompareAmount { get; set; }
        public ulong SwapsAmount { get; set; }
        public double ExecutionTimeMs { get; set; }

        public ResultsAfterSorting(ulong compareAmount, ulong swapsAmount, double executionTimeMs)
        {
            this.CompareAmount = compareAmount;
            this.SwapsAmount = swapsAmount;
            this.ExecutionTimeMs = executionTimeMs;
        }
    }

}
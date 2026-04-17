using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace ArrayRelatedFunctions
{
    public class Visualization
    {
        private List<AlgorithmData> _stats;
        private class AlgorithmData
        {
            public string Name { get; set; }
            public int CompareAmount { get; set; }
            public int SwapsAmount { get; set; }
            public double Time { get; set; }
        }

        public Visualization()
        {
            _stats = new List<AlgorithmData>();
        }

        public void AddAlgorithmStats(string name, int compareAount, int swapsAmount, double time)
        {
            _stats.Add(new AlgorithmData
            {
                Name = name,
                CompareAmount = compareAount,
                SwapsAmount = swapsAmount,
                Time = time
            });
        }

        public void ShowStats()
        {
            Form form = new Form();
            form.Text = "Result";
            form.Size = new Size(800, 600);
            form.StartPosition = FormStartPosition.CenterScreen;

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace ArrayRelatedFunctions
{
    public class AlgorithmData
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public int Size { get; set; }
        public int CompareAmount { get; set; }
        public int SwapsAmount { get; set; }
        public double Time { get; set; }
    }
    public class Visualization
    {
        private List<AlgorithmData> _stats;
        
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

            TabControl tabControl = new TabControl();

            tabControl.TabPages.Add()
        }
    }
}

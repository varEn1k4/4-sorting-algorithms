using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Variables;

namespace ArrayRelatedFunctions
{
    public static class Visualization
    {
        public static Action<float[]> CreateAnimationAction(Canvas sortCanvas)
        {
            return (currentArray) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    sortCanvas.Children.Clear();
                    double width = sortCanvas.ActualWidth / currentArray.Length;

                    float max = currentArray.Length > 0 ? currentArray.Max() : 1;
                    float min = currentArray.Length > 0 ? currentArray.Min() : 0;
                    float maxAbs = Math.Max(Math.Abs(max), Math.Abs(min));

                    if (maxAbs == 0)
                    {
                        maxAbs = 1;
                    }

                    double zeroLine = sortCanvas.ActualHeight / 2;

                    //x
                    Line axis = new Line
                    {
                        X1 = 0,
                        X2 = sortCanvas.ActualWidth,
                        Y1 = zeroLine,
                        Y2 = zeroLine,
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 1
                    };
                    sortCanvas.Children.Add(axis);

                    for (int i = 0; i < currentArray.Length; i++)
                    {
                        float val = currentArray[i];
                        double rectHeight = (Math.Abs(val) / maxAbs) * zeroLine;

                        Rectangle rect = new Rectangle
                        {
                            Width = width > 1 ? width - 0.5 : 1,
                            Height = rectHeight,
                            Fill = val >= 0 ? Brushes.SteelBlue : Brushes.IndianRed
                        };

                        Canvas.SetLeft(rect, i * width);
                        Canvas.SetTop(rect, val >= 0 ? zeroLine - rectHeight : zeroLine);

                        sortCanvas.Children.Add(rect);
                    }
                }, System.Windows.Threading.DispatcherPriority.Background);

                System.Threading.Thread.Sleep(10);
            };
        } 
    }

}
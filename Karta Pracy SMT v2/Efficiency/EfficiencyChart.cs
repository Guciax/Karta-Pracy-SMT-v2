using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT_v2
{
    public class EfficiencyChart
    {
        private static List<EffPointStruct> chartPoints = new List<EffPointStruct>();
        private static DateTime lastEfficiencyDateOnChart = DateTime.Now;
        static float numberOfPoints = 16;
        static float margin = 5;
        static float yScale = 0;
        static float xInterval = 0;
        static float maxY = 0;

        private class EffPointStruct
        {
            public DateTime date { get; set; }
            public  float value { get; set; }
        }

        public static void AddPoint( PictureBox pb)
        {
            var eff= Efficiency.CurrentShiftEfficiency.CalculateCurrentShiftEfficiency();
            if ((DateTime.Now.Minute == 0 || DateTime.Now.Minute == 30) & (DateTime.Now - lastEfficiencyDateOnChart).TotalMinutes > 20) 
            {
                chartPoints.Add(new EffPointStruct { date = DateTime.Now, value = eff });
                DrawChart(pb);
                lastEfficiencyDateOnChart = DateTime.Now;
            }
        }

        public static void DrawChart(PictureBox pb)
        {
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);

            maxY = Math.Max(chartPoints.Select(p => p.value).Max(), 100);
            yScale = (float)pb.Height / 110;
            xInterval = (pb.Width - margin) / numberOfPoints;

            var currentShiftInfo = MST.MES.DateTools.whatDayShiftIsit(DateTime.Now);

            int maxYInt = (int)Math.Round(pb.Height - maxY * yScale, 0);
            LinearGradientBrush linearGradientBrushThisShift = new LinearGradientBrush(new Rectangle(10, maxYInt, 10,pb.Height-maxYInt-20), Color.FromArgb(150, 26, 188, 156), Color.FromArgb(255, 44, 62, 80), LinearGradientMode.Vertical);
            LinearGradientBrush linearGradientBrushPreviousShift = new LinearGradientBrush(new PointF(10, pb.Height - maxY * yScale), new PointF(10, pb.Height - 20), Color.FromArgb(150, 207, 216, 220), Color.FromArgb(0, 207, 216, 220));

            Pen penEfficiencyThisShift = new Pen(Color.FromArgb(255, 26, 188, 156), 2);
            Pen penEfficiencyPreviousShift = new Pen(Color.FromArgb(255, 207, 216, 220), 2);
            Pen penNorm = new Pen(Brushes.Gray, 1);

            GraphicsPath prevShiftPath = new GraphicsPath();
            GraphicsPath thisShiftPath = new GraphicsPath();

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(255, 44, 62, 80));
                var prevShiftPoints = chartPoints.Where(p => MST.MES.DateTools.whatDayShiftIsit(p.date).fixedDate != currentShiftInfo.fixedDate)
                                                 .OrderBy(p => p.date)
                                                 .ToList();
                var currentShiftPoints = chartPoints.Where(p => MST.MES.DateTools.whatDayShiftIsit(p.date).fixedDate == currentShiftInfo.fixedDate)
                                                 .OrderBy(p => p.date)
                                                 .ToList();
                prevShiftPoints.Add(currentShiftPoints.First());

                float xLocation = numberOfPoints - chartPoints.Count;
                DrawPoints(prevShiftPoints,ref xLocation, g, pb, penEfficiencyPreviousShift, prevShiftPath, linearGradientBrushPreviousShift);

                

                DrawPoints(currentShiftPoints, ref xLocation, g, pb, penEfficiencyThisShift, thisShiftPath, linearGradientBrushThisShift);
                g.DrawLine(penNorm, 10, pb.Height - 100 * yScale, pb.Width, pb.Height - 100 * yScale);
                g.DrawString("100%", Form1.DefaultFont, Brushes.Gray, pb.Width - 40, pb.Height - 107 * yScale);
            }

            pb.Image = bmp;
        }

        internal static void ClearPoints()
        {
            chartPoints.Clear();
        }

        private static void DrawPoints(List<EffPointStruct> pointsList, ref float xLocation, Graphics g, PictureBox pb, Pen penEffLine, GraphicsPath path, LinearGradientBrush gradientBrush)
        {
            if (pointsList.Count == 0) return;
            EffPointStruct prevPoint = null;
            path.AddLine(xLocation * xInterval, pb.Height - 20, xLocation * xInterval, pb.Height - pointsList.First().value * yScale);
            foreach (var point in pointsList)
            {
                if (prevPoint == null)
                {
                    prevPoint = point;
                    xLocation++;
                    continue;
                }

                float startX = (xLocation - 1) * xInterval;
                float startY = pb.Height - prevPoint.value * yScale;
                float endX = xLocation * xInterval;
                float endY = pb.Height - point.value * yScale;

                g.DrawLine(penEffLine, startX, startY, endX, endY);
                g.DrawString(point.date.ToString("HH:mm"), Form1.DefaultFont, Brushes.Gray, xLocation * xInterval - 20, pb.Height - 18);

                path.AddLine(startX, startY, endX, endY);

                prevPoint = point; ;
                xLocation++;
            }
            xLocation--;
            PointF pt1 = new PointF(xLocation * xInterval, pb.Height - prevPoint.value * yScale);
            PointF pt2 = new PointF(xLocation * xInterval, pb.Height - 20);

            path.AddLine(pt1, pt2);
            //path.AddPolygon(new PointF[] { new PointF(xLocation * xInterval, pb.Height - 20) });

            path.CloseFigure();
            g.FillPath(gradientBrush, path);
        }
    }
}

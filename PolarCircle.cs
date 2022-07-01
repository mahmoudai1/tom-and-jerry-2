using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_Assignments
{
    public class PolarCircle
    {
        public float xc, yc, xcOriginal, ycOriginal, sAngle, eAngle;
        public int r;

        public PolarCircle(int xcent, int ycent, int radius, int startAngle, int endAngle)
        {
            xc = xcent;
            yc = ycent;
            xcOriginal = xcent;
            ycOriginal = ycent;
            r = radius;
            sAngle = startAngle;
            eAngle = endAngle;
        }

        public void drawCircle(Graphics g, float gap, Brush b)
        {
            float angle = this.sAngle;
            while (angle < this.eAngle) // smaller value to draw arcs
            {
                float thRadian = (float)(angle * Math.PI / 180);
                float x = (float)(r * Math.Cos(thRadian)) + xc;
                float y = (float)(r * Math.Sin(thRadian)) + yc;
                g.FillEllipse(b, x, y, 5, 5);
                angle += gap;    // for dashed circle
            }
            //g.FillEllipse(Brushes.Red, xc, yc, 5, 5);     // To show the mid-point
        }

        public void drawCustomCircle(Graphics g, float gap, Brush b, int tomX, int tomY)
        {
            float angle = this.sAngle;
            int i = 0;

            while (angle < this.eAngle) // smaller value to draw arcs
            {
                float thRadian = (float)(angle * Math.PI / 180);
                float x = (float)(r * Math.Cos(thRadian)) + xc;
                float y = (float)(r * Math.Sin(thRadian)) + yc;
                //g.FillEllipse(b, x, y, 5, 5);
                if(i == 0 || (angle + gap) >= this.eAngle)
                    g.DrawLine(Pens.Yellow, tomX + 175, tomY + 50, x, y);
                else
                    g.DrawLine(Pens.Red, tomX + 175, tomY + 50, x, y);

                angle += gap;    // for dashed circle
                i++;
            }
            g.FillEllipse(Brushes.Red, xc, yc, 5, 5);     // To show the mid-point
            //g.DrawLine(Pens.Yellow, tomX + 175, tomY + 50, x, y);
        }

        public PointF getNextPoint(float angle)
        {
            float thRadian = (float)(angle * Math.PI / 180);
            float x = (float)(r * Math.Cos(thRadian)) + xc;
            float y = (float)(r * Math.Sin(thRadian)) + yc;
            return new PointF(x, y);
        }
    }
}

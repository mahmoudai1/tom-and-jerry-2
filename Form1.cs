using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_Assignments
{
    public partial class Form1 : Form
    {
        Bitmap off;
        Timer t = new Timer();

        Bitmap jerryImg = new Bitmap("jerry.png");
        Bitmap tomImg = new Bitmap("tom.png");

        PolarCircle circle;
        PolarCircle catchedCircle = new PolarCircle(0, 0, 200, 0, 360);

        int jerryX = 800, jerryY = 200, tomX = 200, tomY = 200;
        int angleDir = 1, ctTimer = 0;
        bool catched = false;

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
            t.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                circle.eAngle += 10;
                circle.sAngle -= 10;
            }

            if(e.KeyCode == Keys.Up)
            {
                jerryY -= 10;
            }
            else if(e.KeyCode == Keys.Down)
            {
                jerryY += 10;
            }
            else if(e.KeyCode == Keys.Left)
            {
                jerryX -= 10;
            }
            else if(e.KeyCode == Keys.Right)
            {
                jerryX += 10;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.Width, this.Height);
            circle = new PolarCircle(tomX + 175, tomY + 50, 175, 0, 75);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawDouble(e.Graphics);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                tomX = e.X - 100;
                tomY = e.Y - 100;
                circle.xc = tomX + 175;//
                circle.yc = tomY + 50;//
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (circle.sAngle > 25) angleDir = -1;
            else if (circle.sAngle < -90) angleDir = 1;

            circle.sAngle += angleDir * 8;
            circle.eAngle += angleDir * 8;

            if (((jerryX - circle.xc) * (jerryX - circle.xc)) + ((jerryY - circle.yc) * (jerryY - circle.yc)) - (circle.r * circle.r) < 0)
            {
                if (jerryX > circle.xc && jerryX < circle.xc + circle.r
                    && jerryY > circle.getNextPoint(circle.sAngle).Y && jerryY < circle.getNextPoint(circle.eAngle).Y)
                {
                    catchedCircle.xc = circle.xc;
                    catchedCircle.yc = circle.yc;
                    catched = true;
                }
                else
                {
                    catched = false;
                }
            }
            else
            {
                catched = false;
            }

        
            ctTimer++;
            drawDouble(this.CreateGraphics());
        }

        void drawScene(Graphics g)
        {
            g.Clear(Color.Black);
            g.DrawImage(jerryImg, jerryX, jerryY, 120, 120);
            g.DrawImage(tomImg, tomX, tomY, 200, 200);
            circle.drawCustomCircle(g, 2, Brushes.Red, tomX, tomY);
            if (catched)
                catchedCircle.drawCircle(g, 1, Brushes.Yellow);
        }

        void drawDouble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}

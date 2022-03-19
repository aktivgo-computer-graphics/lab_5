using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Windows.Forms;

namespace task
{
    public partial class MainForm : Form
    {
        private readonly Graphics Graph;
        private readonly Pen MyPenBlack;
        private readonly Pen MyPenRed;
        private readonly Pen MyPen;

        private const int scale = 1;
        private int countAngle, countForStitch, countStitch, countBorder;
        private float length;
        private float x0, y0, step, xMax, yMax;
        private float angle, angleBetween, angleStep, angleBase, alpha, beta;

        private bool isVert;
        private float angleMer;
        private float m;

        public MainForm()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Graph.SmoothingMode = SmoothingMode.HighQuality;
            MyPenBlack = new Pen(Color.Black, 2);
            MyPenRed = new Pen(Color.Red, 2);
            MyPen = new Pen(Color.Black, 2);
        }
        
        private void paintButton_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);

            switch (taskSwitcher.SelectedIndex)
            {
                case 0:
                    x0 = ClientSize.Width / 2;
                    y0 = ClientSize.Height / 2;
                    xMax = ClientSize.Width;
                    yMax = ClientSize.Height - 70;
                    step = 10;
                    angle = 45;
                    angleStep = 90;
                    length = 10;
                    Otrezok(length, true);
                    break;
                case 1:
                    x0 = ClientSize.Width / 2;
                    y0 = ClientSize.Height / 2;
                    xMax = ClientSize.Width;
                    yMax = ClientSize.Height - 70;
                    step = 10;
                    angle = 0;
                    angleStep = 90;
                    length = 10;
                    Otrezok(length);
                    break;
                case 2:
                    x0 = ClientSize.Width / 2;
                    y0 = ClientSize.Height / 2;
                    xMax = ClientSize.Width;
                    yMax = ClientSize.Height - 70;
                    step = 0;
                    angle = 0;
                    angleStep = 160;
                    length = 300;
                    countAngle = 4;
                    Otrezok(length, true, 3 * countAngle);
                    break;
                case 3:
                    x0 = 200;
                    y0 = 170;
                    step = 6;
                    angle = 45;
                    alpha = (float)2.2;
                    beta = (float)0.2;
                    countStitch = 500;
                    countBorder = 3;
                    countForStitch = 0;
                    angleBase = 0;
                    Angle_2();
                    break;
                case 4:
                    x0 = ClientSize.Width / 2;
                    y0 = ClientSize.Height / 10;
                    step = 10;
                    angle = 0;
                    alpha = (float)2.3;
                    beta = (float)4.0;
                    countStitch = 500;
                    countForStitch = 0;
                    Angle_1();
                    break;
                case 5:
                    x0 = 1000;
                    y0 = 600;
                    angle = (float)(-Math.PI / 2);
                    angleMer = (float)(Math.PI / 4);
                    m = (float)0.8;
                    PaintTree();
                    break;
                case 6:
                    x0 = 700;
                    y0 = 600;
                    angle = (float)(-Math.PI / 2);
                    isVert = false;
                    PaintHilbertLabyrinths();
                    break;
                case 7:
                    x0 = 700;
                    y0 = 100;
                    angle = (float)(Math.PI / 4);
                    isVert = false;
                    PaintHilbertLabyrinths();
                    break;
            }
        }
        
        private static PointF ScreenCoords(PointF point)
        {
            return new PointF(point.X * scale, point.Y * scale);
        }

        private void Otrezok(float lenght)
        {
            lenght += step;
            angle += angleStep;
            StepOtrezok(lenght);

            MyPen.Color = Color.Black;
            if (x0 > 0 && x0 < xMax && y0 > 0 && y0 < yMax)
            {
                Otrezok(lenght);
            }
        }

        private void Step(float lenght)
        {
            var pointStart = new PointF(x0, y0);

            x0 += (float)(lenght * Math.Cos(angle));
            y0 += (float)(lenght * Math.Sin(angle));
            var pointEnd = new PointF(x0, y0);
            Graph.DrawLine(MyPen, ScreenCoords(pointStart), ScreenCoords(pointEnd));
        }

        private void Otrezok(float lenght, bool flag)
        {
            angle += angleStep;
            if (flag)
            {
                MyPen.Color = Color.Red;
                lenght += 8 * step;
            }
            else
            {
                MyPen.Color = Color.Black;
                lenght -= 6 * step;
            }
            
            StepOtrezok(lenght);

            if (x0 > 0 && x0 < xMax && y0 > 0 && y0 < yMax)
            {
                Otrezok(lenght, !flag);
            }
        }
        
        private void StepOtrezok(float lenght)
        {
            var pointStart = new PointF(x0, y0);

            x0 += (float)(lenght * Math.Cos(angle * Math.PI / 180));
            y0 += (float)(lenght * Math.Sin(angle * Math.PI / 180));
            var pointEnd = new PointF(x0, y0);
            Graph.DrawLine(MyPen, ScreenCoords(pointStart), ScreenCoords(pointEnd));
        }

        private void Otrezok(float lenght, bool flag, int count)
        {
            if (count == 0)
            {
                return;
            }
            if (flag)
            {
                MyPen.Color = Color.Red;
                lenght += step;
            }
            else
            {
                MyPen.Color = Color.Black;
                lenght += 2 * step;
            }
            angle += angleStep;
            StepOtrezok(lenght);

            Otrezok(lenght, !flag, --count);
        }

        private void Angle_1()
        {
            countForStitch++;
            angleBase = (float)(2 * Math.PI * countForStitch / countStitch);
            angle = (float)(angleBase + alpha * Math.Sin(beta * angleBase));
            Step(step);
            if (countForStitch <= countStitch)
            {
                Angle_1();
            }
        }

        private void Angle_2()
        {
            countForStitch++;
            angle = (float)(angleBase + alpha * Math.Sin(beta * countForStitch));
            Step(step);
            if (countForStitch <= countStitch)
            {
                Angle_2();
            }
            else if (countBorder != 0)
            {
                countForStitch = 0;
                angleBase += (float)(Math.PI / 2);
                countBorder--;
                Angle_2();
            }
        }

        private void PaintHilbertLabyrinths()
        {
            const float length = 14;
            const int rec = 5;
            const int i = 1;

            PaintLab(length, rec, i);
        }

        private void PaintLab(float length, int rec, int i)
        {
            if (rec == 0) return;

            isVert = !isVert;
            angle += (float)(i * Math.PI / 2);
            PaintLab(length, rec - 1, -i);
            PaintStep(angle, length, isVert);
            
            isVert = !isVert;
            angle -= (float)(i * Math.PI / 2);
            PaintLab(length, rec - 1, i);
            PaintStep(angle, length, isVert);
            
            PaintLab(length, rec - 1, i);
            
            isVert = !isVert;
            angle -= (float)(i * Math.PI / 2);
            PaintStep(angle, length, isVert);
            PaintLab(length, rec - 1, -i);
            
            isVert = !isVert;
            angle += (float)(i * Math.PI / 2);
        }

        private void PaintStep(float angle, float length, bool isVert)
        {
            var x0 = this.x0;
            var y0 = this.y0;
            
            this.x0 += (float)(length * Math.Cos(angle));
            this.y0 += (float)(length * Math.Sin(angle));

            Graph.DrawLine(isVert ? MyPenRed : MyPenBlack, x0, y0, this.x0, this.y0);
        }

        private void PaintTree()
        {
            PaintBranch(90);
        }

        private void PaintBranch(float length)
        {
            if (length < 30 || length > 100) return;
            
            Step(length);
            
            angle -= angleMer / 2;
            PaintBranch(length * m);
            
            angle += (float)(angleMer + Math.PI / 8);
            PaintBranch(length * m);
            
            angle -= angleMer / 2;
            PaintBranch(length * m);
            
            angle -= (float)(Math.PI / 8);

            x0 -= (float)(length * Math.Cos(angle));
            y0 -= (float)(length * Math.Sin(angle));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Graph.Dispose();
            MyPenRed.Dispose();
        }
    }
}
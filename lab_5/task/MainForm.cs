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

        private float x0;
        private float y0;
        private float angle;
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
        }
        
        private void paintButton_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            
            angle = (float)(-Math.PI / 2);
            angleMer = (float)(Math.PI / 4);
            m = (float)0.8;
            isVert = false;

            switch (taskSwitcher.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    x0 = 1000;
                    y0 = 600;
                    PaintTree();
                    break;
                case 6:
                    x0 = 700;
                    y0 = 600;
                    PaintHilbertLabyrinths();
                    break;
                case 7:
                    x0 = 700;
                    y0 = 600;
                    angle = (float)(Math.PI / 4);
                    PaintHilbertLabyrinths45();
                    break;
            }
        }

        private void PaintHilbertLabyrinths()
        {
            const float length = 14;
            const int rec = 5;
            const int i = 1;

            PaintLab(length, rec, i);
        }
        
        private void PaintHilbertLabyrinths45()
        {
            const float length = 14;
            const int rec = 4;
            const int i = 1;

            PaintLab45(length, rec, i);
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
        
        private void PaintLab45(float length, int rec, int i)
        {
            if (rec == 0) return;

            angle += (float)(i * Math.PI / 4);
            PaintLab45(length, rec - 1, -i);
            PaintStep(angle, length, isVert);

            angle += (float)(i * Math.PI / 4);
            PaintLab45(length, rec - 1, i);
            PaintStep(angle, length, isVert);
            
            angle -= (float)(i * Math.PI / 4);
            PaintLab45(length, rec - 1, -i);
            PaintStep(angle, length, isVert);

            angle -= (float)(i * Math.PI / 4);
            PaintStep(angle, length, isVert);
            PaintLab45(length, rec - 1, -i);
            
            angle += (float)(i * Math.PI / 4);
            PaintLab45(length, rec - 1, i);
            PaintStep(angle, length, isVert);

            angle += (float)(i * Math.PI / 4);
            PaintStep(angle, length, isVert);
            PaintLab45(length, rec - 1, i);
            
            angle -= (float)(i * Math.PI / 4);
            PaintLab45(length, rec - 1, -i);
            PaintStep(angle, length, isVert);

            angle -= (float)(i * Math.PI / 4);
            PaintStep(angle, length, isVert);
            PaintLab45(length, rec - 1, i);

            angle += (float)(i * Math.PI / 4);
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
            
            var x0 = this.x0;
            var y0 = this.y0;
            
            this.x0 += (float)(length * Math.Cos(angle));
            this.y0 += (float)(length * Math.Sin(angle));
            
            Graph.DrawLine(MyPenBlack, x0, y0, this.x0, this.y0);
            
            angle -= angleMer / 2;
            PaintBranch(length * m);
            
            angle += angleMer;
            PaintBranch(length * m);
            
            angle -= angleMer / 2;
            PaintBranch(length * m);
            
            this.x0 -= (float)(length * Math.Cos(angle));
            this.y0 -= (float)(length * Math.Sin(angle));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Graph.Dispose();
            MyPenRed.Dispose();
        }
    }
}
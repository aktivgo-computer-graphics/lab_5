using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Windows.Forms;

namespace task
{
    public partial class MainForm : Form
    {
        private Graphics Graph;
        private Pen MyPen;

        private float x0;
        private float y0;
        private float angle = (float) -Math.PI / 2;
        private float angleMer = (float) Math.PI / 4;
        private float m = (float) 0.8;

        public MainForm()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Graph.SmoothingMode = SmoothingMode.HighQuality;
            MyPen = new Pen(Color.Red);
        }
        
        private void paintButton_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            x0 = 300;
            y0 = 450;
            //PaintHilbertLabyrinths();
            PaintTree();
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

            angle += (float)(i * Math.PI / 2);
            PaintLab(length, rec - 1, -i);
            PaintStep(angle, length);
            
            angle -= (float)(i * Math.PI / 2);
            PaintLab(length, rec - 1, i);
            PaintStep(angle, length);
            
            PaintLab(length, rec - 1, i);
            
            angle -= (float)(i * Math.PI / 2);
            PaintStep(angle, length);
            PaintLab(length, rec - 1, -i);
            
            angle += (float)(i * Math.PI / 2);
        }

        private void PaintStep(float angle, float length)
        {
            var x0 = this.x0;
            var y0 = this.y0;
            
            this.x0 += (float)(length * Math.Cos(angle));
            this.y0 += (float)(length * Math.Sin(angle));
            
            Graph.DrawLine(MyPen, x0, y0, this.x0, this.y0);
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
            
            Graph.DrawLine(MyPen, x0, y0, this.x0, this.y0);
            
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
            MyPen.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace TestDuration
{
    public partial class Form1 : Form
    {
        int baseX = 0, baseY = 0, picW, picH;
        int value = 0;
        System.Windows.Forms.Timer timerSec = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();

            picW = pictureBox1.Size.Width;
            picH = pictureBox1.Size.Height;
            timerSec.Interval = 10;
            timerSec.Tick += new EventHandler(timerSec_Tick);
            timerSec.Start();
        }

        void timerSec_Tick(object sender, EventArgs e)
        {
            this.pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;
            DrawAnalogClock(grfx);
        }

        private void DrawAnalogClock(Graphics g)
        {
            int center = baseX + picH / 2;
            //int centerY = baseY + picW / 2;

            double valueAngle = 2 * Math.PI * (value+45/*Center Angle*/) / 100;

            
            //double radian = valueAngle * (Math.PI / 180);

            g.DrawLine(new Pen(Brushes.Red, 7), center, center,
                center + (int)(100 * Math.Cos(valueAngle)),
                center + (int)(100 * Math.Sin(valueAngle)));


            Image img = Image.FromFile("Gage_Pin.png");

            
            Matrix _mat = new Matrix();
            _mat.Rotate(value);

            g.Transform = _mat;
            g.DrawImage(img, center,center);



            label1.Text = "VAL:" + value + "    Angle:" + valueAngle + "   Rad:";// +radian;
            label2.Text = " SIN:" + ((int)(100 * Math.Sin(valueAngle))) + "    COS:" + ((int)(100 * Math.Cos(valueAngle)));
            label2.Text += " X:" + (center + (int)(100 * Math.Sin(valueAngle))) + " Y:" + (center + (int)(100 * Math.Cos(valueAngle)));
        }


        private void PlusThread()
        {
            for (int i = 0; i <= 60; i++)
            {
                Thread.Sleep(20);
                value = i;
            }
        }
        private void MinusThread()
        {
            for (int i = 60; i >= 0; i--)
            {
                Thread.Sleep(20);
                value = i;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread a = new Thread(new ThreadStart(PlusThread));
            a.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread a = new Thread(new ThreadStart(MinusThread));
            a.Start();
        }
    }
}

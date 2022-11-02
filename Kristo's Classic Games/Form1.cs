using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kristo_s_Classic_Games
{
    public partial class Form1 : Form
    {
        //data
        bool fadeinorout = false;

        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        void MakeRoundedCorners(Control c, int x)
        {
            c.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, c.Width, c.Height, x, x));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MakeRoundedCorners(this,20);
            MakeRoundedCorners(label2, 10);
            MakeRoundedCorners(label1, 10);
            MakeRoundedCorners(label3, 10);
            MakeRoundedCorners(panel2, 20);
            MakeRoundedCorners(panel3, 20);
            MakeRoundedCorners(panel4, 20);
            MakeRoundedCorners(pictureBox1, 20);
            fadeIn.Start();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Gray;
        }

        private void fadeIn_Tick(object sender, EventArgs e)
        {
            switch(fadeinorout)
            {
                case false:
                    if(this.Opacity != 1)
                    {
                        this.Opacity += 0.04;
                    }
                    else if(this.Opacity == 1)
                    {
                        fadeIn.Stop();
                    }
                    break;
                case true:
                    if (this.Opacity != 0)
                    {
                        this.Opacity -= 0.04;
                    }
                    else if (this.Opacity == 0)
                    {
                        this.Close();
                        Application.Exit();
                    }
                    break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            fadeIn.Stop();
            fadeinorout = true;
            fadeIn.Start();
        }
    }
}

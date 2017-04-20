using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{ // стрелка вверх - вращение вправо, z - вращение влево, стелка вправо - движение вправо, стрелка влево - движение влево
  //  стрелка вниз - опустить медленно, пробел - опустить быстро
    public partial class Form1 : Form
    {
        enum Direction
        {
            UP,
            Z,
            RIGHT,
            LEFT,
            DOWN,
            SPACE,
            NONE
        };

        Direction dir;
        Random r;
        Bitmap bmp;
        Graphics g;
        SolidBrush brushG, brushR, brushLB, brushY, brushV, brushDY, brushB;
        Figure1 f1, f2, f3, f4, f5, f6, f7;

        public int x = 0, y = 0;
        public Form1()
        {
            InitializeComponent();

            dir = Direction.NONE;

            /*f1 = new Figure1(110, 100);
            f2 = new Figure1(120, 200);
            f3 = new Figure1(150, 50);
            f4 = new Figure1(170, 270);
            f5 = new Figure1(340, 320);
            f6 = new Figure1(190, 340);
            f7 = new Figure1(200, 460);*/
            f1 = new Figure1(x, y);
            f2 = new Figure1(x, y);
            f3 = new Figure1(x, y);
            f4 = new Figure1(x, y);
            f5 = new Figure1(x, y);
            f6 = new Figure1(x, y);
            f7 = new Figure1(x, y);

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = new Bitmap(@"C:\Users\Aida\Desktop\fon.jpg");
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;

            brushG = new SolidBrush(Color.Green);
            brushR = new SolidBrush(Color.Red);
            brushLB = new SolidBrush(Color.DeepSkyBlue);
            brushY = new SolidBrush(Color.Yellow);
            brushV = new SolidBrush(Color.Violet);
            brushDY = new SolidBrush(Color.DarkOrange);
            brushB = new SolidBrush(Color.Blue);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g.FillPath(brushG, f1.path1); // s
            g.FillPath(brushR, f2.path2); // z
            g.FillPath(brushLB, f3.path3); // i
            g.FillPath(brushY, f4.path4); // o
            g.FillPath(brushV, f5.path5); // T
            g.FillPath(brushDY, f6.path6); // L
            g.FillPath(brushB, f7.path7); // J
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            r = new Random((pictureBox1.Width));
            
           
        }
    }
}

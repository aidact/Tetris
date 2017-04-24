using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris24._04._2017
{
    public partial class Form1 : Form
    {
        const int MAXX = 10; // panel's width
        const int MAXY = 15; // panel's height
        const int ps = 24; 
        bool started = false; // condition of starting
        // colours for figures
        Brush[] bru = {Brushes.White, Brushes.Orange, Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Honeydew,
            Brushes.Violet, Brushes.Tomato, Brushes.SteelBlue, Brushes.PapayaWhip};

        int score;
        bool check = true;
        int[,] ar; // matrix of board
        Figure figure;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void upd_score()
        {
            label1.Text = "Score : " + score.ToString();
        }

        public class Figure
        {
            /*
             * ....
             * 1111
             * ....
             * ....
             * 
             * ....
             * 111.
             * 1...
             * ....
             * 
             * ....
             * .11.
             * .11.
             * ....
             * 
             * .1..
             * .11.
             * ..1.
             * ....
             * */
            string[] nya = {"....1111........", // My cute figures
                            "....111.1.......",
                            ".....11..11.....", 
                            ".1...11...1.....",
                            "....111..1......", 
                            "....111...1.....", 
                            "..1..11..1......"};
            public int x, y; 
            public int clr;
            public int[,] pix; // pixels of board
            public Random rnd;

            public Figure() // smthng like constructor : generates existing game from nya
            {
                x = 3; y = -2;
                rnd = new Random();
                clr = 1 + rnd.Next(9);
                int n = rnd.Next(nya.Length);
                fillpix(out pix, nya[n]);
                n = rnd.Next(4);
                for (int i = 0; i < n; ++i)
                    rotate();
            }

            void fillpix(out int[,] pix, string cc)
            {
                pix = new int[4, 4];
                int i, j;
                for (i = 0; i < 4; ++i)
                    for (j = 0; j < 4; ++j)
                    {
                        if (cc[j * 4 + i] == '1') pix[i, j] = clr; // если не пустой пиксел то заполнить цветом
                        else pix[i, j] = 0; // пустая ячейка
                    }
            }
            public void rotate() // condition of rotation
            {
                int[,] p2 = new int[4, 4]; // 4 to 4 matrix for each figure
                int i, j;
                for (i = 0; i < 4; ++i)
                    for (j = 0; j < 4; ++j)
                    {
                        p2[3 - j, i] = pix[i, j];
                    }
                pix = p2;
            }
        }

        private void draw(int x, int y, int clr)
        {
           
           
                if (clr == 0 || x < 0 || x >= MAXX || y < 0 || y >= MAXY) return;
                g.FillEllipse(bru[clr], new Rectangle(2 + x * ps, 2 + y * ps, ps, ps));
            
        }

        private void draw_field() // drawing the field
        {
            int i, j;
            for (i = 0; i < MAXX; ++i)
                for (j = 0; j < MAXY; ++j)
                    draw(i, j, ar[i, j]);
        }
         
        private void draw_figure(Figure f)
        {
            int i, j;
            for (i = 0; i < 4; ++i)
                for (j = 0; j < 4; ++j)
                    draw(f.x + i, f.y + j, f.pix[i, j]);
        }

        private void button1_Click(object sender, EventArgs e) // start button
        {
            started = true;
            button1.Enabled = false;
            score = 0;
            ar = new int[MAXX, MAXY];
            upd_score(); 
            figure = null; // no figures from the start
            timer1.Enabled = true;
        }

        private int get_ar(int x, int y)
        {
            if (x < 0 || x >= MAXX || y >= MAXY) return 1; // если фигура переходит границы
            if (y < 0) return 0; // если фигуры пока нет
            return ar[x, y];
        }

        private void set_ar(int x, int y, int clr)
        {
            if (x < 0 || x >= MAXX || y < 0 || y >= MAXY) return;
            ar[x, y] = clr;
        }

        private bool figure_ok() // Can we put the figure
        {
            int i, j;
            for (i = 0; i < 4; ++i)
                for (j = 0; j < 4; ++j)
                {
                    if (figure.pix[i, j] != 0 && get_ar(figure.x + i, figure.y + j) != 0)
                        return false;
                }
            return true;
        }

        private void apply_figure() // Adds score if there any, sets current figure to the field
        {
            int i, j;
            for (i = 0; i < 4; ++i)
                for (j = 0; j < 4; ++j)
                    if (figure.pix[i, j] != 0)
                        set_ar(figure.x + i, figure.y + j, figure.pix[i, j]);
            bool[] lines = new bool[MAXY]; // lines[i] = true if i-th line is filled fully
            int numberOfFilledLines = 0; // number of filled lines
            for (i = 0; i < MAXY; ++i)
            {
                bool isThisRowFilled = true;
                for (j = 0; j < MAXX; ++j)
                {
                    if (ar[j, i] == 0)
                    {
                        isThisRowFilled = false;
                        break;
                    }
                }
                lines[i] = isThisRowFilled;
                if (isThisRowFilled)
                {
                    ++numberOfFilledLines;
                }
            }
            if (numberOfFilledLines != 0)
            {
                // Updating score
                int sc = (numberOfFilledLines * (numberOfFilledLines + 1) / 2) * 100;
                score ++;
                upd_score();
                //simpleSound.Play();
            }
            /*Updating our game field**/
            int[,] nar = new int[MAXX, MAXY]; // nar is new ar
            int ty = MAXY; // pointer to the row on top of new ar
            for (i = MAXY - 1; i >= 0; --i)
            {
                if (false == lines[i])
                {
                    --ty;
                    for (j = 0; j < MAXX; ++j)
                    {
                        nar[j, ty] = ar[j, i];
                    }
                }
            }
            ar = nar;
        }

        private void figure_down()
        {
            ++figure.y; // спукаться по у
            if (!figure_ok())
            {
                --figure.y;
                apply_figure();
                figure = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (figure == null)
            {
                figure = new Figure();
                if (!figure_ok())
                {
                    timer1.Enabled = false;
                    /*if (score == 1)
                    {
                        MessageBox.Show("YOU WIN!");
                    }*/
                    MessageBox.Show("GAME OVER!");
                    button1.Enabled = true;
                }
            }
            //if(check == true) 
                figure_down();
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!started) return;
            g = e.Graphics;
            g.DrawRectangle(Pens.LightBlue, new Rectangle(0, 0, ps * MAXX + 4, ps * MAXY + 4));
            g.FillRectangle(Brushes.Black, new Rectangle(1, 1, ps * MAXX + 3, ps * MAXY + 3));
            draw_field();
            if (figure != null) draw_figure(figure);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (figure == null) return;
            if (e.KeyCode == Keys.A)
            {
                figure.x -= 1;
                if (!figure_ok()) figure.x += 1;
            }
            else if (e.KeyCode == Keys.D)
            {
                figure.x += 1;
                if (!figure_ok()) figure.x -= 1;
            }
            else if (e.KeyCode == Keys.S)
            {
                figure.y += 1;
                if (!figure_ok()) figure.y -= 1;
            }
            else if (e.KeyCode == Keys.W)
            {
                figure.rotate();
                if (!figure_ok())
                {
                    for (int i = 0; i < 3; ++i) figure.rotate();
                }
            }
            panel1.Refresh();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
        //  if(check == true) 
              Form1_KeyDown(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // panel1.enableD/`B;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       /* private void button2_Click(object sender, EventArgs e)
        {
            check = false;
         // panel1.Image.Save("game");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            check = true;
        }*/

        
    }
}

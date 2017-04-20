using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris
{
    class Figure1
    {
        public GraphicsPath path1 = new GraphicsPath();
        public GraphicsPath path2 = new GraphicsPath();
        public GraphicsPath path3 = new GraphicsPath();
        public GraphicsPath path4 = new GraphicsPath();
        public GraphicsPath path5 = new GraphicsPath();
        public GraphicsPath path6 = new GraphicsPath();
        public GraphicsPath path7 = new GraphicsPath();
        public Figure1(int x, int y)
        {
            Point[] points1 = // s, light green
            {
                new Point (x, y),
                new Point (x+28, y),
                new Point (x+28, y+14),
                new Point (x+14, y+14),
                new Point (x+14, y+28),
                new Point (x-14, y+28),
                new Point (x-14, y+14),
                new Point (x, y+14)
            };
            Point[] points2 = // z, red
            {
                new Point (x, y),
                new Point (x+28, y),
                new Point (x+28, y+14),
                new Point (x+46, y+14),
                new Point (x+46, y+28),
                new Point (x+14, y+28),
                new Point (x+14, y+14),
                new Point (x, y+14)
            };
            Point[] points3 = // i,light blue
            {
                new Point (x, y),
                new Point (x+14, y),
                new Point (x+14, y+56),
                new Point (x, y+56)
            };
            Point[] points4 = // o, yellow
            {
                new Point (x, y),
                new Point (x+14, y),
                new Point (x+14, y+14),
                new Point (x, y+14)
            };
            Point[] points5 = // T, violet
            {
                new Point (x, y),
                new Point (x+14, y),
                new Point (x+14, y+14),
                new Point (x+28, y+14),
                new Point (x+28, y+28),
                new Point (x-14, y+28),
                new Point (x-14, y+14),
                new Point (x, y+14)
            };
            Point[] points6 = // L, dark yellow
            {
                new Point (x, y),
                new Point (x+14, y),
                new Point (x+14, y+28),
                new Point (x+28, y+28),
                new Point (x+28, y+42),
                new Point (x, y+42)
            };
            Point[] points7 = // J, blue
            {
                new Point (x, y),
                new Point (x+14, y),
                new Point (x+14, y+42),
                new Point (x-14, y+42),
                new Point (x-14, y+28),
                new Point (x, y+28)
            };

            path1.AddPolygon(points1);
            path2.AddPolygon(points2);
            path3.AddPolygon(points3);
            path4.AddPolygon(points4);
            path5.AddPolygon(points5);
            path6.AddPolygon(points6);
            path7.AddPolygon(points7);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D(double dX, double dY)
        {
            this.X = dX;
            this.Y = dY;
        }

        public Point2D Clone()
        {
            return new Point2D(this.X, this.Y);
        }

        public static implicit operator Point(Point2D p)
        {
            return new Point((int)p.X, (int)p.Y);
        }
        public static implicit operator PointF(Point2D p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }
        public static explicit operator Point2D(Point p)
        {
            return new Point2D(p.X, p.Y);
        }
        public static explicit operator Point2D(PointF p)
        {
            return new Point2D(p.X, p.Y);
        }

        public override bool Equals(object obj)
        {
            Point2D that = (Point2D)obj;
            return that.X == this.X && that.Y == this.Y;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", this.X, this.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GrilleCollision
{
    internal class Vec2
    {
        protected float x;
        protected float y;


        public Vec2 (float X, float Y)
        {
            x = X;
            y = Y;
        }

        public Vec2(Vec2 copie)
        {
            this.x = copie.x;
            this.y = copie.y;
        }

        public override string ToString ()
        {
            return "(" + x + ";" + y + ")";
        }

        public float X()
        { return x; }
        public void X(float x) 
        { this.x = x; }

        public float Y()
        { return y; }
        public void Y(float y)
        { this.y = y; }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x + b.x, a.y + b.y);
        }

        public Vec2 RotateRad(float angleRad)
        {
            float vX = (float)Math.Cos(angleRad) * x - (float)Math.Sin(angleRad) * y;
            float vY = (float)Math.Sin(angleRad) * x + (float)Math.Cos(angleRad) * y;

            return new Vec2(vX, vY);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x - b.x, a.y - b.y);

        }

    }
}

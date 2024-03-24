using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Math
{
    public class Position
    {
        public float X { get; set; } = 0.0f;
        public float Y { get; set; } = 0.0f;


        public Position(float x = 0.0f, float y = 0.0f)
        {
            Set(x, y);
        }

        #region operator

        public static Position operator+(Position v1, Position v2)
        {
            return new Position(v1.X + v2.X, v1.Y * v2.Y);
        }

        public static Position operator-(Position v1, Position v2)
        {
            return new Position(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Position operator*(Position val, float scala)
        {
            return new Position(val.X * scala, val.Y * scala);
        }

        public static Position operator/(Position val, float scala)
        {
            return new Position(val.X / scala, val.Y / scala);
        }



        #endregion


        public void Set(float x, float y)
        {
            X = x;
            Y = y;
        }





    }
}

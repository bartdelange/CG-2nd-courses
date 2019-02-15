﻿namespace MatrixTransformations
{
    public class Vector
    {
        public float X { get; }
        public float Y { get; }
        public float W { get; }
        public float Z { get; }

        public Vector() : this(0, 0)
        {}
        
        public Vector(float x, float y, float w = 1, float z = 0)
        {
            X = x;
            Y = y;
            W = w;
            Z = z;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.W + v2.W, v1.Z + v2.Z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// / X \
        /// | Y |
        /// | W |
        /// \ W /
        /// </returns>
        public override string ToString()
        {
            var str = $"/ {X} \\\n";
            str +=    $"| {Y} |";
            str +=    $"| {W} |";
            str +=    $"\\ {Z} /";
            return str;
        }
    }
}

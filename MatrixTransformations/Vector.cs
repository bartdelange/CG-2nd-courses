﻿namespace MatrixTransformations
{
    public class Vector
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float W { get; }

        public Vector() : this(0, 0, 0, 0)
        {}
        
        public Vector(float x, float y, float z = 0, float w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// / X \
        /// | Y |
        /// | Z |
        /// \ W /
        /// </returns>
        public override string ToString()
        {
            var str = $"/ {X} \\\n";
            str +=    $"| {Y} |\n";
            str +=    $"| {Z} |\n";
            str +=    $"\\ {W} /";
            return str;
        }
    }
}

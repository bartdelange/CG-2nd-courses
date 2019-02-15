namespace MatrixTransformations
{
    public class Vector
    {
        public float X { get; }
        public float Y { get; }

        public Vector() : this(0, 0)
        {}
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// / X \
        /// \ Y /
        /// </returns>
        public override string ToString()
        {
            var str = $"/ {X} \\\n\\ {Y} /";

            return str;
        }
    }
}

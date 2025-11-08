namespace PMP_LAB09
{
    internal class Position
    {
        int x;
        int y;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Position Add(Position p_1, Position p_2)
        {
            return new(p_1.X + p_2.X, p_1.Y + p_2.Y);
        }

        public static double Distance(Position p_1, Position p_2)
        {
            return Math.Sqrt(Math.Pow(p_1.X - p_2.X, 2) + Math.Pow(p_1.Y - p_2.Y, 2));
        }
    }
}

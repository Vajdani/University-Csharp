using System.Drawing;

namespace SZFA_LAB02
{
    internal class Square : Rectangle
    {
        public Square(int width) : base(width, width) {}

        public Square(int width, Color colour, bool isHoley) : base(width, width, colour, isHoley) {}

        public override double Perimeter()
        {
            return width * 4;
        }

        public override double Area()
        {
            return width * width;
        }

        public override string ToString()
        {
            return $"Négyzet. {base.ToString()}";
        }

        public override void Copy(Shape other)
        {
            base.Copy(other);

            Square rect = (Square)other;
            if (rect != null)
            {
                this.height = rect.width;
                this.width = rect.width;
            }
        }
    }
}

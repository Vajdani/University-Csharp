using System.Drawing;

namespace SZFA_LAB02
{
    internal class Rectangle : Shape
    {
        protected int height;
        protected int width;

        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }

        public override bool Displayable { get => width > 0 && height > 0; }

        public Rectangle(int height, int width) : base(Color.White)
        {
            this.height = height;
            this.width = width;
        }

        public Rectangle(int height, int width, Color colour, bool isHoley) : base(isHoley, colour)
        {
            this.height = height;
            this.width = width;
        }

        public override double Perimeter()
        {
            return (Height + Width) * 2;
        }

        public override double Area()
        {
            return Height * Width;
        }

        public override string ToString()
        {
            return $"Téglalap. {base.ToString()}";
        }

        public override void Copy(Shape other)
        {
            base.Copy(other);

            Rectangle rect = (Rectangle)other;
            if (rect != null)
            {
                this.height = rect.height;
                this.width = rect.width;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Rectangle) return false;

            Rectangle rect = (Rectangle)obj;
            return rect.height == height && rect.width == width && base.Equals(obj);
        }
    }
}

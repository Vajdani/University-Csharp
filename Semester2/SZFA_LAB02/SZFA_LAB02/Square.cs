using System.Drawing;

namespace SZFA_LAB02
{
    internal class Square : Rectangle
    {
        public virtual int Height {
            get => base.Height;
            set {
                base.Height = value;
                base.Width = value;
            }
        }
        public virtual int Width {
            get => base.Width;
            set
            {
                base.Height = value;
                base.Width = value;
            }
        }

        public Square(int width) : base(width, width) {}

        public Square(int width, Color colour, bool isHoley) : base(width, width, colour, isHoley) {}

        public override double Perimeter()
        {
            return Width * 4;
        }

        public override double Area()
        {
            return Width * Width;
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
                this.Height = rect.Width;
                this.Width = rect.Width;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Square) return false;

            return base.Equals(obj);
        }
    }
}

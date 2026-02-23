using System.Drawing;

namespace SZFA_LAB02
{
    internal class Circle : Shape
    {
        int radius;

        public int Radius { get => radius; set => radius = value; }
    
        public override bool Displayable { get => radius > 0; }

        public Circle(int radius) : base(Color.White)
        {
            this.radius = radius;
        }

        public Circle(int radius, Color colour, bool isHoley) : base(isHoley, colour)
        {
            this.radius = radius;
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * radius;
        }

        public override double Area()
        {
            return radius * radius * Math.PI;
        }

        public override string ToString()
        {
            return $"Kör. {base.ToString()}";
        }

        public override void Copy(Shape other)
        {
            base.Copy(other);

            Circle rect = (Circle)other;
            if (rect != null)
            {
                this.radius = rect.Radius;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Circle) return false;

            Circle rect = (Circle)obj;
            return rect.radius == radius && base.Equals(obj);
        }
    }
}

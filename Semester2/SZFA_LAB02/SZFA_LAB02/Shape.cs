using System.Drawing;

namespace SZFA_LAB02
{
    internal abstract class Shape
    {
        bool isHoley;
        Color colour;

        public Color Colour { get => colour; set => colour = value; }
        public virtual bool Displayable { get; }

        public Shape(bool isHoley, Color colour)
        {
            this.isHoley = isHoley;
            this.colour = colour;
        }

        public Shape(Color colour) : this(false, colour) { }

        public void MakeHoley()
        {
            isHoley = true;
        }

        public abstract double Perimeter();
        public abstract double Area();

        public virtual void Copy(Shape other)
        {
            this.isHoley = other.isHoley;
            this.colour = other.colour;
        }

        public override string ToString()
        {
            return $"{colour} {(isHoley ? "Holey" : "Not holey")} {(Displayable ? "Displayable" : "Not displayable")} Perimeter: {Perimeter()} Area: {Area()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Shape) return false;

            Shape rect = (Shape)obj;
            return rect.colour == colour && rect.isHoley == isHoley;
        }
    }
}

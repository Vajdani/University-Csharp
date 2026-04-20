namespace SZFA_LAB10.Models
{
    internal class PhoneBookItem : IComparable
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public PhoneBookItem(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public int CompareTo(object? obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (obj is not PhoneBookItem && obj is not string)
            {
                throw new ArgumentException(nameof(obj));
            }

            return obj is PhoneBookItem book ? Name.CompareTo(book.Name) : Name.CompareTo(obj as string);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not PhoneBookItem)
            {
                return false;
            }

            return Name.Equals(((PhoneBookItem)obj).Name);
        }

        public override string ToString()
        {
            return $"{Name} ({PhoneNumber})";
        }
    }
}

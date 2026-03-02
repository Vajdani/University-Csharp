namespace SZFA_LAB03
{
    internal class InterfaceTest : IComparable
    {
        public int order;

        public InterfaceTest()
        {
            order = Random.Shared.Next(0, 101);
        }

        public int CompareTo(object? obj)
        {
            if (obj is not InterfaceTest)
            {
                return -1;
            }

            return order.CompareTo(((InterfaceTest)obj).order);
        }
    }
}

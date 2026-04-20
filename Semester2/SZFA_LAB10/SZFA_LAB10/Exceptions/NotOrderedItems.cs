namespace SZFA_LAB10.Exceptions
{
    public class NotOrderedItems : Exception
    {
        public IComparable[] items { get; private set; }

        public NotOrderedItems(IComparable[] items)
        {
            this.items = items;
        }
    }
}

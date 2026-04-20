using SZFA_LAB10.Enums;
using SZFA_LAB10.Exceptions;

namespace SZFA_LAB10.Models
{
    internal class OrderedItemsHandler
    {
        IComparable[] items;
        
        public OrderedItemsHandler(IComparable[] items)
        {
            this.items = items;
        }

        public bool IsOrdered(bool isAscending = true)
        {
            int compareGoal = isAscending ? -1 : 1;
            int i = 0;
            while (i < items.Length - 1 && (isAscending ?
                        items[i].CompareTo(items[i + 1]) <= 0 :
                        items[i].CompareTo(items[i + 1]) >= 0))
            {
                i++;
            }

            return i >= items.Length - 1;
        }

        public void Sort(SortingMethod sortingMethod, bool isAscending = true)
        {
            switch (sortingMethod)
            {
                case SortingMethod.Selection:
                    SelectionSort(sortingMethod, isAscending);
                    break;
                case SortingMethod.Bubble:
                    BubbleSort(sortingMethod, isAscending);
                    break;
                case SortingMethod.Insertion:
                    InsertionSort(sortingMethod, isAscending);
                    break;
            }
        }

        void SelectionSort(SortingMethod sortingMethod, bool isAscending = true)
        {
            for (int i = 0; i < items.Length; i++)
            {
                int min = i;
                for (int j = i + 1; j < items.Length ; j++)
                {
                    if (isAscending ?
                        items[j].CompareTo(items[min]) <= 0:
                        items[j].CompareTo(items[min]) >= 0)
                    {
                        min = j;
                    }
                }

                (items[min], items[i]) = (items[i], items[min]);
            }
        }

        void BubbleSort(SortingMethod sortingMethod, bool isAscending = true)
        {
            
        }

        void InsertionSort(SortingMethod sortingMethod, bool isAscending = true)
        {

        }

        public int BinarySearch(IComparable value, bool isAscending = true)
        {
            if (!IsOrdered())
            {
                throw new NotOrderedItems(items);
            }

            int left = 0;
            int right = items.Length - 1;
            int center = GetCenter(left, right);
            while (left <= right && items[center] != value)
            {
                if (items[center].CompareTo(value) == 1)
                {
                    if (isAscending)
                    {
                        right = center - 1;
                    }
                    else
                    {
                        left = center + 1;
                    }
                }
                else
                {
                    if (isAscending)
                    {
                        left = center + 1;
                    }
                    else
                    {
                        right = center - 1;
                    }
                }

                center = GetCenter(left, right);
            }

            if (left >= right)
            {
                return center;
            }

            return -1;
        }

        public int BinarySearchRecursive(IComparable value, bool isAscending = true)
        {
            if (!IsOrdered())
            {
                throw new NotOrderedItems(items);
            }

            return BinarySearch(0, items.Length - 1, value, isAscending);
        }

        private int BinarySearch(int left, int right, IComparable value, bool isAscending = true)
        {
            if (left > right)
            {
                return -1;
            }

            int center = GetCenter(left, right);
            if (items[center].CompareTo(value) == 0)
            {
                return center;
            }

            if (items[center].CompareTo(value) == 1)
            {
                if (isAscending)
                {
                    right = center - 1;
                }
                else
                {
                    left = center + 1;
                }
            }
            else
            {
                if (isAscending)
                {
                    left = center + 1;
                }
                else
                {
                    right = center - 1;
                }
            }

            return BinarySearch(left, right, value, isAscending);
        }

        public int LowerBoundSearch(IComparable value)
        {
            if (!IsOrdered())
            {
                throw new NotOrderedItems(items);
            }

            int left = 0;
            int right = items.Length - 1;
            int idx = items.Length;
            while (left < right)
            {
                int center = GetCenter(left, right);
                if (items[center].CompareTo(value) >= 0)
                {
                    idx = center;
                    right = center - 1;
                }
                else
                {
                    left = center + 1;
                }
            }

            return idx;
        }

        public int UpperBoundSearch(IComparable value)
        {
            if (!IsOrdered())
            {
                throw new NotOrderedItems(items);
            }

            int left = 0;
            int right = items.Length - 1;
            int idx = items.Length;
            while (left < right)
            {
                int center = GetCenter(left, right);
                if (items[center].CompareTo(value) > 0)
                {
                    idx = center;
                    right = center - 1;
                }
                else
                {
                    left = center + 1;
                }
            }

            return idx;
        }

        public int GetItemCount(IComparable value)
        {
            return UpperBoundSearch(value) - LowerBoundSearch(value);
        }

        public int GetItemCountWithinRange(IComparable left, IComparable right)
        {
            return UpperBoundSearch(right) - LowerBoundSearch(left);
        }

        public IComparable[] GetItemsWithinRange(IComparable left, IComparable right)
        {
            int upper = UpperBoundSearch(right);
            int lower = LowerBoundSearch(left);
            IComparable[] result = new IComparable[upper - lower];
            int count = 0;
            for (int i = lower; i < upper; i++)
            {
                result[count++] = items[i];
            }

            return result;
        }

        private int GetCenter(int left, int right)
        {
            return (int)Math.Floor((left + right) / 2.0);
        }

        public void Reverse()
        {
            int length = items.Length - 1;
            for (int i = 0; i < Math.Floor(length / 2f) + 1; i++)
            {
                (items[i], items[length - i]) = (items[length - i], items[i]);
            }
        }
    }
}

namespace SZFA_LAB04_IntArrayStat
{
    public class ArrayStatistics
    {
        int[] numbers;

        public ArrayStatistics(int[] numbers)
        {
            this.numbers = numbers;
        }

        public int Total()
        {
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;   
            }

            return sum;
        }

        public bool Contains(int number)
        {
            int i = 0;
            while (i < numbers.Length && numbers[i] != number)
            {
                i++;
            }

            return i < numbers.Length;
        }

        public bool Sorted()
        {
            if (numbers.Length <= 1)
            {
                return true;
            }

            int i = 0;
            while (i < numbers.Length - 1 && numbers[i + 1] >= numbers[i])
            {
                i++;
            }

            return i == numbers.Length - 1;
        }

        public int FirstGreater(int number)
        {
            int i = 0;
            while (i < numbers.Length && numbers[i] <= number)
            {
                i++;
            }

            if (i < numbers.Length)
            {
                return i;
            }

            return -1;
        }
    }
}

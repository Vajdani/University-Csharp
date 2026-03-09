namespace SZFA_LAB04_PrimeTool
{
    public class PrimeTool
    {
        int prime;

        public PrimeTool(int prime)
        {
            this.prime = prime;
        }

        public bool IsPrime()
        {
            if (prime <= 1)
            {
                return false;
            }

            int count = 0;
            for (int i = 1; i < prime; i++)
            {
                if (prime % i == 0)
                {
                    count++;
                }
            }

            return count <= 1;
        }
    }
}

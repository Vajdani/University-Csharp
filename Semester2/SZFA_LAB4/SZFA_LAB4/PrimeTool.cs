namespace SZFA_LAB4
{
    public class PrimeTool
    {
        int prime;

        public PrimeTool(int prime)
        {
            this.prime = prime;
        }

        //https://stackoverflow.com/a/44203452
        public bool IsPrime()
        {
            if (prime <= 1) return false;
            if (prime == 2 || prime == 3 || prime == 5) return true;
            if (prime % 2 == 0 || prime % 3 == 0 || prime % 5 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(prime));
            int i = 6;
            while (i <= boundary)
            {
                if (prime % (i + 1) == 0 || prime % (i + 5) == 0)
                    return false;

                i += 6;
            }

            return true;
        }
    }
}

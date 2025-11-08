using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PMP_HF013
{
    internal class Program
    {
        static int GetNum()
        {
            return int.Parse(Console.ReadLine());
        }

        static int Absolute(int num)
        {   
            if (num < 0)
            {
                return -num;
            }

            return num;
        }

        static void Main(string[] args)
        {
            int max = GetNum();
            for (int i = 0; i < 2; i++)
            {
                int num = GetNum();
                if (Absolute(num) > Absolute(max))
                {
                    max = num;
                }
            }

            Console.WriteLine(max);
        }
    }
}

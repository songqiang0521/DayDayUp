using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compute_prime
{
    class Program
    {
        public static bool IsPrime(int candidate)
        {
            if ((candidate & 1) != 0)
            {
                int limit = (int)Math.Sqrt(candidate);
                for (int divisor = 3; divisor <= limit; divisor += 2)
                {
                    if ((candidate % divisor) == 0)
                    {
                        return false;
                    }
                }
                return true;
            }

            return candidate == 2;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 7199369 + 1; i++)
            {
                if (IsPrime(i))
                {
                    Console.WriteLine(i);
                }
            }
            Console.Read();
        }
    }
}

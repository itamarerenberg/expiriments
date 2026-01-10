using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expiriments
{
    internal class AnvetonusoShelAkodoshBoruchHue
    {
        public void Print(int n)
        {

            for (int i = 1; i < 1000; i++)
            {
                Console.WriteLine($"26*{i} = {i * 26}:  {smallGimatria(i * 26)}");
            }
        }


        int smallGimatria(int n)
        {
            if (n > 9)
            {
                int res = 0;
                while (n > 0)
                {
                    res += n % 10;
                    n /= 10;
                }
                return (smallGimatria(res));
            }
            return n;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace muenzautomat
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = ChangeCalculator.GetInsance();
            var coins = calculator.GetChange(3, 15);

            foreach (var (coin, count) in coins)
                Console.WriteLine($"{coin}: {count}");
        }
    }
}

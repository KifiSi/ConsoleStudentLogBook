using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public class Statistics
    {
        public double High;

        public double Low;

        public double Sum;

        public int Count;

        public Statistics()
        {
            Sum = 0;
            Count = 0;
            High = double.MinValue;
            Low = double.MaxValue;
        }

        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }

        public char Letter 
        {
            get
            {
                var result = Average switch
                {
                    >= 4.5 => 'A',
                    >= 3.9 => 'B',
                    >= 3 => 'C',
                    _ => 'D'
                };
                return result;
            }
        }

        public void Add(double number)
        {
            Sum += number;
            Count++;
            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
        }
    }
}
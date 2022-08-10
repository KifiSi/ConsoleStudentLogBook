// See https://aka.ms/new-console-template for more information
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
                switch (Average)
                {
                    case double d when d >= 4.5:
                        return 'A';

                    case double d when d >= 3.9:
                        return 'B';

                    case double d when d >= 3:
                        return 'C';

                    default:
                        return 'D';
                }
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
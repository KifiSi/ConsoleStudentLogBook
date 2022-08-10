using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public class KidInMemory : KidBase
    {
        private List<double> _grades = new List<double>();

        private event GradeAddedDelegate GradeAdded;

        public KidInMemory(string name) : base(name)
        {
            GradeAdded += OnGradeAdded;
        }

        public override void AddGrades(string grade)
        {
            if (double.TryParse(grade, out double result) && (result >= 1 && result <= 6))
            {
                _grades.Add(result);
                if (result < 3)
                {
                    GradeAdded += GradeLessThanThree;
                }
            }
            else if (grade.Contains('+') || grade.Contains('-'))
            {
                switch (grade)
                {
                    case "1+":
                        _grades.Add(1.5);
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2-":
                        _grades.Add(1.75);
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2+":
                        _grades.Add(2.5);
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3-":
                        _grades.Add(2.75);
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3+":
                        _grades.Add(3.5);
                        break;

                    case "4-":
                        _grades.Add(3.75);
                        break;

                    case "4+":
                        _grades.Add(4.5);
                        break;

                    case "5-":
                        _grades.Add(4.75);
                        break;

                    case "5+":
                        _grades.Add(5.5);
                        break;

                    default:
                        throw new ArgumentException("Invalid grade");
                }
            }
            else
            {
                throw new ArgumentException("Invalid grade");
            }

            if (GradeAdded != null)
            {
                GradeAdded(this, new EventArgs());
                GradeAdded -= GradeLessThanThree;
            }
        }

        public override void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName) || !newName.All(Char.IsLetter))
            {
                throw new ArgumentException("Invalid Name");
            }
            Name = newName;
            Console.WriteLine($"The new name is: {Name}");
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            foreach (double n in _grades)
            {
                result.Add(n);
            }
            return result;
        }
    }
}

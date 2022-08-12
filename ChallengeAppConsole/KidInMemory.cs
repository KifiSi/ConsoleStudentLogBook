using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public class KidInMemory : KidBase
    {
        private List<string> _grades = new List<string>();
        private int numberGrade = 1;

        private event GradeAddedDelegate GradeAdded;
        private event GradeDeletedDelegate GradeDeleted;

        public KidInMemory(string name) : base(name)
        {
            GradeAdded += OnGradeAdded;
            GradeDeleted += OnGradeDeleted;
        }

        public override void AddGrades(string grade, string name)
        {
            if (double.TryParse(grade, out double result) && (result >= 1 && result <= 6))
            {
                _grades.Add($"{numberGrade++}. {result}");
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
                        _grades.Add($"{numberGrade++}. 1.5");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2-":
                        _grades.Add($"{numberGrade++}. 1.75");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2+":
                        _grades.Add($"{numberGrade++}. 2.5");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3-":
                        _grades.Add($"{numberGrade++}. 2.75");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3+":
                        _grades.Add($"{numberGrade++}. 3.5");
                        break;

                    case "4-":
                        _grades.Add($"{numberGrade++}. 3.75");
                        break;

                    case "4+":
                        _grades.Add($"{numberGrade++}. 4.5");
                        break;

                    case "5-":
                        _grades.Add($"{numberGrade++}. 4.75");
                        break;

                    case "5+":
                        _grades.Add($"{numberGrade++}. 5.5");
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

        public override void RemoveGrade(string indexOfGrade, string name)
        {
            int indexToRemove = _grades.FindIndex(a => a.Contains($"{indexOfGrade}."));
            if (indexToRemove == -1)
            {
                throw new ArgumentException("Invalid index of grade to remove");
            }
            _grades.RemoveAt(indexToRemove);
            GradeDeleted(this, new EventArgs());
        }

        public override void ShowGrades(string name)
        {
            if (_grades.Count == 0)
            {
                throw new InvalidOperationException($"{name} has no grades");
            }
            foreach (string n in _grades)
            {
                Console.WriteLine(n);
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

            foreach (string n in _grades)
            {
                double gr = Convert.ToDouble(n.Substring(n.IndexOf(".") + 1));

                result.Add(gr);
            }
            return result;
        }
    }
}

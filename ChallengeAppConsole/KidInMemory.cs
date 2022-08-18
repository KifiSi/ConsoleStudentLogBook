using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public class KidInMemory : KidBase
    {
        private List<string> grades = new List<string>();
        private int numberGrade = 1;

        private event GradeAddedDelegate GradeAdded;
        private event GradeDeletedDelegate GradeDeleted;

        public KidInMemory(string name) : base(name)
        {
            GradeAdded += OnGradeAdded;
            GradeDeleted += OnGradeDeleted;
        }

        public override void AddGrade(string grade)
        {
            if ((double.TryParse(grade, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out double result) ||
                double.TryParse(grade, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("pl-PL"), out result) ||
                double.TryParse(grade, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result)) &&
                (result >= 1 && result <= 6))
            {
                grades.Add($"{numberGrade++}. {result}");
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
                        grades.Add($"{numberGrade++}. 1.5");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2-":
                        grades.Add($"{numberGrade++}. 1.75");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2+":
                        grades.Add($"{numberGrade++}. 2.5");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3-":
                        grades.Add($"{numberGrade++}. 2.75");
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3+":
                        grades.Add($"{numberGrade++}. 3.5");
                        break;

                    case "4-":
                        grades.Add($"{numberGrade++}. 3.75");
                        break;

                    case "4+":
                        grades.Add($"{numberGrade++}. 4.5");
                        break;

                    case "5-":
                        grades.Add($"{numberGrade++}. 4.75");
                        break;

                    case "5+":
                        grades.Add($"{numberGrade++}. 5.5");
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

        public override void RemoveGrade(string indexOfGrade)
        {
            int indexToRemove = grades.FindIndex(a => a.Contains($"{indexOfGrade}."));
            if (indexToRemove == -1)
            {
                throw new ArgumentException("Invalid index of grade to remove");
            }
            grades.RemoveAt(indexToRemove);
            GradeDeleted(this, new EventArgs());
        }

        public override void ShowGrades()
        {
            if (grades.Count == 0)
            {
                throw new InvalidOperationException($"{this.Name} has no grades");
            }
            foreach (string n in grades)
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
            else if (newName == this.Name)
            {
                throw new ArgumentException("You write the same name");
            }
            this.Name = newName;
            grades.Clear();
            numberGrade = 1;
            Console.WriteLine($"The new name is: {this.Name}");
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            if (grades.Count == 0)
            {
                throw new InvalidOperationException($"{this.Name} has no grades");
            }

            foreach (string n in grades)
            {
                double gr = Convert.ToDouble(n.Substring(n.IndexOf(".") + 1));

                result.Add(gr);
            }
            return result;
        }
    }
}

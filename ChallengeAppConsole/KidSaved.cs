using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public class KidSaved : KidBase
    {
        private event GradeAddedDelegate GradeAdded;
        private event GradeDeletedDelegate GradeDeleted;

        private string fileName;
        private DateTime date = DateTime.UtcNow;
        private const string autoSave = "audit.txt";

        private int numberGrade = 1;

        public KidSaved(string name) : base(name)
        {
            GradeAdded += OnGradeAdded;
            GradeDeleted += OnGradeDeleted;
            fileName = Name;
            FileExists(fileName);
        }

        public override void AddGrade(string grade)
        {
            double tempGrade = 0;

            if ((double.TryParse(grade, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out double result) ||
                double.TryParse(grade, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("pl-PL"), out result) ||
                double.TryParse(grade, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result)) &&
                (result >= 1 && result <= 6))
            {
                tempGrade = result;
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
                        tempGrade = 1.5;
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2-":
                        tempGrade = 1.75;
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "2+":
                        tempGrade = 2.5;
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3-":
                        tempGrade = 2.75;
                        GradeAdded += GradeLessThanThree;
                        break;

                    case "3+":
                        tempGrade = 3.5;
                        break;

                    case "4-":
                        tempGrade = 3.75;
                        break;

                    case "4+":
                        tempGrade = 4.5;
                        break;

                    case "5-":
                        tempGrade = 4.75;
                        break;

                    case "5+":
                        tempGrade = 5.5;
                        break;

                    default:
                        throw new ArgumentException("Invalid grade");
                }
            }
            else
            {
                throw new ArgumentException("Invalid grade");
            }

            using (var writer = File.AppendText($"{fileName}.txt"))
            using (var audit = File.AppendText($"{autoSave}"))
            {
                writer.WriteLine($"{numberGrade}. {tempGrade}");
                audit.WriteLine($"{date}, {this.Name}, Adding grade {numberGrade}. {tempGrade}");
                numberGrade++;
            }

            if (GradeAdded != null)
            {
                GradeAdded(this, new EventArgs());
                GradeAdded -= GradeLessThanThree;
            }
        }
        public override void RemoveGrade(string indexOfGrade)
        {
            var linesList = File.ReadAllLines($"{fileName}.txt").ToList();
            int indexToRemove = linesList.FindIndex(a => a.Contains($"{indexOfGrade}."));
            if (indexToRemove == -1)
            {
                throw new ArgumentException("Invalid index of grade to remove");
            }
            using (var audit = File.AppendText($"{autoSave}"))
            {
                audit.WriteLine($"{date}, {this.Name}, Removing grade {linesList[indexToRemove]}");
            }
            linesList.RemoveAt(indexToRemove);
            File.WriteAllLines(($"{fileName}.txt"), linesList.ToArray());
            GradeDeleted(this, new EventArgs());
        }

        public override void ShowGrades()
        {
            using (var reader = File.OpenText($"{fileName}.txt"))
            using (var audit = File.AppendText($"{autoSave}"))
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    throw new InvalidOperationException($"{this.Name} has no grades");
                }

                while (line != null)
                {
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
                audit.WriteLine($"{date}, {this.Name}, Showing grades");
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
            FileExists(newName);
            this.Name = newName;
            fileName = newName;
            Console.WriteLine($"The new name is: {this.Name}");
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{fileName}.txt"))
            using (var audit = File.AppendText($"{autoSave}"))
            {
                var line = reader.ReadLine();

                if (line == null)
                {
                    throw new InvalidOperationException($"{this.Name} has no grades");
                }

                while (line != null)
                {
                    string gradeWithoutIndex = (line.Substring(line.IndexOf(".") + 2));
                    if (!(double.TryParse(gradeWithoutIndex, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out double grToAdd) ||
                        double.TryParse(gradeWithoutIndex, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("pl-PL"), out grToAdd) ||
                        double.TryParse(gradeWithoutIndex, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out grToAdd)) ||
                        (grToAdd < 1 || grToAdd > 6))
                    {
                        throw new ArgumentOutOfRangeException($"File {fileName} has been modified, delete or repair the file");
                    }
                    result.Add(grToAdd);
                    line = reader.ReadLine();
                }
                audit.WriteLine($"{date}, {this.Name}, Showing statistics");
            }
            return result;
        }

        private void FileExists(string fileName)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var file = $"{workingDirectory}\\{fileName}.txt";
            if (!File.Exists(file))
            {
                using (var _new = File.Create($"{fileName}.txt"))
                {
                }
            }
            else
            {
                using (var reader = File.OpenText($"{fileName}.txt"))
                {
                    if (reader.ReadLine() != null)
                    {
                        string lastLine = File.ReadLines($"{fileName}.txt").Last();
                        if (lastLine.IndexOf(".") == -1)
                        {
                            throw new ArgumentOutOfRangeException($"File {fileName} has been modified, delete or repair the file");
                        }
                        numberGrade = Convert.ToInt32(lastLine.Substring(0, lastLine.IndexOf("."))) + 1;
                    }
                    else
                    {
                        numberGrade = 1;
                    }

                    Console.WriteLine(numberGrade);
                }
            }
        }

        public override void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("New Grade is added in the file");
        }

        public override void OnGradeDeleted(object sender, EventArgs args)
        {
            Console.WriteLine("Grade removed from file");
        }
    }
}
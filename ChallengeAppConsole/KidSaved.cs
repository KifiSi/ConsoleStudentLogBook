// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;

namespace ChallengeAppConsole
{
    public class KidSaved : KidBase
    {
        private event GradeAddedDelegate GradeAdded;
        private event GradeDeletedDelegate GradeDeleted;

        private string fileName;
        private const string autoSave = "audit";

        private int numberGrade = 1;

        public KidSaved(string name) : base(name)
        {
            GradeAdded += OnGradeAdded;
            GradeDeleted += OnGradeDeleted;
            fileName = Name;
            FileExists(fileName);
        }

        public override void AddGrades(string grade, string name)
        {
            double tempGrade = 0;

            if (double.TryParse(grade, out double result) && (result >= 1 && result <= 6))
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
            using (var audit = File.AppendText($"{autoSave}.txt"))
            {
                var date = DateTime.UtcNow;
                writer.WriteLine($"{numberGrade}. {tempGrade}");
                audit.WriteLine($"{date}, {name}, Adding grade {numberGrade}. {tempGrade}");
                numberGrade++;
            }

            if (GradeAdded != null)
            {
                GradeAdded(this, new EventArgs());
                GradeAdded -= GradeLessThanThree;
            }
        }
        public override void RemoveGrade(string indexOfGrade, string name)
        {
            var linesList = File.ReadAllLines($"{fileName}.txt").ToList();
            int indexToRemove = linesList.FindIndex(a => a.Contains($"{indexOfGrade}."));
            if (indexToRemove == -1)
            {
                throw new ArgumentException("Invalid index of grade to remove");
            }
            using (var audit = File.AppendText($"{autoSave}.txt"))
            {
                var date = DateTime.UtcNow;
                audit.WriteLine($"{date}, {name}, Removing grade {linesList[indexToRemove]}");
            }
            linesList.RemoveAt(indexToRemove);
            File.WriteAllLines(($"{fileName}.txt"), linesList.ToArray());
            GradeDeleted(this, new EventArgs());
        }

        public override void ShowGrades(string name)
        {
            using (var reader = File.OpenText($"{fileName}.txt"))
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    throw new InvalidOperationException($"{name} has no grades");
                }

                while (line != null)
                {
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
            }
        }

        public override void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName) || !newName.All(Char.IsLetter))
            {
                throw new ArgumentException("Invalid Name");
            }
            FileExists(newName);
            Name = newName;
            fileName = newName;
            Console.WriteLine($"The new name is: {Name}");
        }

        public override Statistics GetStatistics(string name)
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{fileName}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    if (!Double.TryParse(line.Substring(line.IndexOf(".") + 1), out double grade) || (grade < 1 || grade > 6))
                    {
                        throw new ArgumentOutOfRangeException($"File {fileName} has been modified, delete or repair the file");
                    }
                    result.Add(grade);
                    line = reader.ReadLine();
                }
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
// See https://aka.ms/new-console-template for more information
namespace ChallengeAppConsole
{
    public class KidSaved : KidBase
    {
        private event GradeAddedDelegate GradeAdded;

        private const string fileName = "Grades";
        private const string autoSave = "audit";

        public KidSaved(string name) : base(name)
        {
            GradeAdded += OnGradeAdded;
        }

        public override void AddGrades(string grade)
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
                writer.WriteLine(tempGrade);
                audit.WriteLine($"{date}: grade {tempGrade}");
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

            using (var reader = File.OpenText($"{fileName}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var grade = double.Parse(line);
                    result.Add(grade);
                    line = reader.ReadLine();
                }
            }
            return result;
        }

        public override void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("New Grade is added in the file");
        }
    }
}
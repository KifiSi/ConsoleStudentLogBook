using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public interface IKidBase
    {
        string Name { get; set; }

        void AddGrade(string grade);

        void RemoveGrade(string indexOfGrade);

        void ChangeName(string newName);

        void ShowGrades();

        Statistics GetStatistics();

    }
}

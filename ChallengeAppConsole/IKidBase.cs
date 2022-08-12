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

        //void AddGrades(string grade);

        void AddGrades(string grade, string name);

        void ChangeName(string newName);

        void RemoveGrade(string indexOfGrade, string name);

        void ShowGrades(string name);

        Statistics GetStatistics();

    }
}

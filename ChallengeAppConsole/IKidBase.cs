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

        void AddGrades(string grade);

        void ChangeName(string newName);

        Statistics GetStatistics();

    }
}

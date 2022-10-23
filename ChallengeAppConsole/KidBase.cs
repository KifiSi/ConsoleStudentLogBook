using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public abstract class KidBase : EventMessage, IKidBase
    {
        protected KidBase(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public abstract void AddGrade(string grade);

        public abstract void RemoveGrade(string indexOfGrade);

        public abstract void ChangeName(string newName);

        public abstract void ShowGrades();

        public abstract Statistics GetStatistics();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public abstract class KidBase : EventMessage, IKidBase
    {
        public KidBase(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        //public abstract void AddGrades(string grade);

        public abstract void AddGrades(string grade, string name);

        public abstract void RemoveGrade(string indexOfGrade, string name);

        public abstract void ShowGrades(string name);

        public abstract void ChangeName(string newName);

        public abstract Statistics GetStatistics(string name);

    }
}

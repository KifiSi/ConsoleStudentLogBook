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

        public abstract void AddGrades(string grade);

        public abstract void ChangeName(string newName);

        public abstract Statistics GetStatistics();
    }
}

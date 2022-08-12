using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public class EventMessage : IEventMessage
    {
        public delegate void GradeAddedDelegate(object sender, EventArgs args);

        public delegate void GradeDeletedDelegate(object sender, EventArgs args);

        public virtual void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("New Grade is added");
        }

        public virtual void GradeLessThanThree(object sender, EventArgs args)
        {
            Console.WriteLine("Oh no! We should inform student’s parents about this fact");
        }

        public virtual void OnGradeDeleted(object sender, EventArgs args)
        {
            Console.WriteLine("Grade removed");
        }
    }
}

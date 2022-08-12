using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole
{
    public interface IEventMessage
    {
        void OnGradeAdded(object sender, EventArgs args);

        void GradeLessThanThree(object sender, EventArgs args);

        void OnGradeDeleted(object sender, EventArgs args);
    }
}

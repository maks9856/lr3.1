using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lr3._1
{
    public class ThreadPriorityHelper
    {
        public IEnumerable<string> GetAllThreadPriorities()
        {
            List<string> priorities = new List<string>();

            foreach (ThreadPriority priority in Enum.GetValues(typeof(ThreadPriority)))
            {
                priorities.Add(priority.ToString());
               
            }

            return priorities;
        }
    }
}

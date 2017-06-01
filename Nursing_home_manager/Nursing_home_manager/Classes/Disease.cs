using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{
    public class Disease
    {

        public string Name { get; set; }
        public int Severity { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public int compareTo(Disease dis)
        {
            if (String.Compare(dis.Name, this.Name, true) == 0 && this.Severity == dis.Severity)
                return 1;
            return 0;
            
        }
    }
}

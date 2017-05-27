using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_home_manager.Classes
{
    class Disease
    {

        public string Name { get; set; }
        public int Severity { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}

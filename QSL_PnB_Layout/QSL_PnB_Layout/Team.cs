using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smite_PnB_Layout
{
    public class Team
    {
        private string filePath;

        public string FilePath { get => filePath; set => filePath = value; }

        public Team(string filePath)
        {
            this.filePath = filePath;
        }
    }
}

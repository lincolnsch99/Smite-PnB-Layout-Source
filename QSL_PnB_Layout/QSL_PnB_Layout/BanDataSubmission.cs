using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smite_PnB_Layout
{
    [System.Serializable]
    public class BanDataSubmission
    {
        private string team;
        public string Team { get => team; set => team = value; }

        private List<string> godNames;
        public List<string> GodNames { get => godNames; set => godNames = value; }

        public BanDataSubmission(string teamName, List<string> gods = null)
        {
            team = teamName;
            this.godNames = gods;
        }
    }
}

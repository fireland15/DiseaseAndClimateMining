using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote.DbModels
{
    public class DiseaseRecord
    {
        public string Year { get; set; }
        public string Location { get; set; }
        public int Week { get; set; }
        public string DiseaseName { get; set; }
        public int NewInfections { get; set; }
    }
}

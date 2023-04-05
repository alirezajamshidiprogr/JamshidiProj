using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamshidiProj
{
    public class ListObject
    {
        public decimal temperature1 { get; set; }
        public decimal temperature2 { get; set; }
        public decimal temperature3 { get; set; }
        public decimal temperature4 { get; set; }

        public decimal humidity1 { get; set; }
        public decimal humidity2 { get; set; }
        public decimal humidity3 { get; set; }
        public decimal humidity4 { get; set; }

        public decimal winddirect1 { get; set; }
        public decimal winddirect2 { get; set; }
        public decimal winddirect3 { get; set; }
        public decimal winddirect4 { get; set; }

        public decimal windspeed1 { get; set; }
        public decimal windspeed2 { get; set; }
        public decimal windspeed3 { get; set; }
        public decimal windspeed4 { get; set; }

        public DateTime DateTime { get; set; }
        public decimal RainFall { get; set; }
    }
}

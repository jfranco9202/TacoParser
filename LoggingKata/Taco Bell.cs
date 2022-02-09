using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingKata
{
    class Taco_Bell : ITrackable
    {
        public Taco_Bell()
        {

        }

        public string Name { get; set; }
        public Point Location { get; set; }
    }

   
}

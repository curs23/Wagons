using System;
using System.Collections.Generic;
using System.Text;

namespace Wagons
{
    class Wagon
    {
        public bool Light { get; set; }

        public void OnLight()
        {
            Light = true;
        }

        public void OffLight()
        {
            Light = false;
        }

        public Wagon()
        {
            Light = false;
        }
    }
}

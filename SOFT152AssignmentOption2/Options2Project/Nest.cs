using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFT152SteeringLibrary;

namespace SteeringProject
{
    class Nest
    {
        public SOFT152Vector NestPosition { set; get; }
        public int NestSize { set; get; }

        public Nest()
        {

        }
        public Nest (SOFT152Vector position)
        {
            NestPosition = new SOFT152Vector(position.X, position.Y);

            NestSize = 20;
        }
    }
}

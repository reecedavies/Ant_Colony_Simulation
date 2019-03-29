using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOFT152SteeringLibrary;

namespace SteeringProject
{
    class Food
    {

        public SOFT152Vector FoodPosition { set; get; }
        public double FoodSize { set; get; }


        public Food (SOFT152Vector position)
        {
            FoodPosition = new SOFT152Vector(position.X, position.Y);

            FoodSize = 20;
        }

        
    }
}

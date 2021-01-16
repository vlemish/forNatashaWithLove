using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsApp
{
    public class PossibleNumberPointer
    {
        public int EntryPoint { get; set; }

        public int EndPoint { get; set; }


        public PossibleNumberPointer(int entryPoint, int endPoint)
        {
            EntryPoint = entryPoint;
            EndPoint = endPoint;
        }
    }
}

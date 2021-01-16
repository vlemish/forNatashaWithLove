using System;

namespace BullsAndCowsApp
{
    public class GuessedNumberEventArgs:EventArgs
    {
        public string Number { get; set; }

        public int Steps { get; set; }


        public GuessedNumberEventArgs(string number, int steps)
        {
            Number = number;
            Steps = steps;
        }
    }
}

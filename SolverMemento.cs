using System.Collections.Generic;

namespace BullsAndCowsApp
{
    public class SolverMemento
    {
        public List<string> AllNumbers { get; set; }

        public int Step { get; set; }      

        public List<Guess> History { get; set; }


        public SolverMemento(List<string> allNumbers, int step, int length, List<Guess> history)
        {
            AllNumbers = new List<string>();            
            Step = step;
            History = history.ConvertAll(guess => new Guess(guess.Question, guess.Answer));

            AllNumbers.AddRange(allNumbers);
        }

        public SolverMemento()
        {
            AllNumbers = new List<string>();
            Step = 0;
            History = new List<Guess>();
        }
        
    }
}

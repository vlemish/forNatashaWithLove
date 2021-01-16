using System.Collections.Generic;
using System.Linq;

namespace BullsAndCowsApp
{
    public class SolverHistory
    {
        // key - is a step, value - current state for the concrete step.
        private Dictionary<int, SolverMemento> _history;


        public SolverHistory()
        {
            _history = new Dictionary<int, SolverMemento>();
        }


        public void Add(SolverMemento memento)
        {
            if (memento != null)
            {
                _history.Add(memento.Step, memento);
            }
        }

        public void Remove(SolverMemento memento)
        {
            if (memento != null && IsAllowedStep(memento.Step))
            {
                // we need to delete the state that user want to restore and all the states that were after it
                _history = _history
                    .Where(p => p.Key < memento.Step)
                    .ToDictionary(p => p.Key, p => p.Value);
            }
        }
        
        public bool IsAllowedStep(int step)
        {
            return _history.ContainsKey(step);
        }

        public int[] GetPossibleSteps()
        {
            return _history.Select(p => p.Key).ToArray();
        }

        public SolverMemento GetByStep(int step)
        {
            return _history.Where(p => p.Key == step)
                .Select(p => p.Value)
                .FirstOrDefault();
        }
    }
}

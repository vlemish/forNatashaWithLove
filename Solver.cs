using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BullsAndCowsApp
{
    public delegate void GuessedNumberHandler(object obj, GuessedNumberEventArgs guessedNumberArgs);


    public class Solver
    {
        private List<string> _allNumbers;

        private int _step = 0;

        private int _length = 0;

        private List<BullsCows> _potenitalAnswers;

        private List<Guess> _history;

        public event GuessedNumberHandler Guessed;


        public List<BullsCows> PossibleAnswers { get; private set; }

        public SolverHistory SolverHistory { get; private set; }

        public int Length
        {
            get { return _length; }
            private set
            {
                if (value > 9 || value < 1)
                {
                    throw new Exception("Value of the length can't be greater than 9 and less than 1. Your input: " + value);
                }

                _length = value;
            }
        }


        public Solver(int length)
        {
            Length = length;
            PossibleAnswers = new List<BullsCows>();
            _allNumbers = new List<string>();
            _history = new List<Guess>();
            _potenitalAnswers = new List<BullsCows>();
            SolverHistory = new SolverHistory();

            SetPotentialNumbers();
            SetAllNumbers();

            PossibleAnswers.AddRange(_potenitalAnswers);
        }

        public Solver()
        {
            Length = 4;
            PossibleAnswers = new List<BullsCows>();
            _allNumbers = new List<string>();
            _history = new List<Guess>();
            _potenitalAnswers = new List<BullsCows>();
            SolverHistory = new SolverHistory();

            SetPotentialNumbers();
            SetAllNumbers();

            PossibleAnswers.AddRange(_potenitalAnswers);
        }


        private void SetAllNumbers()
        {
            for (int i = 0; i < (int)Math.Pow(10, Length); i++)
            {
                _allNumbers.Add(i.ToString($"D{Length}"));
            }
        }

        private void SetPotentialNumbers()
        {
            for (int i = 0; i <= Length; i++)
            {
                for (int j = 0; j <= Length; j++)
                {
                    if (i + j <= Length)
                    {
                        _potenitalAnswers.Add(new BullsCows(i, j));
                    }
                }
            }
        }

        private bool IsTherePossibleAnswers(Guess guess)
        {
            foreach (var number in _allNumbers)
            {
                if (IsNumberConsistantWithQA(number, guess.Question, guess.Answer))
                {
                    return true;
                }
            }

            return false;
        }

        private BullsCows GetBullsCowsCount(string number, string question)
        {
            var tempQuestion = new StringBuilder(question);
            var tempNumber = new StringBuilder(number);

            int bulls = 0;
            int cows = 0;
            for (int i = 0; i < Length; i++)
            {
                if (number[i] == question[i])
                {
                    bulls++;
                    var indexQ = tempQuestion.ToString().IndexOf(question[i]);
                    var indexN = tempNumber.ToString().IndexOf(number[i]);
                    tempQuestion.Remove(indexQ, 1);
                    tempNumber.Remove(indexN, 1);
                }
            }

            for (int i = 0; i < tempQuestion.Length; i++)
            {
                if (tempNumber.ToString().Contains(tempQuestion[i].ToString()))
                {
                    cows++;
                    var index = tempNumber.ToString().IndexOf(tempQuestion[i]);
                    tempNumber.Remove(index, 1);
                }
            }

            return new BullsCows(bulls, cows);
        }

        private bool IsNumberConsistantWithQA(string number, string question, BullsCows answer)
        {
            return GetBullsCowsCount(number, question) == answer;
        }         

        // The best question is the question that gives us the biggest amount of information about the system.
        // In this case, we're trying to delete the biggest amount of possible numbers by one step.
        private string GetBestQuestion(int step)
        {
            if (step > 1)
            {
                var lastGuess = _history.Last();
                _allNumbers.RemoveAll(number => !IsNumberConsistantWithQA(number, lastGuess.Question, lastGuess.Answer));
            }

            try
            {
                Random r = new Random();
                var el = _allNumbers.ElementAt(r.Next(0, _allNumbers.Count));

                PossibleAnswers.Clear();
                foreach (var answer in _potenitalAnswers)
                {
                    if (IsTherePossibleAnswers(new Guess(el, answer)))
                    {
                        PossibleAnswers.Add(answer);
                    }
                }

                return _allNumbers.ElementAt(r.Next(0, _allNumbers.Count));
            }
            catch (Exception)
            {
                throw new Exception("This can't be true...\nYou might made a mistake.");
            }            
        }

        private void SaveState()
        {
            var currentState = new SolverMemento(_allNumbers, _step, Length, _history);
            SolverHistory.Add(currentState);
        }

        public void RestoreState(int step)
        {
            if (!SolverHistory.IsAllowedStep(step))
            {
                throw new Exception("Invalid value of a step!");
            }
            else
            {                
                var memento = SolverHistory.GetByStep(step);

                _step = memento.Step - 1;
                _allNumbers = memento.AllNumbers;
                _history = memento.History;

                SolverHistory.Remove(memento);

                Solve();
            }
        }

        public string Solve()
        {
            _step++;
            var question = GetBestQuestion(_step);
            SaveState();
            return question;
        }

        public string Solve(Guess answer)
        {
            if (answer.Answer.Bulls == Length)
            {
                if (Guessed != null)
                {
                    Guessed(this, new GuessedNumberEventArgs(answer.Question, _step));
                }

                return null;
            }
            else
            {
                _history.Add(answer);
                return Solve();
            }
        }
    }
}

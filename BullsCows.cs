using System.Collections.Generic;

namespace BullsAndCowsApp
{
    public class BullsCows
    {
        public int Bulls { get; set; }

        public int Cows { get; set; }


        public BullsCows(int bulls, int cows)
        {
            Bulls = bulls;
            Cows = cows;
        }     


        public static bool operator ==(BullsCows bullsCows1, BullsCows bullsCows2)
        {
            return bullsCows1.Bulls == bullsCows2.Bulls && bullsCows1.Cows == bullsCows2.Cows;
        }

        public static bool operator !=(BullsCows bullsCows1, BullsCows bullsCows2)
        {
            return bullsCows1.Bulls == bullsCows2.Bulls && bullsCows1.Cows == bullsCows2.Cows;
        }

        public override bool Equals(object obj)
        {
            var bullsCows = obj as BullsCows;

            if (bullsCows == null)
            {
                return false;
            }

            return bullsCows.Bulls == this.Bulls && bullsCows.Cows == this.Cows;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + Bulls.GetHashCode();
                hash = (hash * 23) + Cows.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Bulls}\n{Cows}";
        }
    }
}

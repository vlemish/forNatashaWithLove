namespace BullsAndCowsApp
{
    public class Guess
    {
        public string Question { get; set; }

        public BullsCows Answer { get; set; }


        public Guess(string question, BullsCows answer)
        {           
            Question = question;
            Answer = answer;
        }
    }
}

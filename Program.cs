using System;
using System.Collections.Generic;

namespace BullsAndCowsApp
{
    class Program
    {
        static bool isGuessed = false;


        static void Main(string[] args)
        {
            Console.WriteLine("***Bulls and Cows***");
            Console.WriteLine("RULES: ");
            Console.WriteLine("The rules of the game are pretty simple. You imagine the number (better write it down somewhere to don't forget), and I try to guess it.\n" +
                "After every my guess you answer me how much is there bulls and cows.\n" +
                "Bulls - count of numbers that in your imagined number on its place.\n" +
                "Cows - count of number thats included in your imagined number but not in its right place.\n" +
                "For example, if you'll imagine 5402 and I guess 7012, you've got to answer B - 1; C - 1\n\n");

            var length = ReadNumber("Enter length of the number: ",
                "Wrong input! Value of the length should be an integer in a range of 1-9!\nTry again!",
                a => a > 0 && a <= 9);
            Console.WriteLine();

            Solver solver = new Solver(length);
            solver.Guessed += Solver_Guessed;
            var guess = solver.Solve();
            do
            {
                Console.WriteLine(guess);
                Console.WriteLine("Possible answers for you: ");
                PrintPossibleNumber(solver.PossibleAnswers);

                int bulls = ReadNumber("Bulls: ", "Wrong input for a bull! Try again!", a => a <= length && a >= 0);
                int cows = ReadNumber("Cows: ", "Wrong input for a cow! Try again!", a => a <= length && a >= 0);

                try
                {
                    guess = solver.Solve(new Guess(guess, new BullsCows(bulls, cows)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("What will we do? -We can start over or roll back to some step." +
                        "\nWrite 'so' - to start over or 'rb' - to roll back to some step.");

                    var decision = ReadString("", "", a => a.ToLower().Trim().Equals("so") || a.ToLower().Trim().Equals("rb"));
                    switch (decision)
                    {
                        case "so":
                            {
                                Console.WriteLine("The game started over. Answer more carefully, and I would guess the number!");
                                solver = new Solver(length);
                                break;
                            }
                        case "rb":
                            {
                                var step = ReadNumber($"Possible steps to roll back: {string.Join("  ", solver.SolverHistory.GetPossibleSteps())}\n" +
                                    $"Okey, write the step to roll back: ",
                                    $"Wrong value for a step :(",
                                    a => solver.SolverHistory.IsAllowedStep(a));

                                solver.RestoreState(step);
                                Console.WriteLine("The game rolled back to step: " + step);
                                break;
                            }
                    }
                }

                Console.WriteLine("\n\n");

            } while (!isGuessed);

            Console.WriteLine("Pres any key to exit...");

            Console.ReadKey();
        }

        private static void PrintPossibleNumber(List<BullsCows> possible)
        {
            int count = 1;
            var color = 1;

            foreach (var num in possible)
            {
                if (count == 4)
                {
                    Console.WriteLine();
                    count = 1;
                }

                Console.Write($"B: {num.Bulls}   C: {num.Cows}        ", Console.ForegroundColor = (ConsoleColor)color);
                Console.ResetColor();
                count++;
                color++;
                if (color > 14)
                {
                    color = 1;
                }
            }

            Console.WriteLine();
        }

        private static void Solver_Guessed(object obj, GuessedNumberEventArgs guessedNumberArgs)
        {
            Console.WriteLine($"Your number is {guessedNumberArgs.Number}" +
                $"\nIt took me {guessedNumberArgs.Steps} steps to guess it!");
            isGuessed = true;
        }

        private static int ReadNumber(string prompt, string errorMsg, Func<int, bool> condition)
        {
            int num = 0;
            do
            {
                Console.Write(prompt);
                var isCorrectAnswer = int.TryParse(Console.ReadLine(), out num) && condition(num);
                if (isCorrectAnswer)
                    break;

                Console.WriteLine(errorMsg);
            } while (true);

            return num;
        }

        private static string ReadString(string prompt, string errorMsg, Func<string, bool> condition)
        {
            var input = "";
            do
            {
                input = Console.ReadLine();
                Console.Write(prompt);
                var isCorrectAnswer = condition(input);
                if (isCorrectAnswer)
                    break;

                Console.WriteLine(errorMsg);
            } while (true);

            return input;
        }
    }
}

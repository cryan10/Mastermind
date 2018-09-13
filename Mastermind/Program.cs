using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******** MasterMind ********");
            Console.WriteLine("");
            Console.WriteLine("** Instructions **");
            Console.WriteLine("The goal of this game is to guess my secret code. The code is four digits between 1 and 6. I will give you hints on your progress. A plus (+) sign means you guessed a number in the right spot and right position. A minus (-) sign means you guessed a correct number, but in the wrong position. Plus signs are always listed first. The order of the signs do not indicate anything. You have 10 chances to guess the code. Let's play!");
            Console.WriteLine("");
            Console.Write("Type the letter \"Y\" to begin the game: ");
            string gameOrHowTo = Console.ReadLine().ToUpper();

            //error handling to check if user entered anything other than y
            while (gameOrHowTo != "Y")
            {
                Console.Write("Please type \"Y\" to begin the game: ");
                gameOrHowTo = Console.ReadLine().ToUpper();
            }

            if (gameOrHowTo == "Y")
            {
                do
                {
                    PlayGame();
                    Console.WriteLine("Type \"Y\" to play again: ");
                }

                while (Console.ReadLine().ToUpper() == "Y");
            }

        }


        private static void PlayGame()
        {
            bool gameWon = false;
            int numberOfAttempts = 10;
            int numberOfDigitsInSecret = 4;

            Console.WriteLine("Remember, my secret code is 4 digits between 1 and 6.");

            //hard-coded 4 into parameter here per requirements. 
            //If I were to enhance this code, I could allow the user to choose the length of the code to make this game more challenging.
            int[] codeArray = GetCodeDigits(numberOfDigitsInSecret);


            for (int i = numberOfAttempts; numberOfAttempts > 0; numberOfAttempts--)
            {
                Console.Write("Enter your guess ({0} guesses remaining): ", numberOfAttempts);
                string userInput = Console.ReadLine();

                while (userInput.Length != 4)
                {
                    Console.Write("Your guess must be 4 digits in length. Try again: ");
                    userInput = Console.ReadLine();
                }

                int[] userGuess = GetUserGuess(userInput);

                string feedback = GetAttemptFeedback(userGuess, codeArray);

                if (feedback == "++++")
                {
                    gameWon = true;
                    break;
                }
            }

            if (gameWon == true)
            {
                Console.WriteLine("Congratulations! You win!");
            }
            else
            {
                Console.WriteLine("Your guesses are up! You didn't crack the code. The code is: ");
                for (int i = 0; i < codeArray.Length; i++)
                {
                    Console.Write(codeArray[i]);
                }
                Console.WriteLine();
            }
        }

        public static int[] GetUserGuess(string userInput)
        {
            int[] userInputArray = new int[userInput.Length];

            while (!int.TryParse(userInput, out int userInputAsInt))
            {
                Console.Write("Invalid character in code. Try again: ");
                userInput = Console.ReadLine();
            }

            while (userInput.Contains("7") || userInput.Contains("8") || userInput.Contains("9") || userInput.Contains("0"))
            {
                Console.Write("Each digit must be between the range of 1 and 6. Try again: ");
                userInput = Console.ReadLine();
            }

            for (int i = 0; i < userInput.Length; i++)
            {
                userInputArray[i] = int.Parse(userInput[i].ToString());
            }

            return userInputArray;
        }

        public static int[] GetCodeDigits(int numberOfDigits)
        {
            int digit;
            Random randomNumber = new Random();
            int[] secretCode = new int[numberOfDigits];

            for (int i = 0; i < secretCode.Length; i++)
            {
                digit = randomNumber.Next(1, 7);
                secretCode[i] = digit;
            }

            return secretCode;
        }

        public static string GetAttemptFeedback(int[] userGuess, int[] secretCode)
        {
            List<string> plusSign = new List<string>();
            List<string> minusSign = new List<string>();

            for (int i = 0; i < userGuess.Length; i++)
            {
                if (userGuess[i] == secretCode[i])
                {
                    plusSign.Add("+");
                }
                else
                {
                    for (int x = 0; x < secretCode.Length; x++)
                    {
                        if (userGuess[i] == secretCode[x])
                        {
                            minusSign.Add("-");
                            break;
                        }
                    }
                }
            }

            string plusSignResult = string.Join("", plusSign.ToArray());
            string minusSignResult = string.Join("", minusSign.ToArray());
            string guessResult = plusSignResult + minusSignResult;

            Console.WriteLine("Here's how you did: {0}", guessResult);

            return guessResult;
        }
    }
}
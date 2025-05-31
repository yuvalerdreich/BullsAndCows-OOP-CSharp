using System;
using System.Collections.Generic;
using System.Text;
using static Ex02.GameLogic;

namespace Ex02
{
    public class GameUI
    {
        private const string k_Quit = "Q";
        private GameLogic m_GameLogic = new GameLogic();

        public string GetQuit 
        {
            get
            {
                return k_Quit;
            }
        }


        public int GetMaxNumOfGuessesFromUser()
        {
            string userInput = null;
            int numOfGuesses = 0;
            bool userInputValidity = false;

            while (!(userInputValidity))
            {
                Console.WriteLine("Please enter the max number of guesses you wish for (should be between 4 and 10)");
                userInput = Console.ReadLine();
                if (userInputValidity == isSyntactlyValidNum(userInput, out numOfGuesses))
                {
                    Console.WriteLine("Syntactically invalid input! Your input must be a number.");
                }
                else if (userInputValidity == m_GameLogic.IsValidRange(numOfGuesses))
                {
                    Console.WriteLine("Semantically invalid input! Your input must be between 4 and 10.");
                }
                else
                {
                    userInputValidity = true;
                }
            }

            return numOfGuesses;
        }

        private bool isSyntactlyValidNum(string i_UserInput, out int o_NumOfGuesses)
        {
            bool checkIfNumber = int.TryParse(i_UserInput, out int number);
            bool isValidNum = true;
            o_NumOfGuesses = number;

            if (!checkIfNumber)
            {
                isValidNum = false;
            }

            return isValidNum;
        }

        public string GetGuessFromUser()
        {
            string guessInputStr = null;
            bool userGuessValidity = false;
            List<GameLogic.eGuessOption> userGuessInputAsGuessList;

            while (guessInputStr != k_Quit && !(userGuessValidity))
            {
                Console.WriteLine("Please type your next guess <A B C D> Or Q to quit");
                guessInputStr = Console.ReadLine();
                userGuessInputAsGuessList = ConvertStringToGuessList(guessInputStr);
                if (guessInputStr == k_Quit)
                {
                    break;
                }
                if (userGuessValidity == isSyntactlyValidGuess(guessInputStr))
                {
                    Console.WriteLine("Syntactically invalid input! Your input must consist of 4 uppercase English letters.");
                }
                else if (userGuessValidity == m_GameLogic.IsSemanticalValidGuess(userGuessInputAsGuessList))
                {
                    Console.WriteLine("Semantically invalid input! Your input must consist of unique chars, without repetition, between A and H.");
                }
                else
                {
                    userGuessValidity = true;
                }
            }

            return guessInputStr;
        }

        private bool isSyntactlyValidGuess(string i_UserInputGuess)
        {
            bool isValidGuess = true;

            if (i_UserInputGuess.Length == m_GameLogic.LengthOfGuess)
            {
                foreach (char guessUnit in i_UserInputGuess)
                {
                    if (!(guessUnit >= 'A' && guessUnit <= 'Z'))
                    {
                        isValidGuess = false;
                        break;
                    }
                }
            }
            else
            {
                isValidGuess= false;
            }

            return isValidGuess;
        }

        public List<GameLogic.eGuessOption> ConvertStringToGuessList(string i_UserInput)
        {
            List<GameLogic.eGuessOption> strAsList = new List<GameLogic.eGuessOption>();

            foreach (char c in i_UserInput)
            {
                strAsList.Add((GameLogic.eGuessOption)(c - 'A'));
            }

            return strAsList;
        }

        public string ConvertGuessListToString(List<GameLogic.eGuessOption> i_UserInputAsGuessList)
        {
            StringBuilder guessListAsStr = new StringBuilder();

            foreach (GameLogic.eGuessOption guessUnit in i_UserInputAsGuessList)
            {
                guessListAsStr.Append(guessUnit.ToString());
            }

            return guessListAsStr.ToString();
        }
    }
}


using System;
using System.Collections.Generic;
using Ex02;

namespace Ex02
{
    public class GameUI
    {
        private const string k_Quit = "Q";
        private GameLogic m_GameLogic = new GameLogic();
        private Board m_gameBoard;

        private int getMaxNumOfGuessesFromUser()
        {
            string userInput;
            int numOfGuesses;
            GameLogic.eInputValidity userInputValidity;

            Console.WriteLine("Please enter the max number of guesses you wish for (should be between 4 and 10)");
            userInput = Console.ReadLine();
            userInputValidity = m_GameLogic.IsValidNumOfGuesses(userInput, out numOfGuesses);
            while (userInputValidity != GameLogic.eInputValidity.Valid)
            {
                if (userInputValidity == GameLogic.eInputValidity.SyntaxError)
                {
                    Console.WriteLine("Syntactically invalid input! Your input must be a number.");
                }
                else
                {
                    Console.WriteLine("Semantically invalid input! Your input must be between 4 and 10.");
                }
                userInput = Console.ReadLine();
                userInputValidity = m_GameLogic.IsValidNumOfGuesses(userInput, out numOfGuesses);
            }

            return numOfGuesses;
        }

        private string getGuessFromUser()
        {
            string guessInput;
            GameLogic.eInputValidity userGuessValidity;

            Console.WriteLine("Please type your next guess <A B C D> Or Q to quit");
            guessInput = Console.ReadLine();
            userGuessValidity = m_GameLogic.IsValidGuess(guessInput);
            while (guessInput != k_Quit && userGuessValidity != GameLogic.eInputValidity.Valid)
            {
                if (userGuessValidity == GameLogic.eInputValidity.SyntaxError)
                {
                    Console.WriteLine("Syntactically invalid input! Your input must consist of 4 uppercase English letters.");
                }
                else
                {
                    Console.WriteLine("Semantically invalid input! Your input must consist of unique chars, without repetition, between A and H."); //TODO: 
                }
                guessInput = Console.ReadLine();
                userGuessValidity = m_GameLogic.IsValidGuess(guessInput);
            }

            return guessInput;
        }

        public void RunGame()
        {
            int maxNumOfGuesses, currGuessIdx = 0;
            string currGuess, processedFeedback;
            List<GameLogic.eCharGuessState> feedbackList;
            GameLogic.eGameStatus gameStatus;

            maxNumOfGuesses = getMaxNumOfGuessesFromUser();
            m_gameBoard = new Board(maxNumOfGuesses + 1);
            currGuess = getGuessFromUser();
            while (currGuess != k_Quit)
            {
                currGuessIdx++;
                feedbackList = m_GameLogic.ProcessUserGuess(currGuess);
                processedFeedback = m_gameBoard.ProcessResult(feedbackList);
                m_gameBoard.UpdatePinsAndResult(currGuess, processedFeedback);
                m_gameBoard.DisplayBoard();
                gameStatus = m_GameLogic.CheckIfWonOrLost(feedbackList, currGuessIdx, maxNumOfGuesses);
                if (gameStatus == GameLogic.eGameStatus.Win)
                {
                    m_gameBoard.NoMoreGuessesDisplay(m_GameLogic.ComputerGeneratedCharsStr);
                    Console.WriteLine(String.Format("You guessed after {0} steps!", currGuessIdx));
                    startNewGame();
                    break;
                }
                else if (gameStatus == GameLogic.eGameStatus.Loss)
                {
                    m_gameBoard.NoMoreGuessesDisplay(m_GameLogic.ComputerGeneratedCharsStr);
                    Console.WriteLine("No more guesses allowed. You lost.");
                    startNewGame();
                    break;
                }

                currGuess = getGuessFromUser();
            }

            m_gameBoard.NoMoreGuessesDisplay(m_GameLogic.ComputerGeneratedCharsStr);
        }

        private void startNewGame()
        {
            string userAnswerForNewGame;

            Console.WriteLine("Would you like to start a new game? <Y/N>");
            userAnswerForNewGame = Console.ReadLine();
            if (userAnswerForNewGame == "Y")
            {
                Console.Clear();
                RunGame();
            }
            else
            {
                Console.WriteLine("Bye!");
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class GameExecutor
    {
        private GameUI m_GameUI = new GameUI();
        private GameLogic m_GameLogic = new GameLogic();
        private Board m_GameBoard;

        public void RunGame()
        {
            int maxNumOfGuesses, currGuessIdx = 0;
            string currGuess, processedFeedback;
            List<GameLogic.eGuessUnitState> feedbackList;
            GameLogic.eGameStatus gameStatus;
            List<GameLogic.eGuessOption> currGuessList;

            maxNumOfGuesses = m_GameUI.GetMaxNumOfGuessesFromUser();
            m_GameBoard = new Board(maxNumOfGuesses + 1);
            currGuess = m_GameUI.GetGuessFromUser();
            while (currGuess != m_GameUI.GetQuit)
            {
                currGuessIdx++;
                currGuessList = m_GameUI.ConvertStringToGuessList(currGuess);
                feedbackList = m_GameLogic.ProcessUserGuess(currGuessList);
                processedFeedback = m_GameBoard.ProcessResult(feedbackList);
                m_GameBoard.UpdatePinsAndResult(currGuess, processedFeedback);
                m_GameBoard.DisplayBoard();
                gameStatus = m_GameLogic.CheckIfWonOrLost(feedbackList, currGuessIdx, maxNumOfGuesses);
                if (gameStatus == GameLogic.eGameStatus.Win || gameStatus == GameLogic.eGameStatus.Loss)
                {
                    showWinOrLosePrompt(gameStatus, currGuessIdx);
                    break;
                }

                currGuess = m_GameUI.GetGuessFromUser();
            }
        }

        private void startANewGame()
        {
            string userAnswerForANewGame;

            Console.WriteLine("Would you like to start a new game? <Y/N>");
            userAnswerForANewGame = Console.ReadLine();
            if (userAnswerForANewGame == "Y")
            {
                Console.Clear();
                RunGame();
            }
            else if (userAnswerForANewGame == "N")
            {
                Console.WriteLine("Bye!");
            }
            else
            {
                Console.WriteLine("Wrong input! You shoud press <Y> to continue playing or <N> to exit.");
                startANewGame();
            }
        }

        private void showWinOrLosePrompt(GameLogic.eGameStatus i_GameStatus, int i_LastGuessIdx)
        {
            string computerGeneratedSecretSequenceAsStr = m_GameUI.ConvertGuessListToString(m_GameLogic.ComputerGeneratedSecretSequence);

            m_GameBoard.NoMoreGuessesDisplay(computerGeneratedSecretSequenceAsStr);
            if (i_GameStatus == GameLogic.eGameStatus.Win)
            {
                Console.WriteLine(String.Format("You gueesed after {0} steps!", i_LastGuessIdx));
            }
            else if (i_GameStatus == GameLogic.eGameStatus.Loss)
            {
                Console.WriteLine(String.Format("You reached {0} guesses, which is the max you are allowed. You lost.", i_LastGuessIdx));
            }

            startANewGame();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class GameLogic
    {
        private readonly Random r_Rand;
        private List<eGuessOption> m_ComputerGeneratedSecretSequence; 
        private const int k_LengthOfGuess = 4;

        public GameLogic()
        {
            r_Rand = new Random();
            m_ComputerGeneratedSecretSequence = GenerateComputerInput();

        }

        public enum eGuessUnitState
        {
            Bull,
            Cow
        }

        public enum eGameStatus
        {
            Win,
            Loss,
            Pending
        }

        public enum eGuessOption
        {
            A, B, C, D, E, F, G, H
        }

        public List<eGuessOption> ComputerGeneratedSecretSequence
        {
            get
            {
                return m_ComputerGeneratedSecretSequence;
            }
        }

        public int LengthOfGuess
        {
            get
            {
                return k_LengthOfGuess;
            }
        }

        public List<eGuessOption> GenerateComputerInput()
        {
            List<eGuessOption> genratedSecretSequence = new List<eGuessOption>();
            eGuessOption randomGuess;

            while (genratedSecretSequence.Count < k_LengthOfGuess)
            {
                do
                {
                    randomGuess = (eGuessOption)(r_Rand.Next(0, 8));
                } while (genratedSecretSequence.Contains(randomGuess));

                genratedSecretSequence.Add(randomGuess);
            }

            return genratedSecretSequence;
        }

        public bool IsValidRange(int i_NumOfGuesses)
        {
            bool isValid = true;

            if (!(i_NumOfGuesses >= 4 && i_NumOfGuesses <= 10))
            {
                isValid = false;
            }

            return isValid;
        }

        public bool IsSemanticalValidGuess(List<eGuessOption> i_UserInputAsGuessList)
        {
            bool isSemanticalValid = true;
            List <eGuessOption> arrOfAllCaseOptions = new List<eGuessOption> { eGuessOption.A, eGuessOption.B, eGuessOption.C, eGuessOption.D, eGuessOption.E, eGuessOption.F, eGuessOption.G, eGuessOption.H };

              foreach (eGuessOption guessUnit in i_UserInputAsGuessList)
              {
                  if (!(arrOfAllCaseOptions.Contains(guessUnit)) || countCharAppearences(i_UserInputAsGuessList, guessUnit) > 1)
                  {
                      isSemanticalValid = false;
                      break;
                  }
              }

            return isSemanticalValid;
        }

        private int countCharAppearences(List<eGuessOption> i_Lst, eGuessOption i_CharToCheck)
        {
            int count = 0;

            foreach (eGuessOption c in i_Lst)
            {
                if (c == i_CharToCheck)
                {
                    count++;
                }
            }

            return count;
        }

        public List<eGuessUnitState> ProcessUserGuess(List<eGuessOption> i_CurrUserGuess)
        {
            List<eGuessUnitState> feedback = new List<eGuessUnitState>();
            int guessUnitIdx;

            foreach (eGuessOption guessUnit in i_CurrUserGuess)
            {
                if (m_ComputerGeneratedSecretSequence.Contains(guessUnit))
                {
                    guessUnitIdx = i_CurrUserGuess.IndexOf(guessUnit);
                    if (m_ComputerGeneratedSecretSequence[guessUnitIdx] == guessUnit)
                    {
                        feedback.Add(eGuessUnitState.Bull);
                    }
                    else
                    {
                        feedback.Add(eGuessUnitState.Cow);
                    }
                }
            }

            return feedback;
        }

        public eGameStatus CheckIfWonOrLost(List<eGuessUnitState> i_GuessFeedback, int i_currGuessIdx, int i_MaxNumOfGuesses)
        {
            eGameStatus gameStatus = eGameStatus.Pending;
            int countBulls = 0;

            foreach (eGuessUnitState charState in i_GuessFeedback)
            {
                if (charState == eGuessUnitState.Bull)
                {
                    countBulls++;
                }
                else 
                {
                    break;
                }
            }

            if (countBulls == k_LengthOfGuess)
            {
                gameStatus = eGameStatus.Win;
            }
            else if (i_currGuessIdx == i_MaxNumOfGuesses) 
            {
                gameStatus = eGameStatus.Loss;
            }

            return gameStatus;
        }
    }
}


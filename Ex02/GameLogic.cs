using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class GameLogic
    {
        private readonly Random r_Rand;
        private string m_ComputerGeneratedCharsStr;
        private const int k_NumOfCharsInGuess = 4;

        public GameLogic()
        {
            r_Rand = new Random();
            m_ComputerGeneratedCharsStr = GenerateComputerInput();
        }

        public enum eInputValidity
        {
            Valid,
            SyntaxError,
            SemanticError
        }

        public enum eCharGuessState
        {
            Bull,
            Cow,
        }

        public enum eGameStatus
        {
            Win,
            Loss,
            Pending
        }

        public string ComputerGeneratedCharsStr
        {
            get
            {
                return m_ComputerGeneratedCharsStr;
            }
        }

        public string GenerateComputerInput()
        {
            StringBuilder genratedChars = new StringBuilder();
            char randomChar;

            for (int i = 0; i < k_NumOfCharsInGuess; i++)
            {
                do
                {
                    randomChar = (char)('A' + r_Rand.Next(0, 8));
                } while (genratedChars.ToString().Contains(randomChar)); // TODO: check if there is a simpler way and maybe not use stringbuilder

                genratedChars.Append(randomChar);
            }

            return genratedChars.ToString();
        }

        public eInputValidity IsValidNumOfGuesses(string i_UserInput, out int o_NumOfGuesses)
        {
            eInputValidity isValid = eInputValidity.Valid;
            bool checkIfNumber = int.TryParse(i_UserInput, out int number);

            o_NumOfGuesses = number;
            if (!checkIfNumber)
            {
                isValid = eInputValidity.SyntaxError;
            }
            else
            {
                if (!(number >= 4 && number <= 10))
                {
                    isValid = eInputValidity.SemanticError;
                }
            }

            return isValid;
        }

        public eInputValidity IsValidGuess(string i_UserInput)
        {
            eInputValidity isValid = eInputValidity.Valid;

            if (i_UserInput.Length == 4)
            {
                foreach (char c in i_UserInput)
                {
                    if (!(c >= 'A' && c <= 'Z'))
                    {
                        isValid = eInputValidity.SyntaxError;
                        break;
                    }
                    else if (c > 'H' || countCharAppearences(i_UserInput, c) > 1)
                    {
                        isValid = eInputValidity.SemanticError;
                        break;
                    }
                }
            }
            else
            {
                isValid = eInputValidity.SyntaxError;
            }

            return isValid;
        }

        private int countCharAppearences(string i_Str, char i_CharToCheck)
        {
            int count = 0;

            foreach (char c in i_Str)
            {
                if (c == i_CharToCheck)
                {
                    count++;
                }
            }

            return count;
        }

        public List<eCharGuessState> ProcessUserGuess(string i_currUserGuess)
        {
            char c;
            List<eCharGuessState> feedback = new List<eCharGuessState>();

            for (int i = 0; i < k_NumOfCharsInGuess; i++)
            {
                c = i_currUserGuess[i];
                if (m_ComputerGeneratedCharsStr.Contains(c))
                {
                    if (m_ComputerGeneratedCharsStr[i] == c)
                    {
                        feedback.Add(eCharGuessState.Bull);
                    }
                    else
                    {
                        feedback.Add(eCharGuessState.Cow);
                    }
                }
            }

            return feedback;
        }

        public eGameStatus CheckIfWonOrLost(List<eCharGuessState> i_GuessFeedback, int i_currGuessIdx, int i_MaxNumOfGuesses)
        {
            eGameStatus gameStatus = eGameStatus.Pending;
            int countBulls = 0;

            foreach (eCharGuessState charState in i_GuessFeedback)
            {
                if (charState == eCharGuessState.Bull)
                {
                    countBulls++;
                }
                else
                {
                    if (i_currGuessIdx == i_MaxNumOfGuesses) 
                    {
                        gameStatus = eGameStatus.Loss;
                    }

                    break;
                }
            }

            if (countBulls == k_NumOfCharsInGuess)
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


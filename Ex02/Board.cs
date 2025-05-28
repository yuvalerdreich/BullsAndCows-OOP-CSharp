using System;
using System.Collections.Generic;
using System.Text;
using Ex02;
using Ex02.ConsoleUtils;

namespace Ex02
{
    public class Board
    {
        private const int k_NunSpacesFirstCol = 9;
        private const int k_NumSpacesSecondCol = 7;
        private string[,] m_BoardArray;
        private int m_nextEmptyRow;
        private int m_NumOfRows;

        public Board(int i_NumOfRows)
        {
            m_NumOfRows = i_NumOfRows;
            m_BoardArray = new string[i_NumOfRows, 2];
            m_nextEmptyRow = 0;
            UpdatePinsAndResult("####", "");
            DisplayBoard();
        }

        public void DisplayBoard()
        {
            string lineSeparator = "|=========|=======|";
            string emptyLine = "|         |       |";

            Console.Clear();
            Console.WriteLine("Current board status");
            Console.WriteLine();
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine(lineSeparator);
            for (int i = 0; i < m_nextEmptyRow; i++)
            {
                Console.WriteLine($"| {m_BoardArray[i, 0]} |{m_BoardArray[i, 1]}|");
                Console.WriteLine(lineSeparator);
            }

            for (int i = m_nextEmptyRow; i < m_NumOfRows; i++)
            {
                Console.WriteLine(emptyLine);
                Console.WriteLine(lineSeparator);
            }
        }

        public void UpdatePinsAndResult(string i_Guess, string i_Result)
        {
            string spacedGuess = string.Join(" ", i_Guess.ToCharArray());
            string spacedResult = string.Join(" ", i_Result.ToCharArray());
            int lengthSpacedResult = spacedResult.Length; // mabye not neccesery

            m_BoardArray[m_nextEmptyRow, 0] = spacedGuess;
            m_BoardArray[m_nextEmptyRow, 1] = spacedResult.PadRight(k_NumSpacesSecondCol);
            m_nextEmptyRow++;
        }

        public string ProcessResult(List<GameLogic.eCharGuessState> i_ResultList)
        {
            StringBuilder sortedResultList = new StringBuilder();

            foreach (GameLogic.eCharGuessState charState in i_ResultList)
            {
                if (charState == GameLogic.eCharGuessState.Bull)
                {
                    sortedResultList.Insert(0, 'V');
                }
                else
                {
                    sortedResultList.Append('X');
                }
            }

            return sortedResultList.ToString();
        }

        public void NoMoreGuessesDisplay(string i_ComputerStr)
        {
            revealComputerInput(i_ComputerStr);
            DisplayBoard();
        }

        private void revealComputerInput(string i_ComputerStr)
        {
            string spacedInput = string.Join(" ", i_ComputerStr.ToCharArray());

            m_BoardArray[0, 0] = spacedInput;
        }
    }
}


using System;
using System.Linq;

namespace PigsAndBullsLogic
{
    public class GameLogic
    {
        // Defines
        public const int k_GuessLength = 4;
        private const int k_NumOfOptionalLettersForGuess = 8;
        private const int k_ComputerGuessLine = 0;
        private const int k_NumOfSpacesBetweenLetters = 1;
        private const char k_LetterStartRange = 'A';
        private const char k_EmptyCell = ' ';
        private const char k_ComputerGuessCell = '#';
        private const char k_Bull = 'V';
        private const char k_Pig = 'X';
        private const char k_Quit = 'Q';

        // Data members
        private readonly Random r_Random;
        private GameBoard m_GameBoard;
        private char[] m_ComputerRandomGuess = new char[k_GuessLength];
        private int m_MaxNumOfGuesses;
        private int m_CurrentGuessNum = 0;
        private int m_NumOfBullsInGuess = 0;
        private int m_NumOfPigsInGuess = 0;
        private bool m_IsPlayerWon = false;
        private bool m_IsPlayerLost = false;
        private bool m_IsPlayerChooseToQuit = false;

        public GameLogic()
        {
            r_Random = new Random();
        }

        public void GameManager(char[] i_UserGuess)
        {
            if (!isPlayerChooseToQuit(i_UserGuess))
            {
                countPigsAndBullsInGuess(i_UserGuess);
                updateGameBoard(i_UserGuess);
                if (m_IsPlayerLost)
                {
                    revealComputerSelection();
                }
            }
        }

        public void InitializeGame(int i_MaxNumOfGuesses)
        {
            m_GameBoard = new GameBoard(i_MaxNumOfGuesses, k_GuessLength * 2);
            m_MaxNumOfGuesses = i_MaxNumOfGuesses;
            m_CurrentGuessNum = 0;
            m_IsPlayerWon = false;
            m_IsPlayerLost = false;
            m_IsPlayerChooseToQuit = false;

            chooseComputerRandomGuess();
            initializeGameBoard();
        }

        private void initializeGameBoard()
        {
            for (int column = 0; column < k_GuessLength; column++) // Set computer guess line
            {
                m_GameBoard.SetGuessBoard(k_ComputerGuessLine, column, k_ComputerGuessCell);
                m_GameBoard.SetScoreBoard(k_ComputerGuessLine, column, k_EmptyCell);
            }
            for (int row = (k_ComputerGuessLine + 1); row < m_MaxNumOfGuesses; row++) // Set rest of the game board
            {
                for (int column = 0; column < k_GuessLength; column++)
                {
                    m_GameBoard.SetGuessBoard(row, column, k_EmptyCell);
                    m_GameBoard.SetScoreBoard(row, column, k_EmptyCell);
                }
            }
        }

        private void updateGameBoard(char[] i_UserGuess)
        {
            int userGuessIndex;

            for (int column = 0; column < k_GuessLength; column++) // Set user guess
            {
                userGuessIndex = (k_NumOfSpacesBetweenLetters + 1) * column;
                m_GameBoard.SetGuessBoard(m_CurrentGuessNum, column, i_UserGuess[userGuessIndex]);
            }
            for (int column = 0; column < m_NumOfBullsInGuess; column++) // Set user score
            {
                m_GameBoard.SetScoreBoard(m_CurrentGuessNum, column, k_Bull);
            }
            for (int column = m_NumOfBullsInGuess; column < (m_NumOfBullsInGuess + m_NumOfPigsInGuess); column++)
            {
                m_GameBoard.SetScoreBoard(m_CurrentGuessNum, column, k_Pig);
            }
            for (int column = (m_NumOfBullsInGuess + m_NumOfPigsInGuess); column < k_GuessLength; column++)
            {
                m_GameBoard.SetScoreBoard(m_CurrentGuessNum, column, k_EmptyCell);
            }
        }

        private void chooseComputerRandomGuess()
        {
            int randomNumber;
            int[] allOptionalLettersBucket = new int[k_NumOfOptionalLettersForGuess];

            for (int i = 0; i < k_GuessLength; i++)
            {
                randomNumber = r_Random.Next(0, k_NumOfOptionalLettersForGuess);
                while (allOptionalLettersBucket[randomNumber] > 0)
                {
                    randomNumber = r_Random.Next(0, k_NumOfOptionalLettersForGuess);
                }
                m_ComputerRandomGuess[i] = Convert.ToChar(randomNumber + k_LetterStartRange);
                allOptionalLettersBucket[randomNumber]++;
            }
        }

        private void revealComputerSelection()
        {
            for (int column = 0; column < k_GuessLength; column++)
            {
                m_GameBoard.SetGuessBoard(k_ComputerGuessLine, column, m_ComputerRandomGuess[column]);
            }
        }

        private void countPigsAndBullsInGuess(char[] i_UserGuess)
        {
            int userGuessIndex;

            m_CurrentGuessNum++;
            m_NumOfBullsInGuess = 0;
            m_NumOfPigsInGuess = 0;
            for (int i = 0; i < k_GuessLength; i++)
            {
                userGuessIndex = (k_NumOfSpacesBetweenLetters + 1) * i;
                if (m_ComputerRandomGuess.Contains(i_UserGuess[userGuessIndex]))
                {
                    if (m_ComputerRandomGuess[i].Equals(i_UserGuess[userGuessIndex]))
                    {
                        m_NumOfBullsInGuess++;
                    }
                    else
                    {
                        m_NumOfPigsInGuess++;
                    }
                }
            }

            checkIfPlayerWon();
            checkIfPlayerLost();
        }

        private void checkIfPlayerLost()
        {
            if (m_CurrentGuessNum == m_MaxNumOfGuesses && !m_IsPlayerWon)
            {
                m_IsPlayerLost = true;
            }
        }

        private void checkIfPlayerWon()
        {
            if (m_NumOfBullsInGuess.Equals(k_GuessLength))
            {
                m_IsPlayerWon = true;
            }
        }

        private bool isPlayerChooseToQuit(char[] io_UserGuess)
        {
            if ((io_UserGuess.Length == 1) && (io_UserGuess[0].Equals(k_Quit)))
            {
                m_IsPlayerChooseToQuit = true;
            }

            return m_IsPlayerChooseToQuit;
        }

        public bool IsGameOver()
        {
            bool isGameOver = false;

            if ((m_IsPlayerChooseToQuit) || (m_IsPlayerLost) || (m_IsPlayerWon))
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        // Properties
        public GameBoard GameBoard
        {
            get { return m_GameBoard; }
        }

        public int MaxNumOfGuesses
        {
            get { return m_MaxNumOfGuesses; }
            set { m_MaxNumOfGuesses = value; }
        }

        public int CurrentGuessNum
        {
            get { return m_CurrentGuessNum; }
        }

        public bool IsPlayerWon
        {
            get { return m_IsPlayerWon; }
        }

        public bool IsPlayerLost
        {
            get { return m_IsPlayerLost; }
        }

        public bool IsPlayerChooseToQuit
        {
            get { return m_IsPlayerChooseToQuit; }
        }

        public char[] ComputerRandomGuess
        {
            get { return m_ComputerRandomGuess; }
        }

        public int NumOfBullsInGuess
        {
            get { return m_NumOfBullsInGuess; }
        }

        public int NumOfPigsInGuess
        {
            get { return m_NumOfPigsInGuess; }
        }
    }
}

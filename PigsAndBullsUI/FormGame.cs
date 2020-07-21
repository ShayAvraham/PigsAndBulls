using System;
using System.Drawing;
using System.Windows.Forms;
using PigsAndBullsLogic;

namespace PigsAndBullsUI
{
    public partial class FormGame : Form
    {
        // Defines
        // Match colors and letters
        private const char k_MediumOrchidColor = 'A';
        private const char k_RedColor = 'B';
        private const char k_LimeColor = 'C';
        private const char k_AquaColor = 'D';
        private const char k_BlueColor = 'E';
        private const char k_YellowColor = 'F';
        private const char k_MaroonColor = 'G';
        private const char k_WhiteColor = 'H';

        // User guess char array
        private const char k_EmptyCell = ' ';

        // Buttons position
        private const int k_ComputerAndUserGuessButtonSize = 45;
        private const int k_ScoreBoardButtonSize = 20;
        private const int k_SubmitGuessButtonWidth = 50;
        private const int k_SubmitGuessButtonHeight = 23;
        private const int k_StartPosition = 12;

        // Data members
        private readonly GameLogic r_GameLogic;
        private readonly FormStartMenu r_FormStartMenu;
        private readonly FormPickColor r_FormPickColor;
        private readonly Button[] r_ComputerGuessButtons;
        private readonly Button[,] r_UserGuessButtons;
        private readonly Button[] r_SubmitGuessButtons;
        private readonly Button[,] r_ScoreBoardButtons;
        private readonly int r_NumOfGuesses;


        public FormGame()
        {
            InitializeComponent();
            r_FormStartMenu = new FormStartMenu();
            r_FormStartMenu.ShowDialog();
            if (r_FormStartMenu.DialogResult == DialogResult.OK)
            {
                r_GameLogic = new GameLogic();
                r_FormPickColor = new FormPickColor();
                r_NumOfGuesses = r_FormStartMenu.NumOfGuesses;
                r_ComputerGuessButtons = new Button[GameLogic.k_GuessLength];
                r_UserGuessButtons = new Button[r_NumOfGuesses, GameLogic.k_GuessLength];
                r_SubmitGuessButtons = new Button[r_NumOfGuesses];
                r_ScoreBoardButtons = new Button[r_NumOfGuesses, GameLogic.k_GuessLength];
                Run();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public void Run()
        {
            r_GameLogic.InitializeGame(r_NumOfGuesses);
            buildBoard();
            setFormGameSize();
            enableOrDisableUserGuessButtons(r_GameLogic.CurrentGuessNum, true);
        }

        private void buildBoard()
        {
            Point buttonLocation = new Point(k_StartPosition, k_StartPosition);

            createLineOfComputerGuessButtons(ref buttonLocation);
            for (int i = 0; i < r_NumOfGuesses; i++)
            {
                createLineOfUserGuessButtons(i, ref buttonLocation);
                createSubmitGuessButton(i, buttonLocation.X, buttonLocation.Y + k_ComputerAndUserGuessButtonSize / 4);
                buttonLocation.X += k_SubmitGuessButtonWidth + 5;
                createLineOfScoreBoardButtons(i, ref buttonLocation);
                buttonLocation.X = k_StartPosition;
                buttonLocation.Y += k_ScoreBoardButtonSize + 10;
            }
        }

        private void createLineOfComputerGuessButtons(ref Point io_ButtonLocation)
        {
            for (int i = 0; i < GameLogic.k_GuessLength; i++)
            {
                createComputerGuessButton(i, io_ButtonLocation.X, io_ButtonLocation.Y);
                io_ButtonLocation.X += k_ComputerAndUserGuessButtonSize + 5;
            }

            io_ButtonLocation.X = k_StartPosition;
            io_ButtonLocation.Y += k_ComputerAndUserGuessButtonSize + 20;
        }

        private void createLineOfUserGuessButtons(int i_Row, ref Point io_ButtonLocation)
        {
            for (int column = 0; column < GameLogic.k_GuessLength; column++)
            {
                createUserGuessButton(i_Row, column, io_ButtonLocation.X, io_ButtonLocation.Y);
                io_ButtonLocation.X += k_ComputerAndUserGuessButtonSize + 5;
            }
        }

        private void createLineOfScoreBoardButtons(int i_Row, ref Point io_ButtonLocation)
        {
            int firstButtonLocationX = io_ButtonLocation.X;
            int column = 0;

            for (; column < GameLogic.k_GuessLength / 2; column++)
            {
                createScoreBoardButtons(i_Row, column, io_ButtonLocation.X, io_ButtonLocation.Y);
                io_ButtonLocation.X += k_ScoreBoardButtonSize + 2;
            }

            io_ButtonLocation.X = firstButtonLocationX;
            io_ButtonLocation.Y += k_ScoreBoardButtonSize + 5;
            for (; column < GameLogic.k_GuessLength; column++)
            {
                createScoreBoardButtons(i_Row, column, io_ButtonLocation.X, io_ButtonLocation.Y);
                io_ButtonLocation.X += k_ScoreBoardButtonSize + 2;
            }
        }

        private void createComputerGuessButton(int i_Index, int i_LocationX, int i_LocationY)
        {
            r_ComputerGuessButtons[i_Index] = new Button();
            r_ComputerGuessButtons[i_Index].Name = string.Format("buttonComputerGuess{0}", i_Index);
            r_ComputerGuessButtons[i_Index].Size = new Size(k_ComputerAndUserGuessButtonSize, k_ComputerAndUserGuessButtonSize);
            r_ComputerGuessButtons[i_Index].Location = new Point(i_LocationX, i_LocationY);
            r_ComputerGuessButtons[i_Index].BackColor = Color.Black;
            r_ComputerGuessButtons[i_Index].FlatStyle = FlatStyle.Flat;
            r_ComputerGuessButtons[i_Index].TabStop = false;
            r_ComputerGuessButtons[i_Index].Enabled = false;
            this.Controls.Add(r_ComputerGuessButtons[i_Index]);
        }

        private void createUserGuessButton(int i_Row, int i_Column, int i_LocationX, int i_LocationY)
        {
            r_UserGuessButtons[i_Row, i_Column] = new Button();
            r_UserGuessButtons[i_Row, i_Column].Name = string.Format("buttonUserGuess{0},{1}", i_Row, i_Column);
            r_UserGuessButtons[i_Row, i_Column].Size = new Size(k_ComputerAndUserGuessButtonSize, k_ComputerAndUserGuessButtonSize);
            r_UserGuessButtons[i_Row, i_Column].Location = new Point(i_LocationX, i_LocationY);
            r_UserGuessButtons[i_Row, i_Column].FlatStyle = FlatStyle.Flat;
            r_UserGuessButtons[i_Row, i_Column].TabStop = false;
            r_UserGuessButtons[i_Row, i_Column].Enabled = false;
            r_UserGuessButtons[i_Row, i_Column].Click += buttonUserGuess_Click;
            this.Controls.Add(r_UserGuessButtons[i_Row, i_Column]);
        }

        private void createSubmitGuessButton(int i_Index, int i_LocationX, int i_LocationY)
        {
            r_SubmitGuessButtons[i_Index] = new Button();
            r_SubmitGuessButtons[i_Index].Name = string.Format("buttonSubmitGuess{0}", i_Index);
            r_SubmitGuessButtons[i_Index].Text = "->>";
            r_SubmitGuessButtons[i_Index].Size = new Size(k_SubmitGuessButtonWidth, k_SubmitGuessButtonHeight);
            r_SubmitGuessButtons[i_Index].Location = new Point(i_LocationX, i_LocationY);
            r_SubmitGuessButtons[i_Index].FlatStyle = FlatStyle.Flat;
            r_SubmitGuessButtons[i_Index].TabStop = false;
            r_SubmitGuessButtons[i_Index].Enabled = false;
            r_SubmitGuessButtons[i_Index].Click += buttonSubmitGuess_Click;
            this.Controls.Add(r_SubmitGuessButtons[i_Index]);
        }

        private void createScoreBoardButtons(int i_Row, int i_Column, int i_LocationX, int i_LocationY)
        {
            r_ScoreBoardButtons[i_Row, i_Column] = new Button();
            r_ScoreBoardButtons[i_Row, i_Column].Name = string.Format("buttonSocreBoard{0},{1}", i_Row, i_Column);
            r_ScoreBoardButtons[i_Row, i_Column].Size = new Size(k_ScoreBoardButtonSize, k_ScoreBoardButtonSize);
            r_ScoreBoardButtons[i_Row, i_Column].Location = new Point(i_LocationX, i_LocationY);
            r_ScoreBoardButtons[i_Row, i_Column].FlatStyle = FlatStyle.Flat;
            r_ScoreBoardButtons[i_Row, i_Column].TabStop = false;
            r_ScoreBoardButtons[i_Row, i_Column].Enabled = false;
            this.Controls.Add(r_ScoreBoardButtons[i_Row, i_Column]);
        }

        private char[] createGuessCharArrayFromUserGuessButtonColor(int i_Row)
        {
            char[] guessInput = new char[GameLogic.k_GuessLength * 2];

            setSpacesInGuessCharArray(ref guessInput);
            setLettersInGuessCharArray(ref guessInput, i_Row);

            return guessInput;
        }

        private void setSpacesInGuessCharArray(ref char[] io_GuessCharArry)
        {
            for (int i = 1; i < io_GuessCharArry.Length; i += 2)
            {
                io_GuessCharArry[i] = k_EmptyCell;
            }
        }

        private void setLettersInGuessCharArray(ref char[] io_GuessCharArry, int i_Row)
        {
            int userGuessLetterIndex;

            for (int column = 0; column < GameLogic.k_GuessLength; column++)
            {
                userGuessLetterIndex = column * 2;
                switch (r_UserGuessButtons[i_Row, column].BackColor.Name.ToString())
                {
                    case "MediumOrchid":
                        io_GuessCharArry[userGuessLetterIndex] = k_MediumOrchidColor;
                        break;
                    case "Red":
                        io_GuessCharArry[userGuessLetterIndex] = k_RedColor;
                        break;
                    case "Lime":
                        io_GuessCharArry[userGuessLetterIndex] = k_LimeColor;
                        break;
                    case "Aqua":
                        io_GuessCharArry[userGuessLetterIndex] = k_AquaColor;
                        break;
                    case "Blue":
                        io_GuessCharArry[userGuessLetterIndex] = k_BlueColor;
                        break;
                    case "Yellow":
                        io_GuessCharArry[userGuessLetterIndex] = k_YellowColor;
                        break;
                    case "Maroon":
                        io_GuessCharArry[userGuessLetterIndex] = k_MaroonColor;
                        break;
                    case "White":
                        io_GuessCharArry[userGuessLetterIndex] = k_WhiteColor;
                        break;
                }
            }
        }

        private void setFormGameSize()
        {
            int newWidth = this.Size.Width + 10;
            int newHeight = this.Size.Height + 10;

            this.AutoSize = false;
            this.Size = new Size(newWidth, newHeight);
        }

        private void enableOrDisableUserGuessButtons(int i_Row, bool i_ToEnableButton)
        {
            for (int column = 0; column < GameLogic.k_GuessLength; column++)
            {
                r_UserGuessButtons[i_Row, column].Enabled = i_ToEnableButton;
                r_UserGuessButtons[i_Row, column].TabStop = i_ToEnableButton;
            }

            r_UserGuessButtons[i_Row, 0].Focus(); 
        }

        private void setSubmitGuessButtonEnableOption(Button i_Button, int i_ButtonRow)
        {
            bool setSubmitGuessButtonEnable = false;

            if (isAllGuessButtonsInLineSelected(i_ButtonRow) && isAllGuessButtonsinLineAreWithDiffrentColor(i_ButtonRow))
            {
                setSubmitGuessButtonEnable = true;
            }

            r_SubmitGuessButtons[i_ButtonRow].Enabled = setSubmitGuessButtonEnable;
            r_SubmitGuessButtons[i_ButtonRow].TabStop = setSubmitGuessButtonEnable;
        }

        private bool isAllGuessButtonsInLineSelected(int i_ButtonRow)
        {
            bool AllGuessButtonsInLineSelected = true;

            for (int j = 0; j < GameLogic.k_GuessLength; j++)
            {
                if (r_UserGuessButtons[i_ButtonRow, j].BackColor == new Button().BackColor)
                {
                    AllGuessButtonsInLineSelected = false;
                    break;
                }
            }

            return AllGuessButtonsInLineSelected;
        }

        private bool isAllGuessButtonsinLineAreWithDiffrentColor(int i_Row)
        {
            bool isAllGuessButtonsinLineAreWithDiffrentColor = true;

            for (int j = 0; j < GameLogic.k_GuessLength; j++)
            {
                for (int k = j + 1; k < GameLogic.k_GuessLength; k++)
                {
                    if (r_UserGuessButtons[i_Row, j].BackColor == r_UserGuessButtons[i_Row, k].BackColor)
                    {
                        isAllGuessButtonsinLineAreWithDiffrentColor = false;
                        break;
                    }
                }
            }

            return isAllGuessButtonsinLineAreWithDiffrentColor;
        }

        private void revealComputerGuess()
        {
            int row = 0;

            foreach (char charGuess in r_GameLogic.ComputerRandomGuess)
            {
                switch (charGuess)
                {
                    case (k_MediumOrchidColor):
                        r_ComputerGuessButtons[row].BackColor = Color.MediumOrchid;
                        break;
                    case (k_RedColor):
                        r_ComputerGuessButtons[row].BackColor = Color.Red;
                        break;
                    case (k_LimeColor):
                        r_ComputerGuessButtons[row].BackColor = Color.Lime;
                        break;
                    case (k_AquaColor):
                        r_ComputerGuessButtons[row].BackColor = Color.Aqua;
                        break;
                    case (k_BlueColor):
                        r_ComputerGuessButtons[row].BackColor = Color.Blue;
                        break;
                    case (k_YellowColor):
                        r_ComputerGuessButtons[row].BackColor = Color.Yellow;
                        break;
                    case (k_MaroonColor):
                        r_ComputerGuessButtons[row].BackColor = Color.Maroon;
                        break;
                    case (k_WhiteColor):
                        r_ComputerGuessButtons[row].BackColor = Color.White;
                        break;
                }

                row++;
            }
        }

        private void updateScoreBoardButtonsBackgroundColor(int i_Row)
        {
            int column = 0;

            for (int i = 0; i < r_GameLogic.NumOfBullsInGuess; i++, column++)
            {
                r_ScoreBoardButtons[i_Row, column].BackColor = Color.Black;
            }

            for (int i = 0; i < r_GameLogic.NumOfPigsInGuess; i++, column++)
            {
                r_ScoreBoardButtons[i_Row, column].BackColor = Color.Yellow;
            }
        }

        private void buttonUserGuess_Click(object sender, EventArgs e)
        {
            Button guessButton = sender as Button;
            int buttonRow = r_GameLogic.CurrentGuessNum;
            r_FormPickColor.Location = new Point(this.Location.X - r_FormPickColor.Size.Width, this.Location.Y + guessButton.Location.Y);
            r_FormPickColor.ShowDialog();
            guessButton.BackColor = r_FormPickColor.LastChosenColor;

            setSubmitGuessButtonEnableOption(guessButton, buttonRow);
        }

        private void buttonSubmitGuess_Click(object sender, EventArgs e)
        {
            Button guessButton = sender as Button;
            int buttonRow = r_GameLogic.CurrentGuessNum;
            char[] guessInput = createGuessCharArrayFromUserGuessButtonColor(buttonRow);

            enableOrDisableUserGuessButtons(buttonRow, false);
            r_GameLogic.GameManager(guessInput);
            guessButton.Enabled = false;
            updateScoreBoardButtonsBackgroundColor(buttonRow);
            if (!r_GameLogic.IsGameOver())
            {
                buttonRow++;
                enableOrDisableUserGuessButtons(buttonRow, true);
            }
            else
            {
                revealComputerGuess();
            }
        }
    }
}

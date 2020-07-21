using System;
using System.Windows.Forms;

namespace PigsAndBullsUI
{
    public partial class FormStartMenu : Form
    {
        // Defines
        private const string k_ButtonNumOfChancesText = "Number of chances: {0}";
        private const int k_MaxNumOfGuesses = 8;
        private const int k_MinNumOfGuesses = 4;

        // Data members
        private int m_MaxNumOfGuesses;

        // Properties
        public int NumOfGuesses
        {
            get { return m_MaxNumOfGuesses; }
        }

        public FormStartMenu()
        {
            m_MaxNumOfGuesses = k_MinNumOfGuesses;
            InitializeComponent();
        }

        private void buttonNumOfChances_Click(object sender, EventArgs e)
        {
            m_MaxNumOfGuesses++;
            if (m_MaxNumOfGuesses > k_MaxNumOfGuesses)
            {
                m_MaxNumOfGuesses = k_MinNumOfGuesses;
            }

            buttonNumOfChances.Text = string.Format(k_ButtonNumOfChancesText, m_MaxNumOfGuesses);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

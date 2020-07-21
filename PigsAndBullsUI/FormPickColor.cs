using System;
using System.Drawing;
using System.Windows.Forms;

namespace PigsAndBullsUI
{
    public partial class FormPickColor : Form
    {
        // Data members
        private Color m_LastChosenColor;

        // Properties
        public Color LastChosenColor
        {
            get { return m_LastChosenColor; }
        }

        public FormPickColor()
        {
            InitializeComponent();
            foreach(Button buttonColor in this.Controls)
            {
                buttonColor.Click += buttonColor_Click;
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            Button colorButton = sender as Button;

            if (colorButton != null)
            {
                m_LastChosenColor = colorButton.BackColor;
            }

            Close();
        }
    }
}

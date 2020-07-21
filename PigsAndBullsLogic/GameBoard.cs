namespace PigsAndBullsLogic
{
    public class GameBoard
    {
        // Data members
        private readonly char[,] r_ScoreBoard;
        private readonly char[,] r_GuessBoard;
        private readonly int r_GameBoardLength;
        private readonly int r_GameBoardWidth;

        public GameBoard(int i_Length, int i_Width)
        {
            r_GameBoardLength = i_Length + 1; ;
            r_GameBoardWidth = i_Width;
            r_ScoreBoard = new char[r_GameBoardLength, i_Width / 2];
            r_GuessBoard = new char[r_GameBoardLength, i_Width / 2];
        }

        // Properties
        public int GameBoardLength
        {
            get { return r_GameBoardLength; }
        }

        public int GameBoardWidth
        {
            get { return r_GameBoardWidth; }
        }

        public char GetScoreBoard(int i_Row, int i_Column)
        {

            return r_ScoreBoard[i_Row, i_Column];
        }

        public char GetGuessBoard(int i_Row, int i_Column)
        {

            return r_GuessBoard[i_Row, i_Column];
        }

        public void SetScoreBoard(int i_Row, int i_Column, char value)
        {
            r_ScoreBoard[i_Row, i_Column] = value;
        }

        public void SetGuessBoard(int i_Row, int i_Column, char value)
        {
            r_GuessBoard[i_Row, i_Column] = value;
        }
    }
}


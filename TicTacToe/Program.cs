using System.ComponentModel;
using System.Xml.Serialization;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        public char currentPlayer { get; private set; } = 'X';
        public byte turn { get; private set; } = 0;

        private byte[,] board = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        private char[,]? currentBoard = new char[3, 3];

        private void ChangeCurrentPlayer()
        {
            if(turn % 2 == 0) currentPlayer = 'X';
            else currentPlayer = 'O';
        }

        public void InputSquare()
        {
            sbyte row = -1;
            sbyte column = -1;

            
            while( (row < 0 || row > 2) || (column < 0 || column > 2) )
            {
                Console.Write("Enter row: ");
                row = Convert.ToSByte(Console.ReadLine());

                if (row < 0 || row > 2)
                {
                    Console.WriteLine("Wrong number entered.");
                }

                Console.Write("Enter column: ");
                column = Convert.ToSByte(Console.ReadLine());

                if (column < 0 || column > 2)
                {
                    Console.WriteLine("Wrong number entered.");
                }
            }

            if(IsEmpty(row, column)) currentBoard[row, column] = currentPlayer;
            else
            {
                Console.WriteLine("That square is already taken.");
                InputSquare();
            }
        }

        private bool IsEmpty(sbyte row, sbyte column)
        {
            if (currentBoard[row, column] == '\0') return true;

            return false;
        }

        private bool CheckWin()
        {
            int xX = 0;
            int oX = 0;
            int xY = 0;
            int oY = 0;

            for (int i = 0; i < currentBoard.GetLength(0); i++)
            {
                if (xX >= 3 || oX >= 3 || xY >= 3 || oY >= 3) return true;

                for (int j = 0; j < currentBoard.GetLength(1); j++)
                {
                    if (currentBoard[i, j] == 'X') xX++;
                    if (currentBoard[i, j] == 'O') oX++;
                }

                if (currentBoard[i, 0] == 'X') xY++;
                if (currentBoard[i, 0] == 'O') oY++;
            }

            return false;
        }

        private bool IsDraw()
        {
            for (int i = 0; i < currentBoard.GetLength(0); i++)
            {
                for (int j = 0; j < currentBoard.GetLength(1); j++)
                {
                    if (currentBoard[i, j] == '\0') return false;
                }
            }

            return true;
        }

        private void DisplayBoard()
        {
            Console.Clear();

            Console.WriteLine($"Current player: {currentPlayer} turn: {turn}");
            Console.WriteLine(" 0   1   2");
            for (int row = 0; row < 3; row++)
            {
                Console.WriteLine("  ---|---|---");
                for (int col = 0; col < 3; col++)
                {
                    if (col > 0)
                        Console.Write("|");
                    Console.Write($" {currentBoard[row, col]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  ---|---|---");
        }

        public void Start()
        {
            while (!CheckWin() || !IsDraw())
            {
                DisplayBoard();
                InputSquare();
                turn++;
                ChangeCurrentPlayer();
            }
            DisplayBoard();
        }
    }
}

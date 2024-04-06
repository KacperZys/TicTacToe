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
            int[][] winConditions = new int[8][];
            winConditions[0] = new int[] { 0, 1, 2 };
            winConditions[1] = new int[] { 3, 4 ,5 };
            winConditions[2] = new int[] { 6, 7, 8 };
            winConditions[3] = new int[] { 0, 3, 6 };
            winConditions[4] = new int[] { 1, 4 ,7 };
            winConditions[5] = new int[] { 2, 5, 8 };
            winConditions[6] = new int[] { 0, 4, 8 };
            winConditions[7] = new int[] { 2, 4, 6 };

            foreach (var condition in winConditions)
            {
                char symbol = currentBoard[condition[0] / 3, condition[0] % 3];
                if (symbol != '\0' &&
                    symbol == currentBoard[condition[1] / 3, condition[1] % 3] &&
                    symbol == currentBoard[condition[2] / 3, condition[2] % 3])
                {
                    return true;
                }
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

           Console.WriteLine($" {(currentBoard[0, 0] != '\0' ? currentBoard[0, 0] : ' ')} | {(currentBoard[0, 1] != '\0' ? currentBoard[0, 1] : ' ')}" +
               $" | {(currentBoard[0, 2] != '\0' ? currentBoard[0, 2] : ' ')} ");
           Console.WriteLine("---+---+---");
           Console.WriteLine($" {(currentBoard[1, 0] != '\0' ? currentBoard[1, 0] : ' ')} | {(currentBoard[1, 1] != '\0' ? currentBoard[1, 1] : ' ')}" +
               $" | {(currentBoard[1, 2] != '\0' ? currentBoard[1, 2] : ' ')} ");
           Console.WriteLine("---+---+---");
           Console.WriteLine($" {(currentBoard[2, 0] != '\0' ? currentBoard[2, 0] : ' ')} | {(currentBoard[2, 1] != '\0' ? currentBoard[2, 1] : ' ')}" +
               $" | {(currentBoard[2, 2] != '\0' ? currentBoard[2, 2] : ' ')} ");
        }

        public void Start()
        {
            while (true)
            {
                DisplayBoard();
                InputSquare();
                if (CheckWin() && !IsDraw()) break;
                turn++;
                ChangeCurrentPlayer();
            }
            DisplayBoard();
        }
    }
}

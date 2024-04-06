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
        GameLogic logic = new GameLogic();
        public char currentPlayer { get; private set; } = 'X';
        public byte turn { get; private set; } = 0;

        public char[,] currentBoard { get; private set; } = new char[3, 3];

        private void ChangeCurrentPlayer()
        {
            if(turn % 2 == 0) currentPlayer = 'X';
            else currentPlayer = 'O';
        }

        public void InputSquare()
        {
            sbyte row = -1;
            sbyte column = -1;

            
            while( (row < 1 || row > 3) || (column < 1 || column > 3) )
            {
                Console.Write("Enter row (1-3): ");
                row = Convert.ToSByte(Console.ReadLine());

                if (row < 1 || row > 3)
                {
                    Console.WriteLine("Wrong number entered.");
                }

                Console.Write("Enter column (1-3): ");
                column = Convert.ToSByte(Console.ReadLine());

                if (column < 1 || column > 3)
                {
                    Console.WriteLine("Wrong number entered.");
                }

                
            }


            row--;
            column--;

            if (!logic.IsEmpty(currentBoard, row, column))
            {
                Console.WriteLine("That square is already taken.");
                InputSquare();
            } else currentBoard[row, column] = currentPlayer;


        }

        

        private void DisplayBoard()
        {

            Console.WriteLine($"Current player: {currentPlayer} turn: {turn + 1}");
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
                Console.Clear();
                DisplayBoard();
                InputSquare();
                if (logic.CheckWin(currentBoard, currentPlayer))
                {
                    Console.Clear();
                    DisplayBoard();
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    break;
                }
                else if (logic.IsDraw(currentBoard))
                {
                    Console.Clear();
                    DisplayBoard();
                    Console.WriteLine("It's a draw!");
                    break;
                }

                turn++;
                ChangeCurrentPlayer();
            }
        }
    }

    class GameLogic
    {
        public bool IsEmpty(char[,] currentBoard, sbyte row, sbyte column)
        {
            if (currentBoard[row, column] == '\0') return true;

            return false;
        }

        public bool CheckWin(char [,] currentBoard, char currentPlayer)
        {
            int[][] winConditions = new int[8][];
            winConditions[0] = new int[] { 0, 1, 2 };
            winConditions[1] = new int[] { 3, 4, 5 };
            winConditions[2] = new int[] { 6, 7, 8 };
            winConditions[3] = new int[] { 0, 3, 6 };
            winConditions[4] = new int[] { 1, 4, 7 };
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

        public bool IsDraw(char[,] currentBoard)
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
    }
}

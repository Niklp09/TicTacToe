using System.Net.Mail;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TicTacToe.");

            string playerOneName = "";
            string playerTwoName = "";

            Console.Write("Player 1, please enter your name: ");
            playerOneName = Console.ReadLine()!;

            Console.Write("Player 2, please enter your name: ");
            playerTwoName = Console.ReadLine()!;

            Console.WriteLine(
                "Player 1 is called "
                    + playerOneName
                    + " (X), Player 2 is called "
                    + playerTwoName
                    + " (0)."
            );
            Console.WriteLine("***Starting game in 2 seconds***");
            System.Threading.Thread.Sleep(2000);

            int[] fields = [0, 0, 0, 0, 0, 0, 0, 0, 0]; //{1, 0, 2, 0, 0, 1, 0, 2, 2};
            string[] idxToChar = ["A", "B", "C"];
            int nextPlayer = 1;
            bool gameRunning = true;

            while (gameRunning)
            {
                string currentPlayerName = playerOneName;
                if (nextPlayer == 2)
                {
                    currentPlayerName = playerTwoName;
                }
                // Draw main "scene"
                Console.Clear();
                Console.WriteLine("\n    1   2   3");
                Console.WriteLine("  -------------");

                int line = 0;
                for (int i = 0; i < 8; i += 3)
                {
                    string[] lineFields = new string[3];

                    for (int idx = 0; idx < 3; idx++)
                    {
                        if (fields[i + idx] == 1)
                        {
                            lineFields[idx] = "X";
                        }
                        else if (fields[i + idx] == 2)
                        {
                            lineFields[idx] = "0";
                        }
                        else
                        {
                            lineFields[idx] = " ";
                        }
                    }

                    Console.WriteLine(
                        idxToChar[line]
                            + " | "
                            + lineFields[0]
                            + " | "
                            + lineFields[1]
                            + " | "
                            + lineFields[2]
                            + " |"
                    );
                    Console.WriteLine("  -------------");
                    line++;
                }

                Console.Write(
                    currentPlayerName
                        + " it's your turn. Select a position by writing 'column,row' (e.g. 'a,1'): "
                );

                // Input handling
                string userInput = Console.ReadLine()!.ToLower();

                string[] parts = userInput
                    .Split(",")
                    .Select(part => part.Trim())
                    .Where(part => !string.IsNullOrWhiteSpace(part))
                    .ToArray();

                if (parts.Length != 2)
                {
                    Console.WriteLine("Bad input, try again!");
                    System.Threading.Thread.Sleep(2000);
                    continue;
                }

                string inputColumn = parts[0];
                int inputRow = int.Parse(parts[1]);

                if (
                    (inputColumn != "a" && inputColumn != "b" && inputColumn != "c")
                    || inputRow < 1
                    || inputRow > 3
                )
                {
                    Console.WriteLine("Bad input, try again!");
                    System.Threading.Thread.Sleep(2000);
                    continue;
                }

                int column = 0;
                if (inputColumn == "b")
                {
                    column = 3;
                }

                if (inputColumn == "c")
                {
                    column = 6;
                }

                int pos = column + (inputRow - 1);
                if (fields[pos] != 0)
                {
                    Console.WriteLine("This position is already used!");
                    System.Threading.Thread.Sleep(2000);
                    continue;
                }

                fields[pos] = nextPlayer;

                // Check for winner
                // All patterns
                int[] list1 = [0, 1, 2];
                int[] list2 = [3, 4, 5];
                int[] list3 = [6, 7, 8];

                int[] list4 = [0, 3, 6];
                int[] list5 = [1, 4, 7];
                int[] list6 = [2, 5, 8];

                int[] list7 = [0, 4, 8];
                int[] list8 = [2, 4, 6];

                int[][] lists = [list1, list2, list3, list4, list5, list6, list7, list8];

                bool gameWon = false;
                foreach (int[] subList in lists)
                {
                    int fieldsFound = 0;

                    for (int idx = 0; idx < 3; idx++)
                    {
                        if (fields[subList[idx]] == nextPlayer)
                            fieldsFound++;
                    }

                    if (fieldsFound == 3)
                        gameWon = true;
                }

                // Check if grid is full
                bool gridIsFull = false;
                int count = 0;
                for (int idx = 0; idx < 9; idx++)
                {
                    if (fields[idx] != 0)
                        count++;
                }

                if (count == 9)
                {
                    gridIsFull = true;
                }
                Console.WriteLine(count);
                Console.WriteLine(gridIsFull);

                // Winner handling
                if (gameWon)
                {
                    Console.WriteLine(
                        "***"
                            + currentPlayerName
                            + " won the game! To play another time, write 'again'. To stop the game, type 'quit'"
                    );
                }

                if (gridIsFull)
                {
                    Console.WriteLine(
                        "*** No more place in this game! To play another time, write 'again'. To stop the game, type 'quit'"
                    );
                }

                if (gameWon || gridIsFull)
                {
                    Console.Write("> ");
                    string wonInput = Console.ReadLine()!.ToLower();

                    if (wonInput != "again" && wonInput != "quit")
                    {
                        Console.WriteLine("Bad input, try again!");
                        System.Threading.Thread.Sleep(2000);
                        continue;
                    }

                    // Reset game
                    if (wonInput == "again")
                    {
                        gameWon = false;
                        nextPlayer = 1;
                        for (int idx = 0; idx < 9; idx++)
                        {
                            fields[idx] = 0;
                        }
                        Console.WriteLine("***Starting game in 2 seconds***");
                        System.Threading.Thread.Sleep(2000);
                    }

                    // Quit game
                    if (wonInput == "quit")
                    {
                        gameRunning = false;
                        Console.WriteLine(
                            "***Quitting game, goodbye "
                                + playerOneName
                                + " and "
                                + playerTwoName
                                + "."
                        );
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                }

                if (nextPlayer == 1)
                {
                    nextPlayer++;
                }
                else
                {
                    nextPlayer--;
                }
                //ch
            }
        }
    }
}

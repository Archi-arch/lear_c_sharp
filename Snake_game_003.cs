using System;
using System.Reflection.Metadata;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 3;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;

InitializeGame();
while (!shouldExit)
{
    if (TerminalResized())
    {
        Console.Clear();
        Console.WriteLine("Terminal was resized. Program exit");
        shouldExit = true;
        continue;
    }
    Move();
}

// Returns true if the Terminal was resized 
bool TerminalResized()
{
    return height != Console.WindowHeight - 3 || width != Console.WindowWidth - 5;
}

// Displays random food at a random location
void ShowFood()
{
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer()
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops the player from moving
void FreezePlayer()
{
    System.Threading.Thread.Sleep(1000);
    player = states[0];
}

// Reads directional input from the Console and moves the player
void Move(bool quitIfInvalid = false)
{
    int lastX = playerX;
    int lastY = playerY;


    int messageRow = height - 1;
    ConsoleKey inputKey = Console.ReadKey(true).Key;

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            playerY--;
            ClearMessage(messageRow);
            break;
        case ConsoleKey.DownArrow:
            playerY++;
            ClearMessage(messageRow);
            break;
        case ConsoleKey.LeftArrow:
            playerX--;
            ClearMessage(messageRow);
            break;
        case ConsoleKey.RightArrow:
            playerX++;
            ClearMessage(messageRow);
            break;
        case ConsoleKey.Escape:
            shouldExit = true;
            ClearMessage(messageRow);
            break;
        default:
            if (quitIfInvalid)
            {
                shouldExit = true;
            }
            else
            {
                Console.SetCursorPosition(0, messageRow);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid key use arrows");
                Console.ResetColor();
            }
            break;

    }

    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++)
    {
        Console.Write(" ");
    }

    // Keep player position within the bounds of the Terminal window
    playerY = (playerY < 0) ? 0 : (playerY >= messageRow - 1 ? messageRow - 1 : playerY);
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

void ClearMessage(int row)
{
    // Перевіряємо, чи ми в межах консолі
    if (row >= 0 && row < Console.BufferHeight)
    {
        Console.SetCursorPosition(0, row);
        // Друкуємо рядок пробілів довжиною на все вікно
        Console.Write(new string(' ', Console.WindowWidth));
    }
}

// Clears the console, displays the food and player
void InitializeGame()
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}
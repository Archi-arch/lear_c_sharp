// using System;
using System.Reflection.Metadata;

Random random = new Random();
Console.CursorVisible = false;

int topDividerRow = 1;
int bottomDividerRow = Console.WindowHeight - 2;
int messageRow = bottomDividerRow + 1;

int minHeight = 2;
int height = bottomDividerRow;
int width = Console.WindowWidth - 5;
bool shouldExit = false;


int screenWidthAtStart = Console.WindowWidth;
int screenHeightAtStart = Console.WindowHeight;

// Console position of the player
int playerX = 1;
int playerY = minHeight;

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
        TerminateGame("Terminal was resized. Program exit.");

    }
    Move();

    if (playerY == foodY && (playerX + player.Length > foodX && playerX < foodX + foods[food].Length))
    {
        ChangePlayer();
        ShowFood();
        MessageText("Congratulation go next food" , ConsoleColor.Green);
    }
}

// Returns true if the Terminal was resized 
bool TerminalResized()
{
    return screenHeightAtStart != Console.WindowHeight || screenWidthAtStart != Console.WindowWidth;
}

// Displays random food at a random location
void ShowFood()
{

    if (foodX != 0 || foodY != 0)
    {
        Console.SetCursorPosition(foodX, foodY);
        Console.Write(new string(' ', foods[food].Length));
    }
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(minHeight, bottomDividerRow);

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




    ConsoleKey inputKey = Console.ReadKey(true).Key;

    switch (inputKey)
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
            TerminateGame("Game terminated by user.");
            break;
        default:
            if (quitIfInvalid)
            {
                TerminateGame("Invalid key pressed. Exiting...");
            }
            else
            {
                MessageText("Invalid key please use arrows", ConsoleColor.Red);
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
    int maxX = Console.WindowWidth - 1 - player.Length;

    playerX = (playerX < 1) ? 1 : (playerX > maxX ? maxX : playerX);
    playerY = (playerY < minHeight) ? minHeight : (playerY >= bottomDividerRow ? bottomDividerRow - 1 : playerY);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

void ClearMessage(int row)
{

    if (row >= 0 && row < Console.BufferHeight)
    {
        Console.SetCursorPosition(0, row);

        Console.Write(new string(' ', Console.WindowWidth));
    }
}

void DrawInterface()
{
    Console.Clear();
    Console.SetCursorPosition(0, 0);
    Console.Write("Something text");
    Console.SetCursorPosition(0, topDividerRow);
    Console.Write(new string('-', Console.WindowWidth));



    for (int i = topDividerRow + 1; i < bottomDividerRow; i++)
    {
        // Ліва межа
        Console.SetCursorPosition(0, i);
        Console.Write("|");

        // Права межа
        // Використовуємо WindowWidth - 1, щоб лінія була в самому краї
        Console.SetCursorPosition(Console.WindowWidth - 1, i);
        Console.Write("|");
    }

    // НИЗ: Лінія
    Console.SetCursorPosition(0, bottomDividerRow);
    Console.Write(new string('-', Console.WindowWidth));
}

// Clears the console, displays the food and player
void TerminateGame(string message = "")
{
    Console.Clear();
    Console.CursorVisible = true;
    if (!string.IsNullOrEmpty(message))
    {
        Console.Write(message);

    }
    Environment.Exit(0);

}

void MessageText(string message = "" , ConsoleColor color = ConsoleColor.White)
{
    ClearMessage(messageRow);
    Console.SetCursorPosition(0, messageRow);
    Console.ForegroundColor = color;
    Console.Write(message);
    Console.ResetColor();

}

void InitializeGame()
{
    Console.Clear();
    DrawInterface();
    ShowFood();
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}
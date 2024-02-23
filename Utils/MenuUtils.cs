using Voyager.FileResizer.Services;

namespace Voyager.FileResizer.Utils;

public static class MenuUtils
{
    #region Menus
    private const string MAIN_MENU = @"
[1] Resize Files
[2] Exit";

    private const string SIZE_MENU = "\nEnter the desired height:";

    private const string SIZE_MENU_ERROR = @"
Invalid height! The value must be an integer above 0. Do you want to try again?

[1] Yes
[2] Back to main menu";

    private const string INVALID_OPTION_ERROR ="\nInvalid option. Try again!";
    #endregion

    public static void DrawMainMenu()
    {
        Console.WriteLine(MAIN_MENU);

        string option;
        option = Console.ReadLine() ?? "";

        switch (option)
        {
            case "1":
                Console.Clear();
                DrawSizeMenu();
                break;
            case "2":
                Environment.Exit(0);
                break;
            default:
                Console.Clear();
                Console.WriteLine(INVALID_OPTION_ERROR);
                Thread.Sleep(2000);
                Console.Clear();
                DrawMainMenu();
                break;
        }
    }

    public static void DrawSizeMenu()
    {
        Console.WriteLine(SIZE_MENU);

        string option;
        option = Console.ReadLine() ?? "";

        if (!int.TryParse(option, out int height))
        {
            DrawSizeMenuError();
            return;
        }

        ResizerService.Resize(height);
        Console.WriteLine("\nPress any key to continue ...");
        Console.ReadKey();
        Console.Clear();
        DrawMainMenu();
    }

    public static void DrawSizeMenuError()
    {
        Console.Clear();
        Console.WriteLine(SIZE_MENU_ERROR);

        string option;
        option = Console.ReadLine() ?? "";

        switch (option)
        {
            case "1":
                Console.Clear();
                DrawSizeMenu();
                break;
            case "2":
                Console.Clear();
                DrawMainMenu();
                break;
            default:
                Console.Clear();
                Console.WriteLine(INVALID_OPTION_ERROR);
                Thread.Sleep(2000);
                Console.Clear();
                DrawMainMenu();
                break;
        }
    }
}

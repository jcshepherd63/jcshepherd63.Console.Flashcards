using MenuEnums;
using Spectre.Console;
using StudyArea;

namespace Menus;

internal class MainMenu
{
    private static Enum MainMenuSelection()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow bold underline]Welcome to the Flashcard Application\n[/]");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuEnum.MenuEnum>()
            .Title("[yellow]Which section would you like to go to?[/]")
            .AddChoices(Enum.GetValues<MainMenuEnum.MenuEnum>()));

        return selection;
    }

    public static void MainMenuRouter()
    {
        var selection = MainMenu.MainMenuSelection();

        switch (selection)
        {
            case MainMenuEnum.MenuEnum.Study_Session_Menu:
                {
                    StudySessionMenu.StudySessionRouter();
                    break;
                }
            case MainMenuEnum.MenuEnum.FlashCards_Menu:
                {
                    FlashcardMenu.FlashcardMenuRouter();
                    break; 
                }
            case MainMenuEnum.MenuEnum.Stacks_Menu:
                {
                    StacksMenu.StacksMenuRouter();
                    break;
                }
            case MainMenuEnum.MenuEnum.Close_Application:
                {
                    break;
                }
        }
    }


}


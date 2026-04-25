using StackMethods;
using Menus;
using Spectre.Console;
using DTOs;
using StudyAreaCRUD;

namespace StudyArea;

internal class StudySessionMenu
{
    private static string StudySessionSelection()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow bold]What would you like to do in the study area?\n[/]");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices("Study a stack of flashcards", "View past study sessions", "Go back to the Main Menu"));

        return selection;
    }

    public static FlashcardStackDTO GetStackToStudy()
    {
        Console.Clear();
        List<FlashcardStackDTO> choices = StackService.GetStacks();
        FlashcardStackDTO selection = null;
        try
        {
            if (choices == null || !choices.Any())
            {
                Console.WriteLine("There are no flashcard stacks to study currently. Press any key to return to the main menu.");
                Console.ReadLine();
                Console.Clear();
                MainMenu.MainMenuRouter();
                return null;
            }

            selection = AnsiConsole.Prompt<FlashcardStackDTO>(
                new SelectionPrompt<FlashcardStackDTO>()
                .Title("[yellow bold]Which stack would you like to study?[/]")
                .AddChoices(choices));
        }
        catch(Exception e)
        {
            Console.WriteLine("An error occurred while selecting a stack. Returning to the main menu.");
            Console.ReadLine();
            Console.Clear();
            MainMenu.MainMenuRouter();
        }

        return selection;
    }

    public static void StudySessionRouter()
    {
        var selection = StudySessionSelection();

        switch (selection)
        {
            case "Study a stack of flashcards":
                {
                    var stack = GetStackToStudy();
                    if (stack == null)
                    {
                        Console.WriteLine("No stack selected. Returning to the main menu.");
                        ReturnToMainMenu();
                        return;
                    }
                    var stackName = stack.ToString();
                    var stackId = StudyAreaService.GetStackId(stackName);
                    (int totalCount,int count) = StudySessionController.DisplayFlashcards(stackName);
                    var session = StudySessionController.ObjectCreation(totalCount, count, stackId);
                    StudyAreaService.AddStudySession(session);
                    ReturnToMainMenu();
                    break;
                }
            case "View past study sessions":
                {
                    StudySessionController.DisplayStudySessions();
                    ReturnToMainMenu();
                    break;
                }
            case "Go back to the Main Menu":
                {
                    ReturnToMainMenu();
                    break;
                }
        }
    }
    private static void ReturnToMainMenu()
    {
        AnsiConsole.MarkupLine("[red bold]PRESS ANY KEY TO RETURN TO MAIN MENU[/]");
        Console.ReadKey();
        MainMenu.MainMenuRouter();
    }
}

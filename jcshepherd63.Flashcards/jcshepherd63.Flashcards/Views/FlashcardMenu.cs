using FlashcardMethods;
using FlashcardContr;
using MenuEnums;
using Models;
using Spectre.Console;
using StackMethods;
using DTOs;

namespace Menus;

internal class FlashcardMenu
{
    private static Enum FlashcardMenuSelection()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow bold underline]What would you like to do in the flashcard menu?\n[/]");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<FlashcardsEnum.FlashcardEnum>()
            .AddChoices(Enum.GetValues<FlashcardsEnum.FlashcardEnum>()));

        return selection;
    }

    internal static void FlashcardMenuRouter()
    {
        var selection = FlashcardMenu.FlashcardMenuSelection();

        switch (selection)
        {
            case FlashcardsEnum.FlashcardEnum.View_Flashcards:
                {
                    FlashcardController.FlashcardDisplay();
                    ReturnToMainMenu();
                    break;
                }
            case FlashcardsEnum.FlashcardEnum.Add_Flashcard:
                {
                    var prompt = GetFlashcardPrompt();
                    var answer = GetFlashcardAnswer();
                    var stackName = FlashcardController.GetStackForFlashcard();
                    int stackId = StackService.GetIdByStackName(stackName);
                    Flashcard flashcard = new(prompt, answer, stackId);
                    FlashcardService.AddFlashcard(flashcard);
                    ReturnToMainMenu();
                    break;
                }
            case FlashcardsEnum.FlashcardEnum.Update_Flashcard:
                {
                    var card = GetFlashcardToUpdate();
                    var prompt = GetFlashcardPrompt();
                    var answer = GetFlashcardAnswer();
                    var stackName = FlashcardController.GetStackForFlashcard();
                    int stackId = StackService.GetIdByStackName(stackName);
                    Flashcard flashcard = new(card.id, prompt, answer, stackId);
                    FlashcardService.UpdateFlashcard(flashcard);
                    ReturnToMainMenu();
                    break;
                }
            case FlashcardsEnum.FlashcardEnum.Delete_Flashcard:
                {
                    var flashcardToDelete = GetFlashcardToDelete();
                    FlashcardService.DeleteFlashcard(flashcardToDelete);
                    ReturnToMainMenu();
                    break;
                }
            case FlashcardsEnum.FlashcardEnum.Return_To_Main_Menu:
                {
                    MainMenu.MainMenuRouter();
                    break;
                }
        }
    }

    private static string GetFlashcardPrompt()
    {
        var prompt = AnsiConsole.Ask<string>("[red italic]What question would you like this flashcard to display?[/]");
        return prompt;
    }

    private static string GetFlashcardAnswer()
    {
        var answer = AnsiConsole.Ask<string>("[red italic]What is the answer for this flashcard?[/]");
        return answer;
    }

    private static FlashcardDTO GetFlashcardToUpdate()
    {
        List<FlashcardDTO> flashcards = FlashcardService.GetFlashcards();
        var answer = AnsiConsole.Prompt<FlashcardDTO>(
            new SelectionPrompt<FlashcardDTO>()
            .Title("[yellow bold]Which flashcard would you like to update?[/]")
            .AddChoices(flashcards));
        return answer;
    }

    private static FlashcardDTO GetFlashcardToDelete()
    {
        List<FlashcardDTO> flashcards = FlashcardService.GetFlashcards();
        var answer = AnsiConsole.Prompt<FlashcardDTO>(
            new SelectionPrompt<FlashcardDTO>()
            .Title("[yellow bold]Which flashcard would you like to Delete?[/]")
            .AddChoices(flashcards));
        return answer;
    }

    private static void ReturnToMainMenu()
    {
        AnsiConsole.MarkupLine("[red bold]PRESS ANY KEY TO RETURN TO MAIN MENU[/]");
        Console.ReadKey();
        MainMenu.MainMenuRouter();
    }
}

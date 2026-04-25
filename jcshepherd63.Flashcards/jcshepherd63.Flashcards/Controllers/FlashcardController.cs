using DTOs;
using FlashcardMethods;
using Menus;
using Microsoft.Identity.Client;
using Spectre.Console;
using StackMethods;

namespace FlashcardContr;

public class FlashcardController
{
    public static void FlashcardDisplay()
    {
        Table table = new Table()
            .DoubleBorder()
            .BorderColor(Color.Blue)
            .AddColumn("ID")
            .AddColumn("Question")
            .AddColumn("Answer");

        var flashcards = FlashcardService.GetFlashcards();
        var count = 1;
        foreach (var flashcard in flashcards)
        {
            table.AddRow(count.ToString(), flashcard.prompt.ToString(), flashcard.answer.ToString());
            count++;
        }
        AnsiConsole.Write(table);
    }

    internal static string GetStackForFlashcard()
    {
        Console.Clear();
        List<FlashcardStackDTO> choices = StackService.GetStacks();
        FlashcardStackDTO selection = null;
        try
        {
                selection = AnsiConsole.Prompt<FlashcardStackDTO>(
                new SelectionPrompt<FlashcardStackDTO>()
                .Title("[yellow bold]Which stack does this flashcard belong to?[/]")
                .AddChoices(choices));
        }catch(Exception e)
        {
            Console.WriteLine("There are no flashcard stacks to add this flashcard to currently. Press any key to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
            MainMenu.MainMenuRouter();
        }

        return selection.ToString();
    }
}

using DTOs;
using FlashcardMethods;
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
        var selection = AnsiConsole.Prompt<FlashcardStackDTO>(
            new SelectionPrompt<FlashcardStackDTO>()
            .Title("[yellow bold]Which stack does this flashcard belong to?[/]")
            .AddChoices(choices));


        return selection.ToString();
    }
}

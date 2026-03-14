using FlashcardMethods;
using Models;
using Spectre.Console;
using StudyAreaCRUD;

namespace StudyArea;

internal class StudySessionController
{
    private static Table SetupTable()
    {
        var table = new Table()
            .DoubleBorder()
            .BorderColor(Color.Blue1);

        table.AddColumn("[yellow bold]Question#[/]");
        table.AddColumn("[yellow bold]Question[/]");

        return table;
    }

    private static Table SetupSessionViewerTable()
    {
        var table = new Table()
            .DoubleBorder()
            .BorderColor(Color.Blue);
        table.AddColumn("Date");
        table.AddColumn("Score");
        table.AddColumn("Total Possible Score");
        table.AddColumn("Percent Correct");
        table.AddColumn("Stack");

        return table;
    }

    private static string CheckAnswer()
    {
        var answer = AnsiConsole.Ask<string>("What is the answer for this flashcard?");
        return answer;
    }

    public static (int, int) DisplayFlashcards(string stackName)
    {
        Console.Clear();
        var table = SetupTable();
        var flashcards = FlashcardService.GetFlashcardsByStackName(stackName);
        var count = 1;
        var totalCorrect = 0;

        foreach (var flashcard in flashcards)
        {
            table.AddRow(count.ToString(), flashcard.prompt);
            count++;
            AnsiConsole.Write(table);
            var answer = CheckAnswer();
            if (answer.ToLower().Trim() == flashcard.answer.ToLower().Trim())
            {
                Console.Clear();
                Console.WriteLine("Correct!");
                totalCorrect++;
                Console.WriteLine($"{totalCorrect}/{flashcards.Count()}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect");
                Console.WriteLine($"{totalCorrect}/{flashcards.Count()}");
            }

            table.RemoveRow(0);
        }
        return (totalCorrect, count);
    }

    public static StudySession ObjectCreation(int totalCorrect, int count, int stackId)
    {
        StudySession session = new();
        session.date = DateTime.Now;
        session.score = totalCorrect;
        session.totalPossibleScore = count - 1; 
        session.stackId = stackId;
        return session;
    }

    public static void ListStudySessions()
    {
        List<StudySessionDTO> sessions = StudyAreaService.GetStudySessions();

        foreach(var session in sessions)
        {
            Console.WriteLine(session.ToString());
        }
    }

    public static void DisplayStudySessions()
    {
        Console.Clear();
        Table table = SetupSessionViewerTable();
        List<StudySessionDTO> sessions = StudyAreaService.GetStudySessions();

        foreach(var session in sessions)
        {
            double percentCorrect = (double)session.score / (double)session.totalPossibleScore;
            table.AddRow(session.date.ToString(), session.score.ToString(), session.totalPossibleScore.ToString(), $"{percentCorrect:P2}", session.stackName);
        }

        AnsiConsole.Write(table);
    }
}

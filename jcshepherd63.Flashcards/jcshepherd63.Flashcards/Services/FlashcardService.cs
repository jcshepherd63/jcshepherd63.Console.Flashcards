using DatabaseCreation;
using DTOs;
using Microsoft.Data.SqlClient;
using Models;
using Dapper;
using Spectre.Console;

namespace FlashcardMethods;

internal class FlashcardService
{
    public static List<FlashcardDTO> GetFlashcards()
    {
        List<FlashcardDTO> flashcards = new();
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var getCmd = "SELECT * FROM Flashcards;";
            flashcards = connection.Query<FlashcardDTO>(getCmd).ToList();
            connection.Close();
        }
        return flashcards;
    }

    public static List<FlashcardDTO> GetFlashcardsByStackName(string stackName)
    {
        List<FlashcardDTO> flashcards = new();
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var getCmd = @"SELECT f.* FROM Flashcards AS f
                                JOIN Stacks AS s ON f.stackId = s.id
                                WHERE s.stackName = @stackName;";
            flashcards = connection.Query<FlashcardDTO>(getCmd, new { stackName }).ToList();
            connection.Close();
        }
        return flashcards;
    }

    public static void AddFlashcard(Flashcard flashcard)
    {
        using (var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var addCmd = "INSERT INTO Flashcards (answer, prompt, stackId) VALUES (@answer, @prompt, @stackId)";
            try
            {
                connection.Execute(addCmd, flashcard);
            }
            catch
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red bold]This stack does not exist currently. Please add the stack and try again.\n[/]");
            }
            connection.Close();
        }
    }

    public static void UpdateFlashcard(Flashcard flashcard)
    {
        using (var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var updateCmd = @"UPDATE Flashcards SET 
                                answer = @answer,
                                prompt = @prompt,
                                stackId = @stackId
                                WHERE id = @id;";
            connection.Execute(updateCmd, flashcard);
            connection.Close();
        }
    }

    public static void DeleteFlashcard(FlashcardDTO flashcard)
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var deleteCmd = "DELETE FROM Flashcards WHERE id = @id";
            connection.Execute(deleteCmd, flashcard);
            connection.Close();
        }
    }

    public static void DeleteFlashcardByStack(FlashcardStack stack)
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var deleteCmd = "DELETE FROM Flashcards WHERE stackId = @id;";
            connection.Execute(deleteCmd, stack);
            connection.Close();
        }
    }
}

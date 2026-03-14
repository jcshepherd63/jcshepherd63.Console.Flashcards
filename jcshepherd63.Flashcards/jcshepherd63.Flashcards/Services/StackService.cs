using DatabaseCreation;
using DTOs;
using Dapper;
using Models;
using Microsoft.Data.SqlClient;

namespace StackMethods;

internal class StackService
{
    public static List<FlashcardStackDTO> GetStacks()
    {
        List<FlashcardStackDTO> stacks = new();
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var getCmd = "SELECT * FROM Stacks;";
            stacks = connection.Query<FlashcardStackDTO>(getCmd).ToList();
            connection.Close();
        }
        return stacks;
    }

    public static void AddStack(FlashcardStack stack)
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var addCmd = "INSERT INTO Stacks (stackName) VALUES (@stackName);";
            connection.Execute(addCmd, stack);
            connection.Close();
        }
    }

    public static void UpdateStack(FlashcardStack stack)
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var updateCmd = @"UPDATE Stacks SET
                                stackName = @stackName
                                WHERE id = @id;";
            connection.Execute(updateCmd, stack);
            connection.Close();
        }
    }

    public static void DeleteStack(FlashcardStack stack)
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var deleteCmd = "DELETE FROM Stacks WHERE id = @id;";
            connection.Execute(deleteCmd, stack);
            connection.Close();
        }
    }

    public static int GetIdByStackName(string stackName)
    {
        int stackId;
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var getCmd = "SELECT id FROM Stacks WHERE stackName = @stackName";
            stackId = connection.QuerySingle<int>(getCmd, new {stackName});
            connection.Close();
        }
        return stackId;
    }
}

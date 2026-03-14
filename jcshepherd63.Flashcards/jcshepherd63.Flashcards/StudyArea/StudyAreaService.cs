using Models;
using DTOs;
using Microsoft.Data.SqlClient;
using StudyArea; 
using DatabaseCreation;
using Dapper;

namespace StudyAreaCRUD;

internal class StudyAreaService
{
    public static int GetStackId(string stackName)
    {
        using (var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var getCmd = "SELECT id FROM Stacks WHERE stackName = @stackName";
            int stackId = connection.QuerySingle<int>(getCmd, new {stackName = stackName});
            connection.Close();
            return stackId;
        }
    }

    public static void AddStudySession(StudySession session)
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var addCmd = "INSERT INTO Study_Sessions (date, score, stackId, totalPossibleScore) VALUES (@date, @score, @stackId, @totalPossibleScore)";
            connection.Execute(addCmd, session);
            connection.Close();
        }
    }

    public static List<StudySessionDTO> GetStudySessions()
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var getCmd = @"SELECT s.date, s.score, s.TotalPossibleScore, st.stackName FROM Study_Sessions AS s
                                JOIN Stacks AS st ON s.stackId = st.id";
            var studySessions = connection.Query<StudySessionDTO>(getCmd).ToList();
            connection.Close();
            return studySessions;
        }
    }

    public static void DeleteStudySessionByStack(FlashcardStack stack)
    {
        using(var connection = new SqlConnection(DatabaseSetup.GetDbConnectionString()))
        {
            connection.Open();
            var deleteCmd = "DELETE FROM Study_Sessions WHERE stackId = @id";
            connection.Execute(deleteCmd, stack);
            connection.Close();
        }
    }
}

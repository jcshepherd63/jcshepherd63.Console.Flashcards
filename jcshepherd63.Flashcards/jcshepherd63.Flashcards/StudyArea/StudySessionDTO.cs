namespace StudyArea;

internal class StudySessionDTO
{
    public DateTime date { get; set; }
    public int score { get; set; }
    public string stackName { get; set; }
    public int totalPossibleScore { get; set; }

    public override string ToString()
    {
        return $"Date: {date}\nScore: {score}\nTotal Possible Score: {totalPossibleScore}\nStack Name: {stackName}";
    }
}

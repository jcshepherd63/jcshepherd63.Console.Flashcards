namespace Models;

internal class StudySession
{
    public int id { get; set; }
    public DateTime date { get; set; }
    public int score { get; set; }
    public int stackId { get; set; }
    public int totalPossibleScore { get; set; }
}

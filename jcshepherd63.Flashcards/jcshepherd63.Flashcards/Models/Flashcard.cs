namespace Models;

internal class Flashcard
{
    public int id { get; set; }
    public string answer { get; set; }
    public string prompt { get; set; }
    public int stackId { get; set; }

    public Flashcard() { }

    public Flashcard(string prompt, string answer, int stackId)
    {
        this.prompt = prompt;
        this.answer = answer;
        this.stackId = stackId;
    }

    public Flashcard(int id, string prompt, string answer, int stackId)
    {
        this.id = id;
        this.prompt = prompt;
        this.answer = answer;
        this.stackId = stackId;
    }
}


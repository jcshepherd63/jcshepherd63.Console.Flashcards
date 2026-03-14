namespace DTOs;

internal class FlashcardDTO
{
    public int id { get; set; }
    public string answer { get; set; }
    public string prompt { get; set; }

    public override string ToString()
    {
        return prompt;
    }
}

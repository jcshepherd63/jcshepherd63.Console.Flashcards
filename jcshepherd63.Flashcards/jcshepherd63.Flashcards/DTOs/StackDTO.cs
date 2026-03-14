namespace DTOs;

internal class FlashcardStackDTO
{
    public string stackName { get; set; }

    public override string ToString()
    {
        return stackName.ToString();
    }
}

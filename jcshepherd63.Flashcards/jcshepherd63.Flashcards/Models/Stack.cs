namespace Models;

internal class FlashcardStack
{
    public int id { get; set; }
    public string stackName { get; set; }

    public FlashcardStack() { }

    public FlashcardStack(string stackName)
    {
        this.stackName = stackName;
    }

    public FlashcardStack(int id, string stackName)
    {
        this.id = id;
        this.stackName = stackName;
    }
}

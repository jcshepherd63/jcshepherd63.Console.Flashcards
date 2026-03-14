using Spectre.Console;
using StackMethods;


namespace StacksController;

public class StackController
{
    public static void StackDisplay()
    {
        Table table = new Table()
            .DoubleBorder()
            .BorderColor(Color.Blue)
            .AddColumn("Stack Name");
        var stacks = StackService.GetStacks();
        foreach (var stack in stacks)
        {
            table.AddRow(stack.stackName);
        }
        AnsiConsole.Write(table);
    }
}
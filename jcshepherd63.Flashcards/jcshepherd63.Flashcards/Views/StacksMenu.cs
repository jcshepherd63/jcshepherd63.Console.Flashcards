using MenuEnums;
using Models;
using FlashcardMethods;
using StackMethods;
using Spectre.Console;
using StudyAreaCRUD;
using StacksController;
using DTOs;

namespace Menus;

internal class StacksMenu
{
    private static Enum StackMenuSelection()
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<StacksEnum.StackEnum>()
            .AddChoices(Enum.GetValues<StacksEnum.StackEnum>()));
        
        return selection;
    }

    public static void StacksMenuRouter()
    {
        var selection = StacksMenu.StackMenuSelection();

        switch (selection)
        {
            case StacksEnum.StackEnum.View_Stacks:
                {
                    StackController.StackDisplay();
                    ReturnToMainMenu();
                    break;
                }
            case StacksEnum.StackEnum.Add_Stack:
                {
                    var name = GetStackName();
                    FlashcardStack stack = new(name);
                    StackService.AddStack(stack);
                    ReturnToMainMenu();
                    break;
                }
            case StacksEnum.StackEnum.Update_Stack:
                {
                    var stackToUpdate = GetStackToUpdate();
                    var id = StackService.GetIdByStackName(stackToUpdate.stackName);
                    var name = GetStackName();
                    FlashcardStack stack = new(id, name);
                    StackService.UpdateStack(stack);
                    ReturnToMainMenu();
                    break;
                }
            case StacksEnum.StackEnum.Delete_Stack:
                {
                    FlashcardStack stackToDelete = new();
                    var stack = GetStackToDelete();
                    stackToDelete.id = StackService.GetIdByStackName(stack.stackName);
                    StudyAreaService.DeleteStudySessionByStack(stackToDelete);
                    FlashcardService.DeleteFlashcardByStack(stackToDelete);
                    StackService.DeleteStack(stackToDelete);
                    ReturnToMainMenu();
                    break;
                }
            case StacksEnum.StackEnum.Return_To_Main_Menu:
                {
                    MainMenu.MainMenuRouter();
                    break;
                }
        }
    }

    private static string GetStackName()
    {
        var name = AnsiConsole.Ask<string>("[red italic]What is the name of this stack?[/]");
        return name;
    }

    private static FlashcardStackDTO GetStackToUpdate()
    {
        List<FlashcardStackDTO> stacks = StackService.GetStacks();
        var answer = AnsiConsole.Prompt<FlashcardStackDTO>(
            new SelectionPrompt<FlashcardStackDTO>()
            .Title("[yellow bold]Which stack would you like to update?[/]")
            .AddChoices(stacks));

        return answer;
    }

    private static FlashcardStackDTO GetStackToDelete()
    {
        List<FlashcardStackDTO> stacks = StackService.GetStacks();
        var answer = AnsiConsole.Prompt<FlashcardStackDTO>(
            new SelectionPrompt<FlashcardStackDTO>()
            .Title("[yellow bold]Which stack would you like to delete?[/]")
            .AddChoices(stacks));

        return answer;
    }

    private static void ReturnToMainMenu()
    {
        AnsiConsole.MarkupLine("[red bold]PRESS ANY KEY TO RETURN TO MAIN MENU[/]");
        Console.ReadKey();
        MainMenu.MainMenuRouter();
    }
}


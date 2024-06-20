using Godot;
using System;

public partial class InteractionMachine : Node
{
	public GameState State;

	public void Process(string input)
    {
        string[] command = input.Split(" "); 
        ApplyInteractionLogic(command);
    }

    private void ApplyInteractionLogic(string[] command)
    {
        switch(command[0])
        {
            case "makeBatch":
                State.State["Sausages"] = (State.State["Sausages"].ToInt() - 10).ToString();
                State.State["Batch"] = "10";
                break;
            case "sale":
                State.State["Money"] = (State.State["Money"].ToInt() + 10).ToString();
                State.State["Batch"] = (State.State["Batch"].ToInt() - 1).ToString();
                break;
            default:
                break;
        }
    }
}

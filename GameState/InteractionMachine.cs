using Godot;
using System;

public partial class InteractionMachine : Node
{
	public GameState gameState;

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
                gameState.State["Sausages"] = (gameState.State["Sausages"].ToInt() - 1).ToString();
                break;
            case "attack":
                string srcName = field.map.Get(command[1].ToInt(), command[2].ToInt());
                string dstName = field.map.Get(command[3].ToInt(), command[4].ToInt());
                GD.Print(srcName);
                GD.Print(dstName);
                gameState.collection[srcName].data["health"] = 
                    (gameState.collection[srcName].data["health"].ToInt() - gameState.collection[dstName].data["attack"].ToInt()).ToString();
                gameState.collection[dstName].data["health"] = 
                    (gameState.collection[dstName].data["health"].ToInt() - gameState.collection[srcName].data["attack"].ToInt()).ToString();
                break;
            default:
                break;
        }
    }
}

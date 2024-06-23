using Godot;
using System;

public partial class InteractionMachine : Node
{
	public GameState State;
    public Timer freshnessTimer;
    public GameWorldText gwText;
    
    private Random random;

    private string interactionMessage;

    public override void _Ready()
    {
        random = new Random();
    }

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
                freshnessTimer.Start();
                break;
            case "sale":
                SaleInteraction();
                break;
            default:
                break;
        }
    }

    private void SaleInteraction()
    {
        interactionMessage = "";

        if(State.State["Batch"].ToInt() >= 1)
        {
            interactionMessage = "Hotdog sold!\n";
            int salePrice = 10;
            if(State.State.ContainsKey("Bread"))
            {
                int usedBread = 1;
                while(!ChanceRoll(8))
                {
                    usedBread += 1;
                }
                State.State["Bread"] = (State.State["Bread"].ToInt() - usedBread).ToString();
                
                interactionMessage += "-" + usedBread + " bread\n";
                GD.Print("Used bread " + usedBread);
            }

            if(State.State.ContainsKey("Freshness"))
            {
                int saleFreshness = (int)freshnessTimer.TimeLeft/2;
                salePrice += saleFreshness;
                interactionMessage += "+" + saleFreshness + " in tips\n";
                GD.Print("Sale freshness " + saleFreshness);
            }

            State.State["Money"] = (State.State["Money"].ToInt() + salePrice).ToString();
            GD.Print("Sale price " + salePrice);

            State.State["Batch"] = (State.State["Batch"].ToInt() - 1).ToString();

            interactionMessage += "+" + salePrice + " final sale";
        }

        else
        {
            interactionMessage = "Nothing to sell :(";
        }

        gwText.SetMessage(interactionMessage);
    }

    private bool ChanceRoll(int successChance)
    {
        if(random.Next(10) <= successChance - 1)
            return true;
        return false;
    } 
}

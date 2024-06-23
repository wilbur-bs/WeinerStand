using Godot;
using System;

public partial class InteractionMachine : Node
{
	public GameState State;
    public Timer freshnessTimer;
    public GameWorldText gwText;
    public int baseSalePrice = 5;
    
    private Random random;

    private string interactionMessage;

    public override void _Ready()
    {
        random = new Random();
    }

	public void Process(string input)
    {
        GD.Print("Interaction: " + input);
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
            case "buySausages":
                State.State["Sausages"] = (State.State["Sausages"].ToInt() + 20).ToString();
                State.State["Money"] = (State.State["Money"].ToInt() - 20).ToString();
                gwText.SetMessage("+20 sausages\n-20 money");
                break;
            case "buyBread":
                State.State["Bread"] = (State.State["Bread"].ToInt() + 20).ToString();
                State.State["Money"] = (State.State["Money"].ToInt() - 20).ToString();
                gwText.SetMessage("+20 bread\n-20 money");
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
            int salePrice = baseSalePrice;
            if(State.State.ContainsKey("Bread"))
            {
                int usedBread = 1;
                while(!ChanceRoll(8))
                {
                    usedBread += 1;
                }
                State.State["Bread"] = (State.State["Bread"].ToInt() - usedBread).ToString();
                
                interactionMessage += "-" + usedBread + " bread\n";
            }

            if(State.State.ContainsKey("Freshness"))
            {
                int saleFreshness = (int)freshnessTimer.TimeLeft/2;
                salePrice += saleFreshness;
                interactionMessage += "+" + saleFreshness + " in tips\n";
            }

            State.State["Money"] = (State.State["Money"].ToInt() + salePrice).ToString();

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

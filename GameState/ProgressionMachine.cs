using Godot;
using System;
using System.Collections.Generic;

public partial class ProgressionMachine : Node
{
	[Export]
	public float RoundTime = 5;

	public Timer roundTimer;
	public GameState gameState;
	public GameWorldMachine gameWorldMachine;

	public UiController uiController;
	public CanvasLayer inRoundMenu;
	public CanvasLayer postRoundMenu;

	// resource sets
	private List<string> resourceSet1;
	private List<string> resourceSet2;
	private Random random;

	public void Initialize()
	{
		roundTimer.Timeout += () => EndRound();

		resourceSet1 = new List<string>() {
			"bread", "freshness"
		};

		resourceSet2 = new List<string>() {
			"cleanliness", "condiments", "propane"
		};

		random = new Random();
	}

	public void NewRound()
	{
		roundTimer.WaitTime = RoundTime;
		roundTimer.Start();

		gameWorldMachine.StartSpawning();

		//inRoundMenu.Visible = true;
		//postRoundMenu.Visible = false;
		uiController.SwitchToInRoundUi();

		GD.Print("Round Start");
	}

	public void EndRound()
	{
		roundTimer.Stop();

		gameWorldMachine.StopSpawning();

		//inRoundMenu.Visible = false;
		//postRoundMenu.Visible = true;
		string resource = GetNexResource();
		GD.Print("Next Resource: " + resource);
		ApplyNewRoundRules(resource);
		uiController.SwitchToPostRoundUi(resource);

		GD.Print("Round End");
	}

	private string GetNexResource()
	{
		string nextResource;
		if(resourceSet1.Count > 0)
		{
			nextResource = resourceSet1[random.Next(resourceSet1.Count)];
			resourceSet1.Remove(nextResource);
			return nextResource;
		}
		else if(resourceSet2.Count > 0)
		{
			nextResource = resourceSet2[random.Next(resourceSet2.Count)];
			resourceSet2.Remove(nextResource);
			return nextResource;
		}
		else
		{
			return "";
		}
	}

	private void ApplyNewRoundRules(string newRound)
	{
		switch(newRound)
        {
			case "bread":
				gameState.State.Add("Bread", "100");
                break;
			case "freshness":
				gameState.State.Add("Freshness", "0");
                break;
			default:
				break;
		}
	}
}

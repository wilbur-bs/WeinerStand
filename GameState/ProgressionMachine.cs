using Godot;
using System;
using System.Collections.Generic;

public partial class ProgressionMachine : Node
{
	[Export]
	public float RoundTime = 15;

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

		// Initialize round
		gameWorldMachine.StopSpawning();
		uiController.SwitchToPostRoundUi("start");
	}

	public void NewRound()
	{
		roundTimer.WaitTime = RoundTime;
		roundTimer.Start();

		gameWorldMachine.StartSpawning();
		uiController.SwitchToInRoundUi();

		GD.Print("Round Start");
	}

	public void EndRound()
	{
		GD.Print("Round End");
		roundTimer.Stop();

		gameWorldMachine.StopSpawning();
		gameState.State["Batch"] = "0";
		gameState.State["Money"] = (gameState.State["Money"].ToInt() - 100).ToString();
		if(EndGameCheck() != true)
		{
			string resource = GetNexResource();
			ApplyNewRoundRules(resource);
			uiController.SwitchToPostRoundUi(resource);

			GD.Print("Next Resource: " + resource);
		}
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
			case "cleanliness":
				gameState.State.Add("Cleanliness", "100");
                break;
			case "condiments":
				gameState.State.Add("Condiments", "100");
				break;
			case "propane":
				gameState.State.Add("Propane", "100");
				break;
			default:
				break;
		}
	}

	private bool EndGameCheck()
	{
		if(gameState.State["Money"].ToInt() <= 0)
		{
			uiController.SwitchToPostRoundUi("end");
			return true;
		}
		return false;
	}
}

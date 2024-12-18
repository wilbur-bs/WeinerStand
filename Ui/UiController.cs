using Godot;
using System;

public partial class UiController : Node
{
	public GameState State;
	public DialougeMachine DialougeMachine;
	public ProgressionMachine progressionMachine;
	public InteractionMachine interactionMachine;

	private bool inRound = false;

	// in round menu
	public DialogueUi Menu;
	public Timer RoundTimer;
	public ProgressBar ProgressBar;

	public CanvasLayer inRoundMenu;

	// post round menu
	public DialogueUi PostRoundScreen;
	public CanvasLayer postRoundMenu;
	private string currentDialogueQuery;
	private int currentDialogueIndex;
	private string currentDialogueString;
	private string currentTitleString;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RefreshUi();
	}

	public void SwitchToInRoundUi()
	{
		inRound = true;

		inRoundMenu.Visible = true;
		postRoundMenu.Visible = false;
	}

	public void SwitchToPostRoundUi(string resource)
	{
		inRound = false;

		currentDialogueIndex = 0;
		currentDialogueQuery = resource + "_" + currentDialogueIndex.ToString();
		NavigateDialogue();

		inRoundMenu.Visible = false;
		postRoundMenu.Visible = true;
	}

	private void SetPostRoundButtons()
	{
		PostRoundScreen.ClearButtons();
		if(currentDialogueQuery == "end_0")
		{
			Button button = new() {
				Text = "Aw man"
			};
			button.Pressed += () => ((GameBootstraper)GetParent()).GameEnded();
			PostRoundScreen.AddButton(button);
		}
		else if(currentDialogueQuery != "default_0")
		{
			Button button = new() {
				Text = "Got it"
			};
			button.Pressed += () => NavigateDialogue();
			PostRoundScreen.AddButton(button);
		}
		else
		{
			Button button = new() {
				Text = "Buy 20 Sausages (-20 Money)"
			};
			button.Pressed += () => interactionMachine.Process("buySausages");
			button.Pressed += () => RefreshGamestateDialogue();
			PostRoundScreen.AddButton(button);

			if(State.State.ContainsKey("Bread"))
			{
				button = new() {
					Text = "Buy 20 Bread (-20 Money)"
				};
				button.Pressed += () => interactionMachine.Process("buyBread");
				button.Pressed += () => RefreshGamestateDialogue();
				PostRoundScreen.AddButton(button);
			}

			button = new() {
				Text = "Next Day"
			};
			button.Pressed += () => progressionMachine.NewRound();
			PostRoundScreen.AddButton(button);
		}
	}

	private void NavigateDialogue()
	{
		GD.Print("QueryString: " + currentDialogueQuery);
		if(DialougeMachine.Descriptions.ContainsKey(currentDialogueQuery))
		{
			currentTitleString = DialougeMachine
				.Titles[currentDialogueQuery.Substring(0, currentDialogueQuery.Length-2)];

			currentDialogueString = DialougeMachine.Descriptions[currentDialogueQuery];

			if(currentDialogueQuery == "end_0")
			{
				currentDialogueString += "\nTotal hotdogs sold: " + State.State["Hotdogs Sold"] + "\n";
			}
		}
		else
		{
			currentDialogueQuery = "default_0";
			GD.Print("QueryString: " + currentDialogueQuery);

			currentTitleString = DialougeMachine
				.Titles["default"];
				
			RefreshGamestateDialogue();
		}

		SetPostRoundButtons();

		// Navigate to next dialog text
		if(currentDialogueQuery != "default_0")
		{
			currentDialogueIndex += 1;
			currentDialogueQuery = currentDialogueQuery.Substring(0, currentDialogueQuery.Length-2)
				+ "_" + currentDialogueIndex.ToString();
		}
	}

	private void RefreshGamestateDialogue()
	{
		currentDialogueString = DialougeMachine.Descriptions["default_0"];
		currentDialogueString += "- Money: " + State.State["Money"] + "\n";
		currentDialogueString += "- Sausages: " + State.State["Sausages"] + "\n";
		if(State.State.ContainsKey("Bread"))
		{
			currentDialogueString += "- Bread: " + State.State["Bread"] + "\n";
		}
	}

	private void RefreshUi()
	{
		if(inRound)
		{
			Menu.SetText("Resources: \n" + State.GetStateString()  + "\n");
			ProgressBar.Value = 100*(RoundTimer.TimeLeft/RoundTimer.WaitTime);
		}
		else
		{
			PostRoundScreen.SetText(currentTitleString);
			PostRoundScreen.SetText(currentDialogueString);
		}
	}


}

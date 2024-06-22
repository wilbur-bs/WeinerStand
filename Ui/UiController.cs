using Godot;
using System;

public partial class UiController : Node
{
	public GameState State;
	public DialougeMachine DialougeMachine;
	public ProgressionMachine progressionMachine;

	private bool inRound;

	// in round menu
	public EventScreen Menu;
	public Timer RoundTimer;
	public ProgressBar ProgressBar;

	public CanvasLayer inRoundMenu;

	// post round menu
	public EventScreen PostRoundScreen;
	public CanvasLayer postRoundMenu;
	private string currentDialogueQuery;
	private int currentDialogueIndex;
	private string currentDialogueString;
	private string currentTitleString;


	//private string currentDescription;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

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
		if(currentDialogueQuery != "default_0")
		{
			Button button = new() {
				Text = "Got it"
			};
			button.Pressed += () => NavigateDialogue();
			PostRoundScreen.AddOptionButton(button);
		}
		else
		{
			Button button = new() {
				Text = "Next Day"
			};
			button.Pressed += () => progressionMachine.NewRound();
			PostRoundScreen.AddOptionButton(button);
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
		}
		else
		{
			currentDialogueQuery = "default_0";
			currentTitleString = DialougeMachine
				.Titles["default"];
				
			currentDialogueString = DialougeMachine.Descriptions["default_0"] 
				+ "Resources: \n" + State.GetStateString()  + "\n";
		}

		SetPostRoundButtons();

		if(currentDialogueQuery != "default_0")
		{
			currentDialogueIndex += 1;
			currentDialogueQuery = currentDialogueQuery.Substring(0, currentDialogueQuery.Length-2)
				+ "_" + currentDialogueIndex.ToString();
		}
	}

	private void RefreshUi()
	{
		if(inRound)
		{
			Menu.SetDescriptionText("Resources: \n" + State.GetStateString()  + "\n");
			ProgressBar.Value = 100*(RoundTimer.TimeLeft/RoundTimer.WaitTime);
		}
		else
		{
			PostRoundScreen.SetTitleText(currentTitleString);
			PostRoundScreen.SetDescriptionText(currentDialogueString);
		}
	}


}

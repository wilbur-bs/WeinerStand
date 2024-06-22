using Godot;
using System;

public partial class UiController : Node
{
	public GameState State;
	public EventScreen Menu;
	public Timer RoundTimer;
	public ProgressBar ProgressBar;

	private string currentDescription;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RefreshUi();
	}

	private void RefreshUi()
	{
		currentDescription = "Resources: \n" + State.GetStateString()  + "\n";
		Menu.SetDescriptionText("Resources: \n" + State.GetStateString()  + "\n");
		ProgressBar.Value = 100*(RoundTimer.TimeLeft/RoundTimer.WaitTime);
	}

	


}

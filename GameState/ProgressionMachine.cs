using Godot;
using System;

public partial class ProgressionMachine : Node
{
	[Export]
	public float RoundTime = 10;

	public Timer roundTimer;
	public CanvasLayer inRoundMenu;
	public CanvasLayer postRoundMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void Initialize()
	{
		roundTimer.Timeout += () => EndRound();
	}

	public void NewRound()
	{
		roundTimer.WaitTime = RoundTime;
		roundTimer.Start();
		inRoundMenu.Visible = true;
		postRoundMenu.Visible = false;
		GD.Print("Round Start");
	}

	public void EndRound()
	{
		roundTimer.Stop();
		inRoundMenu.Visible = false;
		postRoundMenu.Visible = true;
		GD.Print("Round End");
	}


}

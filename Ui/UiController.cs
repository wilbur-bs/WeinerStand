using Godot;
using System;

public partial class UiController : Node
{
	public GameState State;
	public EventScreen Menu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RefreshDescription();
	}

	private void RefreshDescription()
	{
		Menu.SetDescriptionText("Resources: \n" + State.GetStateString()  + "\n");
	}

	


}

using Godot;
using System;

public partial class GameWorldController : Node3D
{
	[Export]
	public Area3D area;
	public InteractionMachine interactionMachine;

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
		area.AreaEntered += (enterer) => interactionMachine.Process("sale");
	}


}

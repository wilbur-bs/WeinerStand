using Godot;
using System;

public partial class Customer : Node3D
{
	[Export]
	public Vector3 MovementVector = new Vector3(1,0,0);
	[Export]
	public float Speed = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = Position + (MovementVector * Speed * (float)delta);
	}
}

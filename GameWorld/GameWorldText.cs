using Godot;
using System;

public partial class GameWorldText : Label3D
{
	public float fadePace = 0.5f;
	public Color currentColor;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FontSize = 100;
		OutlineSize = 0;
		HorizontalAlignment = HorizontalAlignment.Left;
		VerticalAlignment = VerticalAlignment.Bottom;
		Text = "Welcome!";

		currentColor = Modulate;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		currentColor.A -= fadePace * (float)delta;
		Modulate = currentColor;
	}

	public void SetMessage(string message)
	{
		Text = message;
		currentColor.A = 1f;
	}
}

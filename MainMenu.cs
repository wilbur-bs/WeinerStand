using Godot;
using System;

public partial class MainMenu : Node
{
	[Export]
	Button startButton;
	[Export]
	CanvasLayer mainMenu;

	Node game;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton.Pressed += () => StartGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void StartGame()
	{
		game = ResourceLoader.Load<PackedScene>("res://MainGame.tscn").Instantiate();
		AddChild(game);
		mainMenu.Visible = false;
		((GameBootstraper)game).GameEnd += () => EndGame();
	}

	public void EndGame()
	{
		game.QueueFree();
		mainMenu.Visible = true;
	}
}

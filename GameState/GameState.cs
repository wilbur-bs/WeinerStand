using Godot;
using System;
using System.Collections.Generic;

public partial class GameState : Node
{
	public Dictionary<string, string> State {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		State = new Dictionary<string, string>
        {
			{ "Batch", "0"},
            { "Money", "0" },
			{ "Sausages", "100" },
        };

		GD.Print(GetStateString());
		GD.Print("Game State Initialized!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public string GetStateString()
	{
		string ret = "";

        foreach (var pair in State)
        {
            ret += $"{pair.Key}: {pair.Value} \n";
        }

        return ret;
	}
}
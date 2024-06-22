using Godot;
using System;
using System.Collections.Generic;

public partial class DialougeMachine : Node
{
	public Dictionary<string, string> Titles;
	public Dictionary<string, string> Descriptions;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Titles = new Dictionary<string, string>();
		Titles.Add("default", "Prepare for the next day!");
		Titles.Add("bread", "WHAAT? Bread is a resource?");
		Titles.Add("freshness", "WHAAT? Freshness is a resource?");

		Descriptions = new Dictionary<string, string>();
		Descriptions.Add("default_0", "Resources:\n");
		Descriptions.Add("bread_0", "Nice work with the last day, but I helped you out with the buns. A real weiner master works their OWN buns.\n");
		Descriptions.Add("bread_1", "My stand only uses the BEST buns, so the bread is delicate. Make sure to stock up on extra! Customers wont want a hotdog without the bun.\n - Each bun has a 10% chance of being wasted. -\n");
		Descriptions.Add("freshness_0", "WHAAT? Freshness is a resource?");
	}
}

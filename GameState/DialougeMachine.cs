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
		Titles.Add("start", "WHAAT? Sausages are a resource?");
		Titles.Add("end", "WHAAT? Money was a resource?");
		Titles.Add("bread", "WHAAT? Bread is a resource?");
		Titles.Add("freshness", "WHAAT? Freshness is a resource?");
		Titles.Add("cleanliness", "WHAAT? Cleanliness is a resource?");
		Titles.Add("condiments", "WHAAT? Condiments are a resource?");
		Titles.Add("propane", "WHAAT? Propane is a resource?");

		Descriptions = new Dictionary<string, string>();
		Descriptions.Add("default_0", "Resources:\n");
		Descriptions.Add("start_0", "Hey kid! It’s me, THE Jimothy Hotdog. I hear you want to help out with my hotdog stand.\nI dunno… this job aint for the faint of heart. It takes skill and talent to work here and become REAL weiner master.\n Let's start out with a few test days to see if you have what it takes to be a weiner master.\n");
		Descriptions.Add("start_1", "Here is 50 bucks to start out. You need the money to buy sausages to turn into hotdogs.\n Also keep in mind that the stand is in prime location, which comes with fees. Make sure your earnings for the day cover the stand's fees, or I'll have to take my stand back.\n You can spend into the negative while you prepare, just make sure you can make it back in time to pay for the fees.\n\n - 100 money is taken after every day \n - Ending the day with 0 money will end the game ");
		Descriptions.Add("end_0", "Looks like you ran out of money. I told you it wasn't easy running a hotdog stand. Maybe try again another time.\n");
		Descriptions.Add("bread_0", "Nice work with the last day, but I helped you out with the buns. A real weiner master works their OWN buns.\n");
		Descriptions.Add("bread_1", "My stand only uses the BEST buns, so the bread is delicate. Make sure to stock up on extra! Customers wont want a hotdog without the bun.\n\n - Each bun has a 10% chance of being wasted. -\n");
		Descriptions.Add("freshness_0", "WHAAT? Freshness is a resource?");
		Descriptions.Add("cleanliness_0", "WHAAT? Cleanliness is a resource?");
		Descriptions.Add("condiments_0", "WHAAT? Condiments are a resource?");
		Descriptions.Add("propane_0", "WHAAT? Propane is a resource?");
	}
}

using Godot;
using System;

public partial class PostRoundMenuUi : Node
{

	private AttributeUi attributeUi;
	private DialogueUi dialogueUi;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		attributeUi = (AttributeUi)FindChild("AttributeUi");
		attributeUi.NewLine("Oh", "[right]Boy");

		dialogueUi = (DialogueUi)FindChild("DialogueUi");
		dialogueUi.SetText("[center]Oh boy!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

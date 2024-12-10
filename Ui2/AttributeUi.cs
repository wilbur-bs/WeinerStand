using Godot;
using System;

public partial class AttributeUi : Node
{
	private VBoxContainer uiContainer;
	private HBoxContainer templateLine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		uiContainer = (VBoxContainer)FindChild("UiContainer");
		templateLine = (HBoxContainer)FindChild("TemplateLine");
		
		NewLine("Oh", "[center]Boy");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private HBoxContainer NewLine()
	{
		HBoxContainer newLine = (HBoxContainer)templateLine.Duplicate();
		uiContainer.AddChild(newLine);
		return newLine;
	}

	public void NewLine(string key, string value)
	{
		HBoxContainer newLine = NewLine();
		LinkButton keyText = (LinkButton)newLine.GetNode("AttributeKeyLabel");
		keyText.Text = key;
		RichTextLabel valueText = (RichTextLabel)newLine.GetNode("AttributeValueLabel");
		valueText.Text = value;
	}
}

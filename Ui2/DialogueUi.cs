using Godot;
using System;

public partial class DialogueUi : Control
{
	[Export]
	public RichTextLabel descriptionText;
	public VBoxContainer optionsContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		descriptionText = (RichTextLabel)FindChild("DescriptionText");
		optionsContainer = (VBoxContainer)FindChild("OptionsContainer");
	}

	public void SetText(string descriptionInput)
	{
		descriptionText.Text = descriptionInput;
	}

	public void AddButton(Button buttonInput)
	{
		optionsContainer.AddChild(buttonInput);
	}

	public void ClearButtons()
	{
		int childCount = optionsContainer.GetChildCount();
		Node currentNode;
		for(int index = 0; index < childCount; index++)
		{
			currentNode = optionsContainer.GetChild(0); 
			optionsContainer.RemoveChild(currentNode);
			currentNode.QueueFree();
		}
	}
}

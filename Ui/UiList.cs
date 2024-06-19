using Godot;
using System;

public partial class UiList : Control
{
	private ScrollContainer scrollContainer;
	private VBoxContainer listContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scrollContainer = new(){
			LayoutMode = 1,
			AnchorsPreset = 15,
			HorizontalScrollMode = ScrollContainer.ScrollMode.Disabled,
			Name = "ScrollContainer"
		};
		AddChild(scrollContainer);

		// create vbox container
		listContainer = new(){
			Name = "ListContainer",
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		};
		scrollContainer.AddChild(listContainer);
	}

	public void AddOptionButton(Button buttonInput)
	{
		listContainer.AddChild(buttonInput);
	}

	public void ClearList()
	{
		int childCount = listContainer.GetChildCount();
		Node currentNode;
		for(int index = 0; index < childCount; index++)
		{
			currentNode = listContainer.GetChild(0); 
			listContainer.RemoveChild(currentNode);
			currentNode.QueueFree();
		}
	}
}

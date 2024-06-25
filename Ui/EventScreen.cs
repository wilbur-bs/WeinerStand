using Godot;
using System;

public partial class EventScreen : Control
{
	private VBoxContainer newContainer;
	private Label titleText;
	private Label descriptionText;
	private VBoxContainer optionsContainer;
	private UiList buttonList;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// create vbox container
		newContainer = new(){
			LayoutMode = 1,
			AnchorsPreset = 15,
			Name = "EventScreen"
		};
		AddChild(newContainer);

		// add title text
		titleText = new(){
			AnchorsPreset = 1,
			Name = "TitleText",
			Text = "Title",
			HorizontalAlignment = HorizontalAlignment.Center
		};
		newContainer.AddChild(titleText);

		// add description text
		descriptionText = new(){
			AnchorsPreset = 1,
			//SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "DescriptionText",
			Text = "Description",
			AutowrapMode =  TextServer.AutowrapMode.Word
		};
		newContainer.AddChild(descriptionText);

		buttonList = new(){
			AnchorsPreset = 1,
			SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "ButtonsList"
		};
		newContainer.AddChild(buttonList);
	}

	public void SetTitleText(string titleInput)
	{
		titleText.Text = titleInput;
	}

	public void SetDescriptionText(string descriptionInput)
	{
		descriptionText.Text = descriptionInput;
	}

	public void AddOptionButton(Button buttonInput)
	{
		buttonList.AddOptionButton(buttonInput);
	}

	public void ClearButtons()
	{
		buttonList.ClearList();
	}
}

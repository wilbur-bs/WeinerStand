using Godot;
using System;

public partial class GameBootstraper : Node
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GameState gameState = new GameState();
		AddChild(gameState);

        Timer newTimer = new Timer
        {
            Name = "RoundTimer"
        };
        AddChild(newTimer);

        newTimer = new Timer
        {
            Name = "FreshnessTimer"
        };
        AddChild(newTimer);

        Camera3D camera = new Camera3D();
        AddChild(camera);
        camera.Position = new Vector3(0,0,15);

        DirectionalLight3D light = new DirectionalLight3D();
        AddChild(light);
        light.Position = new Vector3(0,12,15);

        CanvasLayer canvas = new CanvasLayer();
		AddChild(canvas);

        VBoxContainer uiContainer = new VBoxContainer()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "UiContainer"
        };
        canvas.AddChild(uiContainer);

        Control topPadding = new Control()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "TopPadding"
        };
        uiContainer.AddChild(topPadding);
        
		EventScreen menu = new EventScreen()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "EventMenu"
        };
		uiContainer.AddChild(menu);
        menu.SetTitleText("Welcome!");

        UiController uiController = new UiController();
        AddChild(uiController);
        uiController.State = gameState;
        uiController.Menu = menu;

        InteractionMachine interactionMachine = new InteractionMachine();
        AddChild(interactionMachine);
        interactionMachine.State = gameState;

        Button button = new() {
			Text = "Make Batch"
		};
        button.Pressed += () => interactionMachine.Process("makeBatch");
		menu.AddOptionButton(button);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

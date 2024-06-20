using Godot;
using System;

public partial class GameBootstraper : Node
{
    [Export]
    public GameWorldController gameWorldController;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GameState gameState = new GameState();
		AddChild(gameState);

        Timer roundTimer = new Timer
        {
            Name = "RoundTimer"
        };
        AddChild(roundTimer);

        Timer freshnessTimer = new Timer
        {
            Name = "FreshnessTimer"
        };
        AddChild(freshnessTimer);

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

        VBoxContainer topContainer = new VBoxContainer()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "TopContainer"
        };
        uiContainer.AddChild(topContainer);

        ProgressBar roundBar = new ProgressBar()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
            SizeFlagsStretchRatio = 0.1f,
            ShowPercentage = false,
			Name = "RoundProgress"
        };
        topContainer.AddChild(roundBar);

        Control topPadding = new Control()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "TopPadding"
        };
        topContainer.AddChild(topPadding);
        
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
        uiController.RoundTimer = roundTimer;
        uiController.ProgressBar = roundBar;

        InteractionMachine interactionMachine = new InteractionMachine();
        AddChild(interactionMachine);
        interactionMachine.State = gameState;

        Button button = new() {
			Text = "Make Batch"
		};
        button.Pressed += () => interactionMachine.Process("makeBatch");
		menu.AddOptionButton(button);

        gameWorldController.interactionMachine = interactionMachine;
        gameWorldController.Initialize();

        ProgressionMachine progressionMachine = new ProgressionMachine();
        progressionMachine.roundTimer = roundTimer;
        progressionMachine.Initialize();

        progressionMachine.NewRound();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

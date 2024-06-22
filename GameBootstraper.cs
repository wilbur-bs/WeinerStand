using Godot;
using System;

public partial class GameBootstraper : Node
{
    [Export]
    public GameWorldController gameWorldController;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // Game State
        Timer roundTimer = new Timer
        {
            Name = "RoundTimer"
        };
        AddChild(roundTimer);

        Timer freshnessTimer = new Timer
        {
            Autostart = false,
            WaitTime = 10,
            Name = "FreshnessTimer"
        };
        freshnessTimer.Timeout += () => freshnessTimer.Stop();
        AddChild(freshnessTimer);

        GameState gameState = new GameState();
		AddChild(gameState);
        gameState.FreshnessTimer = freshnessTimer;

        InteractionMachine interactionMachine = new InteractionMachine();
        AddChild(interactionMachine);
        interactionMachine.State = gameState;
        interactionMachine.freshnessTimer = freshnessTimer;

        ProgressionMachine progressionMachine = new ProgressionMachine();
        progressionMachine.roundTimer = roundTimer;
        progressionMachine.gameState = gameState;
        AddChild(progressionMachine);
        progressionMachine.Initialize();

        DialougeMachine dialougeMachine = new();
        AddChild(dialougeMachine);

        // Game World
        /*
        Camera3D camera = new Camera3D();
        AddChild(camera);
        camera.Position = new Vector3(0,0,15);

        DirectionalLight3D light = new DirectionalLight3D();
        AddChild(light);
        light.Position = new Vector3(0,12,15);
        */
        GameWorldMachine gameWorldMachine = new GameWorldMachine();
        AddChild(gameWorldMachine);

        // In-Round Ui
        CanvasLayer inRoundCanvas = new CanvasLayer();
		AddChild(inRoundCanvas);

        VBoxContainer uiContainer = new VBoxContainer()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "UiContainer"
        };
        inRoundCanvas.AddChild(uiContainer);


        // Ui top
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
        

        // Ui bottom
		EventScreen menu = new EventScreen()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "EventMenu"
        };
		uiContainer.AddChild(menu);
        menu.SetTitleText("Welcome!");

        // Post-Round Ui
        CanvasLayer postRoundCanvas = new CanvasLayer();
		AddChild(postRoundCanvas);

        /*VBoxContainer uiContainer = new VBoxContainer()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "UiContainer"
        };*/
        EventScreen postMenu = new EventScreen()
        {
            LayoutMode = 1,
			AnchorsPreset = 15,
            SizeFlagsVertical = Godot.Control.SizeFlags.ExpandFill,
			Name = "PostMenu"
        };
        postRoundCanvas.AddChild(postMenu);
        postMenu.SetTitleText("Welcome to Post!");

        // Ui controller
        UiController uiController = new UiController();
        AddChild(uiController);
        uiController.State = gameState;
        uiController.Menu = menu;
        uiController.RoundTimer = roundTimer;
        uiController.ProgressBar = roundBar;
        uiController.inRoundMenu = inRoundCanvas;
        uiController.postRoundMenu = postRoundCanvas;
        uiController.PostRoundScreen = postMenu;
        uiController.DialougeMachine = dialougeMachine;
        uiController.progressionMachine = progressionMachine;

        // Buttons
        Button button = new() {
			Text = "Make Batch"
		};
        button.Pressed += () => interactionMachine.Process("makeBatch");
		menu.AddOptionButton(button);

        // Init functions
        gameWorldController.interactionMachine = interactionMachine;
        gameWorldController.Initialize();

        gameWorldMachine.Initialize();

        //progressionMachine.inRoundMenu = inRoundCanvas;
        //progressionMachine.postRoundMenu = postRoundCanvas;
        progressionMachine.gameWorldMachine = gameWorldMachine;
        progressionMachine.uiController = uiController;
        progressionMachine.NewRound();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

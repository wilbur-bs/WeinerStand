using Godot;
using System;

public partial class GameBootstraper : Node
{
    [Export]
    public GameWorldController gameWorldController;

    [Signal]
    public delegate void GameEndEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        #region "Game State"
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

        DialougeMachine dialougeMachine = new();
        AddChild(dialougeMachine);

        #endregion

        #region "Game World"
        GameWorldMachine gameWorldMachine = new GameWorldMachine();
        AddChild(gameWorldMachine);

        GameWorldText gameWorldText = new();
        gameWorldText.Position = new Vector3(-10, 2, 0);
        AddChild(gameWorldText);

        #endregion

        #region "Ui"
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
			Name = "EventMenu",
            Theme = (Theme)GD.Load("res://Game.theme")
        };
		uiContainer.AddChild(menu);
        menu.SetTitleText("Make sure you have hotdogs in your batch!");

        // Post-Round Ui
        CanvasLayer postRoundCanvas = new CanvasLayer();
		AddChild(postRoundCanvas);

        
        EventScreen postMenu = new EventScreen()
        {
            LayoutMode = 1,
            AnchorLeft = 0.5f,
            AnchorRight = 1f,
            AnchorTop = 0f,
            AnchorBottom = 1f,
			Name = "PostMenu",
            Theme = (Theme)GD.Load("res://Game.theme")
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
        uiController.interactionMachine = interactionMachine;

        // Buttons
        Button button = new() {
			Text = "Make Batch"
		};
        button.Pressed += () => interactionMachine.Process("makeBatch");
		menu.AddOptionButton(button);
        #endregion

        #region "Finish inits"
        gameWorldController.interactionMachine = interactionMachine;
        gameWorldController.Initialize();

        gameWorldMachine.Initialize();

        progressionMachine.gameWorldMachine = gameWorldMachine;
        progressionMachine.uiController = uiController;
        progressionMachine.Initialize();

        interactionMachine.gwText = gameWorldText;
        #endregion
	}


    public void GameEnded()
    {  
        EmitSignal(SignalName.GameEnd);
    }


}

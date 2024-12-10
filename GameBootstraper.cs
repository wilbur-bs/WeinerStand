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
        CanvasLayer inRoundCanvas = (CanvasLayer)FindChild("InRoundCanvas");
        DialogueUi menu = (DialogueUi)FindChild("InGameMenu").FindChild("DialogueUi");

        // Ui controller
        UiController uiController = new UiController();
        AddChild(uiController);
        uiController.State = gameState;
        uiController.Menu = menu;
        uiController.RoundTimer = roundTimer;
        uiController.ProgressBar = (ProgressBar)FindChild("RoundProgressBar");
        uiController.inRoundMenu = inRoundCanvas;
        uiController.postRoundMenu = (CanvasLayer)FindChild("PostRoundCanvas");
        uiController.PostRoundScreen = (DialogueUi)FindChild("PostRoundCanvas").FindChild("DialogueUi");
        uiController.DialougeMachine = dialougeMachine;
        uiController.progressionMachine = progressionMachine;
        uiController.interactionMachine = interactionMachine;

        // Buttons
        Button button = new() {
			Text = "Make Batch"
		};
        button.Pressed += () => interactionMachine.Process("makeBatch");
		menu.ClearButtons();
        menu.AddButton(button);
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

using Godot;
using System;

public partial class GameWorldMachine : Node
{
	private Node gameWorld;
	private PackedScene customerScene;
	private Timer waitTimer;
	private Random random;

	private Vector3 customerPosition;
	private Vector3 customerRotation;

	public void Initialize()
	{
		gameWorld = GetParent();
		customerScene = ResourceLoader.Load<PackedScene>("res://GameWorld/Customer/Customer.tscn");
		customerPosition = new Vector3(13, 0, 0);
		customerRotation = new Vector3(0, 180, 0);
		random = new Random();

		waitTimer = new Timer();
		AddChild(waitTimer);
		waitTimer.Timeout += () => SpawnCustomer();
		waitTimer.Timeout += () => NewWait();

		NewWait();
	}
	
	public void SpawnCustomer()
	{
		Node3D newCustomer = (Node3D)customerScene.Instantiate();
		newCustomer.Position = customerPosition;
		newCustomer.RotationDegrees = customerRotation;
		gameWorld.AddChild(newCustomer);
	}

	public void NewWait()
	{
		waitTimer.WaitTime = random.Next(1,3);
		waitTimer.Start();
	}

}

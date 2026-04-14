using Godot;
using System;

public partial class InteractableComponent : Area2D
{
	[Signal]
    public delegate void InteractableActivatedEventHandler();

    [Signal]
    public delegate void InteractableDeactivatedEventHandler();

    public override void _Ready()
    {
        // Підписка на події Area2D
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node2D body)
    {
        EmitSignal(SignalName.InteractableActivated);
    }

    private void OnBodyExited(Node2D body)
    {
        EmitSignal(SignalName.InteractableDeactivated);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
}

using Godot;
using System;

public partial class Door : StaticBody2D
{
	private AnimatedSprite2D sprite;
	private CollisionShape2D collisionShape;
	private InteractableComponent interactableComponent;
	private StaticBody2D doorCollider;
	private StaticBody2D doorframeCollider;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        interactableComponent = GetNode<InteractableComponent>("InteractableComponent");
		doorCollider = GetNode<StaticBody2D>("DoorCollider");
		doorframeCollider = GetNode<StaticBody2D>("DoorFrameCollider");

		interactableComponent.InteractableActivated += OnInteractableActivated;
		interactableComponent.InteractableDeactivated += OnInteractableDeactivated;
		CollisionLayer = 1;
	}

	private void OnInteractableActivated()
    {
        sprite.Play("open_door");
        GD.Print("activated");
		doorCollider.CollisionLayer = 2;
    }

    private void OnInteractableDeactivated()
    {
        sprite.Play("close_door");
        GD.Print("deactivated");
		doorCollider.CollisionLayer = 1;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

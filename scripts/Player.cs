using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private Vector2 prevDirection = Vector2.Down.Normalized();
	private AnimatedSprite2D sprite;
	private NavigationAgent2D agent;


    public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		agent = GetNode<NavigationAgent2D>("NavigationAgent2D");

		agent.PathDesiredDistance = 4f;
		agent.TargetDesiredDistance = 4f;
		agent.Radius = 4f;

	}

	public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            agent.TargetPosition = GetGlobalMousePosition();
        }
    }



	public override void _PhysicsProcess(double delta)
    {
		if (agent.IsNavigationFinished())
        {
            Velocity = Vector2.Zero;
			if (prevDirection.Y<0)
			{
				Play("idle_back");
			}
			else if (prevDirection.X<0)
			{
				Play("idle_left");
			}
			else if (prevDirection.X>0)
			{
				Play("idle_right");
			}
			else if (prevDirection.Y>0)
			{
				Play("idle_front");
			}			
            return;
        }

        Vector2 nextPosition = agent.GetNextPathPosition();
        Vector2 direction = (nextPosition - GlobalPosition).Normalized();
        Velocity = direction * Speed;
		if (direction != Vector2.Zero)
		{
			if (direction.Y<0)
			{
				Play("walk_back");
			}
			else if (direction.X<0)
			{
				Play("walk_left");
			}
			else if (direction.X>0)
			{
				Play("walk_right");
			}
			else if (direction.Y>0)
			{
				Play("walk_front");
			}
			prevDirection = direction;
		}
        MoveAndSlide();
    }

    private void Play(string a)
    {
        if(sprite.Animation!=a)
			sprite.Play(a);
    }

}

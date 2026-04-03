using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private Vector2 prevDirection = Vector2.Down.Normalized();
	private AnimatedSprite2D sprite;

    public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}


	public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
        direction = direction.Normalized();
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
		else
		{
			if (prevDirection.X<0)
			{
				Play("idle_left");
			}
			if (prevDirection.X>0)
			{
				Play("idle_right");
			}
			if (prevDirection.Y<0)
			{
				Play("idle_back");
			}
			if (prevDirection.Y>0)
			{
				Play("idle_front");
			}			
		}
		GD.Print(direction);
        MoveAndSlide();
    }

    private void Play(string a)
    {
        if(sprite.Animation!=a)
			sprite.Play(a);
    }

}

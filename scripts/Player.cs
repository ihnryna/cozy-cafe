using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private Vector2 prevDirection = Vector2.Down.Normalized();
	private AnimatedSprite2D sprite;
	private NavigationAgent2D agent;
	private Sprite2D leftHand;
	private Sprite2D rightHand;

	private Item _targetItem;
	private bool _canPick = false;
	private bool _isPicking = false;
	private Sprite2D pickingHand;


    public override void _Ready()
	{

		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		leftHand = GetNode<Sprite2D>("LeftHandSprite");
		rightHand = GetNode<Sprite2D>("RightHandSprite");

		agent.PathDesiredDistance = 4f;
		agent.TargetDesiredDistance = 4f;
		agent.Radius = 4f;
		
		rightHand.Visible = false;
		leftHand.Visible = false;

		AddToGroup("player");
	}

	public override void _Input(InputEvent @event)
{
    if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
    {
        var space = GetWorld2D().DirectSpaceState;

        var query = new PhysicsPointQueryParameters2D
        {
            Position = GetGlobalMousePosition(),
            CollideWithAreas = true,
            CollideWithBodies = true
        };

        var result = space.IntersectPoint(query);

        foreach (var hit in result)
		{
			var node = hit["collider"].As<Node>();
			if (node.Name=="InteractableComponent")
				continue;

			if (node.Name=="ClickArea")
			{
				node = node.GetParent();
			}

			while (node != null && node is not Item)
				node = node.GetParent();

			if (node is Item item)
			{
				//SetTargetItem(item);
				return;
			}
		}	

        _targetItem = null;
        agent.TargetPosition = GetGlobalMousePosition();
    }
}

	public void SetTargetItem(Item item, MouseButton button)
	{
		_targetItem = item;
		_canPick = false;
		if(button == MouseButton.Left)
		{
			pickingHand = leftHand;
		} else if (button == MouseButton.Right)
		{
			pickingHand = rightHand;
		}

		agent.TargetPosition = item.GlobalPosition;

		GD.Print("GO TO ITEM");
	}


	public void OnItemInRange(Item item)
	{
		if (_targetItem == item)
		{
			GD.Print("IN RANGE");
			_canPick = true;
		}
	}


	public override void _PhysicsProcess(double delta)
    {
		if (_targetItem != null && _canPick && !_isPicking)
		{
			_isPicking = true;

			PickItem(_targetItem);

			_targetItem.Visible = false;
			_targetItem.SetProcess(false);

			_targetItem = null;
			_canPick = false;
			_isPicking = false;

		}

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

	public void PickItem(Item item)
	{
		GD.Print("PickItem");
    	pickingHand.Texture = item.Icon;
		pickingHand.Visible = true;
	}

}

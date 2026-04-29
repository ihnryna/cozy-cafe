using Godot;
using System;

public partial class Item : Area2D
{
    [Export] public Texture2D Icon;
	private InteractableComponent interactableComponent;

    private Area2D clickArea;

    public override void _Ready()
    {
        clickArea = GetNode<Area2D>("ClickArea");
		interactableComponent = GetNode<InteractableComponent>("InteractableComponent");
		interactableComponent.InteractableActivated += OnInteractableActivated;
		interactableComponent.InteractableDeactivated += OnInteractableDeactivated;

		((ClickArea)clickArea).Owner = this;
		GD.Print(" NEW ITEM");


		var player = GetTree().GetFirstNodeInGroup("player") as Player;

        this.Position = player.Position;

        clickArea.InputEvent += OnClick;
    }

    private void OnClick(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event is InputEventMouseButton mb && mb.Pressed)
        {
			GD.Print(" Item : OnClick");
            var player = GetTree().GetFirstNodeInGroup("player") as Player;
            player?.SetTargetItem(this, mb.ButtonIndex);
        }
    }

    private void OnInteractableActivated()
    {
        GD.Print("Item : ENTER");
		var player = GetTree().GetFirstNodeInGroup("player") as Player;
    	player?.OnItemInRange(this);
    }

    private void OnInteractableDeactivated()
    {
        GD.Print("Item : OUT");
    }
}
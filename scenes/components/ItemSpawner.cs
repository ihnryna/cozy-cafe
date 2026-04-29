using Godot;
using System;

public partial class ItemSpawner : Area2D
{
    [Export] public PackedScene ItemScene;
        private Area2D clickArea;


    public override void _Ready()
	{
		InputPickable = true;
        clickArea = GetNode<Area2D>("ClickArea");
        ((ClickArea)clickArea).Owner = this;
        clickArea.InputEvent += OnClick;
	}
    private void OnClick(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event is InputEventMouseButton mb && mb.Pressed)
        {
            GD.Print("ItemSpawner");

            SpawnItem(GetGlobalMousePosition());
        }
    }

    private void SpawnItem(Vector2 position)
    {
                GD.Print("1");

        var item = ItemScene.Instantiate<Item>();
                        GD.Print("2");

        item.GlobalPosition = position;
        item.ZIndex = 10;
        item.Visible = true;
                GD.Print("3");

GetTree().CurrentScene.AddChild(item);
        GD.Print("Spawned item");
    }
}
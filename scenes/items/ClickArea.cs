using Godot;
using System;

public partial class ClickArea : Area2D
{
    public Object Owner;

    public override void _Ready()
    {
        InputPickable = true;
        this.ZIndex = 10;
    }
}
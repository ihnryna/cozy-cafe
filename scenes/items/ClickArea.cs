using Godot;
using System;

public partial class ClickArea : Area2D
{
    public Item Owner;

    public override void _Ready()
    {
        InputPickable = true;
    }
}
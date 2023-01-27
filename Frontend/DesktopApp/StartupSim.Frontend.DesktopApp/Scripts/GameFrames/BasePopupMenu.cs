using System;
using Godot;
using Godot.Collections;

public abstract class BasePopupMenu : WindowDialog
{
    protected Array<Button> Buttons;
    protected Label Label;

    public Action Exit { get; set; }

    public override void _Ready()
    {
        Connect("popup_hide", this, "OnExit");
        GetCloseButton().Connect("popup_hide", this, "OnExit");
    }
    
    public abstract void InitializeFields(Main core);

    protected void OnExit()
    {
        base.Hide();
        Exit();
    }
}
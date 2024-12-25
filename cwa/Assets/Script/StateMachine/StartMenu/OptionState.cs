using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OptionState :  StartMenuState
{
    Slider masterVolumnSlider;
    Slider pov;
    public OptionState (Canvas canvas):base(canvas) {
    }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override CanvasState GetNextState()
    {
       return CanvasState.OptionMenu;
    }

    public override void InitializeSubState()
    {
    }

    public override void onTriggerEnter(Collider other)
    {
    }

    public override void onTriggerExit(Collider other)
    {
    }

    public override void onTriggerStay(Collider other)
    {
    }

    public override void UpdateState()
    {

    }
}

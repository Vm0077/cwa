using System.Collections;
using UnityEngine;

public class TitleMenuState :  StartMenuState
{
    bool toOption;
    bool toGameSave;
    public TitleMenuState (Canvas canvas):base(canvas) {

    }

    public override CanvasState GetNextState()
    {
       return CanvasState.TitleMenu;
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
